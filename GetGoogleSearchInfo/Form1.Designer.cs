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
            this.SuspendLayout();
            // 
            // button_getData
            // 
            this.button_getData.Location = new System.Drawing.Point(427, 28);
            this.button_getData.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.button_getData.Name = "button_getData";
            this.button_getData.Size = new System.Drawing.Size(178, 50);
            this.button_getData.TabIndex = 0;
            this.button_getData.Text = "データ取得";
            this.button_getData.UseVisualStyleBackColor = true;
            this.button_getData.Click += new System.EventHandler(this.button_getData_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(152, 46);
            this.textBox1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(251, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "検索ワード";
            // 
            // button1_getSuggest
            // 
            this.button1_getSuggest.Location = new System.Drawing.Point(427, 146);
            this.button1_getSuggest.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.button1_getSuggest.Name = "button1_getSuggest";
            this.button1_getSuggest.Size = new System.Drawing.Size(178, 42);
            this.button1_getSuggest.TabIndex = 3;
            this.button1_getSuggest.Text = "サジェエスト取得";
            this.button1_getSuggest.UseVisualStyleBackColor = true;
            this.button1_getSuggest.Click += new System.EventHandler(this.button1_getSuggest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 257);
            this.Controls.Add(this.button1_getSuggest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_getData);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
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
    }
}

