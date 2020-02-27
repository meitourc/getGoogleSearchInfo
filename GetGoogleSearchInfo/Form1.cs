using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetGoogleSearchInfo
{
    public partial class Form1 : Form
    {

        static string DELIMITER = ","; //CSV読み書き用区切り文字
        static string DOUBLE_QUOTATION = "\""; //ダブルクォーテーション
        static string LINE_FEED_CODE = "\r\n"; //改行コード

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// データ取得ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_getData_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            Stream st = wc.OpenRead("https://www.google.co.jp/search?q=テスト&ie=UTF-8&oe=UTF-8&num=20");

            Encoding enc = Encoding.GetEncoding("UTF-8");
            StreamReader sr = new StreamReader(st, enc);
            string html = sr.ReadToEnd();
            scrapingData(html);

            st.Close();

        }


        private void scrapingData(string html)
        {

            //string html = getItemPictureHtml(asinUrl);
            html = html.Replace("\r", "").Replace("\n", "");

            string pattern = "";
            pattern = "imgTagWrapperId(.*)</h3";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(html);


            foreach (var item in match.Groups)
            {
                Console.WriteLine("\n-------------------------------------------------------------------\n");
                Console.WriteLine(item);


            }

        }
    }
}

