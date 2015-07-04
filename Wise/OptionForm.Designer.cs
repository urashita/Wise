namespace Wise
{
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.googleGroupBox = new System.Windows.Forms.GroupBox();
            this.google300RadioButton = new System.Windows.Forms.RadioButton();
            this.google200RadioButton = new System.Windows.Forms.RadioButton();
            this.google100RadioButton = new System.Windows.Forms.RadioButton();
            this.yahooGroupBox = new System.Windows.Forms.GroupBox();
            this.yahoo300RadioButton = new System.Windows.Forms.RadioButton();
            this.yahoo200RadioButton = new System.Windows.Forms.RadioButton();
            this.yahoo100RadioButton = new System.Windows.Forms.RadioButton();
            this.bingGroupBox = new System.Windows.Forms.GroupBox();
            this.bing100RadioButton = new System.Windows.Forms.RadioButton();
            this.bing50RadioButton = new System.Windows.Forms.RadioButton();
            this.bing20RadioButton = new System.Windows.Forms.RadioButton();
            this.googleGroupBox.SuspendLayout();
            this.yahooGroupBox.SuspendLayout();
            this.bingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // googleGroupBox
            // 
            this.googleGroupBox.Controls.Add(this.google300RadioButton);
            this.googleGroupBox.Controls.Add(this.google200RadioButton);
            this.googleGroupBox.Controls.Add(this.google100RadioButton);
            resources.ApplyResources(this.googleGroupBox, "googleGroupBox");
            this.googleGroupBox.Name = "googleGroupBox";
            this.googleGroupBox.TabStop = false;
            // 
            // google300RadioButton
            // 
            resources.ApplyResources(this.google300RadioButton, "google300RadioButton");
            this.google300RadioButton.Name = "google300RadioButton";
            this.google300RadioButton.UseVisualStyleBackColor = true;
            // 
            // google200RadioButton
            // 
            resources.ApplyResources(this.google200RadioButton, "google200RadioButton");
            this.google200RadioButton.Name = "google200RadioButton";
            this.google200RadioButton.UseVisualStyleBackColor = true;
            // 
            // google100RadioButton
            // 
            resources.ApplyResources(this.google100RadioButton, "google100RadioButton");
            this.google100RadioButton.Name = "google100RadioButton";
            this.google100RadioButton.UseVisualStyleBackColor = true;
            // 
            // yahooGroupBox
            // 
            this.yahooGroupBox.Controls.Add(this.yahoo300RadioButton);
            this.yahooGroupBox.Controls.Add(this.yahoo200RadioButton);
            this.yahooGroupBox.Controls.Add(this.yahoo100RadioButton);
            resources.ApplyResources(this.yahooGroupBox, "yahooGroupBox");
            this.yahooGroupBox.Name = "yahooGroupBox";
            this.yahooGroupBox.TabStop = false;
            // 
            // yahoo300RadioButton
            // 
            resources.ApplyResources(this.yahoo300RadioButton, "yahoo300RadioButton");
            this.yahoo300RadioButton.Name = "yahoo300RadioButton";
            this.yahoo300RadioButton.UseVisualStyleBackColor = true;
            // 
            // yahoo200RadioButton
            // 
            resources.ApplyResources(this.yahoo200RadioButton, "yahoo200RadioButton");
            this.yahoo200RadioButton.Name = "yahoo200RadioButton";
            this.yahoo200RadioButton.UseVisualStyleBackColor = true;
            // 
            // yahoo100RadioButton
            // 
            resources.ApplyResources(this.yahoo100RadioButton, "yahoo100RadioButton");
            this.yahoo100RadioButton.Name = "yahoo100RadioButton";
            this.yahoo100RadioButton.UseVisualStyleBackColor = true;
            // 
            // bingGroupBox
            // 
            this.bingGroupBox.Controls.Add(this.bing100RadioButton);
            this.bingGroupBox.Controls.Add(this.bing50RadioButton);
            this.bingGroupBox.Controls.Add(this.bing20RadioButton);
            resources.ApplyResources(this.bingGroupBox, "bingGroupBox");
            this.bingGroupBox.Name = "bingGroupBox";
            this.bingGroupBox.TabStop = false;
            // 
            // bing100RadioButton
            // 
            resources.ApplyResources(this.bing100RadioButton, "bing100RadioButton");
            this.bing100RadioButton.Name = "bing100RadioButton";
            this.bing100RadioButton.TabStop = true;
            this.bing100RadioButton.UseVisualStyleBackColor = true;
            // 
            // bing50RadioButton
            // 
            resources.ApplyResources(this.bing50RadioButton, "bing50RadioButton");
            this.bing50RadioButton.Name = "bing50RadioButton";
            this.bing50RadioButton.TabStop = true;
            this.bing50RadioButton.UseVisualStyleBackColor = true;
            // 
            // bing20RadioButton
            // 
            resources.ApplyResources(this.bing20RadioButton, "bing20RadioButton");
            this.bing20RadioButton.Checked = true;
            this.bing20RadioButton.Name = "bing20RadioButton";
            this.bing20RadioButton.TabStop = true;
            this.bing20RadioButton.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.yahooGroupBox);
            this.Controls.Add(this.bingGroupBox);
            this.Controls.Add(this.googleGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "OptionForm";
            this.googleGroupBox.ResumeLayout(false);
            this.googleGroupBox.PerformLayout();
            this.yahooGroupBox.ResumeLayout(false);
            this.yahooGroupBox.PerformLayout();
            this.bingGroupBox.ResumeLayout(false);
            this.bingGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox googleGroupBox;
        private System.Windows.Forms.GroupBox yahooGroupBox;
        private System.Windows.Forms.GroupBox bingGroupBox;
        private System.Windows.Forms.RadioButton google300RadioButton;
        private System.Windows.Forms.RadioButton google200RadioButton;
        private System.Windows.Forms.RadioButton google100RadioButton;
        private System.Windows.Forms.RadioButton yahoo300RadioButton;
        private System.Windows.Forms.RadioButton yahoo200RadioButton;
        private System.Windows.Forms.RadioButton yahoo100RadioButton;
        private System.Windows.Forms.RadioButton bing20RadioButton;
        private System.Windows.Forms.RadioButton bing100RadioButton;
        private System.Windows.Forms.RadioButton bing50RadioButton;
    }
}