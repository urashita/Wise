namespace Wise
{
    partial class AddItemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddItemForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.itemDataGroupBox = new System.Windows.Forms.GroupBox();
            this.siteURLComboBox = new System.Windows.Forms.ComboBox();
            this.siteNameComboBox = new System.Windows.Forms.ComboBox();
            this.siteNameLabel = new System.Windows.Forms.Label();
            this.siteURLLabel = new System.Windows.Forms.Label();
            this.searchDataGroupBox = new System.Windows.Forms.GroupBox();
            this.searchWordTextBox = new System.Windows.Forms.TextBox();
            this.searchWordLabel = new System.Windows.Forms.Label();
            this.itemDataGroupBox.SuspendLayout();
            this.searchDataGroupBox.SuspendLayout();
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
            // itemDataGroupBox
            // 
            this.itemDataGroupBox.Controls.Add(this.siteURLComboBox);
            this.itemDataGroupBox.Controls.Add(this.siteNameComboBox);
            this.itemDataGroupBox.Controls.Add(this.siteNameLabel);
            this.itemDataGroupBox.Controls.Add(this.siteURLLabel);
            resources.ApplyResources(this.itemDataGroupBox, "itemDataGroupBox");
            this.itemDataGroupBox.Name = "itemDataGroupBox";
            this.itemDataGroupBox.TabStop = false;
            // 
            // siteURLComboBox
            // 
            this.siteURLComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.siteURLComboBox, "siteURLComboBox");
            this.siteURLComboBox.Name = "siteURLComboBox";
            this.siteURLComboBox.SelectedIndexChanged += new System.EventHandler(this.siteURLComboBox_SelectedIndexChanged);
            // 
            // siteNameComboBox
            // 
            this.siteNameComboBox.DropDownWidth = 250;
            this.siteNameComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.siteNameComboBox, "siteNameComboBox");
            this.siteNameComboBox.Name = "siteNameComboBox";
            this.siteNameComboBox.SelectedIndexChanged += new System.EventHandler(this.siteNameComboBox_SelectedIndexChanged);
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
            // searchDataGroupBox
            // 
            this.searchDataGroupBox.Controls.Add(this.searchWordTextBox);
            this.searchDataGroupBox.Controls.Add(this.searchWordLabel);
            resources.ApplyResources(this.searchDataGroupBox, "searchDataGroupBox");
            this.searchDataGroupBox.Name = "searchDataGroupBox";
            this.searchDataGroupBox.TabStop = false;
            // 
            // searchWordTextBox
            // 
            resources.ApplyResources(this.searchWordTextBox, "searchWordTextBox");
            this.searchWordTextBox.Name = "searchWordTextBox";
            // 
            // searchWordLabel
            // 
            resources.ApplyResources(this.searchWordLabel, "searchWordLabel");
            this.searchWordLabel.Name = "searchWordLabel";
            // 
            // AddItemForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchDataGroupBox);
            this.Controls.Add(this.itemDataGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "AddItemForm";
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
        private System.Windows.Forms.ComboBox siteNameComboBox;
        private System.Windows.Forms.ComboBox siteURLComboBox;
    }
}