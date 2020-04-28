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

namespace Music_Player
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {         
                string path = Environment.CurrentDirectory.ToString() + @"\Users.txt";
                string domain = textBox1.Text + " " + textBox2.Text;   
                           
                if (textBox1.Text == "" || textBox2.Text == "")
                    MessageBox.Show("Please enter a username or password");
                else
                {
                    string readText = File.ReadAllText(path);
                    if (readText.Contains(domain)==false)
                    {
                        label5.Text = "Invalid e-mail address or password";
                        label5.ForeColor = Color.Red;
                    }
                    else
                    {
                        Form1 f = new Form1();
                        this.Hide();
                        f.Show();
                    }                   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            this.Hide();
            f.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            this.Hide();
            f.Show();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.DarkBlue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
