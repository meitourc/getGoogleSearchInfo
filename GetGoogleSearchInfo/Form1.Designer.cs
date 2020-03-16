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
            this.button1_geMetaInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_getData
            // 
            this.button_getData.Location = new System.Drawing.Point(925, 56);
            this.button_getData.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.button_getData.Name = "button_getData";
            this.button_getData.Size = new System.Drawing.Size(386, 100);
            this.button_getData.TabIndex = 0;
            this.button_getData.Text = "データ取得";
            this.button_getData.UseVisualStyleBackColor = true;
            this.button_getData.Click += new System.EventHandler(this.button_getData_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(329, 92);
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
            this.label1.Location = new System.Drawing.Point(171, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "検索ワード";
            // 
            // button1_getSuggest
            // 
            this.button1_getSuggest.Location = new System.Drawing.Point(925, 292);
            this.button1_getSuggest.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.button1_getSuggest.Name = "button1_getSuggest";
            this.button1_getSuggest.Size = new System.Drawing.Size(386, 84);
            this.button1_getSuggest.TabIndex = 3;
            this.button1_getSuggest.Text = "サジェエスト取得";
            this.button1_getSuggest.UseVisualStyleBackColor = true;
            this.button1_getSuggest.Click += new System.EventHandler(this.button1_getSuggest_Click);
            // 
            // button1_geMetaInfo
            // 
            this.button1_geMetaInfo.Location = new System.Drawing.Point(925, 496);
            this.button1_geMetaInfo.Name = "button1_geMetaInfo";
            this.button1_geMetaInfo.Size = new System.Drawing.Size(408, 95);
            this.button1_geMetaInfo.TabIndex = 6;
            this.button1_geMetaInfo.Text = "メタデータ取得";
            this.button1_geMetaInfo.UseVisualStyleBackColor = true;
            this.button1_geMetaInfo.Click += new System.EventHandler(this.button1_geMetaInfo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1398, 859);
            this.Controls.Add(this.button1_geMetaInfo);
            this.Controls.Add(this.button1_getSuggest);
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
        private System.Windows.Forms.Button button1_geMetaInfo;
    }
}

