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

using AngleSharp.Html.Parser;
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
            textBox1.Text = "プログラミング";
            button_exec_control();
        }

        /// <summary>
        /// データ取得ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_getData_Click(object sender, EventArgs e)
        {

            string url = getKeyWordUrl();

            //htmlデータ取得
            string html = getHtml(url);
            //Console.WriteLine(html);

            //スクレイピング処理
            scrapingData(html);

            //CSV書き込み
            csvWrite();
        }


        /// <summary>
        /// 実行ボタンの活性/非活性のコントロール処理
        /// </summary>
        private string getKeyWordUrl()
        {
            KEY_WORD = textBox1.Text;
            string url = "https://www.google.co.jp/search?q= " + KEY_WORD + "&ie=UTF-8&oe=UTF-8&num=20";
            return url;
        }


        /// <summary>
        /// 実行ボタンの活性/非活性のコントロール処理
        /// </summary>
        private void button_exec_control()
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
        private string getHtml(string url)
        {
            string html = "";
            try
            {
                //指定したURLに対してrequestを投げてresponseを取得
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 70.0.3538.77 Safari / 537.36";
                using (var res = (HttpWebResponse)req.GetResponse())
                using (var resSt = res.GetResponseStream())
                using (var sr = new StreamReader(resSt, Encoding.UTF8))
                {
                    //HTMLを取得する。
                    html = sr.ReadToEnd();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex + "Errorが発生しました。");
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
            //string suggestUrl = @"http://www.google.com/complete/search?hl=en&q=" + suggestKeyword + @"&output=toolbar";
            string suggestUrl = @"http://www.google.com/complete/search?hl=ja&q=" + suggestKeyword + @"&output=toolbar";

            string html = getHtml(suggestUrl);
            getSuggestData(html);
        }


        private List<string> getSuggestData(string html)
        {
            html = html.Replace("\r", "").Replace("\n", "");
            string pattern = "";
            pattern = "<suggestion data=\"(.*?)\"";
            MatchCollection matche = Regex.Matches(html, pattern);

            var suggetDataList = new List<string>();

            int id = 1;
            foreach (Match m in matche)
            {
                string siteTitle = m.Groups[1].ToString();
                suggetDataList.Add(siteTitle);
                Console.WriteLine(siteTitle);
            }
            return suggetDataList;
        }

        private static string Sanitize(string xml)
        {
            var sb = new System.Text.StringBuilder();

            foreach (var c in xml)
            {
                var code = (int)c;

                if (code == 0x9 ||
                    code == 0xa ||
                    code == 0xd ||
                    (0x20 <= code && code <= 0xd7ff) ||
                    (0xe000 <= code && code <= 0xfffd) ||
                    (0x10000 <= code && code <= 0x10ffff))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
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

        /// <summary>
        /// サイトのページからメタディスクリプションを抽出
        /// </summary>
        /// <param name="html"></param>
        private void scrapingMetaDescription(string html)
        {
            html = html.Replace("\r", "").Replace("\n", "");
            string pattern = "";

            //@空白改行をリプレイスする
            string beforeReplacePattern = "\\s";
            string afterReplacePattern = "";
            Regex regex = new Regex(beforeReplacePattern);
            html = regex.Replace(html, afterReplacePattern);
            //replace済みhtml出力（デバッグ甩）
            Console.WriteLine("----------------------------------------------------------------------------------------\n");
            Console.WriteLine("html:" +  html + "\n");
            Console.WriteLine("----------------------------------------------------------------------------------------\n");

            //メタディスクリプションスクレイピングパターン
            pattern = "metacontent=.(.*?).name=.description.";
            
            //正規表現でメタディスクリプション抜き出し
            regex = new Regex(pattern);
            Match match = regex.Match(html);
            string metaDescription = match.Groups[1].ToString();
            

            if (metaDescription == "")
            {
                pattern = "<meta name=[\"|\']description[\"|\']content=[\"|\']([\\s\\S])[\"|\']>";

                //@[\"|\']を「.」に変える。
                pattern = "<meta name=[\"|\']description[\"|\']content=[\"|\']([\\s\\S])[\"|\']>";
                regex = new Regex(pattern);
                match = regex.Match(html);

                //@matchの中身の存在確認してチェック
                metaDescription = match.Groups[1].ToString();
            }



            Console.WriteLine("メタディスクリプション：" + metaDescription);



        }


        /// <summary>
        /// google検索結果からサイトのURLを抽出
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private List<string> getSiteUrlList(string html)
        {
            html = html.Replace("\r", "").Replace("\n", "");
            string pattern = "";
            pattern = @"<h3.*?>(.*?)</h3>";
            MatchCollection matche = Regex.Matches(html, pattern);

            var siteUrlList = new List<string>();

            int id = 1;
            foreach (Match m in matche)
            {
                string pattern2 = "<a href=\"(.*?)\"";
                string siteTitle = m.Groups[1].ToString();

                Regex regex = new Regex(pattern2);
                Match match = regex.Match(siteTitle);
                siteTitle = match.Groups[1].ToString();
                siteUrlList.Add(siteTitle);
                //Console.WriteLine(siteTitle);
            }
            return siteUrlList;
        }


        /// <summary>
        /// メタディスクリプション取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_meta_discription_Click(object sender, EventArgs e)
        {
            string url = getKeyWordUrl();
            string html = getHtml(url);
            List<string> siteUrlList = getSiteUrlList(html);


            int i = 0;
            foreach (var siteUrl in siteUrlList)
            {
                if(i == 5)
                {
                    break;
                }

                string targetHtml = "";
                Console.WriteLine(siteUrl);
                targetHtml = getHtml(siteUrl);

                if (targetHtml != "")
                {
                    scrapingMetaDescription(targetHtml);
                }
                i++;
            }

            //string targetHtml2 = "";
            //targetHtml2 = getHtml("https://www.amazon.co.jp/%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0-%E3%82%B3%E3%83%B3%E3%83%94%E3%83%A5%E3%83%BC%E3%82%BF%E3%83%BB%E3%82%A4%E3%83%B3%E3%82%BF%E3%83%BC%E3%83%8D%E3%83%83%E3%83%88-%E5%92%8C%E6%9B%B8/b?ie=UTF8&amp;node=492352");
            //if(targetHtml2 != "")
            //{
            //    scrapingMetaDescription(targetHtml2);
            //}
        }
    }
}

