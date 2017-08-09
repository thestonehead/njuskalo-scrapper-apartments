namespace StanoviScrapper
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.pbProcess = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.txtApartments = new System.Windows.Forms.TextBox();
            this.btnImportCsv = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReprocess = new System.Windows.Forms.Button();
            this.btnOpenBrowser = new System.Windows.Forms.Button();
            this.btnStarApartment = new System.Windows.Forms.Button();
            this.btnHideApartment = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(0, 0);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 18;
            this.gmap.MinZoom = 0;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(1004, 898);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 10D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick);
            this.gmap.Load += new System.EventHandler(this.gMapControl1_Load);
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(7, 26);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(321, 20);
            this.txtQuery.TabIndex = 1;
            this.txtQuery.Text = "http://www.njuskalo.hr/iznajmljivanje-stanova?locationId=1153&price[min]=100&pric" +
    "e[max]=400&adsWithImages=1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Njuškalo search query string:";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(7, 52);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 3;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // pbProcess
            // 
            this.pbProcess.Location = new System.Drawing.Point(88, 52);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(240, 23);
            this.pbProcess.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Processed pages:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Processed apartments:";
            // 
            // txtPages
            // 
            this.txtPages.Location = new System.Drawing.Point(228, 81);
            this.txtPages.Name = "txtPages";
            this.txtPages.ReadOnly = true;
            this.txtPages.Size = new System.Drawing.Size(100, 20);
            this.txtPages.TabIndex = 7;
            // 
            // txtApartments
            // 
            this.txtApartments.Location = new System.Drawing.Point(228, 107);
            this.txtApartments.Name = "txtApartments";
            this.txtApartments.ReadOnly = true;
            this.txtApartments.Size = new System.Drawing.Size(100, 20);
            this.txtApartments.TabIndex = 7;
            // 
            // btnImportCsv
            // 
            this.btnImportCsv.Location = new System.Drawing.Point(7, 155);
            this.btnImportCsv.Name = "btnImportCsv";
            this.btnImportCsv.Size = new System.Drawing.Size(75, 23);
            this.btnImportCsv.TabIndex = 8;
            this.btnImportCsv.Text = "Import Csv";
            this.btnImportCsv.UseVisualStyleBackColor = true;
            this.btnImportCsv.Click += new System.EventHandler(this.btnImportCsv_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(7, 126);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(75, 23);
            this.btnExportCsv.TabIndex = 8;
            this.btnExportCsv.Text = "ExportCsv";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 216);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(351, 682);
            this.webBrowser.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReprocess);
            this.panel1.Controls.Add(this.btnOpenBrowser);
            this.panel1.Controls.Add(this.btnStarApartment);
            this.panel1.Controls.Add(this.btnHideApartment);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQuery);
            this.panel1.Controls.Add(this.btnExportCsv);
            this.panel1.Controls.Add(this.btnProcess);
            this.panel1.Controls.Add(this.btnImportCsv);
            this.panel1.Controls.Add(this.pbProcess);
            this.panel1.Controls.Add(this.txtApartments);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPages);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 216);
            this.panel1.TabIndex = 10;
            // 
            // btnReprocess
            // 
            this.btnReprocess.Location = new System.Drawing.Point(7, 79);
            this.btnReprocess.Name = "btnReprocess";
            this.btnReprocess.Size = new System.Drawing.Size(75, 23);
            this.btnReprocess.TabIndex = 11;
            this.btnReprocess.Text = "Reprocess";
            this.btnReprocess.UseVisualStyleBackColor = true;
            this.btnReprocess.Click += new System.EventHandler(this.btnReprocess_Click);
            // 
            // btnOpenBrowser
            // 
            this.btnOpenBrowser.Location = new System.Drawing.Point(169, 187);
            this.btnOpenBrowser.Name = "btnOpenBrowser";
            this.btnOpenBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnOpenBrowser.TabIndex = 10;
            this.btnOpenBrowser.Text = "Browser";
            this.btnOpenBrowser.UseVisualStyleBackColor = true;
            this.btnOpenBrowser.Click += new System.EventHandler(this.btnOpenBrowser_Click);
            // 
            // btnStarApartment
            // 
            this.btnStarApartment.Location = new System.Drawing.Point(88, 187);
            this.btnStarApartment.Name = "btnStarApartment";
            this.btnStarApartment.Size = new System.Drawing.Size(75, 23);
            this.btnStarApartment.TabIndex = 9;
            this.btnStarApartment.Text = "Favourite";
            this.btnStarApartment.UseVisualStyleBackColor = true;
            this.btnStarApartment.Click += new System.EventHandler(this.btnStarApartment_Click);
            // 
            // btnHideApartment
            // 
            this.btnHideApartment.Location = new System.Drawing.Point(7, 187);
            this.btnHideApartment.Name = "btnHideApartment";
            this.btnHideApartment.Size = new System.Drawing.Size(75, 23);
            this.btnHideApartment.TabIndex = 9;
            this.btnHideApartment.Text = "Hide";
            this.btnHideApartment.UseVisualStyleBackColor = true;
            this.btnHideApartment.Click += new System.EventHandler(this.btnHideApartment_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gmap);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1359, 898);
            this.splitContainer1.SplitterDistance = 1004;
            this.splitContainer1.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 898);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ProgressBar pbProcess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPages;
        private System.Windows.Forms.TextBox txtApartments;
        private System.Windows.Forms.Button btnImportCsv;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnStarApartment;
        private System.Windows.Forms.Button btnHideApartment;
        private System.Windows.Forms.Button btnOpenBrowser;
        private System.Windows.Forms.Button btnReprocess;
    }
}

