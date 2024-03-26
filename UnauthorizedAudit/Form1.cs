using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnauthorizedAudit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void scan(string web_path, string[] file_path)
        {
            string[] filters = null;
            int num = 0;
            try
            {
                filters = (textBox3.Text).Split('&');
            }
            catch { MessageBox.Show("过滤语法有误"); return; }
            int len = filters.Length;
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            foreach (string a in file_path)
            {
                string result = a.Replace("\\", "/");
                string reqpath = web_path + result;
                if (checkBox1.Checked)
                {
                    richTextBox1.AppendText("[DEBUG] [访问目标] " + reqpath + "\n");
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                }
                WebRequest webrequest = WebRequest.Create(reqpath);
                HttpWebResponse res;
                try
                {
                    res = (HttpWebResponse)webrequest.GetResponse();
                }
                catch (WebException ex)
                {
                    res = (HttpWebResponse)ex.Response;
                }
                StreamReader sr = new StreamReader(res.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8);
                var web_result = sr.ReadToEnd();
                if (checkBox1.Checked)
                {
                    richTextBox1.AppendText("[DEBUG] [网页返回信息] " + web_result + "\n");
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                }
                foreach (string filter in filters)
                {
                    if (!web_result.Contains(filter))
                    {
                        num += 1;
                    }
                }
                if (num == len)
                {
                    richTextBox1.AppendText("[可能存在未授权访问] " + web_path + result + "\n");
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                }
                num = 0;
            }
            MessageBox.Show("扫描完成");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                string[] files = GetFiles();
                string resultname = "";
                foreach (string file in files)
                {
                    string file_ = file.Replace(textBox1.Text,"");
                    resultname += file_ + "\n";
                }
                string[] arr = resultname.Split('\n');
                scan(textBox2.Text, arr);
            }
            else { MessageBox.Show("请完整填写参数"); }
        }
        private string[] GetFiles()
        {
            string folderPath = textBox1.Text;
            string[] files = null;
            if (checkBox2.Checked)
            {
                string[] aspxfiles = Directory.GetFiles(folderPath, "*.aspx", SearchOption.AllDirectories);
                string[] ashxfiles = Directory.GetFiles(folderPath, "*.ashx", SearchOption.AllDirectories);
                string[] aspfiles = Directory.GetFiles(folderPath, "*.asp", SearchOption.AllDirectories);
                files = aspxfiles.Concat(aspxfiles).ToArray().Concat(ashxfiles).ToArray().Concat(aspfiles).ToArray();
            }
            if (checkBox3.Checked)
            {
                string[] phpfiles = Directory.GetFiles(folderPath, "*.php", SearchOption.AllDirectories);
                if (files == null)
                {
                    files = phpfiles;
                }
                else { files = files.Concat(phpfiles).ToArray(); }
            }
            if (checkBox4.Checked)
            {
                string[] jspfiles = Directory.GetFiles(folderPath, "*.jsp", SearchOption.AllDirectories);
                if (files == null)
                {
                    files = jspfiles;
                }
                else { files = files.Concat(jspfiles).ToArray(); }
                
            }
            if (checkBox1.Checked)
            {
                foreach (string file in files)
                {
                    richTextBox1.AppendText("[DEBUG] [文件遍历] " + file + "\n");
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                }
            }
            return files;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择源代码路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.SelectedPath;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("通常Debug模式在未知网页触发授权检测时的返回值时使用，用来查看网页的返回数据。调试过程中请减少扫描的文件数量，推荐选择2~5个文件尝试。");
        }
    }
}
