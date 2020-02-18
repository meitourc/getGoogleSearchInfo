using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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


        }
    }
}
