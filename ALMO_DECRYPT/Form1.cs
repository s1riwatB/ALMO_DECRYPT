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
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using OpenPgpLib;

namespace ALMO_DECRYPT
{
    public partial class Form1 : Form
    {
        public int debug_no = 0;
        public Form1()
        {
            InitializeComponent();
            debug_no = 1;
            pictureBox1.BackgroundImage = null;
            pictureBox2.BackgroundImage = null;
            pictureBox3.BackgroundImage = null;
            pictureBox4.BackgroundImage = null;
            pictureBox5.BackgroundImage = null;
            pictureBox6.BackgroundImage = null;
            pictureBox7.BackgroundImage = null;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            debug_no = 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                debug_no = 3;
                string config_file_path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Config.db";
                string[] lines = File.ReadAllLines(config_file_path);
                if (File.Exists(config_file_path))
                {
                    // Read a text file line by line.  


                    textBox4.Text = lines[0].Split('=')[1].ToString();
                    textBox5.Text = lines[1].Split('=')[1].ToString();
                    textBox6.Text = lines[2].Split('=')[1].ToString();
                    textBox7.Text = lines[3].Split('=')[1].ToString();
                    textBox8.Text = lines[4].Split('=')[1].ToString();
                    textBox9.Text = lines[5].Split('=')[1].ToString();
                    textBox10.Text = lines[6].Split('=')[1].ToString();
                    textBox11.Text = lines[7].Split('=')[1].ToString();
                    textBox12.Text = lines[8].Split('=')[1].ToString();
                    textBox13.Text = lines[9].Split('=')[1].ToString();
                    textBox14.Text = lines[10].Split('=')[1].ToString();
                    textBox15.Text = lines[11].Split('=')[1].ToString();
                    textBox16.Text = lines[12].Split('=')[1].ToString();
                    textBox17.Text = lines[13].Split('=')[1].ToString();
                    textBox18.Text = lines[14].Split('=')[1].ToString();
                    textBox19.Text = lines[15].Split('=')[1].ToString();
                    textBox20.Text = lines[16].Split('=')[1].ToString();

                    //label27.Text = lines[10].Split('=')[1].ToString();
                    //label28.Text = lines[11].Split('=')[1].ToString();
                    //label29.Text = lines[12].Split('=')[1].ToString();
                    //label30.Text = lines[13].Split('=')[1].ToString();
                    //label31.Text = lines[14].Split('=')[1].ToString();
                    //label32.Text = lines[15].Split('=')[1].ToString();
                    //label33.Text = lines[16].Split('=')[1].ToString();
                    statusStrip1.Text = lines[17].Split('=')[1].ToString();
                    debug_no = 4;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(debug_no.ToString() + " " + ex.Message.ToString());
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            debug_no = 5;
            if (checkBox1.Checked)
            {
                postVer(textBox14.Text, label28);
            }
            if (checkBox2.Checked)
            {
                postVer(textBox15.Text, label29);
            }
            if (checkBox3.Checked)
            {
                postVer(textBox16.Text, label30);
            }
            if (checkBox4.Checked)
            {
                postVer(textBox17.Text, label31);
            }
            if (checkBox5.Checked)
            {
                postVer(textBox18.Text, label32);
            }
            if (checkBox6.Checked)
            {
                postVer(textBox19.Text, label33);
            }
            if (checkBox7.Checked)
            {
                postVer(textBox20.Text, label34);
            }
        }

        private void postVer(string text, Label aMLO_TH_VERSION)
        {
            debug_no = 6;
            try
            {

                var client = new RestClient(statusStrip1.Text.ToString());
                debug_no = 7;
                client.Authenticator = new HttpBasicAuthenticator(textBox1.Text.ToString(), textBox2.Text.ToString());
                client.AddDefaultHeader("x-api-key", textBox3.Text.ToString());
                debug_no = 8;
                client.AddDefaultHeader("ContentType", "application/json");
                var request = new RestRequest(text, Method.POST, DataFormat.Json);
                var response = client.Post(request);
                if (((RestSharp.RestResponseBase)response).StatusDescription == "OK")
                {
                    debug_no = 9;
                    var x = ((RestSharp.RestResponseBase)response).Content.ToString();
                    x = x.Replace("[", "");
                    x = x.Replace("]", "");
                    debug_no = 10;
                    VER obj = JsonConvert.DeserializeObject<VER>(x);
                    debug_no = 11;
                    aMLO_TH_VERSION.Text = obj.VERSION_NUMBER + "::" + obj.CREATE_DATE;
                    debug_no = 12;
                }
                else {
                    MessageBox.Show(((RestSharp.RestResponseBase)response).StatusDescription);
                }
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(debug_no.ToString() + "_" +ex.Message.ToString());
            }

        }
        private void postData(string text, PictureBox picbox)
        {
            debug_no = 13;
            picbox.Visible = true;
            try
            {
                var client = new RestClient(statusStrip1.Text.ToString());
                client.Authenticator = new HttpBasicAuthenticator(textBox1.Text.ToString(), textBox2.Text.ToString());
                debug_no = 14;
                client.AddDefaultHeader("x-api-key", textBox3.Text.ToString());
                client.AddDefaultHeader("ContentType", "application/json");
                var request = new RestRequest(text, Method.POST, DataFormat.Json);
                debug_no = 15;
                var response = client.Post(request);
                if (((RestSharp.RestResponseBase)response).StatusDescription == "OK")
                {
                    var x = ((RestSharp.RestResponseBase)response).Content.ToString();
                    obj obj = JsonConvert.DeserializeObject<obj>(x);
                    debug_no = 16;
                    //BUG PATH FIX
                    if (textBox4.Text.Substring(textBox4.Text.Length - 1) != "\\")
                    {
                        textBox4.Text = textBox4.Text + "\\";
                    }
                    debug_no = 17;
                    File.WriteAllBytes(textBox4.Text + obj.FileName, Convert.FromBase64String(obj.Result));
                    debug_no = 18;
                    OpenPgp.DecryptFile(textBox4.Text + obj.FileName, textBox4.Text + text.Split('/')[text.Split('/').Length - 1].ToString() + ".ZIP", textBox5.Text, textBox6.Text);
                    debug_no = 19;
                    File.Delete(textBox4.Text + obj.FileName);
                    debug_no = 20;
                    picbox.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + "ok.png");
                    picbox.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + "ok.png");
                    debug_no = 21;
                    picbox.Refresh();
                    debug_no = 22;
                }
                else {
                    MessageBox.Show(((RestSharp.RestResponseBase)response).StatusDescription);
                }

            }
            catch (Exception ex)
            {
                picbox.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + "error.png");
                picbox.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory.ToString() + "error.png");
                picbox.Refresh();
                MessageBox.Show(debug_no.ToString() + "_"+ ex.Message.ToString());
            }

        }


        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox4.Text = folderDlg.SelectedPath;
                //Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    textBox5.Text = file;
                }
                catch (IOException)
                {
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
            pictureBox6.Image = null;
            pictureBox7.Image = null;
            if (checkBox1.Checked)
            {
                postData(textBox7.Text, pictureBox1);
            }
            if (checkBox2.Checked)
            {
                postData(textBox8.Text, pictureBox2);
            }
            if (checkBox3.Checked)
            {
                postData(textBox9.Text, pictureBox3);
            }
            if (checkBox4.Checked)
            {
                postData(textBox10.Text, pictureBox4);
            }
            if (checkBox5.Checked)
            {
                postData(textBox11.Text, pictureBox5);
            }
            if (checkBox6.Checked)
            {
                postData(textBox12.Text, pictureBox6);
            }
            if (checkBox7.Checked)
            {
                postData(textBox13.Text, pictureBox7);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            var formPopup = new Form2();
            formPopup.ShowDialog(this);
        }
    }
    class obj
    {
        public string ReturnStatus { get; set; }
        public string ReturnMessage { get; set; }
        public string KeyID { get; set; }
        public string FileName { get; set; }
        public string mimeType { get; set; }
        public string TotalRecord { get; set; }
        public string Result { get; set; }
    }
    class VER
    {
        public string LIST_NAME { get; set; }
        public string VERSION_NUMBER { get; set; }
        public string CREATE_DATE { get; set; }

    }
}
