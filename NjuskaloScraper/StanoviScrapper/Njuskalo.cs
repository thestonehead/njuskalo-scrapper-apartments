using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace StanoviScrapper
{
    class Njuskalo
    {
        HttpClient client;
        public const string njuskaloUrl = "http://www.njuskalo.hr";

        public int PageProcessedCount { get; private set; }
        public int ListItemProcessedCount { get; private set; }
        public int ListItemExceptionCount { get; private set; }
        public List<Apartment> Apartments { get;  set; }
        public int ApartmentsProcessedCount { get; private set; }
        public int ApartmentExceptionCount { get; private set; }
        public bool IsRunning { get; internal set; }

        public event EventHandler Progress;
        public event EventHandler Done;

        public Njuskalo()
        {
            Apartments = new List<Apartment>(1000);

            client = new HttpClient();
            client.BaseAddress = new Uri(njuskaloUrl);

        }

        public async Task Load(string query)
        {
            var buffer = new BufferBlock<Apartment>();
            try
            {
                IsRunning = true;
                await LoadAllPages(query, Environment.ProcessorCount, buffer);
                await ProcessApartments(buffer);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                IsRunning = false;
                if (Done != null) Done(this, new EventArgs());
            }
        }

        async Task LoadAllPages(string query, int pagesInParallel, ITargetBlock<Apartment> target)
        {
            bool noMorePages = false;
            int page = 1;
            List<Task<bool>> tasks = new List<Task<bool>>(pagesInParallel);
            do
            {
                for (int i = page; i < page + pagesInParallel; i++)
                {
                    tasks.Add(LoadListPage(query, i, target));
                }
                page += pagesInParallel;

                await Task.WhenAll(tasks);

                noMorePages = tasks.Any(t => !t.Result);

                tasks.Clear();
            } while (!noMorePages);
            target.Complete();
        }



        async Task<bool> LoadListPage(string query, int pageNum, ITargetBlock<Apartment> target)
        {
            HtmlDocument doc = new HtmlDocument();

            doc.Load(await client.GetStreamAsync(query + "&page=" + pageNum), Encoding.UTF8);
            var pageItems = doc.DocumentNode.QuerySelectorAll(".content-main ul.EntityList-items li.EntityList-item--Regular");


            foreach (var item in pageItems)
            {
                var car = ProcessListItem(item);
                if (car != null)
                    target.Post(car);
            }

            PageProcessedCount++;
            if (Progress != null) Progress(this, new EventArgs());
            return pageItems.Any();
        }


        Apartment ProcessListItem(HtmlNode listItem)
        {
            try
            {
                var link = listItem.QuerySelector("h3>a");
                var description = listItem.QuerySelector(".entity-description-main");
                var id = int.Parse(link.Attributes["name"].Value.Trim());

                if (Apartments.Any(t => t.Id == id))
                {
                    return null;
                }

                var apartment = new Apartment()
                {
                    Link = njuskaloUrl + "/" + link.Attributes["href"].Value.Trim(),
                    Id = id,
                    Title = link.InnerText.Trim()
                };
                ListItemProcessedCount++;
                return apartment;
            }
            catch (Exception ex)
            {
                ListItemExceptionCount++;
                Debug.WriteLine(ex.Message);
                return null;
            }
        }


        async Task ProcessApartments(ISourceBlock<Apartment> source)
        {
            List<Task> waitingList = new List<Task>(1000);

            while (await source.OutputAvailableAsync())
            {
                Apartment apartment = source.Receive();

                waitingList.Add(LoadApartmentPage(apartment));
            }
            await Task.WhenAll(waitingList);
        }

        async Task LoadApartmentPage(Apartment apartment)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.Load(await client.GetStreamAsync(apartment.Link), Encoding.UTF8);
                var summaryTable = doc.DocumentNode.QuerySelector("table.table-summary tbody");
                var priceElement = doc.DocumentNode.QuerySelector(".price.price--hrk");
                var publishedElement = doc.DocumentNode.QuerySelector("time[pubdate]");
                var restOfTextElement = doc.DocumentNode.QuerySelector(".passage-standard.passage-standard--alpha");
                var userelement = doc.DocumentNode.QuerySelector(".Profile-username");

                var location = doc.DocumentNode.QuerySelector(".gmaps-view-location > a");
                if (location != null)
                {
                    var locationHref = location.GetAttributeValue("href", null);
                    var latitude = Regex.Match(locationHref, @".*\?q\=([0-9\.]*),").Groups[1].Value;
                    var longitude = Regex.Match(locationHref, @".*,([0-9\.]*)").Groups[1].Value;
                    apartment.Location = new Location()
                    {
                        Latitude = double.Parse(latitude, CultureInfo.InvariantCulture),
                        Longitude = double.Parse(longitude, CultureInfo.InvariantCulture)
                    };
                }

                var numberRegex = new Regex("([0-9]+)");

                apartment.Published = DateTime.Parse(publishedElement.Attributes["datetime"].Value);
                apartment.Price = int.Parse(numberRegex.Match(priceElement.InnerText.Replace(",", "").Replace(".", "").Trim()).Value);
                apartment.Description = restOfTextElement.InnerText.Trim();
                apartment.Seller = userelement.InnerText.Trim();

                foreach (var row in summaryTable.Children().Where(t => t.NodeType != HtmlNodeType.Text))
                {
                    var head = row.QuerySelector("th");
                    var cell = row.QuerySelector("td");
                    switch (head.InnerText)
                    {
                        case "Županija:":
                            apartment.State = GetInnerTextValue<string>(cell);
                            break;
                        case "Grad/Općina:":
                            apartment.County = GetInnerTextValue<string>(cell);
                            break;
                        case "Naselje:":
                            apartment.Neighbourhood = GetInnerTextValue<string>(cell);
                            break;
                        case "Tip stana:":
                            apartment.Type = GetInnerTextValue<string>(cell);
                            break;
                        case "Broj etaža:":
                            apartment.Levels = GetInnerTextValue<string>(cell);
                            break;
                        case "Broj soba:":
                            apartment.Rooms = GetInnerTextValue<string>(cell);
                            break;
                        case "Kat:":
                            apartment.Floor = GetInnerTextValue<string>(cell);
                            break;
                        case "Lift:":
                            apartment.Lift = GetInnerTextValue<bool>(cell);
                            break;
                        //case "Teretni lift:":
                        //    apartment.CargoLift = GetInnerTextValue<bool>(cell);
                        //    break;
                        case "Stambena površina:":
                            apartment.Area = GetInnerTextValue<float>(cell);
                            break;
                        case "Površina balkona:":
                            apartment.BalconyArea = GetInnerTextValue<float>(cell);
                            break;
                        case "Godina izgradnje:":
                            apartment.BuiltYear = GetInnerTextValue<int>(cell);
                            break;
                        case "Godina zadnje adaptacije:":
                            apartment.LastAdaptationYear = GetInnerTextValue<int>(cell);
                            break;
                        case "Dostupno od:":
                            apartment.AvailableFrom = GetInnerTextValue<string>(cell);
                            break;
                        case "Režije:":
                            apartment.Utilities = GetInnerTextValue<string>(cell);
                            break;
                        case "Namještenost:":
                            apartment.Furniture = GetInnerTextValue<string>(cell);
                            break;
                        //case "Iznos mjesečnog najma:":
                        //    apartment.Price = GetInnerTextValue<string>(cell);
                        //    break;
                        case "Orijentacija:":
                            apartment.Orientation = GetInnerTextValue<string>(cell);
                            break;
                        case "Blizina busa:":
                            apartment.CloseToBus = GetInnerTextValue<bool>(cell);
                            break;
                        case "Blizina tramvaja:":
                            apartment.CloseToTram = GetInnerTextValue<bool>(cell);
                            break;
                        case "Ulaz:":
                            apartment.Entrance = GetInnerTextValue<string>(cell);
                            break;
                        case "Broj parkirnih mjesta:":
                            apartment.ParkingSpaces = GetInnerTextValue<int>(cell);
                            break;
                        default:
                            Debug.WriteLine("Missing parameter: " + head.InnerText + " on " + apartment.Id);
                            break;
                    }
                }
                apartment.IsNew = true;
                ApartmentsProcessedCount++;
                Apartments.Add(apartment);
                if (Progress != null) Progress(this, new EventArgs());
            }
            catch (Exception ex)
            {
                ApartmentExceptionCount++;
            }
        }

        private dynamic GetInnerTextValue<T>(HtmlNode cell)
        {
            if (typeof(T) == typeof(string))
                return cell.InnerText.Trim();
            if (typeof(T) == typeof(int))
                return int.Parse(Regex.Match(cell.InnerText.Trim(), @"\d+").Value);
            if (typeof(T) == typeof(float))
                return float.Parse(cell.InnerText.Trim());
            if (typeof(T) == typeof(bool))
                return cell.InnerText.Trim().Equals("Da", StringComparison.InvariantCultureIgnoreCase);
            //if (typeof(T) == typeof(DateTime))
            //    return DateTime.ParseExact(cell.InnerText.Trim(), "dd.MM.yyyy.", CultureInfo.CurrentCulture);
            return default(T);
        }


    }
}
