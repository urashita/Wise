/*----------------------------------------*/
/*                                        */
/* EditItemForm.cs                        */
/*                                        */
/* WISE (C) Shoji Urashita                */
/*----------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wise
{
    public partial class EditItemForm : Form
    {
        private MainForm mainForm;


        public EditItemForm(MainForm mainForm, string siteName, string siteURL, string searchWord)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            siteNameTextBox.Text = siteName;
            siteURLTextBox.Text = siteURL;
            siteURLTextBox.Enabled = false;
            searchWordTextBox.Text = searchWord;
            searchWordTextBox.Enabled = false;
        }


        private void button_ok_Click(object sender, EventArgs e)
        {
            int rtnval = this.mainForm.changeSiteName(this.siteNameTextBox.Text, this.siteURLTextBox.Text);

            if (rtnval == Wise.MainForm.SUCCESS)
            {
                this.Dispose();
            }
            else if (rtnval == Wise.MainForm.SameNameExist || rtnval == Wise.MainForm.NameURLMatch)
            {
                DialogResult result = MessageBox.Show(Wise.Properties.Resources.MessageBoxSiteNameExist,
                    "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
