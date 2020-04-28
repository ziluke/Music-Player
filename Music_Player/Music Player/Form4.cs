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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 f = new Form2();
                string path = Environment.CurrentDirectory.ToString() + @"\Users.txt";

                if (ok == true && ok1 == true)
                {
                        using (StreamWriter file = new StreamWriter(path, true))
                        {
                            file.WriteLine(textBox1.Text + " " + textBox2.Text);
                        }
                        label5.Text = "";
                        MessageBox.Show("Registration successfull!");
                        this.Hide();
                        f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        bool ok;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory.ToString() + @"\Users.txt";
            string domain = textBox1.Text;
            string readText = File.ReadAllText(path);
            bool isValid = Validator.EmailIsValid(textBox1.Text);
            if (isValid == false)
            {
                label6.ForeColor = Color.Red;
                label6.Text = "Not a valid E-mail address";
                ok = false;
            }
            else
            {
                if (readText.Contains(domain))
                {
                    label6.Text = "E-mail is already used";
                    label6.ForeColor = Color.Red;
                    ok = false;
                }
                else
                {
                    label6.Text = "";
                    ok = true;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength < 6 && textBox2.TextLength != 0)
                label4.Text = "Password too short";
            else
                label4.Text = "";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            label4.Text = "The password should be between 6-8 characters";
        }

        bool ok1;
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Contains(textBox2.Text) == false)
            {
                label5.ForeColor = Color.Red;
                label5.Text = "The passwords do not match";
                ok1 = false;
            }
            else
            {
                ok1 = true;
                label5.Text = "";
            }
        }
    }
}
