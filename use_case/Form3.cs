using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace use_case
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (string line in File.ReadAllLines(Form1.historyDirectory))
            {
                if (line.Contains("function") &! line.Contains("=>"))
                {
                    string functionName0 = line.Split('(')[0];
                    string functionName1 = functionName0.Split(' ')[functionName0.Split(' ').Length - 1];
                    listBox1.Items.Add($"PHP -> {functionName1}");
                }
                if (line.Contains("Scenario") || line.Contains("Сценарий"))
                {
                    string scenario = line.Split(':')[1];
                    listBox1.Items.Add($"Feature -> {scenario}");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string text = File.ReadAllText(Form1.historyDirectory);
            //foreach(string miniText in text.Split())
        }
    }
}
