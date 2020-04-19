namespace GetGoogleSearchInfo
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_getData = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1_getSuggest = new System.Windows.Forms.Button();
            this.button1_meta_data = new System.Windows.Forms.Button();
            this.textBox_UrlDataNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5_status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_getData
            // 
            this.button_getData.Location = new System.Drawing.Point(78, 249);
            this.button_getData.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.button_getData.Name = "button_getData";
            this.button_getData.Size = new System.Drawing.Size(257, 68);
            this.button_getData.TabIndex = 0;
            this.button_getData.Text = "見出し一覧取得";
            this.button_getData.UseVisualStyleBackColor = true;
            this.button_getData.Click += new System.EventHandler(this.button_getData_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(232, 57);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(539, 40);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "検索ワード";
            // 
            // button1_getSuggest
            // 
            this.button1_getSuggest.Location = new System.Drawing.Point(371, 249);
            this.button1_getSuggest.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.button1_getSuggest.Name = "button1_getSuggest";
            this.button1_getSuggest.Size = new System.Drawing.Size(257, 68);
            this.button1_getSuggest.TabIndex = 3;
            this.button1_getSuggest.Text = "サジェエスト取得";
            this.button1_getSuggest.UseVisualStyleBackColor = true;
            this.button1_getSuggest.Click += new System.EventHandler(this.button1_getSuggest_Click);
            // 
            // button1_meta_data
            // 
            this.button1_meta_data.Location = new System.Drawing.Point(664, 249);
            this.button1_meta_data.Name = "button1_meta_data";
            this.button1_meta_data.Size = new System.Drawing.Size(257, 68);
            this.button1_meta_data.TabIndex = 7;
            this.button1_meta_data.Text = "メタデータ取得";
            this.button1_meta_data.UseVisualStyleBackColor = true;
            this.button1_meta_data.Click += new System.EventHandler(this.button1_meta_data_Click);
            // 
            // textBox_UrlDataNum
            // 
            this.textBox_UrlDataNum.Location = new System.Drawing.Point(968, 66);
            this.textBox_UrlDataNum.Name = "textBox_UrlDataNum";
            this.textBox_UrlDataNum.Size = new System.Drawing.Size(217, 31);
            this.textBox_UrlDataNum.TabIndex = 8;
            this.textBox_UrlDataNum.TextChanged += new System.EventHandler(this.textBox_UrlDataNum_TextChanged);
            this.textBox_UrlDataNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_getUrlDataNum_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(810, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "抽出URL数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(964, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "数字のみ入力可能です。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "処理ステータス：";
            // 
            // label5_status
            // 
            this.label5_status.AutoSize = true;
            this.label5_status.Location = new System.Drawing.Point(258, 151);
            this.label5_status.Name = "label5_status";
            this.label5_status.Size = new System.Drawing.Size(100, 24);
            this.label5_status.TabIndex = 10;
            this.label5_status.Text = "スタンバイ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 382);
            this.Controls.Add(this.label5_status);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_UrlDataNum);
            this.Controls.Add(this.button1_meta_data);
            this.Controls.Add(this.button1_getSuggest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_getData);
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "Form1";
            this.Text = "Google検索情報取得";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_getData;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1_getSuggest;
        private System.Windows.Forms.Button button1_meta_data;
        private System.Windows.Forms.TextBox textBox_UrlDataNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5_status;
    }
}

