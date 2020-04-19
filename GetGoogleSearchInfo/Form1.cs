/**
ランサーズの実務案件壁打ちNo1
URL:https://crowdworks.jp/public/jobs/4933744

機能仕様
今回作成しない：ジュール1：google検索結果1ページ目のURLを比較するツール
今回作成しない：モジュール2：google検索結果1ページ目のサイトの種類をURLから判定するツール
モジュール3：google検索結果1ページ目のサイトの見出しの一覧を作成するツール
モジュール4：google検索結果1ページ目のタイトル、サジェストを抜き出して重複を排除して一覧にするツール
モジュール5：google検索結果1ページ目のタイトル、メタキーワード、メタディスクリプション、コンテンツを抜き出して一覧にするツール（※takuma追記：今回はコンテンツの部分は実装しない）
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

using static GetGoogleSearchInfo.MessageManagement;

namespace GetGoogleSearchInfo
{
    public partial class Form1 : Form
    {
        static string DELIMITER = ","; //CSV読み書き用区切り文字
        static string DOUBLE_QUOTATION = "\""; //ダブルクォーテーション
        static string LINE_FEED_CODE = "\r\n"; //改行コード
        string KEY_WORD = "プログラミング";
        static int GET_URL_NUM = 5;
        static int PROC_STATUS = PROC_STANDBY;

        static List<TitleData> titleDataList = new List<TitleData>(); //CSV読み込みデータ格納リスト
        static List<SuggestData> suggestDataList = new List<SuggestData>(); //CSV読み込みデータ格納リスト
        static List<MetaDataData> metaDataDataList = new List<MetaDataData>(); //CSV読み込みデータ格納リスト
                                                                               //static List<MetaKeywordData> metaKeywordDataList = new List<MetaKeywordData>(); //CSV読み込みデータ格納リスト


        //------------------------------------------------------
        //コンストラクタ
        //------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
        }

        //------------------------------------------------------
        //初期処理
        //------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            initializeDataList();
            textBox1.Text = KEY_WORD;
            textBox_UrlDataNum.Text = GET_URL_NUM.ToString();
            label3.Visible = false;
            conponentExecControl(PROC_STANDBY);
            button_exec_control();
        }

        //------------------------------------------------------
        //コンポーネント制御関連
        //------------------------------------------------------

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
        /// URL取得数テキストボックス変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_UrlDataNum_TextChanged(object sender, EventArgs e)
        {
            button_exec_control();
        }

        /// <summary>
        /// textBox_getUrlDataNumを数字のみの入力に制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_getUrlDataNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                label3.Visible = true;
                e.Handled = true;

            }
            else
            {
                label3.Visible = false;
            }
        }

        /// <summary>
        /// 設定値入力に関する実行ボタンの活性/非活性のコントロール処理
        /// </summary>
        private void button_exec_control()
        {
            if (textBox1.Text != "" && textBox_UrlDataNum.Text != "")
            {
                button_getData.Enabled = true;
                button1_meta_data.Enabled = true;
            }
            else
            {
                button_getData.Enabled = false;
                button1_meta_data.Enabled = false;
            }
        }


        /// <summary>
        /// 処理途中はボタンおよびラベルを無効化する
        /// </summary>
        private void conponentExecControl(int procStatus)
        {
            PROC_STATUS = procStatus;
            if (PROC_STATUS == PROC_STANDBY)
            {
                label5_status.Text = "--スタンバイ--";
                label5_status.ForeColor = System.Drawing.Color.Black;
                buttonAndTextBoxControl(true);
            }

            if (PROC_STATUS == PROC_START)
            {
                label5_status.Text = "--処理を実行しています--";
                label5_status.ForeColor = System.Drawing.Color.Black;
                buttonAndTextBoxControl(false);
            }
            else if (PROC_STATUS == PROC_END)
            {
                label5_status.Text = "--処理が完了しました--";
                label5_status.ForeColor = System.Drawing.Color.Blue;
                buttonAndTextBoxControl(true);
            }
            else if (PROC_STATUS == PROC_ERROR)
            {
                label5_status.Text = "--処理の途中にエラーが発生しました--\n" +
                                    "※データが正しく取得できなかった可能性があります。";
                label5_status.ForeColor = System.Drawing.Color.Red;
                buttonAndTextBoxControl(true);
            }
        }
        
        /// <summary>
        /// ボタンとラベルのコントロール（実行中は非活性）
        /// </summary>
        /// <param name="statusIsReady"></param>
        private void buttonAndTextBoxControl(bool statusIsReady)
        {
            //
            if (statusIsReady)
            {
                textBox1.Enabled = true;
                textBox_UrlDataNum.Enabled = true;
                button_getData.Enabled = true;
                button1_getSuggest.Enabled = true;
                button1_meta_data.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox_UrlDataNum.Enabled = false;
                button_getData.Enabled = false;
                button1_getSuggest.Enabled = false;
                button1_meta_data.Enabled = false;
            }
        }


        //------------------------------------------------------
        //実行ボタン
        //------------------------------------------------------
        /// <summary>
        /// 見出し一覧取得ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_getData_Click(object sender, EventArgs e)
        {
            conponentExecControl(PROC_START);
            initializeDataList();
            string url = getKeyWordUrl();

            //htmlデータ取得
            string html = getHtml(url);
            //Console.WriteLine(html);

            //スクレイピング処理
            scrapingData(html);

            //CSV書き込み
            csvWrite_TitleList();

            conponentExecControl(PROC_END);

        }

        /// <summary>
        /// サジェスト取得ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_getSuggest_Click(object sender, EventArgs e)
        {
            conponentExecControl(PROC_START);
            initializeDataList();
            string suggestKeyword = textBox1.Text;
            //string suggestUrl = @"http://www.google.com/complete/search?hl=en&q=" + suggestKeyword + @"&output=toolbar";
            string suggestUrl = @"http://www.google.com/complete/search?hl=ja&q=" + suggestKeyword + @"&output=toolbar";

            string html = getHtml(suggestUrl);
            getSuggestData(html);
            csvWrite_Suggest();

            if (PROC_STATUS != PROC_ERROR)
            {
                conponentExecControl(PROC_END);
            }
        }


        /// <summary>
        /// メタキーワード取得ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_geMetaInfo_Click(object sender, EventArgs e)
        {
            conponentExecControl(PROC_START);
            initializeDataList();
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

            if (PROC_STATUS != PROC_ERROR)
            {
                conponentExecControl(PROC_END);
            }

        }


        /// <summary>
        /// メタデータ取得ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_meta_data_Click(object sender, EventArgs e)
        {
            conponentExecControl(PROC_START);


            string url = getKeyWordUrl();
            string html = getHtml(url);
            List<string> siteUrlList = getSiteUrlList(html);
            scrapingMetaData(siteUrlList);
            csvWrite_description();

            if (PROC_STATUS != PROC_ERROR)
            {
                conponentExecControl(PROC_END);
            }


        }

        //------------------------------------------------------
        //共通処理
        //------------------------------------------------------
        /// <summary>
        /// データ格納甩のstatic変数を初期化
        /// </summary>
        private void initializeDataList()
        {
            titleDataList = new List<TitleData>(); //CSV読み込みデータ格納リスト
            suggestDataList = new List<SuggestData>(); //CSV読み込みデータ格納リスト
            metaDataDataList = new List<MetaDataData>(); //CSV読み込みデータ格納リスト
        }

        /// <summary>
        /// Googleキーワード検索結果のURLを取得
        /// </summary>
        private string getKeyWordUrl()
        {
            KEY_WORD = textBox1.Text;
            if (textBox_UrlDataNum.Text != "")
            {
                GET_URL_NUM = int.Parse(textBox_UrlDataNum.Text);
            }

            string url = "https://www.google.co.jp/search?q= " + KEY_WORD + "&ie=UTF-8&oe=UTF-8&num=20";
            return url;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Errorが発生しました。");
                conponentExecControl(PROC_ERROR);
            }
            return html;
        }

        /// <summary>
        /// google検索結果からサイトのURL一覧を取得
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
                //Console.WriteLine(suggestKeyword);
            }
            return siteUrlList;
        }

        /// <summary>
        /// htmlから空行やタブ等を取り除きます。
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private string replaceHtmlData(string html)
        {
            //@空白改行をリプレイスする
            //html = html.Replace("\r", "").Replace("\n", "");
            string beforeReplacePattern = "\\s";
            string afterReplacePattern = "";
            Regex regex = new Regex(beforeReplacePattern);
            html = regex.Replace(html, afterReplacePattern);
            //replace済みhtml出力（デバッグ甩）
            //Console.WriteLine("----------------------------------------------------------------------------------------\n");
            //Console.WriteLine("html:" +  html + "\n");
            //Console.WriteLine("----------------------------------------------------------------------------------------\n");
            return html;
        }

        //------------------------------------------------------
        //CSV書き込み処理
        //------------------------------------------------------
        /// <summary>
        /// CSV書き込み_見出し一覧
        /// </summary>
        private void csvWrite_TitleList()
        {
            string output_file_path = @".\googleSearchResult.csv";
            string strData = ""; //1行分のデータ

            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(output_file_path,
                                                                   false,
                                                                   System.Text.Encoding.Default);
                //ヘッダ書き込み
                strData = "ID" + DELIMITER + "Keyword" + DELIMITER + "Title" + DELIMITER + "Date";
                sw.WriteLine(strData);

                foreach (var data in titleDataList)
                {
                    strData = data.id + DELIMITER
                        + data.keyWord + DELIMITER
                        + "\"" + data.title + "\"" + DELIMITER
                        + data.getDate;
                    sw.WriteLine(strData);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Errorが発生しました。");
                conponentExecControl(PROC_ERROR);
            }

        }

        /// <summary>
        /// CSV書き込み_サジェスト
        /// </summary>
        private void csvWrite_Suggest()
        {
            string output_file_path = @".\googleSuggestResult.csv";
            string strData = ""; //1行分のデータ

            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(output_file_path,
                                                                       false,
                                                                       System.Text.Encoding.Default);
                //ヘッダ書き込み
                strData = "ID" + DELIMITER + "Keyword" + DELIMITER + "SuggestKeyword";
                sw.WriteLine(strData);
                foreach (var data in suggestDataList)
                {
                    strData = data.id + DELIMITER
                    + data.keyWord + DELIMITER
                    + "\"" + data.suggestKeyword + "\"";
                    sw.WriteLine(strData);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Errorが発生しました。");
                conponentExecControl(PROC_ERROR);
            }

        }

        /// <summary>
        /// CSV書き込み_メタデータ
        /// </summary>
        private void csvWrite_description()
        {
            string output_file_path = @".\metaDataResult.csv";
            string strData = ""; //1行分のデータ


            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(output_file_path,
                                            false,
                                            System.Text.Encoding.Default);
                //ヘッダ書き込み
                strData = "ID" + DELIMITER + "SiteUrl" + DELIMITER + "MetaKeyword" + DELIMITER + "MetaDescription";
                sw.WriteLine(strData);

                foreach (var data in metaDataDataList)
                {
                    strData = "\"" + data.id + "\"" + DELIMITER
                    + "\"" + data.siteUrl + "\"" + DELIMITER
                    + "\"" + data.metaKeyword + "\"" + DELIMITER
                    + "\"" + data.metaDescription + "\"";
                    sw.WriteLine(strData);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Errorが発生しました。");
                conponentExecControl(PROC_ERROR);
            }
        }



        //------------------------------------------------------
        //スクレイピング処理
        //------------------------------------------------------
        private void scrapingMetaData(List<string> siteUrlList)
        {
            int count = 0;
            int id = 0;
            foreach (var siteUrl in siteUrlList)
            {
                MetaDataData metaDataData = new MetaDataData();
                if (count >= GET_URL_NUM)
                {
                    break;
                }
                string targetHtml = "";
                Console.WriteLine(siteUrl);
                targetHtml = getHtml(siteUrl);
                
                //メタキーワード取得
                string metaKeyword = "";
                if (targetHtml != "")
                {
                    metaKeyword = scrapingMetaKeyword(targetHtml);
                }
                if (metaKeyword == "")
                {
                    metaKeyword = "データを取得できませんでした。";
                }

                //メタディスクリプション取得
                string metaDescription = "";
                if (targetHtml != "")
                {
                    metaDescription = scrapingMetaDescription(targetHtml);
                }
                if(metaDescription == "")
                {
                    metaDescription = "データを取得できませんでした。";
                }

                metaDataData.id = id;
                metaDataData.siteUrl = siteUrl;
                metaDataData.metaKeyword = metaKeyword;
                metaDataData.metaDescription = metaDescription;
                metaDataDataList.Add(metaDataData);

                id++;
                count++;
            }
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
            int count = 0;
            foreach (Match m in matche)
            {
                if(count >= GET_URL_NUM)
                {
                    break;
                }
                string pattern2 = "\\)\">(.*?)<";
                string  siteTitle = m.Groups[1].ToString();

                Regex regex = new Regex(pattern2);
                Match match = regex.Match(siteTitle);

                if (match.Groups.Count > 0)
                {
                    siteTitle = match.Groups[1].ToString();
                }
                TitleData searchResultData = new TitleData();
                searchResultData.id = id;
                searchResultData.keyWord = KEY_WORD;
                searchResultData.title = @siteTitle;
                DateTime dt = DateTime.Now;
                searchResultData.getDate = dt.ToString();

                titleDataList.Add(searchResultData);
                Console.WriteLine(siteTitle);
                Console.WriteLine("\n--takuma--\n");
                id++;
                count++;
            }
        }

    
        /// <summary>
        /// サジェストデータ取得処理
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private void getSuggestData(string html)
        {
            html = html.Replace("\r", "").Replace("\n", "");
            string pattern = "";
            pattern = "<suggestion data=\"(.*?)\"";
            MatchCollection matche = Regex.Matches(html, pattern);

            if (matche.Count > 0)
            {
                int id = 0;
                foreach (Match m in matche)
                {
                    SuggestData suggestData = new SuggestData();
                    suggestData.id = id;
                    suggestData.keyWord = KEY_WORD;
                    suggestData.suggestKeyword = m.Groups[1].ToString();
                    suggestDataList.Add(suggestData);
                    id++;
                }
            }
        }

        /// <summary>
        /// サイトのページからメタディスクリプションを抽出
        /// </summary>
        /// <param name="html"></param>
        private string scrapingMetaDescription(string html)
        {
            string pattern = ""; //メタディスクリプション抽出正規表現パターン
            int matchGrouphNum = 0; //マッチ数
            string metaDescription = ""; //htmlから抽出したメタディスクリプション

            //@空白改行をリプレイスする
            html = replaceHtmlData(html);

            //メタディスクリプション抽出正規表現パターン_No1
            pattern = "metacontent=.(.*?).name=.description.";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(html);

            matchGrouphNum = match.Groups.Count;
            //Console.WriteLine("mgn:" + matcGrouphNum); //デバッグ甩
            if(matchGrouphNum > 0)
            {
                metaDescription = match.Groups[1].ToString();
            }

            //メタディスクリプション抽出正規表現パターン_No2
            if (metaDescription == "")
            {
                //@[\"|\']を「.」に変える。
                //メタディスクリプションスクレイピング正規表現_No2
                pattern = "metaname=.description.content=.(.*?).>";
                regex = new Regex(pattern);
                match = regex.Match(html);

                //@matchの中身の存在確認してチェック
                if (matchGrouphNum > 0)
                {
                    metaDescription = match.Groups[1].ToString();
                }
            }
            return metaDescription;
            Console.WriteLine("メタディスクリプション：" + metaDescription);
        }



        /// <summary>
        /// サイトのページからメタキーワードを抽出
        /// </summary>
        /// <param name="html"></param>
        private string scrapingMetaKeyword(string html)
        {
            string pattern = ""; //メタディスクリプション抽出正規表現パターン
            int matchGrouphNum = 0; //マッチ数
            string metaKeyword = ""; //htmlから抽出したメタディスクリプション

            html = replaceHtmlData(html);

            //メタディスクリプション抽出正規表現パターン_No1
            pattern = "<metaname=.keywords.content=.(.*?).>";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(html);

            matchGrouphNum = match.Groups.Count;
            //Console.WriteLine("mgn:" + matcGrouphNum); //デバッグ甩
            if (matchGrouphNum > 0)
            {
                metaKeyword = match.Groups[1].ToString();
            }

            //メタディスクリプション抽出正規表現パターン_No2
            if (metaKeyword == "")
            {
                //@[\"|\']を「.」に変える。
                //メタディスクリプションスクレイピング正規表現_No2
                pattern = "metaname=.keyword.content=.(.*?).>";
                regex = new Regex(pattern);
                match = regex.Match(html);

                //@matchの中身の存在確認してチェック
                if (matchGrouphNum > 0)
                {
                    metaKeyword = match.Groups[1].ToString();
                }
            }
            return metaKeyword;
            Console.WriteLine("メタディスクリプション：" + metaKeyword);
        }


    }
}

