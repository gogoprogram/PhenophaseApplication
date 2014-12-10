using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form6 : Form
    {
        private int axis_interval;
        private Dictionary<int, DateTime> rev_dict;

        private Dictionary<int, DateTime> get_re_dict()
        {
            return this.rev_dict;
        }

        public Form6(int interval)
        {
            InitializeComponent();

            axis_interval = interval;

            SetupChart();
            //panel1.AutoScroll = true;
            //panel1.VerticalScroll.Enabled = false;
            //panel1.VerticalScroll.Visible = false;

        }

        
        private void SetupChart()
        {
            //position the title
            chart1.Titles[0].Position.Auto = false;
            chart1.Titles[0].Position.X = 40F;
            chart1.Titles[0].Position.Y = 4F;
            chart1.Titles[0].Position.Width = 50F;
            chart1.Titles[0].Position.Height = 8F;
            //chart1.Titles[0].Docking = Docking.Top;
            //chart1.Titles["Title1"].DockedToChartArea = chart1.ChartAreas["ChartArea1"].Name;
            //chart1.Titles["Title1"].Docking = Docking.Top;
            //chart1.Titles["Title1"].Alignment = ContentAlignment.MiddleCenter;
            //chart1.Titles["Title1"].IsDockedInsideChartArea = false;

            //align the chartAreas
            // First set the ChartArea.InnerPlotPosition property.
            chart1.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            chart1.ChartAreas["ChartArea2"].InnerPlotPosition.Auto = true;
            chart1.ChartAreas["ChartArea3"].InnerPlotPosition.Auto = true;

            // Set the alignment properties so the "ChartArea2" chart area will allign to "ChartArea1"
            chart1.ChartAreas["ChartArea2"].AlignWithChartArea = "ChartArea1";
            chart1.ChartAreas["ChartArea2"].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            chart1.ChartAreas["ChartArea2"].AlignmentStyle = AreaAlignmentStyles.All;

            chart1.ChartAreas["ChartArea3"].AlignWithChartArea = "ChartArea1";
            chart1.ChartAreas["ChartArea3"].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            chart1.ChartAreas["ChartArea3"].AlignmentStyle = AreaAlignmentStyles.All;
           


            //disable the chart axis labels 
            //chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Enabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas["ChartArea2"].AxisX.LabelStyle.Enabled = false;
            //changing the start, end and interval of axis
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            //chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 366;
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = axis_interval;

            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 3;
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 1;
            
            chart1.ChartAreas["ChartArea2"].AxisX.Minimum = 0;
            //chart1.ChartAreas["ChartArea2"].AxisX.Maximum = 366;
            chart1.ChartAreas["ChartArea2"].AxisX.Interval = axis_interval;

            chart1.ChartAreas["ChartArea3"].AxisX.Minimum = 0;
            //chart1.ChartAreas["ChartArea3"].AxisX.Maximum = 366;
            chart1.ChartAreas["ChartArea3"].AxisX.Interval = axis_interval;
            
            //set the chart type for each series
            chart1.Series["Series1"].ChartType = SeriesChartType.Point;  //status series
            chart1.Series["Series3"].ChartType = SeriesChartType.Point; //event series
            chart1.Series["Series2"].ChartType = SeriesChartType.Line;  //intensity series

            chart1.Series["Series4"].ChartType = SeriesChartType.Line;  //climate var1 series
            chart1.Series["Series5"].ChartType = SeriesChartType.Line;  //climate var2 series

            //making all points of series3 to black
            chart1.Series["Series3"].BorderWidth = 1;
            chart1.Series["Series3"].Color = System.Drawing.Color.Black;
            chart1.Series["Series3"].MarkerStyle = MarkerStyle.Circle;
            //making all points of series1 to white with black border
            chart1.Series["Series1"].BorderWidth = 1;
            chart1.Series["Series1"].Color = System.Drawing.Color.White;
            chart1.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
            chart1.Series["Series1"].MarkerBorderColor = System.Drawing.Color.Black;

            //making all points of series2 to white
            chart1.Series["Series2"].BorderWidth = 1;
            chart1.Series["Series2"].Color = System.Drawing.Color.Black;
            chart1.Series["Series2"].MarkerColor = System.Drawing.Color.White;
            chart1.Series["Series2"].MarkerStyle = MarkerStyle.Circle;
            chart1.Series["Series2"].MarkerBorderColor = System.Drawing.Color.Black;
            chart1.Series["Series2"].MarkerSize = 5;
            //show the point values as Labels
            chart1.Series["Series2"].IsValueShownAsLabel = false;


            //set primary and secondary axis 
            chart1.ChartAreas["ChartArea3"].AxisY2.Enabled = AxisEnabled.True;
            chart1.Series["Series4"].XAxisType = AxisType.Primary;
            chart1.Series["Series4"].YAxisType = AxisType.Primary;
            chart1.Series["Series5"].XAxisType = AxisType.Primary;
            chart1.Series["Series5"].YAxisType = AxisType.Secondary;
            //---------------------------------------------------------
            
            
            // The palette is automatically applied on the chart, so by default,
            // you do not have access to the series colors.  Calling this method
            // will give you programmatic access to the series colors in the chart.
            chart1.ApplyPaletteColors();
            // Color the primary axis (AxisY) with the colors of the first series
            chart1.ChartAreas["ChartArea3"].AxisY.LineColor = chart1.Series["Series4"].Color;
            chart1.ChartAreas["ChartArea3"].AxisY.LabelStyle.ForeColor = chart1.Series["Series4"].Color;
            chart1.ChartAreas["ChartArea3"].AxisY2.LineColor = chart1.Series["Series5"].Color;
            chart1.ChartAreas["ChartArea3"].AxisY2.LabelStyle.ForeColor = chart1.Series["Series5"].Color;

            chart1.Series["Series4"].MarkerStyle = MarkerStyle.Square;
            chart1.Series["Series4"].MarkerSize = 3;

            chart1.Series["Series5"].MarkerStyle = MarkerStyle.Square;
            chart1.Series["Series5"].MarkerSize = 3;


            // Set Title font
            //chart1.ChartAreas["ChartArea2"].AxisX.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);
            chart1.ChartAreas["ChartArea2"].AxisX.TitleFont = new System.Drawing.Font("Trebuchet MS", 12, System.Drawing.FontStyle.Bold);
            // Set Title color
            chart1.ChartAreas["ChartArea2"].AxisX.TitleForeColor = Color.Black;

            //set zoom feature 
            // Enable range selection and zooming UI by default
            chart1.ChartAreas["ChartArea3"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea3"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea3"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea3"].AxisX.ScrollBar.IsPositionedInside = false;
            chart1.ChartAreas["ChartArea3"].AxisX.IsMarginVisible = false;
            chart1.ChartAreas["ChartArea3"].AxisY.IsMarginVisible = true;
            //chart1.ChartAreas["ChartArea3"].AlignmentStyle = AreaAlignmentStyles.All;
            
            
            chart1.ChartAreas["ChartArea2"].AxisX.ScrollBar.Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = false;
            /*
            chart1.ChartAreas["ChartArea2"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea2"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea2"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea2"].AxisX.ScrollBar.IsPositionedInside =false;
            //chart1.ChartAreas["ChartArea2"].CursorX.AutoScroll = true;

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = false;
            //chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.Size = 5;
            */

            //chart1.ChartAreas["ChartArea3"].AxisX.IsLabelAutoFit = true;
            //chart1.ChartAreas["ChartArea3"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont | LabelAutoFitStyles.WordWrap;
               
            ShowGrids();
            ShowLegends();

        }

        private void setXAxisMax(int max)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = max;
            chart1.ChartAreas["ChartArea2"].AxisX.Maximum = max;
            chart1.ChartAreas["ChartArea3"].AxisX.Maximum = max;

        }

        private void ShowGrids()
        {
            /*Chart Area 1*/
            chart1.ChartAreas["ChartArea1"].AxisX.LineColor = Color.White;
            //minor grid
            chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineColor = Color.LightSlateGray;
            //major grid
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 1;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            //ticks
            chart1.ChartAreas["ChartArea1"].AxisX.MinorTickMark.Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorTickMark.Enabled = false;
            /*Chart Area 2*/
            //minor grid
            chart1.ChartAreas["ChartArea2"].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea2"].AxisX.MinorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas["ChartArea2"].AxisY.MinorGrid.Enabled = false;
            //major grid
            chart1.ChartAreas["ChartArea2"].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas["ChartArea2"].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea2"].AxisX.MajorGrid.LineWidth = 1;
            chart1.ChartAreas["ChartArea2"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chart1.ChartAreas["ChartArea2"].AxisY.MajorGrid.LineColor = Color.LightGray;
            //ticks
            chart1.ChartAreas["ChartArea2"].AxisX.MinorTickMark.Enabled = true;
            chart1.ChartAreas["ChartArea2"].AxisX.MajorTickMark.LineDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas["ChartArea2"].AxisX.MajorTickMark.Interval = axis_interval;
            chart1.ChartAreas["ChartArea2"].AxisX.MinorTickMark.Interval = axis_interval/3;

            /*Chart Area 3*/
            //minor grid
            chart1.ChartAreas["ChartArea3"].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea3"].AxisX.MinorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas["ChartArea3"].AxisY.MinorGrid.Enabled = false;
            //major grid
            chart1.ChartAreas["ChartArea3"].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas["ChartArea3"].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea3"].AxisY2.MajorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea3"].AxisX.MajorGrid.LineWidth = 1;
            chart1.ChartAreas["ChartArea3"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chart1.ChartAreas["ChartArea3"].AxisY.MajorGrid.LineColor = Color.LightGray;
            //ticks
            chart1.ChartAreas["ChartArea3"].AxisX.MinorTickMark.Enabled = true;
            chart1.ChartAreas["ChartArea3"].AxisX.MajorTickMark.LineDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas["ChartArea3"].AxisX.MajorTickMark.Interval = axis_interval;
            chart1.ChartAreas["ChartArea3"].AxisX.MinorTickMark.Interval = axis_interval/3;

            chart1.ChartAreas["ChartArea3"].AxisY2.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);

        }

        private void ShowCustomLabels(double max, string abnd_name)
        {
            //chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle
            Axis axisY = chart1.ChartAreas["ChartArea1"].AxisY;

            // Set Y axis custom labels
            axisY.CustomLabels.Add(0, 4, "EVENT");
            axisY.CustomLabels.Add(0, 2, "STATUS");

            if (!string.IsNullOrEmpty(abnd_name))
            {
                axisY = chart1.ChartAreas["ChartArea2"].AxisY;
                CustomLabel customLabel;
                customLabel = axisY.CustomLabels.Add(0, max, "STATUS+INTENSITY");
                customLabel.RowIndex = 2;

                axisY = chart1.ChartAreas["ChartArea2"].AxisY;
                CustomLabel customLabel2;
                customLabel2 = axisY.CustomLabels.Add(0, max, abnd_name);
                customLabel2.RowIndex = 1;
            }
            /*  When assigning CustomLabels you have to assume complete control over Axis Labels , however , 
                this could be avoided if you place Custom Labels away from Axis by setting the CustomLabel.RowIndex property to a nonzero value. 
                Bydefault all the labels are painted at RowIndex zero including CustomLabels and RowIndex determines the distance away from the Axis. */

            //Axis axisX = chart1.ChartAreas["ChartArea2"].AxisX;
            //CustomLabel customLabel2;
            //customLabel2 = axisX.CustomLabels.Add(0, 366, "DAY OF YEAR");
            //customLabel2.RowIndex = 1;

        }

        private void ShowAxisLabels(ArrayList phenoAbundances)
        {
            //chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle
            Axis axisY = chart1.ChartAreas["ChartArea2"].AxisY;
            int Labelposition = 0;
            // Set Y axis custom labels
            foreach (string abnd in phenoAbundances)
            {
                axisY.CustomLabels.Add(0, (0 + 2 * Labelposition), abnd);
                Labelposition++;
            }

        }

        private void ShowLegends()
        {

            chart1.Legends["Legend1"].DockedToChartArea = "ChartArea3";
            chart1.Legends["Legend1"].IsDockedInsideChartArea = false;

            // Disable legend item for the first series
            chart1.Series["Series2"].IsVisibleInLegend = false;
            chart1.Series["Series1"].IsVisibleInLegend = false;
            chart1.Series["Series3"].IsVisibleInLegend = false;

            //chart1.Series["Series4"].IsVisibleInLegend = false;
            //chart1.Series["Series5"].IsVisibleInLegend = false;

            //chart1.Legends["Legend2"].DockedToChartArea = "ChartArea3";
            //chart1.Legends["Legend2"].IsDockedInsideChartArea = false;
            chart1.Legends["Legend2"].LegendStyle = LegendStyle.Column;
            // Set legend docking
            //chart1.Legends["Legend2"].Docking = Docking.Right;
            // Set legend alignment
            //chart1.Legends["Legend2"].Alignment = StringAlignment.Center;
            //positioning the chart
            chart1.Legends["Legend2"].Position.Auto = false;
            chart1.Legends["Legend2"].Position = new ElementPosition(75, 95, 25, 5);

            chart1.Legends["Legend1"].Position.Auto = false;
            chart1.Legends["Legend1"].Position = new ElementPosition(20, 97, 50, 3);


            //chart1.Legends["Legend1"].Title = "Marker Definition";
            chart1.Legends["Legend1"].LegendStyle = LegendStyle.Row;
            // Set legend docking
           // chart1.Legends["Legend1"].Docking = Docking.Bottom;
            // Set legend alignment
            chart1.Legends["Legend1"].Alignment = StringAlignment.Center;


            // Add custom legend item with line style
            LegendItem legendItem;
            /*legendItem = new LegendItem();
            legendItem.Name = "Null Values";
            legendItem.ImageStyle = LegendImageStyle.Marker;
            //legendItem.ShadowOffset = 1;
            legendItem.Color = Color.Black;
            legendItem.MarkerStyle = MarkerStyle.Cross;
            legendItem.MarkerSize = 7;
            legendItem.MarkerBorderColor = Color.White;
            chart1.Legends["Legend1"].CustomItems.Add(legendItem);
            */
            legendItem = new LegendItem();
            legendItem.Name = "Event Occured";
            legendItem.ImageStyle = LegendImageStyle.Marker;
            //legendItem.ShadowOffset = 1;
            legendItem.Color = Color.Black;
            legendItem.MarkerStyle = MarkerStyle.Circle;
            legendItem.MarkerSize = 5;
            legendItem.MarkerBorderColor = Color.Black;
            chart1.Legends["Legend1"].CustomItems.Add(legendItem);

            legendItem = new LegendItem();
            legendItem.Name = "Event Not Occured";
            legendItem.ImageStyle = LegendImageStyle.Marker;
            //legendItem.ShadowOffset = 1;
            legendItem.Color = Color.White;
            legendItem.MarkerStyle = MarkerStyle.Circle;
            legendItem.MarkerSize = 5;
            legendItem.MarkerBorderColor = Color.Black;
            chart1.Legends["Legend1"].CustomItems.Add(legendItem);

        }

        public void FillChart(ArrayList year_lst, string plantId, DateTime[] eventdates, string fgTable, string pheno_title, string pheno_abundance, string var1, string var2)
        {
            string years = "(";
            foreach (string y in year_lst)
                years = years + y + ",";
            years = years.Remove(years.LastIndexOf(','), 1);
            years += ")";

            Dictionary<DateTime, int> dcdoy = get_DOY_mapping(year_lst);

            this.Text = "Chart of " + plantId + " for year(s) " + years;

            Helper h = new Helper("phenophaseDBConnection");

            string condition = " WHERE YEAR(DATE) IN " + years + " AND PLANT_ID = '" + plantId + "'";

            ArrayList cols = new ArrayList();
            cols.Add("DATE");
            cols.Add(pheno_title);
            if (!string.IsNullOrEmpty(pheno_abundance))
                cols.Add(pheno_abundance);
            DataTable dt = h.get_table(fgTable, cols, condition);

            cols.Clear();

            condition = " WHERE TITLE_NAME = '" + pheno_title + "'";
            cols = h.get_data("pheno_title_info", "TITLE_DISPLAY_NAME", condition);
            //set the title of chart
            chart1.Titles[0].Text = cols[0].ToString() + " (" + plantId + ")";
            string abund_name = "";
            if (!string.IsNullOrEmpty(pheno_abundance))
                abund_name = "# " + cols[0].ToString();

            //setting the X-axis Title
            chart1.ChartAreas["ChartArea3"].AxisX.Title = "DAY OF YEAR(S)" + years;

            string[] percent_abundances = { "GR_202", "DS_202", "DS_214" };
            ArrayList desc = new ArrayList();
            foreach (string abnd in percent_abundances)
            {
                if (pheno_abundance.Equals(abnd))
                {
                    condition = " WHERE TITLE_NAME = '" + pheno_abundance + "'";
                    desc = h.get_data("pheno_title_info", "TITLE_DESCRIPTION", condition);
                    desc = parse_event_desc(desc[0].ToString());
                    ShowAxisLabels(desc);
                    abund_name = "% " + cols[0].ToString();
                    break;
                }
            }

            populate_dataPoints(dt, dcdoy, eventdates, abund_name);

            //chart1.DataManipulator.InsertEmptyPoints(1, IntervalType.Number, "Series1, Series2, Series3");

            ArrayList ev_stat_dates = new ArrayList();

            condition = " WHERE YEAR(DATE) IN " + years + " AND PLANT_ID = '" + plantId + "' AND " + pheno_title + " = 1";
            ev_stat_dates = h.get_data(fgTable, "DATE", condition);

            set_EventPoints(ev_stat_dates, dcdoy);

            //-----------------climate----------------------------
            Helper h2 = new Helper("phenologyDBConnection");

            string cmtableId = plantId.Substring(0, 2).ToLower();

            string climate_table = cmtableId + "_std_clim";
            
            condition = " WHERE YEAR(DATE) IN " + years;

            cols.Clear();
            cols.Add("DATE");
            cols.Add(var1);
            cols.Add(var2);

            DataTable dtb = h2.get_table(climate_table, cols, condition);

            climate_dataPoints(dtb, dcdoy);
            
            set_Series45PointLabels(dcdoy);

            /***********SET AXIS TITLES*************/
            chart1.ChartAreas["ChartArea3"].AxisY.Title = var1;
            chart1.ChartAreas["ChartArea3"].AxisY2.Title = var2;

            chart1.Series["Series4"].LegendText = var1;
            chart1.Series["Series5"].LegendText = var2;
            //chart1.ChartAreas["ChartArea1"].AxisX.IsStartedFromZero = true;
            //chart1.ChartAreas["ChartArea2"].AxisX.IsStartedFromZero = true;
            //chart1.ChartAreas["ChartArea3"].AxisX.IsStartedFromZero = true;
            //chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
            //chart1.ChartAreas["ChartArea1"].AxisX.Maximum = dcdoy.Values.Max();
            //chart1.ChartAreas["ChartArea2"].AxisX.Maximum = dcdoy.Values.Max();
            //chart1.ChartAreas["ChartArea3"].AxisX.Maximum = dcdoy.Values.Max();

        }

        private ArrayList parse_event_desc(string desc)
        {
            string[] values = desc.Split(';');
            ArrayList per_values = new ArrayList();
            foreach (string sv in values)
            {
                string[] v = sv.Split('=');
                per_values.Add(v[1]);
            }
            return per_values;
        }

        private void climate_dataPoints(DataTable dtb, Dictionary<DateTime, int> dcDoy)
        {
            int[] s45xVal = new int[dtb.Rows.Count];
            double[] s4yVal = new double[dtb.Rows.Count];
            double[] s5yVal = new double[dtb.Rows.Count];

            bool[] s4yisNullVal = new bool[dtb.Rows.Count];
            bool[] s5yisNullVal = new bool[dtb.Rows.Count];
            
            DateTime obsdate;
            int row_num = 0;
            double var1, var2;

            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow dr in dtb.Rows)
                {
                    obsdate = Convert.ToDateTime(dr[0].ToString());
                    
                    var1 = double.Parse(dr[1].ToString());
                    if (var1.Equals(-99.99))
                        var1 = double.NaN;
                    
                    var2 = double.Parse(dr[2].ToString());
                    if (var2.Equals(-99.99))
                        var2 = double.NaN;
                    
                    s45xVal[row_num] = dcDoy[obsdate];//obsdate.DayOfYear;
                    s4yVal[row_num] = var1;
                    s5yVal[row_num] = var2;

                    row_num++;
                }
            }

            //bind the arrays to the X and Y values of data points
            chart1.Series["Series4"].Points.DataBindXY(s45xVal, s4yVal);
            chart1.Series["Series5"].Points.DataBindXY(s45xVal, s5yVal);

            set_Series45EmptyPoints();

            //chart1.Series["Series4"].ToolTip = "(#VALX,#VALY)";
            //chart1.Series["Series5"].ToolTip = "(#VALX,#VALY)";

        }

        private void set_Series45EmptyPoints()
        {
            foreach (Series series in chart1.Series)
            {
                if (series.Name.Equals("Series4") || series.Name.Equals("Series5"))
                {
                    /*Set empty points visual appearance attributes*/
                    //series.EmptyPointStyle.Color = Color.Red;
                    //series.EmptyPointStyle.BorderWidth = 2;
                    //series.EmptyPointStyle.BorderDashStyle = ChartDashStyle.DashDot;
                    //series.EmptyPointStyle.MarkerStyle = MarkerStyle.None;

                    // Set empty points to transparent mode
                    series.EmptyPointStyle.BorderWidth = 0;
                    series.EmptyPointStyle.BorderDashStyle = ChartDashStyle.NotSet;
                    series.EmptyPointStyle.MarkerStyle = MarkerStyle.None;
                    /*-------------SET MARKER------------*/
                    //series.EmptyPointStyle.MarkerSize = 3;
                    //series.EmptyPointStyle.MarkerStyle = MarkerStyle.Cross;
                    //series.EmptyPointStyle.MarkerBorderColor = Color.Black;
                    //series.EmptyPointStyle.MarkerColor = Color.Red;
                    //series.EmptyPointStyle.Color = series.Color;
                }
            }
            // Set empty points values of Series1 to average.
            chart1.Series["Series4"]["EmptyPointValue"] = "Zero";//"Average";
            chart1.Series["Series5"]["EmptyPointValue"] = "Zero";//"Average";

        }

        private void populate_dataPoints(DataTable dtb, Dictionary<DateTime, int> dcDoy, DateTime[] eventDates, string abdName)
        {
            int[] s12xVal = new int[dtb.Rows.Count];
            int[] s1yVal = new int[dtb.Rows.Count];
            int[] s2yVal = new int[dtb.Rows.Count];

            bool[] s2yisNullVal = new bool[dtb.Rows.Count];
            //event series
            int[] s3xVal = new int[eventDates.Length];
            int[] s3yVal = new int[eventDates.Length];


            DateTime obsdate;
            int row_num = 0, phT_val, phA_val;

            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow dr in dtb.Rows)
                {
                    obsdate = Convert.ToDateTime(dr[0].ToString());
                    phT_val = int.Parse(dr[1].ToString());

                    if (!string.IsNullOrEmpty(abdName))
                    {
                        /*Support of -99 as NULL value*/
                        phA_val = int.Parse(dr[2].ToString());
                        if (phA_val == -99)
                        {
                            phA_val = 0;
                            s2yisNullVal[row_num] = true;
                        }
                        else
                        {
                            s2yisNullVal[row_num] = false;
                        }
                        /*if (dr[2].ToString().Equals("null"))
                        {
                            phA_val = 0;
                            s2yisNullVal[row_num] = true;
                        }
                        else
                        {
                            phA_val = int.Parse(dr[2].ToString());
                            s2yisNullVal[row_num] = false;
                        }*/

                        s2yVal[row_num] = phA_val;
                    }
                    else
                    {
                        phA_val = 0;
                        s2yisNullVal[row_num] = true;
                    }
                    s12xVal[row_num] = dcDoy[obsdate]; //obsdate.DayOfYear;
                    s1yVal[row_num] = 1;

                    row_num++;
                }
                
                int num_events = 0;
                foreach (DateTime ed in eventDates)
                {
                    s3xVal[num_events] = dcDoy[ed];  //ed.DayOfYear;
                    s3yVal[num_events] = 2;
                    num_events++;
                }
            }

            //bind the arrays to the X and Y values of data points
            chart1.Series["Series1"].Points.DataBindXY(s12xVal, s1yVal);
            chart1.Series["Series2"].Points.DataBindXY(s12xVal, s2yVal);
            chart1.Series["Series3"].Points.DataBindXY(s3xVal, s3yVal);

            if (!string.IsNullOrEmpty(abdName))
            {
                set_Series2Axis(s2yVal.Max(), s2yVal.Min());
                set_NullMarkers(s2yisNullVal);
            }
            else
            {
                chart1.Series["Series2"].BorderWidth = 0;
                chart1.Series["Series2"].ChartType = SeriesChartType.Point;
                //chart1.Series["Series2"].MarkerSize = 0;
                chart1.ChartAreas["ChartArea2"].AxisY.MajorTickMark.Enabled = false;
                chart1.ChartAreas["ChartArea2"].AxisY.MinorTickMark.Enabled = false;

                for (int p = 0; p < chart1.Series["Series2"].Points.Count; p++)
                {
                    chart1.Series["Series2"].Points[p].MarkerStyle = MarkerStyle.None;
                    chart1.Series["Series2"].Points[p].MarkerSize = 0;
                }
            }

            ShowCustomLabels(s2yVal.Max(), abdName);
        }

        private Dictionary<DateTime, int> get_DOY_mapping(ArrayList years)
        {
            Dictionary<DateTime, int> dict = new Dictionary<DateTime, int>();

            rev_dict = new Dictionary<int, DateTime>();
            
            string[] arr_year = years.ToArray(typeof(string)) as string[];
            DateTime date = new DateTime(int.Parse(arr_year[0]), 1, 1);

            int num_days = 0, days_to_add = 0;

            for (int i = 0; i < arr_year.Length; i++)
            {
                if (DateTime.IsLeapYear(int.Parse(arr_year[i])))
                    num_days = 366;
                else
                    num_days = 365;
                
                if(i != 0)
                    days_to_add += DateTime.IsLeapYear(int.Parse(arr_year[i - 1])) ? 366 : 365;
               
                for (int j = 0; j < num_days; j++)
                {
                    if (i == 0)
                    {
                        dict.Add(date, date.DayOfYear);
                        rev_dict.Add(date.DayOfYear, date);
                    }
                    else
                    {
                        dict.Add(date, date.DayOfYear + days_to_add);
                        rev_dict.Add(date.DayOfYear + days_to_add, date);
                    }
                    date = date.AddDays(1);
                }
            }

            //MessageBox.Show(dict[new DateTime(2013,12,31)].ToString());
            //MessageBox.Show("Max val:"+dict.Values.Max().ToString());
            setXAxisMax(dict.Values.Max());
            
            return dict;
        }

        private void set_Series45PointLabels(Dictionary<DateTime, int> dcdy)
        {
            int[] months = { 1, 4, 7, 10 };

            DateTime minDate = dcdy.Keys.Min();
            DateTime maxDate = dcdy.Keys.Max();

            Series series4 = chart1.Series["Series4"];
            
            for(int y = minDate.Year; y <= maxDate.Year; y++)
            {
                for(int i = 0; i < months.Length; i++)
                { 
                    DateTime pointDate = new DateTime(y, months[i], 1);
                    DataPoint dataPoint = series4.Points.FindByValue(dcdy[pointDate], "X");
                    //dataPoint.Color = System.Drawing.Color.Black;
                    if(dataPoint != null)
                        dataPoint.AxisLabel = pointDate.ToString("M/d/yyyy");

                }
            }

        }

        private void set_NullMarkers(bool[] s2yNullVal)
        {
            for (int i = 0; i < s2yNullVal.Length; i++)
            {
                if (s2yNullVal[i] == true)
                {
                    //chart1.Series["Series2"].Points[i].MarkerColor = System.Drawing.Color.Black;
                    //chart1.Series["Series2"].Points[i].MarkerSize = 8;
                    //chart1.Series["Series2"].Points[i].MarkerStyle = MarkerStyle.Cross;
                    chart1.Series["Series2"].Points[i].MarkerSize = 0;
                    chart1.Series["Series2"].Points[i].MarkerStyle = MarkerStyle.None;
                }
            }
        }

        private void set_Series2Axis(int max, int min)
        {
            int interval = 1;
            if (max > 0 && min >= 0)
                interval = (int)Math.Ceiling((double)(max - min) / 10);

            //changing the start, end and interval of axis
            chart1.ChartAreas["ChartArea2"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea2"].AxisY.Maximum = max;
            chart1.ChartAreas["ChartArea2"].AxisY.Interval = interval;
        }

        private void set_EventPoints(ArrayList evStat_Dates, Dictionary<DateTime, int> dcDoy)
        {

            Series series1 = chart1.Series["Series1"];
            Series series2 = chart1.Series["Series2"];

            DataPoint dataPoint;
            //changing color of a specific point
            //chart1.Series["Series1"].Points[0].Color = System.Drawing.Color.Black;

            for (int i = 0; i < evStat_Dates.Count; i++)
            {
                dataPoint = series1.Points.FindByValue(dcDoy[Convert.ToDateTime(evStat_Dates[i])], "X");
                dataPoint.Color = System.Drawing.Color.Black;

                dataPoint = series2.Points.FindByValue(dcDoy[Convert.ToDateTime(evStat_Dates[i])], "X");
                dataPoint.MarkerColor = System.Drawing.Color.Black;
            }
        }
        ToolTip tooltip = new ToolTip();
        Point? prevPosition = null;

        private void Chart1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Dictionary<int, DateTime> revDict = get_re_dict();
            
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            
            // Call HitTest
            var results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
                        var pointY2Pixel = result.ChartArea.AxisY2.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (2 pixels around)
                        if ( (Math.Abs(pos.X - pointXPixel) < 2 && Math.Abs(pos.Y - pointYPixel) < 2) || 
                            (Math.Abs(pos.X - pointXPixel) < 2 && Math.Abs(pos.Y - pointY2Pixel) < 2) )
                        {
                            int day = (int)prop.XValue;
                            double val = (double)prop.YValues[0];
                            DateTime d = revDict[day];
                            string sd = d.ToString("MM-dd-yyyy");
                            tooltip.Show("X=" + sd + ",Y=" + val.ToString(), this.chart1, pos.X, pos.Y - 15);
                        }
                    }
                }
            }
              
        }


        private void chart1_SizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("" + chart1.Size.Width + " " + chart1.Size.Height);
            //chart1.Titles[0].Position.X = (float) (chart1.Size.Width * 0.04);
            //chart1.Titles[0].Position.Y = (float) (chart1.Size.Height * 0.01);
            //chart1.Titles[0].Position.Width = (int) (chart1.Size.Width * (0.07));
            //chart1.Titles[0].Position.Height = (int) (chart1.Size.Height * (0.01));
        }

        private void btnSaveChart_Click(object sender, EventArgs e)
        {
            Helper helperSave = new Helper();
            helperSave.saveDialog(this, "chart1");
        }

        
        /*
        public void ChangeChartSize(int width, int height)
        {
            //this.Size = new Size(width, height);
            chart1.Size = new Size(width, height);
            chart1.Invalidate();
        }*/


    }
}
