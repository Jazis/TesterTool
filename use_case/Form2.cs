using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace use_case
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void when()
        {
            textBox3.Text = "/**" + Environment.NewLine +
                $" * @When {textBox1.Text}" + Environment.NewLine +
                $" * @When {textBox2.Text}" + Environment.NewLine +
                "*/";
        }

        public void then()
        {
            textBox3.Text = "/**" + Environment.NewLine +
                $" * @Then {textBox1.Text}" + Environment.NewLine +
                $" * @Then {textBox2.Text}" + Environment.NewLine +
                "*/";
        }

        public void def()
        {
            textBox3.Text = "/*" + Environment.NewLine +
                $" -> {textBox1.Text}" + Environment.NewLine +
                $" -> {textBox2.Text}" + Environment.NewLine +
                " */";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            if (radioButton1.Checked == true) { when(); }
            if (radioButton2.Checked == true) { then(); }
            if (radioButton3.Checked == true) { def(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text + " :arg";
        }
    }
}
