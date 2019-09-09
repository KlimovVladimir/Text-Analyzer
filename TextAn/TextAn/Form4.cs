using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace TextAn
{
    public partial class Form4 : Form
    {
        public Form4(Form2 form2, Form1 form1, TextAnalysys analys)
        {
            InitializeComponent();
            this.form2 = form2;
            this.form1 = form1;
            this.analys = analys;
        }
        Form2 form2;
        Form1 form1;
        TextAnalysys analys;
        private void button1_Click(object sender, EventArgs e)
        {
            if (form2 != null )
            {

                if (listBox1.SelectedIndex == 0) form2.chart1.Series[0].ChartType = SeriesChartType.Pie;
                if (listBox1.SelectedIndex == 1) form2.chart1.Series[0].ChartType = SeriesChartType.Column;
                if (listBox1.SelectedIndex == 2) form2.chart1.Series[0].ChartType = SeriesChartType.BoxPlot;
                if (listBox1.SelectedIndex == 3) form2.chart1.Series[0].ChartType = SeriesChartType.Spline;
                if (listBox1.SelectedIndex == 4) form2.chart1.Series[0].ChartType = SeriesChartType.Point;
                form2.chart1.Update();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            analys.SetTop(listBox2.SelectedIndex+1);
            if (form2 != null)
            {
                if (analys.ByCount == false) analys.chartbuild();
                if (analys.ByCount == true) analys.chartbuildByCounts();
                form2.chart1.Update();
            }
        }

    }
}
