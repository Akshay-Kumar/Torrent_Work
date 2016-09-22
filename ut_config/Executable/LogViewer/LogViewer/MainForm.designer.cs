using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace LiveLogViewer
{
partial class MainForm : System.Windows.Forms.Form
{

    //Form overrides dispose to clean up the component list.
    [System.Diagnostics.DebuggerNonUserCode()]
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }

    //Required by the Windows Form Designer

    private System.ComponentModel.IContainer components = null;
    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.ClearButton = new System.Windows.Forms.Button();
        this.AddButton = new System.Windows.Forms.Button();
        this.MainTabControl = new System.Windows.Forms.TabControl();
        this.DeleteButton = new System.Windows.Forms.Button();
        this.LogOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
        this.LastUpdateCaption = new System.Windows.Forms.Label();
        this.LastUpdatedLabel = new System.Windows.Forms.Label();
        this.FreezeButton = new System.Windows.Forms.Button();
        this.btnFullLog = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // ClearButton
        // 
        this.ClearButton.Enabled = false;
        this.ClearButton.Location = new System.Drawing.Point(99, 4);
        this.ClearButton.Name = "ClearButton";
        this.ClearButton.Size = new System.Drawing.Size(70, 23);
        this.ClearButton.TabIndex = 2;
        this.ClearButton.Text = "Clear";
        this.ClearButton.UseVisualStyleBackColor = true;
        this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
        // 
        // AddButton
        // 
        this.AddButton.Location = new System.Drawing.Point(12, 4);
        this.AddButton.Name = "AddButton";
        this.AddButton.Size = new System.Drawing.Size(81, 23);
        this.AddButton.TabIndex = 1;
        this.AddButton.Text = "Add";
        this.AddButton.UseVisualStyleBackColor = true;
        this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
        // 
        // MainTabControl
        // 
        this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.MainTabControl.HotTrack = true;
        this.MainTabControl.Location = new System.Drawing.Point(12, 33);
        this.MainTabControl.Multiline = true;
        this.MainTabControl.Name = "MainTabControl";
        this.MainTabControl.SelectedIndex = 0;
        this.MainTabControl.Size = new System.Drawing.Size(610, 516);
        this.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
        this.MainTabControl.TabIndex = 5;
        // 
        // DeleteButton
        // 
        this.DeleteButton.Enabled = false;
        this.DeleteButton.Location = new System.Drawing.Point(256, 4);
        this.DeleteButton.Name = "DeleteButton";
        this.DeleteButton.Size = new System.Drawing.Size(75, 23);
        this.DeleteButton.TabIndex = 4;
        this.DeleteButton.Text = "Delete";
        this.DeleteButton.UseVisualStyleBackColor = true;
        this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
        // 
        // LogOpenFileDialog
        // 
        this.LogOpenFileDialog.Multiselect = true;
        // 
        // LastUpdateCaption
        // 
        this.LastUpdateCaption.AutoSize = true;
        this.LastUpdateCaption.Location = new System.Drawing.Point(417, 9);
        this.LastUpdateCaption.Name = "LastUpdateCaption";
        this.LastUpdateCaption.Size = new System.Drawing.Size(74, 13);
        this.LastUpdateCaption.TabIndex = 5;
        this.LastUpdateCaption.Text = "Last Updated:";
        // 
        // LastUpdatedLabel
        // 
        this.LastUpdatedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.LastUpdatedLabel.AutoEllipsis = true;
        this.LastUpdatedLabel.Location = new System.Drawing.Point(497, 4);
        this.LastUpdatedLabel.Name = "LastUpdatedLabel";
        this.LastUpdatedLabel.Size = new System.Drawing.Size(125, 26);
        this.LastUpdatedLabel.TabIndex = 6;
        // 
        // FreezeButton
        // 
        this.FreezeButton.Enabled = false;
        this.FreezeButton.Location = new System.Drawing.Point(175, 4);
        this.FreezeButton.Name = "FreezeButton";
        this.FreezeButton.Size = new System.Drawing.Size(75, 23);
        this.FreezeButton.TabIndex = 3;
        this.FreezeButton.Text = "Freeze";
        this.FreezeButton.UseVisualStyleBackColor = true;
        this.FreezeButton.Click += new System.EventHandler(this.FreezeButton_Click);
        // 
        // btnFullLog
        // 
        this.btnFullLog.Location = new System.Drawing.Point(337, 4);
        this.btnFullLog.Name = "btnFullLog";
        this.btnFullLog.Size = new System.Drawing.Size(76, 23);
        this.btnFullLog.TabIndex = 7;
        this.btnFullLog.Text = "Full log";
        this.btnFullLog.UseVisualStyleBackColor = true;
        this.btnFullLog.Click += new System.EventHandler(this.btnFullLog_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(634, 561);
        this.Controls.Add(this.btnFullLog);
        this.Controls.Add(this.FreezeButton);
        this.Controls.Add(this.LastUpdatedLabel);
        this.Controls.Add(this.LastUpdateCaption);
        this.Controls.Add(this.DeleteButton);
        this.Controls.Add(this.MainTabControl);
        this.Controls.Add(this.AddButton);
        this.Controls.Add(this.ClearButton);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.KeyPreview = true;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Log Viewer";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
        this.ResumeLayout(false);
        this.PerformLayout();

    }
    internal System.Windows.Forms.Button ClearButton;
    internal System.Windows.Forms.Button AddButton;
    internal System.Windows.Forms.TabControl MainTabControl;
    internal System.Windows.Forms.Button DeleteButton;
    internal System.Windows.Forms.OpenFileDialog LogOpenFileDialog;
    internal System.Windows.Forms.Label LastUpdateCaption;
    internal System.Windows.Forms.Label LastUpdatedLabel;
    internal System.Windows.Forms.Button FreezeButton;
    private System.Windows.Forms.Button btnFullLog;
}
}