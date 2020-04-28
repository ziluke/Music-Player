using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Music_Player
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try
            {
                if (textBox1.Text == "")
                    MessageBox.Show("Please enter an e-mail address");
                else
                {
                    m.From = new MailAddress("lukasvictor99@gmail.com");    
                    sc.Host = "smtp.gmail.com";
                    sc.Port = 587;
                    sc.Credentials = new NetworkCredential("lukasvictor99@gmail.com", "MonsterTruck99");
                    sc.EnableSsl = true;

                    if (ok == true)
                    {
                        string path = Environment.CurrentDirectory.ToString() + @"\Users.txt";
                        string line;
                        StreamReader file = new StreamReader(path);
                        bool ok1=false;
                        StringBuilder sb = new StringBuilder();

                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Contains(textBox1.Text))
                            {
                                ok1 = true;
                                sb.AppendLine(line.ToString());
                                m.To.Add(textBox1.Text);
                                m.Subject = "Your account";
                                m.IsBodyHtml = true;
                                m.Body = sb.ToString();
                                sc.Send(m);
                            }
                            
                        }
                        if (!ok1)
                            MessageBox.Show("Naspa");
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There has been an error: "+ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            this.Hide();
            f.Show();
        }
        bool ok;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool isValid = Validator.EmailIsValid(textBox1.Text);
            if (isValid == false)
            {
                label2.ForeColor = Color.Red;
                label2.Text = "Not a valid E-mail address";
                ok = false;
            }
            else
            {
                label2.Text = "";
                ok = true;
            }

        }
    }
}
