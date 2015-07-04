namespace Wise
{
    partial class ExportCSVForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportCSVForm));
            this.exportButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.searchEngineGroupBox = new System.Windows.Forms.GroupBox();
            this.bingRadioButton = new System.Windows.Forms.RadioButton();
            this.yahooRadioButton = new System.Windows.Forms.RadioButton();
            this.googleRadioButton = new System.Windows.Forms.RadioButton();
            this.termGroupBox = new System.Windows.Forms.GroupBox();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.specifiedTermRadioButton = new System.Windows.Forms.RadioButton();
            this.allTermRadioButton = new System.Windows.Forms.RadioButton();
            this.searchEngineGroupBox.SuspendLayout();
            this.termGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // exportButton
            // 
            resources.ApplyResources(this.exportButton, "exportButton");
            this.exportButton.Name = "exportButton";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // searchEngineGroupBox
            // 
            this.searchEngineGroupBox.Controls.Add(this.bingRadioButton);
            this.searchEngineGroupBox.Controls.Add(this.yahooRadioButton);
            this.searchEngineGroupBox.Controls.Add(this.googleRadioButton);
            resources.ApplyResources(this.searchEngineGroupBox, "searchEngineGroupBox");
            this.searchEngineGroupBox.Name = "searchEngineGroupBox";
            this.searchEngineGroupBox.TabStop = false;
            // 
            // bingRadioButton
            // 
            resources.ApplyResources(this.bingRadioButton, "bingRadioButton");
            this.bingRadioButton.Name = "bingRadioButton";
            this.bingRadioButton.TabStop = true;
            this.bingRadioButton.UseVisualStyleBackColor = true;
            // 
            // yahooRadioButton
            // 
            resources.ApplyResources(this.yahooRadioButton, "yahooRadioButton");
            this.yahooRadioButton.Name = "yahooRadioButton";
            this.yahooRadioButton.TabStop = true;
            this.yahooRadioButton.UseVisualStyleBackColor = true;
            // 
            // googleRadioButton
            // 
            resources.ApplyResources(this.googleRadioButton, "googleRadioButton");
            this.googleRadioButton.Checked = true;
            this.googleRadioButton.Name = "googleRadioButton";
            this.googleRadioButton.TabStop = true;
            this.googleRadioButton.UseVisualStyleBackColor = true;
            // 
            // termGroupBox
            // 
            this.termGroupBox.Controls.Add(this.endDateTimePicker);
            this.termGroupBox.Controls.Add(this.label1);
            this.termGroupBox.Controls.Add(this.startDateTimePicker);
            this.termGroupBox.Controls.Add(this.specifiedTermRadioButton);
            this.termGroupBox.Controls.Add(this.allTermRadioButton);
            resources.ApplyResources(this.termGroupBox, "termGroupBox");
            this.termGroupBox.Name = "termGroupBox";
            this.termGroupBox.TabStop = false;
            // 
            // endDateTimePicker
            // 
            resources.ApplyResources(this.endDateTimePicker, "endDateTimePicker");
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.ValueChanged += new System.EventHandler(this.endDateTimePicker_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // startDateTimePicker
            // 
            resources.ApplyResources(this.startDateTimePicker, "startDateTimePicker");
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.ValueChanged += new System.EventHandler(this.startDateTimePicker_ValueChanged);
            // 
            // specifiedTermRadioButton
            // 
            resources.ApplyResources(this.specifiedTermRadioButton, "specifiedTermRadioButton");
            this.specifiedTermRadioButton.Name = "specifiedTermRadioButton";
            this.specifiedTermRadioButton.UseVisualStyleBackColor = true;
            this.specifiedTermRadioButton.CheckedChanged += new System.EventHandler(this.specifiedTermRadioButton_CheckedChanged);
            // 
            // allTermRadioButton
            // 
            resources.ApplyResources(this.allTermRadioButton, "allTermRadioButton");
            this.allTermRadioButton.Checked = true;
            this.allTermRadioButton.Name = "allTermRadioButton";
            this.allTermRadioButton.TabStop = true;
            this.allTermRadioButton.UseVisualStyleBackColor = true;
            this.allTermRadioButton.CheckedChanged += new System.EventHandler(this.allTermRadioButton_CheckedChanged);
            // 
            // ExportCSVForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.termGroupBox);
            this.Controls.Add(this.searchEngineGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.exportButton);
            this.Name = "ExportCSVForm";
            this.Load += new System.EventHandler(this.ExportCSV_Load);
            this.searchEngineGroupBox.ResumeLayout(false);
            this.searchEngineGroupBox.PerformLayout();
            this.termGroupBox.ResumeLayout(false);
            this.termGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox searchEngineGroupBox;
        private System.Windows.Forms.GroupBox termGroupBox;
        private System.Windows.Forms.RadioButton specifiedTermRadioButton;
        private System.Windows.Forms.RadioButton allTermRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.RadioButton bingRadioButton;
        private System.Windows.Forms.RadioButton yahooRadioButton;
        private System.Windows.Forms.RadioButton googleRadioButton;
    }
}