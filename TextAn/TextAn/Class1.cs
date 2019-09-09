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
    public class TextAnalysys
    {
        public TextAnalysys(Form1 form)
        {
            form1 = form;
        }
        Form1 form1;
        Form2 form2;
        Form3 form3;
        Form4 form4;
        OpenFileDialog dialog;
        public int top=5;
        string[] ex;
        public bool ByCount = false;

        public void SetTop(int val)
        {
            top = val;
        }

        public void Load()
        {
            string readStr = File.ReadAllText("Root\\exceptions.txt", Encoding.GetEncoding(1251));
            ex = readStr.Split(new[] { '-', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void CountWEX()
        {

           // form1.label3.Text = "Текущая книга: ";
            string s = "";
            form1.label1.Text = "Кол - во: ";
            if (dialog != null)
            {
                StreamReader sr = new StreamReader(dialog.FileName, Encoding.GetEncoding(1251));
                int count = 0;
                string[] words;
                while (sr.EndOfStream != true)
                {
                    s = sr.ReadLine();
                    words = s.Split(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ', ',', ':', '?', '!', '\t', '-', '–', '(', ')', '\n', '\r', '—', '[', ']', '.', '»', '«', ' ', '…', ';', '„', '&', '#' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string word in words)
                    {
                        count++;

                    }
                }
                form1.label1.Text += Convert.ToString(count);
                //form1.label3.Text += System.IO.Path.GetFileName(dialog.FileName);
                sr.Close();
            }
            else MessageBox.Show("Выберите книгу!");

        }

        bool EX(string word)
        {
            Regex theReg = new Regex(@"[a-zA-Z]");
            MatchCollection theMatches = theReg.Matches(word);
            foreach (string e in ex)
            {
                if (word == e) return true;
            }

            foreach (Match theMatch in theMatches)
            {
                return true;
            }
            return false;
        }

        public void CountEX()
        {
            ByCount = true;
            string s = "";
            form1.label2.Text = "Кол - во: ";
            //form1.label3.Text = "Текущая книга: ";
            List<string> dict = new List<string>();
            if (dialog != null)
            {
                StreamWriter sw = new StreamWriter("Root\\Final.txt", false, Encoding.GetEncoding(1251));
                StreamReader sr = new StreamReader(dialog.FileName, Encoding.GetEncoding(1251));
                int count = 0;
                string[] words;
                while (sr.EndOfStream != true)
                {
                    s = sr.ReadLine();
                    words = s.Split(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ', ',', ':', '?', '!', '\t', '-', '–', '(', ')', '\n', '\r', '—', '[', ']', '.', '»', '«', ' ', '…', ';', '„', '&', '#' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string word in words)
                    {
                        if (!EX(word))
                        {
                            dict.Add(word);
                            count++;
                        }
                    }
                }

               
                dict.Sort();
                bool t = false;
                int cont = 1;            
                    for (int i = 0; i < count; i++)
                {
                    for (int j = i + 1; j < count; j++)
                    {
                        t = false;
                        Regex theReg = new Regex(dict[i], RegexOptions.IgnoreCase);
                        if (theReg.IsMatch(dict[j])) { dict.RemoveAt(j); count--; cont++; j--; continue; }


                        
                        if (dict[i].Length == 4 && dict[j].Length == 4)
                        {

                            Regex theReg1 = new Regex(dict[i].Substring(0, dict[i].Length - 1), RegexOptions.IgnoreCase);
                            if (theReg1.IsMatch(dict[j].Substring(0, dict[j].Length - 1)))
                            { t = false; dict.RemoveAt(j); count--; cont++; j--; continue; }
                        }


                        if (dict[i].Length == 5 && dict[j].Length == 5)
                        { 
                           Regex theReg2 = new Regex(dict[i].Substring(0, dict[i].Length - 2), RegexOptions.IgnoreCase);
                           if (theReg2.IsMatch(dict[j].Substring(0, dict[j].Length - 2)))
                           { t = false; dict.RemoveAt(j); count--; cont++; j--; continue; }
                        }

                        if (dict[i].Length > 5 && dict[j].Length > 5)
                        {
                            Regex theReg3 = new Regex(dict[i].Substring(0, dict[i].Length - 3), RegexOptions.IgnoreCase);
                            if (theReg3.IsMatch(dict[j].Substring(0, dict[j].Length - 3)))
                            { t = false; dict.RemoveAt(j); count--; cont++; j--; continue; }
                        }


                    }
                    sw.WriteLine(dict[i] + ": " + cont);
                    cont = 1;
                }
                sr.Close();
                sw.Close();
                form1.label2.Text += Convert.ToString(count);
                if (form2 != null && !form2.IsDisposed)
                {
                    chartbuildByCounts();
                }
            }
            else MessageBox.Show("Выберите книгу!");
        }


        public void CountPEX()
        {
            ByCount = false;
            string s = "";
            form1.label4.Text = "Кол - во: ";
            //form1.label3.Text = "Текущая книга: ";
            if (dialog != null)
            {
                StreamWriter sw = new StreamWriter("Root\\Final.txt", false, Encoding.GetEncoding(1251));
                StreamReader sr = new StreamReader(dialog.FileName, Encoding.GetEncoding(1251));
                int count = 0;
                string[] words;
                while (sr.EndOfStream != true)
                {
                    s = sr.ReadLine();
                    words = s.Split(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ', ',', ':', '?', '!', '\t', '-', '–', '(', ')', '\n', '\r', '—', '[', ']', '.', '»', '«', ' ', '…', ';', '„', '&', '#' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string word in words)
                    {
                        if (!EX(word))
                        {
                            sw.WriteLine(word);
                            count++;
                        }
                    }
                }
                 form1.label4.Text += Convert.ToString(count);         
                sr.Close();
                sw.Close();
                if (form2 != null && !form2.IsDisposed)
                {
                    chartbuild();
                }
            }
            else MessageBox.Show("Выберите книгу!");
        }

        public void chartbuild()
        {
            string s = "";
           
            StreamReader sr = new StreamReader("Root\\Final.txt", Encoding.GetEncoding(1251));
            StreamWriter sw1 = new StreamWriter("Root\\Counts.txt", false, Encoding.GetEncoding(1251));
            StreamWriter sw2 = new StreamWriter("Root\\Words.txt", false, Encoding.GetEncoding(1251));
            List<string> strings = new List<string>();
            form2.chart1.Series[0].Points.Clear();
            form2.richTextBox1.Text = "";
            char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'э', 'ю', 'я' };
            char[] Rus = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Ч', 'Ц', 'Ч', 'Ш', 'Щ', 'Э', 'Ю', 'Я' };
            int i = 0, j = 0, count1 = 0, count2 = 0;
            int[] count = new int[30];
            while (sr.EndOfStream != true)
            {
                s = sr.ReadLine();    
                strings.Add(s);
                for (i = 0; i < 30; i++)
                {
                    if (s[0] == rus[i] || s[0] == Rus[i])
                    {
                        count[i]++;
                    }
                }
            }
            strings.Sort();
            for (i = 0; i < 30; i++)
            {
                form2.richTextBox1.Text += rus[i] + ": " + Convert.ToString(count[i]) + "\n";
                sw1.WriteLine(rus[i] + ": " + Convert.ToString(count[i]));
            }
            for (j = 0; j < top; j++)
            {
                for (i = 0; i < 30; i++)
                {
                    if (count[i] > count1)
                    {
                        count1 = count[i];
                        count2 = i;
                    }

                }


                form2.chart1.ChartAreas[0].BackColor = Color.LightSkyBlue;
                form2.chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                form2.chart1.Series[0].Points.AddXY(Convert.ToString(rus[count2]), Convert.ToString(count1));
                count[count2] = 0;
                count1 = 0;
                count2 = 0;
            }
            foreach (string item in strings)
            {
                sw2.WriteLine(item);
            }

            sr.Close();
            sw1.Close();
            sw2.Close();
        }


        public void chartbuildByCounts()
        {
            string s = "";
            string[] words;
            int counts = 0;
            StreamReader sr = new StreamReader("Root\\Final.txt", Encoding.GetEncoding(1251));
            StreamWriter sw1 = new StreamWriter("Root\\Counts.txt", false, Encoding.GetEncoding(1251));
            StreamWriter sw2 = new StreamWriter("Root\\Words.txt", false, Encoding.GetEncoding(1251));
            List<string> strings = new List<string>();
            form2.chart1.Series[0].Points.Clear();
            form2.richTextBox1.Text = "";
            int[] count = new int[100000];
            char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'э', 'ю', 'я' };
            char[] Rus = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Ч', 'Ц', 'Ч', 'Ш', 'Щ', 'Э', 'Ю', 'Я' };
            int i = 0, j = 0, count1 = 0, count2 = 0;
            while (sr.EndOfStream != true)
            {
                s = sr.ReadLine();
                words = s.Split(new[] { ':', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                count[counts] = Convert.ToInt32(words[1]);
                strings.Add(s);
                counts++;
            }
            strings.Sort();     

                for (j = 0; j < top; j++)
            {
                for (i = 0; i < counts; i++)
                {
                    if (count[i] > count1)
                    {
                        count1 = count[i];
                        count2 = i;
                    }

                }


                form2.chart1.ChartAreas[0].BackColor = Color.LightSkyBlue;
                form2.chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                form2.chart1.Series[0].Points.AddXY(Convert.ToString(strings[count2]), Convert.ToString(count1));
                count[count2] = 0;
                count1 = 0;
                count2 = 0;
            }
            foreach (string item in strings)
            {
                form2.richTextBox1.Text += item + "\n";
                sw2.WriteLine(item);
            }

            sr.Close();
            sw1.Close();
            sw2.Close();
        }





        public void OpenForm2()
        {
            if (form2 == null || form2.IsDisposed)
            {
                form2 = new Form2();
                form2.Show();
            }
            chartbuild();
        }

        public void OpenForm3()
        {
            if (form3 == null || form3.IsDisposed)
            {
                form3 = new Form3();
                form3.Show();
            }
            string readStr = File.ReadAllText("Root\\exceptions.txt", Encoding.GetEncoding(1251));
            form3.richTextBox2.Text += readStr;
        }

        public void OpenForm4()
        {
            if (form4 == null || form4.IsDisposed)
            {
                form4 = new Form4(form2,form1, this);
                form4.Show();
            }
        }

        public void OpenBook()
        {
            form1.label3.Text = "Текущая книга: ";
            dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK) ;
            form1.label3.Text += System.IO.Path.GetFileName(dialog.FileName);
        }
    }
}
