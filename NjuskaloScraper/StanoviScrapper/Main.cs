using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StanoviScrapper
{
    public partial class Main : Form
    {
        private Njuskalo njuskalo;

        public List<Apartment> Apartments { get; set; }
        public int SelectedApartmentId { get; set; }

        public Main()
        {
            InitializeComponent();
            Apartments = new List<Apartment>();
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gmap.SetPositionByKeywords("Zagreb, Croatia");
            gmap.ShowCenter = false;
            CreateShoppingOverlay("kaufland");
            CreateShoppingOverlay("konzum");
            CreateShoppingOverlay("plodine");
            CreateShoppingOverlay("spar");
            CreateShoppingOverlay("lidl");
            CreateTramOverlay();
            CreatePOIOverlay();
        }

        private void CreateShoppingOverlay(string name)
        {
            GMapOverlay markers = new GMapOverlay(name);
            var kauflandLocations = Newtonsoft.Json.JsonConvert.DeserializeObject<Location[]>(File.ReadAllText($"locations\\{name}_locations.json"));
            Bitmap kauflandImg = new Bitmap(Image.FromFile($"img\\marker_{name}.png"));
            foreach (var loc in kauflandLocations)
            {
                var marker = new GMarkerGoogle(new GMap.NET.PointLatLng(loc.Latitude, loc.Longitude), kauflandImg);
                marker.ToolTipText = name;
                markers.Markers.Add(marker);
            }
            gmap.Overlays.Add(markers);

        }

        private void CreateTramOverlay()
        {
            GMapOverlay markers = new GMapOverlay("TramStops");
            var poiLocations = Newtonsoft.Json.JsonConvert.DeserializeObject<POI[]>(File.ReadAllText($"locations\\tram_locations.json"));
            foreach (var loc in poiLocations)
            {
                var marker = new GMarkerGoogle(new GMap.NET.PointLatLng(loc.Latitude, loc.Longitude), GMarkerGoogleType.blue_pushpin);
                marker.ToolTipText = "Tramvajska stanica";
                markers.Markers.Add(marker);
            }
            gmap.Overlays.Add(markers);
        }

        private void CreatePOIOverlay()
        {
            GMapOverlay markers = new GMapOverlay("POI");
            var poiLocations = Newtonsoft.Json.JsonConvert.DeserializeObject<POI[]>(File.ReadAllText($"locations\\poi_locations.json"));
            foreach (var loc in poiLocations)
            {
                var marker = new GMarkerGoogle(new GMap.NET.PointLatLng(loc.Latitude, loc.Longitude), GMarkerGoogleType.green_pushpin);
                marker.ToolTipText = loc.Title;
                markers.Markers.Add(marker);
            }
            gmap.Overlays.Add(markers);
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            njuskalo = new Njuskalo();
            pbProcess.Style = ProgressBarStyle.Marquee;
            pbProcess.ForeColor = Color.LawnGreen;
            pbProcess.MarqueeAnimationSpeed = 30;

            njuskalo.Progress += (s, eargs) =>
            {
                txtPages.Text = njuskalo.PageProcessedCount.ToString();
                txtApartments.Text = $"{njuskalo.ApartmentsProcessedCount}/{njuskalo.ListItemProcessedCount}";
            };

            njuskalo.Done += (s, eargs) =>
            {
                pbProcess.Style = ProgressBarStyle.Continuous;
                pbProcess.Value = pbProcess.Maximum;
                Apartments = njuskalo.Apartments;
                CreateApartmentOverlay(Apartments);
            };

            njuskalo.Load(txtQuery.Text.Replace(Njuskalo.njuskaloUrl, ""));
        }

        private async void btnReprocess_Click(object sender, EventArgs e)
        {
            njuskalo = new Njuskalo();
            pbProcess.Style = ProgressBarStyle.Marquee;
            pbProcess.ForeColor = Color.LawnGreen;
            pbProcess.MarqueeAnimationSpeed = 30;

            njuskalo.Apartments = Apartments;

            njuskalo.Progress += (s, eargs) =>
            {
                txtPages.Text = njuskalo.PageProcessedCount.ToString();
                txtApartments.Text = $"{njuskalo.ApartmentsProcessedCount}/{njuskalo.ListItemProcessedCount}";
            };

            njuskalo.Done += (s, eargs) =>
            {
                pbProcess.Style = ProgressBarStyle.Continuous;
                pbProcess.Value = pbProcess.Maximum;
                Apartments = njuskalo.Apartments;
                CreateApartmentOverlay(Apartments);
            };

            njuskalo.Load(txtQuery.Text.Replace(Njuskalo.njuskaloUrl, ""));
        }

        private void CreateApartmentOverlay(List<Apartment> apartments)
        {
            var markers = gmap.Overlays.FirstOrDefault(t => t.Id == "Apartments");
            if (markers == null)
            {
                markers = new GMapOverlay("Apartments");
                gmap.Overlays.Add(markers);
            }
            markers.Markers.Clear();
            foreach (var apartment in apartments)
            {
                if (apartment.Location == null)
                    continue;
                if (apartment.Hide)
                    continue;
                var marker = new GMarkerGoogle(new GMap.NET.PointLatLng(apartment.Location.Latitude, apartment.Location.Longitude), apartment.Star ? GMarkerGoogleType.yellow : (apartment.IsNew ? GMarkerGoogleType.green : GMarkerGoogleType.red));
                marker.ToolTipText = apartment.Price + " - " + apartment.Title;
                marker.Tag = apartment.Id;
                markers.Markers.Add(marker);
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (njuskalo != null && njuskalo.IsRunning)
            {
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.DefaultExt = ".csv";
            dialog.Filter = "Csv files|*.csv";
            dialog.FileName = Path.Combine(Environment.CurrentDirectory, "apartments.csv");
            dialog.OverwritePrompt = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var apartment in Apartments)
                apartment.IsNew = false;

            pbProcess.Style = ProgressBarStyle.Marquee;
            ServiceStack.Text.CsvConfig.ItemSeperatorString = "|";

            if (File.Exists(dialog.FileName))
                File.Delete(dialog.FileName);
            using (var stream = File.OpenWrite(dialog.FileName))
            using (var writer = new StreamWriter(stream, Encoding.Unicode))
            {
                ServiceStack.Text.CsvSerializer.SerializeToWriter(Apartments, writer);
            }
            Debug.WriteLine("Export to CSV finished.");
            pbProcess.Style = ProgressBarStyle.Continuous;
            pbProcess.Value = pbProcess.Maximum;
        }

        private void btnImportCsv_Click(object sender, EventArgs e)
        {
            if (njuskalo != null && njuskalo.IsRunning)
            {
                return;
            }

            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.DefaultExt = ".csv";
            dialog.Filter = "Csv files|*.csv";
            dialog.InitialDirectory = Environment.CurrentDirectory;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ServiceStack.Text.CsvConfig.ItemSeperatorString = "|";
            using (var stream = File.OpenRead(dialog.FileName))
            using (var reader = new StreamReader(stream, Encoding.Unicode))
            {
                Apartments = ServiceStack.Text.CsvSerializer.DeserializeFromReader<List<Apartment>>(reader);
                CreateApartmentOverlay(Apartments);
                MessageBox.Show($"Imported {Apartments.Count} apartments.");
            }
            Debug.WriteLine("Import from CSV finished.");
            pbProcess.Style = ProgressBarStyle.Continuous;
            pbProcess.Value = pbProcess.Maximum;
        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item.Tag != null)
            {
                SelectedApartmentId = (int)item.Tag;
                webBrowser.Url = new Uri(Apartments.First(t => t.Id == SelectedApartmentId).Link);
            }
        }

        private void btnStarApartment_Click(object sender, EventArgs e)
        {
            Apartments.First(t => t.Id == SelectedApartmentId).Star = !Apartments.First(t => t.Id == SelectedApartmentId).Star;
            CreateApartmentOverlay(Apartments);
        }

        private void btnHideApartment_Click(object sender, EventArgs e)
        {
            Apartments.First(t => t.Id == SelectedApartmentId).Hide = true;
            CreateApartmentOverlay(Apartments);
        }

        private void btnOpenBrowser_Click(object sender, EventArgs e)
        {
            Process.Start(Apartments.First(t => t.Id == SelectedApartmentId).Link);
        }


    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class POI : Location
    {
        public string Title { get; set; }
    }
}
