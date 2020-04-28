using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;


namespace Music_Player
{
    

    public partial class Form1 : Form
    {
        WindowsMediaPlayer wp = new WindowsMediaPlayer();
        FileInfo[] songs;
        DirectoryInfo d;

        public Form1()
        {                   
            int i = 0;
            InitializeComponent();          
            string path;
            using (browse = new FolderBrowserDialog())
            {
                DialogResult result = browse.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(browse.SelectedPath))
                {
                    path = browse.SelectedPath;
                    string[] files = Directory.GetFiles(path);
                    d = new DirectoryInfo(path);
                    songs = d.GetFiles("*.mp3");
                    url.song = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);
                    listBox1.Items.Clear();
                    listBox1.BeginUpdate();
                    foreach (FileInfo file in songs)
                    {
                        listBox1.Items.Add(file.Name.Remove(file.Name.Length - 4));
                    }
                    listBox1.EndUpdate();
                    url.nr = listBox1.Items.Count;
                    url.name = new string[url.nr];
                    foreach (FileInfo file in songs)
                    {
                        url.name[i] = file.Name.Remove(file.Name.Length - 4);
                        i++;
                    }
                }
            }
        }

        // Play/Pause
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (wp.playState == WMPPlayState.wmppsPlaying)
                {
                    wp.controls.pause();
                }
                else
                {
                    wp.controls.play();
                }
                
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("No songs");
            }  
        }

        //Next
        private void button3_Click(object sender, EventArgs e)
        {
            try
            { //Shuffle
                if (checkBox1.Checked)
                {
                    Random rnd = new Random();
                    url.pos = rnd.Next(0, url.nr);
                    wp.URL = url.song[url.pos];
                    label1.Text = url.name[url.pos];
                    wp.controls.play();
                }
                else
                    if (checkBox2.Checked && url.pos == url.nr - 1)
                    {
                        url.pos = 0;
                        label1.Text = url.name[url.pos];
                        wp.URL = url.song[url.pos];
                        wp.controls.play();
                        url.pos++;
                    }
                    else
                    {
                        if (url.pos == url.nr - 1)
                        {
                            label1.Text = url.name[url.pos];
                            wp.URL = url.song[url.pos];
                            wp.controls.play();
                        }
                        else
                        {

                            label1.Text = url.name[url.pos];
                            wp.URL = url.song[url.pos];
                            wp.controls.play();
                            url.pos++;
                        }
                    }
            }
            catch (IndexOutOfRangeException)
            { }
        }

        //Back
        private void button4_Click(object sender, EventArgs e)
        {
            try {
                if (url.pos != 0)
                {
                        url.pos--;
                        wp.URL = url.song[url.pos];
                        wp.controls.play();
                        label1.Text = url.name[url.pos];
                }
                  
                wp.URL = url.song[url.pos];
                wp.controls.play();
                }
            catch(IndexOutOfRangeException)
            {
            }         
        }

        //Select Item from ListBox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedItem==null)
            {}
            else
            {
                string s = listBox1.SelectedItem.ToString();
                for (int i = 0; i < url.nr; i++)
                    if (url.song[i].Contains(s))
                        url.pos = i;
                wp.URL = url.song[url.pos];
                label1.Text = s;
                wp.controls.play();
            }
        }

        FolderBrowserDialog browse = new FolderBrowserDialog();
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {    
            DirectoryInfo d;
            int i = 0;
            string path;
            using (browse)
            {
                DialogResult result = browse.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(browse.SelectedPath))
                {
                    path = browse.SelectedPath;
                    string[] files = Directory.GetFiles(path);
                    d = new DirectoryInfo(path);
                    songs = d.GetFiles("*.mp3");
                    url.song = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);
                    listBox1.Items.Clear();
                    listBox1.BeginUpdate();
                    foreach (FileInfo file in songs)
                    {
                        listBox1.Items.Add(file.Name.Remove(file.Name.Length - 4));
                    }
                    listBox1.EndUpdate();
                    url.nr = listBox1.Items.Count;
                    url.name = new string[url.nr];
                    foreach (FileInfo file in songs)
                    {
                        url.name[i] = file.Name.Remove(file.Name.Length - 4); ;
                        i++;
                    }
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        Timer t;
        void t_Tick(object sender, EventArgs e)
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = (int)wp.currentMedia.duration;
            trackBar1.Value = (int)wp.controls.currentPosition;
            var timespan = TimeSpan.FromSeconds(wp.controls.currentPosition);
            var timeend = TimeSpan.FromSeconds(wp.currentMedia.duration);
            label2.Text = timespan.ToString(@"mm\:ss");
            label3.Text = timeend.ToString(@"mm\:ss");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            wp.controls.pause();
            wp.controls.currentPosition = trackBar1.Value;
            wp.controls.play();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                wp.URL = url.song[url.pos];
                t = new Timer();
                t.Interval = 1000;
                t.Tick += new EventHandler(t_Tick);
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < url.nr; i++)
            {
                if (url.song[i].ToUpper().Contains(textBox1.Text.ToUpper()))
                {
                    listBox1.Items.Add(url.name[i]);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.BeginUpdate();
            foreach(FileInfo file in songs)
            {
                listBox1.Items.Add(file.Name.Remove(file.Name.Length - 4));
            }
            listBox1.EndUpdate();
        }
    }
}
