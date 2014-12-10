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
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class FrmGraph : Form
    {
        private int axis_interval;
        private Dictionary<int, DateTime> rev_dict;

        private Dictionary<int, DateTime> get_re_dict()
        {
            return this.rev_dict;
        }

        public FrmGraph(int interval)
        {
            InitializeComponent();
            axis_interval = interval;
        }

        private void FrmGraph_Load(object sender, EventArgs e)
        {
            SetupChart();
        }
       
        private void SetupChart()
        {
            //position the title
            chart1.Titles[0].Position.Auto = false;
            chart1.Titles[0].Position.X = 40F;
            chart1.Titles[0].Position.Y = 4F;
            chart1.Titles[0].Position.Width = 50F;
            chart1.Titles[0].Position.Height = 8F;

            // First set the ChartArea.InnerPlotPosition property.
            chart1.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            chart1.ChartAreas["ChartArea2"].InnerPlotPosition.Auto = true;
            
            // Set the alignment properties so the "ChartArea2" chart area will allign to "ChartArea1"
            chart1.ChartAreas["ChartArea2"].AlignWithChartArea = "ChartArea1";
            chart1.ChartAreas["ChartArea2"].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            chart1.ChartAreas["ChartArea2"].AlignmentStyle = AreaAlignmentStyles.All;

            //change color of x-axis
            chart1.ChartAreas["ChartArea1"].AxisX.LineColor = Color.White;
            //disable the chart axis labels 
            //chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Enabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
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

            //set the chart type for each series
            chart1.Series["Series1"].ChartType = SeriesChartType.Point;  //status series
            chart1.Series["Series3"].ChartType = SeriesChartType.Point; //event series
            chart1.Series["Series2"].ChartType = SeriesChartType.Line;  //intensity series
                        
            //making all points of series3 to black
            chart1.Series["Series3"].BorderWidth = 1;
            chart1.Series["Series3"].Color = System.Drawing.Color.Black;
            chart1.Series["Series3"].MarkerStyle = MarkerStyle.Circle;

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

           

            // Set Title font
            //chart1.ChartAreas["ChartArea2"].AxisX.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);
            chart1.ChartAreas["ChartArea2"].AxisX.TitleFont = new System.Drawing.Font("Trebuchet MS", 12, System.Drawing.FontStyle.Bold);
            // Set Title color
            chart1.ChartAreas["ChartArea2"].AxisX.TitleForeColor = Color.Black;

            //set zoom feature 
            // Enable range selection and zooming UI by default
            chart1.ChartAreas["ChartArea2"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea2"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea2"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea2"].AxisX.ScrollBar.IsPositionedInside = false;
            chart1.ChartAreas["ChartArea2"].AxisX.IsMarginVisible = false;
            chart1.ChartAreas["ChartArea2"].AxisY.IsMarginVisible = true;

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = false;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = false;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = false;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = false;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = false;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = false;
            //chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
            //chart1.ChartAreas["ChartArea1"].AxisY.IsMarginVisible = false;


            ShowGrids();
            ShowLegends();

        }

        private void ShowGrids()
        {
            /*Chart Area 1*/
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
            chart1.ChartAreas["ChartArea2"].AxisX.MinorTickMark.Interval = axis_interval / 3;
            
        }
        
        private void ShowCustomLabels(double max, string abnd_name, bool disable_label)
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

                if (disable_label)
                {
                    axisY = chart1.ChartAreas["ChartArea2"].AxisY;
                    CustomLabel customLabel3;
                    customLabel3 = axisY.CustomLabels.Add(0, max, "");
                    customLabel3.RowIndex = 0;
                }

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
            // Disable legend item for the first series
            chart1.Series["Series2"].IsVisibleInLegend = false;
            chart1.Series["Series1"].IsVisibleInLegend = false;
            chart1.Series["Series3"].IsVisibleInLegend = false;

            //chart1.Legends["Legend1"].Title = "Marker Definition";
            chart1.Legends["Legend1"].LegendStyle = LegendStyle.Row;
            // Set legend docking
            chart1.Legends["Legend1"].Docking = Docking.Bottom;
            // Set legend alignment
            chart1.Legends["Legend1"].Alignment = StringAlignment.Center;


            // Add custom legend item with line style
            LegendItem legendItem;
            /*LegendItem legendItem = new LegendItem();
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

        public void FillChart(ArrayList year_lst, string plantId, DateTime[] eventdates, string fgTable, string pheno_title, string pheno_abundance)
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
            chart1.ChartAreas["ChartArea2"].AxisX.Title = "DAY OF YEAR(S)" + years;

            bool disable_axisLabel = populate_dataPoints(dt, dcdoy, eventdates, abund_name);

            string[] percent_abundances = { "GR_202", "DS_202", "DS_214" };
            ArrayList desc = new ArrayList();
            foreach (string abnd in percent_abundances)
            {
                if (pheno_abundance.Equals(abnd))
                {
                    condition = " WHERE TITLE_NAME = '" + pheno_abundance + "'";
                    desc = h.get_data("pheno_title_info", "TITLE_DESCRIPTION", condition);
                    desc = parse_event_desc(desc[0].ToString());
                    if(!disable_axisLabel)
                        ShowAxisLabels(desc);
                    abund_name = "% " + cols[0].ToString();
                    break;
                }
            }

            

            ArrayList ev_stat_dates = new ArrayList();

            condition = " WHERE YEAR(DATE) IN " + years  + " AND PLANT_ID = '" + plantId + "' AND " + pheno_title + " = 1";
            ev_stat_dates = h.get_data(fgTable, "DATE", condition);

            set_EventPoints(ev_stat_dates, dcdoy);
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

                if (i != 0)
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

            setXAxisMax(dict.Values.Max());

            return dict;
        }

        private void setXAxisMax(int max)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = max;
            chart1.ChartAreas["ChartArea2"].AxisX.Maximum = max;
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

        private bool populate_dataPoints(DataTable dtb, Dictionary<DateTime, int> dcDoy, DateTime[] eventDates, string abdName)
        {
            int[] s12xVal = new int[dtb.Rows.Count]; 
            int[] s1yVal = new int[dtb.Rows.Count]; 
            int[] s2yVal = new int[dtb.Rows.Count];

            bool[] s2yisNullVal = new bool[dtb.Rows.Count];
            bool empty_series = false;

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
                    s12xVal[row_num] = dcDoy[obsdate];//obsdate.DayOfYear;
                    s1yVal[row_num] = 1;
                    
                    row_num++;
                }
                int num_events = 0;
                foreach (DateTime ed in eventDates)
                {
                    s3xVal[num_events] = dcDoy[ed];//ed.DayOfYear;
                    s3yVal[num_events] = 2;
                    num_events++;
                }
            }
           
            //bind the arrays to the X and Y values of data points
            chart1.Series["Series1"].Points.DataBindXY(s12xVal, s1yVal);
            chart1.Series["Series2"].Points.DataBindXY(s12xVal, s2yVal);
            chart1.Series["Series3"].Points.DataBindXY(s3xVal, s3yVal);

            /*-----------FIX SHOWING the CUSTOM LABEL------------------------*/
            if (s2yVal.Max() == 0)
            {
                s2yVal[row_num - 5] = 100; //put a dummy value for custom label
                empty_series = true;
                //chart1.ChartAreas["ChartArea2"].AxisY.LabelStyle.Enabled = false;
                chart1.ChartAreas["ChartArea2"].AxisY.MajorTickMark.Enabled = false;
            }
            /*-------------------------------------------------------------*/

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


            ShowCustomLabels(s2yVal.Max(), abdName, empty_series);

            return empty_series;
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
            if(max > 0 && min >= 0)
                interval = (int)Math.Ceiling((double)(max - min) / 10);
            
            //changing the start, end and interval of axis
            chart1.ChartAreas["ChartArea2"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea2"].AxisY.Maximum = max;
            chart1.ChartAreas["ChartArea2"].AxisY.Interval = interval;
        }

        private void set_EventPoints(ArrayList evStat_Dates, Dictionary<DateTime, int> dcDoy)
        {

            Series series3 = chart1.Series["Series1"];
            Series series2 = chart1.Series["Series2"]; //intensity series
            //changing color of a specific point
            //chart1.Series["Series1"].Points[0].Color = System.Drawing.Color.Black;
            DataPoint dataPoint;

            for (int i = 0; i < evStat_Dates.Count; i++)
            {
                dataPoint = series3.Points.FindByValue(dcDoy[Convert.ToDateTime(evStat_Dates[i])], "X");
                dataPoint.Color = System.Drawing.Color.Black;
                //change intersity series data points
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

                        // check if the cursor is really close to the point (2 pixels around)
                        if ((Math.Abs(pos.X - pointXPixel) < 2 && Math.Abs(pos.Y - pointYPixel) < 2) )
                        {
                            int day = (int)prop.XValue;
                            int ints = (int)prop.YValues[0];
                            DateTime d = revDict[day];
                            string sd = d.ToString("MM-dd-yyyy");
                            tooltip.Show("X=" + sd +",Y="+ints.ToString(), this.chart1, pos.X, pos.Y - 15);
                            //tooltip.Show("X=" + sd, this.chart1, pos.X, pos.Y - 15);
                        }
                    }
                }
            }

        }


        private void btnSaveChart_Click(object sender, EventArgs e)
        {
            Helper helperSave = new Helper();
            helperSave.saveDialog(this, "chart1");
        }

       

    }
}
