using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

using System.Windows.Forms.DataVisualization.Charting;

namespace TextAn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            analys = new TextAnalysys(this);
        }
        TextAnalysys analys;

        private void button1_Click(object sender, EventArgs e)
        {

            analys.CountWEX();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            analys.CountEX();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            analys.CountPEX();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            analys.Load();
        }

       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            analys.OpenForm2();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            analys.OpenForm3();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            analys.OpenBook();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            analys.OpenForm4();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.WriteAllText("Root\\Final.txt", string.Empty);
            File.WriteAllText("Root\\Counts.txt", string.Empty);
            File.WriteAllText("Root\\Words.txt", string.Empty);
        }

        
    }
}

