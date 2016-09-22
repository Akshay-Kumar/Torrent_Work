using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiveLogViewer
{
    public partial class SearchBox : Form
    {
        RichTextBoxFinds option = RichTextBoxFinds.None;
        string keyword = string.Empty;
        public SearchBox()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchLog();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchLog();
            }
        }

        private void SearchLog()
        {
            keyword = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                if (rbMatchCase.Checked == true)
                {
                    option = RichTextBoxFinds.MatchCase;
                }
                else if (rbMatchWholeWord.Checked == true)
                {
                    option = RichTextBoxFinds.WholeWord;
                }
                else if (rbNone.Checked == true)
                {
                    option = RichTextBoxFinds.None;
                }
                SourceCode.GlobalPageTracker.logTabPageObj.SearchText(keyword, option);
            }
        }

        private void SearchBox_Load(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SourceCode.GlobalPageTracker.searchBoxObj.Hide();
                SourceCode.GlobalPageTracker.loggerFormObj.Focus();
            }
        }
    }
}
