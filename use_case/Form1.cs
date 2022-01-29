using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;

namespace use_case
{
    public partial class Form1 : Form
    {
        public static string test0;
        public static string historyDirectory = Directory.GetCurrentDirectory() + "/historyFeature.txt";
        public static List<string> test = new List<string>();
        //public static MouseEventArgs d;
        public Form1()
        {
            InitializeComponent();
        }

        public void creating(string platform)
        {   
            richTextBox1.Text = "# language: ru" + Environment.NewLine + textBox1.Text.Replace("Feature", "Функционал")
                                                                .Replace("Background", "Контекст").Replace("When", "Если")
                                                                .Replace("Then", "Тогда").Replace("Scenario", $"@allure.label.framework:behat{Environment.NewLine}  @allure.label.feature:{platform}{Environment.NewLine}  Сценарий")
                                                                .Replace("Given", "Также").Replace("  And", "  И")
                                                                .Replace("@visual", "@визуал").Replace("@functional", "@функционал");
            foreach (string elem in listBox1.Items)
            {
                richTextBox1.Text = richTextBox1.Text.Replace(elem.Split('|')[0], elem.Split('|')[1]);
            }
        }

        public void oldTestsTransformer()
        {
            string functionName0 = textBox1.Text.Split('(')[0];
            string functionName1 = functionName0.Split(' ')[functionName0.Split(' ').Length-1];
            richTextBox1.Text += "public function " + functionName1 + "(TableNode $table)\n{\n";
            int counter = 0;
            richTextBox1.Text += "\t$tableRows = $table->getHash();\n";
            richTextBox1.Text += "\t$substeps = [\n";
            foreach (string line in textBox1.Text.Split('\n'))
            {
                if (line.Contains(';') & line.Contains('(') & ! 
                    line.Contains("substeps") &!
                    line.Contains("$lines = $assertion->getStrings();")
                    )
                {
                    richTextBox1.Text += "\t\t" + counter + " => function () {\n";
                    richTextBox1.Text += "\t\t\t" + line + "\n";
                    richTextBox1.Text += "\t\t},\n";
                    counter++;
                }
            }
            richTextBox1.Text += "\t$this->executeSubstepsFromDataTable($tableRows, $substeps);";
            richTextBox1.Text += "\n}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (radioButton3.Checked == true)
            {
                string platform = "";
                if (radioButton1.Checked == true) { platform = "vpbx"; creating(platform); }
                if (radioButton2.Checked == true) { platform = "telephony"; creating(platform); }
                if (radioButton5.Checked == true) { platform = "core"; creating(platform); }
                if (radioButton6.Checked == true) { platform = "robocall"; creating(platform); }
                if (radioButton1.Checked == false &
                    radioButton2.Checked == false &
                    radioButton5.Checked == false &
                    radioButton6.Checked == false)
                 { MessageBox.Show("Platform not selected!"); }
            }
            if (radioButton4.Checked == true)
            {
                oldTestsTransformer();
            }
            if (File.Exists(historyDirectory) == false) { File.Create(historyDirectory).Close(); }
            using (StreamWriter streamWriter = new StreamWriter(historyDirectory))
                streamWriter.Write(textBox1.Text + "\n\n----------------------------------------------\n\n" + richTextBox1.Text + "\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox3.Text + "|" + textBox4.Text);
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
            foreach (string elem in File.ReadAllLines("binds.txt"))
                listBox1.Items.Add(elem);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamWriter streamWriter = new StreamWriter("binds.txt"))
            {
                foreach (string elem in listBox1.Items)
                    streamWriter.WriteLine(elem);
            }
            MessageBox.Show("Saved successfully!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Jazis");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (File.Exists(historyDirectory) == true)
            {
                Form3 form3 = new Form3();
                form3.Show();
            }
        }
    }
}
