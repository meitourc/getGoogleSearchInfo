﻿/**
ランサーズの実務案件壁打ちNo1
URL:https://crowdworks.jp/public/jobs/4933744

機能仕様
今回作成しない：ジュール1：google検索結果1ページ目のURLを比較するツール
今回作成しない：モジュール2：google検索結果1ページ目のサイトの種類をURLから判定するツール
モジュール3：google検索結果1ページ目のサイトの見出しの一覧を作成するツール
モジュール4：google検索結果1ページ目のタイトル、サジェストを抜き出して重複を排除して一覧にするツール
モジュール5：google検索結果1ページ目のタイトル、メタキーワード、メタディスクリプション、コンテンツを抜き出して一覧にするツール 

**/

using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
            string url = "https://www.google.co.jp/search?q=テスト&ie=UTF-8&oe=UTF-8&num=20";
            //WebClient wc = new WebClient();
            //Stream st = wc.OpenRead("https://www.google.co.jp/search?q=テスト&ie=UTF-8&oe=UTF-8&num=20");
            //Encoding enc = Encoding.GetEncoding("UTF-8");
            //StreamReader sr = new StreamReader(st, enc);
            //string html = sr.ReadToEnd();
            //st.Close();
            string html = getSearchResultHtml(url);
            //Console.WriteLine(html);



            //scrapingData(html);


        }

        /// <summary>
        /// 引数urlにアクセスした際に取得できるHTMLを返す
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>取得したHTML</returns>
        //private string getSearchResultHtml(string url)
        //{
        //    var chrome = new ChromeDriver(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        //    chrome.Url = url;

        //    Console.ReadKey();
        //    chrome.Quit();
        //    return "s";


        //}


        ///// <summary>
        ///// 引数urlにアクセスした際に取得できるHTMLを返す
        ///// </summary>
        ///// <param name="url">URL</param>
        ///// <returns>取得したHTML</returns>
        private string getSearchResultHtml(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows XP)";
            req.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 70.0.3538.77 Safari / 537.36";
            string html = "";
            //指定したURLに対してrequestを投げてresponseを取得
            using (var res = (HttpWebResponse)req.GetResponse())
            using (var resSt = res.GetResponseStream())
            using (var sr = new StreamReader(resSt, Encoding.UTF8))
            {
                //HTMLを取得する。
                html = sr.ReadToEnd();
                //Console.WriteLine(html);
                scrapingData(html);
            }
            return html;
        }


        private void scrapingData(string html)
        {

            //string html = getItemPictureHtml(asinUrl);
            html = html.Replace("\r", "").Replace("\n", "");

            string pattern = "";
            //pattern = "imgTagWrapperId(.*)</h3>";

           //html = @"ccc<h3 class=aaa>test</h3>ddd<h3 class=aaa>test2</h3>ddd";
            //html = @"ccc<h3 class=aaa>test</h3>";

            html = @"aaa<h3>test</h3><h3>test2</h3>bbb";
            html = @"<h3>aba</h3>";
            //pattern = @"<h3(.*?)</h3>";


            pattern = @"<h3>(.*?)</h3>";
            pattern = @"<h3>(aba)</h3>";


            //pattern = @"<h3[^>]*>(.*)</h3>";


            //Regex regex = new Regex(pattern);
            //Match match = regex.Match(html);


            //MatchCollection matche = Regex.Matches(html, @"<h3.*>(.*?)</h3>");
            MatchCollection matche = Regex.Matches(html, pattern);

            foreach (Match m in matche)
            {
                Console.WriteLine("\n--takuma--\n");
                //Console.WriteLine(m.Groups[1]);
                Console.WriteLine(m.Value);
                Console.WriteLine("\n--takuma--\n");

            }

            //foreach (var item in match.Groups)
            //{
            //    Console.WriteLine("\n--takuma--\n");
            //    Console.WriteLine(item);
            //    Console.WriteLine("\n--takuma--\n");
            //}

        }
    }
}

