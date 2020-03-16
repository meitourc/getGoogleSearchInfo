/**
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
using System.Xml.Linq;

namespace GetGoogleSearchInfo
{
    public partial class Form1 : Form
    {
        static string DELIMITER = ","; //CSV読み書き用区切り文字
        static string DOUBLE_QUOTATION = "\""; //ダブルクォーテーション
        static string LINE_FEED_CODE = "\r\n"; //改行コード
        string KEY_WORD = "";
        static List<SearchResultData> searchResultDataList = new List<SearchResultData>(); //CSV読み込みデータ格納リスト

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button_exec_control();
        }

        /// <summary>
        /// データ取得ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_getData_Click(object sender, EventArgs e)
        {
            KEY_WORD = textBox1.Text;
            string url = "https://www.google.co.jp/search?q= " + KEY_WORD  + "&ie=UTF-8&oe=UTF-8&num=20";

            //htmlデータ取得
            string html = getSearchResultHtml(url);
            //Console.WriteLine(html);

            //スクレイピング処理
            scrapingData(html);

            //CSV書き込み
            csvWrite();
        }

        /// <summary>
        /// 実行ボタンの活性/非活性のコントロール処理
        /// </summary>
        void button_exec_control()
        {
            if (textBox1.Text != "")
            {
                button_getData.Enabled = true;
            }
            else
            {
                button_getData.Enabled = false;
            }

        }

        ///// <summary>
        ///// 引数urlにアクセスした際に取得できるHTMLを返す
        ///// </summary>
        ///// <param name="url">URL</param>
        ///// <returns>取得したHTML</returns>
        private string getSearchResultHtml(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 70.0.3538.77 Safari / 537.36";
            string html = "";
            //指定したURLに対してrequestを投げてresponseを取得
            using (var res = (HttpWebResponse)req.GetResponse())
            using (var resSt = res.GetResponseStream())
            using (var sr = new StreamReader(resSt, Encoding.UTF8))
            {
                //HTMLを取得する。
                html = sr.ReadToEnd();
            }
            return html;
        }


        /// <summary>
        /// グーグル検索結果取得処理
        /// </summary>
        /// <param name="html"></param>
        private void scrapingData(string html)
        {
            html = html.Replace("\r", "").Replace("\n", "");
            string pattern = "";
            pattern = @"<h3.*?>(.*?)</h3>";
            MatchCollection matche = Regex.Matches(html, pattern);

            Console.WriteLine(html);


            int id = 1;
            foreach (Match m in matche)
            {
                string pattern2 = "\\)\">(.*?)<";
                string  siteTitle = m.Groups[1].ToString();

                Regex regex = new Regex(pattern2);
                Match match = regex.Match(siteTitle);
                siteTitle = match.Groups[1].ToString();

                SearchResultData searchResultData = new SearchResultData();
                searchResultData.id = id;
                searchResultData.keyWord = KEY_WORD;
                searchResultData.title = @siteTitle;
                DateTime dt = DateTime.Now;
                searchResultData.getDate = dt.ToString();

                searchResultDataList.Add(searchResultData);
                Console.WriteLine(siteTitle);
                Console.WriteLine("\n--takuma--\n");
                id++;
            }
        }

        /// <summary>
        /// CSV書き込み
        /// </summary>
        private void csvWrite()
        {
            string output_file_path = @".\googleSearchResult.csv";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                output_file_path,
                false,
                System.Text.Encoding.Default);

            string strData = ""; //1行分のデータ

            //ヘッダ書き込み
            strData = "id" + DELIMITER + "keyword" + DELIMITER + "title" + DELIMITER + "date";
            sw.WriteLine(strData);

            foreach (var data in searchResultDataList)
            {
                strData = data.id + DELIMITER
                    + data.keyWord + DELIMITER
                    + "\"" + data.title + "\"" + DELIMITER
                    + data.getDate;
                sw.WriteLine(strData);
            }
            sw.Close();
        }
    
        /// <summary>
        /// キーワードテキストボックス変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button_exec_control();
        }

        /// <summary>
        /// サジェスト取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_getSuggest_Click(object sender, EventArgs e)
        {
            
            string suggestKeyword = textBox1.Text;
            string suggestUrl = @"http://www.google.com/complete/search?hl=en&q=" + suggestKeyword + @"&output=toolbar";
            getSuggestData(suggestUrl);
        }


        private void getSuggestData(string suggestUrl)
        {
            var req = (HttpWebRequest)WebRequest.Create(suggestUrl);
            req.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 70.0.3538.77 Safari / 537.36";
            string suggestData = "";

            //指定したURLに対してrequestを投げてresponseを取得
            using (var res = (HttpWebResponse)req.GetResponse())
            using (var resSt = res.GetResponseStream())
            using (var sr = new StreamReader(resSt, Encoding.UTF8))
            {
                //HTMLを取得する。
                suggestData = sr.ReadToEnd();
            }

            XElement xml = XElement.Load(suggestUrl);
            IEnumerable<XElement> infos = from item in xml.Elements("CompleteSuggestion")
                                          select item;

            //メンバー情報分ループして、コンソールに表示
            int No = 0;
            foreach (XElement info in infos)
            {
                XElement item = info.Element("suggestion");
                XAttribute attr = item.Attribute("data");
                string suggestDataResult = attr.Value;
                Console.WriteLine(suggestDataResult);

                //Console.WriteLine(info.Element("suggestion data").Value);
                //Console.WriteLine(info);
                //Console.WriteLine(info.Attribute("suggestion");
            }
        }

        /// <summary>
        /// メタデータ取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_geMetaInfo_Click(object sender, EventArgs e)
        {
            string url = "https://ja.wikipedia.org/wiki/%E3%83%86%E3%82%B9%E3%83%88";
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 70.0.3538.77 Safari / 537.36";
            string html = "";
            //指定したURLに対してrequestを投げてresponseを取得
            using (var res = (HttpWebResponse)req.GetResponse())
            using (var resSt = res.GetResponseStream())
            using (var sr = new StreamReader(resSt, Encoding.UTF8))
            {
                //HTMLを取得する。
                html = sr.ReadToEnd();
            }

        }
    }
}

