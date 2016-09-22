namespace LiveLogViewer
{
    partial class SearchBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSearch = new System.Windows.Forms.Button();
            this.rbMatchCase = new System.Windows.Forms.RadioButton();
            this.rbMatchWholeWord = new System.Windows.Forms.RadioButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(284, -1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rbMatchCase
            // 
            this.rbMatchCase.AutoSize = true;
            this.rbMatchCase.Location = new System.Drawing.Point(3, 21);
            this.rbMatchCase.Name = "rbMatchCase";
            this.rbMatchCase.Size = new System.Drawing.Size(82, 17);
            this.rbMatchCase.TabIndex = 2;
            this.rbMatchCase.TabStop = true;
            this.rbMatchCase.Text = "Match Case";
            this.rbMatchCase.UseVisualStyleBackColor = true;
            // 
            // rbMatchWholeWord
            // 
            this.rbMatchWholeWord.AutoSize = true;
            this.rbMatchWholeWord.Location = new System.Drawing.Point(96, 21);
            this.rbMatchWholeWord.Name = "rbMatchWholeWord";
            this.rbMatchWholeWord.Size = new System.Drawing.Size(118, 17);
            this.rbMatchWholeWord.TabIndex = 3;
            this.rbMatchWholeWord.TabStop = true;
            this.rbMatchWholeWord.Text = "Match Whole Word";
            this.rbMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(225, 21);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 4;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(-1, 1);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(285, 20);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // SearchBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 41);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.rbNone);
            this.Controls.Add(this.rbMatchWholeWord);
            this.Controls.Add(this.rbMatchCase);
            this.Controls.Add(this.btnSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "SearchBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchBox";
            this.Load += new System.EventHandler(this.SearchBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchBox_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RadioButton rbMatchCase;
        private System.Windows.Forms.RadioButton rbMatchWholeWord;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.TextBox txtSearch;
    }
}