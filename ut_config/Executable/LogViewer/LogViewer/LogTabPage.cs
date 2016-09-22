using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace LiveLogViewer
{
    public class LogTabPage : TabPage
    {
        //The textbox where the log will be displayed
        //internal RichTextBox TextBox = new RichTextBox();

        internal RichTextBox TextBox = new RichTextBox
        {
                  Dock = DockStyle.Fill,
                  Font = new Font("Courier New", 10)
        };
        //The LogWatcher that monitors the file
        internal LogWatcher Watcher;

        //Constructor for the LogTabPage
        public LogTabPage(string FileName, string Suffix)
        {
            //Display the filename on the Tab
            this.Text = Path.GetFileName(string.Format("{0} {1}", FileName, Suffix));
            //Configure the TextBox
            TextBox.Dock = DockStyle.Fill;
            TextBox.BackColor = Color.White;
            TextBox.ReadOnly = true;
            //Add the TextBox to the LogTabPage
            this.Controls.Add(TextBox);

            //Create the LogWatcher
            CreateWatcher(FileName);
        }

        private void CreateWatcher(string FileName)
        {
            Watcher = new LogWatcher(FileName);

            //Set the directory of the file to monitor 
            Watcher.Path = Path.GetDirectoryName(FileName);

            //Raise events when the LastWrite or Size attribute is changed
            Watcher.NotifyFilter = (NotifyFilters.LastWrite | NotifyFilters.Size);

            //Filter out events for only this file
            Watcher.Filter = Path.GetFileName(FileName);

            //Subscribe to the event
            Watcher.TextChanged += new LogWatcher.LogWatcherEventHandler(Watcher_Changed);

            //Enable the event
            Watcher.EnableRaisingEvents = true;
        }

        //Occurs when the file is changed
        void Watcher_Changed(object sender, LogWatcherEventArgs e)
        {
            //Invoke the AppendText method if required
            if (TextBox.InvokeRequired)
            {
                this.Invoke(new Action(delegate() { AppendText(e.Contents); }));
            }
            else
            {
                AppendText(e.Contents);
            }
        }

        private void AppendText(string Text)
        {
            //Append the new text to the TextBox
            TextBox.Text += Text;

            //If the Frozen function isn't enabled then scroll to the bottom of the TextBox
            if (!MainForm.Frozen)
            {
                TextBox.SelectionStart = TextBox.Text.Length;
                TextBox.SelectionLength = 0;
                TextBox.ScrollToCaret();
            }
        }

        public void SearchText(string keyword,RichTextBoxFinds option)
        {
            TextBox.SelectAll();
            TextBox.SelectionBackColor = Color.White;
            int start = 0;
            int end = TextBox.Text.Length;
            int temp = 0;
            while(end>start && temp>-1)
            {
                temp = TextBox.Find(keyword,start,end,option);
                TextBox.SelectionBackColor = Color.Yellow;
                start = temp + keyword.Length;
                TextBox.ScrollToCaret();
            }
            //TextBox.SelectionFont = new Font(TextBox.SelectionFont, FontStyle.Underline);
            //TextBox.SelectionFont = new Font("Verdana", 12, FontStyle.Bold);
            //TextBox.SelectionColor = Color.Red;
        }

        /// <summary>
        /// public void SaveMyFile()
        /// Save selected error log to plain text file
        /// </summary>
        public void SaveMyFile(string selectedText)
        {
            try
            {
                // Create a SaveFileDialog to request a path and file name to save to.
                SaveFileDialog saveFile = new SaveFileDialog();
                StreamWriter sw = null;
                FileStream fs = null;
                // Initialize the SaveFileDialog to specify the RTF extension for the file.
                saveFile.DefaultExt = "*.txt";
                saveFile.Filter = "Text Files|*.txt";
                saveFile.FileName = string.Format("{0:MM-dd-yyyy_hh-mm-ss-fffftt}_log.txt",DateTime.Now);
                // Determine if the user selected a file name from the saveFileDialog. 
                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile.FileName.Length > 0)
                {
                    // Save the contents of the RichTextBox into the file.
                    fs = new FileStream(saveFile.FileName, FileMode.CreateNew, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.Write(selectedText);
                    if (FileExists(saveFile.FileName))
                    {
                        MessageBox.Show("Errorlog saved successfully to " + saveFile.FileName,"SUCCESS",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error in saving log", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {/* Error Logger */}
        }

        private bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}

