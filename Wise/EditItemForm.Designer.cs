namespace Wise
{
    partial class EditItemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditItemForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.siteNameLabel = new System.Windows.Forms.Label();
            this.siteURLLabel = new System.Windows.Forms.Label();
            this.searchWordLabel = new System.Windows.Forms.Label();
            this.searchWordTextBox = new System.Windows.Forms.TextBox();
            this.itemDataGroupBox = new System.Windows.Forms.GroupBox();
            this.siteURLTextBox = new System.Windows.Forms.TextBox();
            this.siteNameTextBox = new System.Windows.Forms.TextBox();
            this.searchDataGroupBox = new System.Windows.Forms.GroupBox();
            this.itemDataGroupBox.SuspendLayout();
            this.searchDataGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // siteNameLabel
            // 
            resources.ApplyResources(this.siteNameLabel, "siteNameLabel");
            this.siteNameLabel.Name = "siteNameLabel";
            // 
            // siteURLLabel
            // 
            resources.ApplyResources(this.siteURLLabel, "siteURLLabel");
            this.siteURLLabel.Name = "siteURLLabel";
            // 
            // searchWordLabel
            // 
            resources.ApplyResources(this.searchWordLabel, "searchWordLabel");
            this.searchWordLabel.Name = "searchWordLabel";
            // 
            // searchWordTextBox
            // 
            resources.ApplyResources(this.searchWordTextBox, "searchWordTextBox");
            this.searchWordTextBox.Name = "searchWordTextBox";
            // 
            // itemDataGroupBox
            // 
            this.itemDataGroupBox.Controls.Add(this.siteURLTextBox);
            this.itemDataGroupBox.Controls.Add(this.siteNameTextBox);
            this.itemDataGroupBox.Controls.Add(this.siteNameLabel);
            this.itemDataGroupBox.Controls.Add(this.siteURLLabel);
            resources.ApplyResources(this.itemDataGroupBox, "itemDataGroupBox");
            this.itemDataGroupBox.Name = "itemDataGroupBox";
            this.itemDataGroupBox.TabStop = false;
            // 
            // siteURLTextBox
            // 
            resources.ApplyResources(this.siteURLTextBox, "siteURLTextBox");
            this.siteURLTextBox.Name = "siteURLTextBox";
            // 
            // siteNameTextBox
            // 
            resources.ApplyResources(this.siteNameTextBox, "siteNameTextBox");
            this.siteNameTextBox.Name = "siteNameTextBox";
            // 
            // searchDataGroupBox
            // 
            this.searchDataGroupBox.Controls.Add(this.searchWordLabel);
            this.searchDataGroupBox.Controls.Add(this.searchWordTextBox);
            resources.ApplyResources(this.searchDataGroupBox, "searchDataGroupBox");
            this.searchDataGroupBox.Name = "searchDataGroupBox";
            this.searchDataGroupBox.TabStop = false;
            // 
            // EditItemForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchDataGroupBox);
            this.Controls.Add(this.itemDataGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "EditItemForm";
            this.itemDataGroupBox.ResumeLayout(false);
            this.itemDataGroupBox.PerformLayout();
            this.searchDataGroupBox.ResumeLayout(false);
            this.searchDataGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label siteNameLabel;
        private System.Windows.Forms.Label siteURLLabel;
        private System.Windows.Forms.Label searchWordLabel;
        private System.Windows.Forms.TextBox searchWordTextBox;
        private System.Windows.Forms.GroupBox itemDataGroupBox;
        private System.Windows.Forms.GroupBox searchDataGroupBox;
        private System.Windows.Forms.TextBox siteURLTextBox;
        private System.Windows.Forms.TextBox siteNameTextBox;
    }
}