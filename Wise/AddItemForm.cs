/*----------------------------------------*/
/*                                        */
/* AddItemForm.cs                         */
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
    public partial class AddItemForm : Form
    {
        private MainForm mainForm;

        public AddItemForm(MainForm form1)
        {
            InitializeComponent();
            this.mainForm = form1;

            try
            {
                mainForm.AddItemForm(this.siteNameComboBox, this.siteURLComboBox);
            }
            catch (Exception)
            {

            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void okButton_Click(object sender, EventArgs e)
        {

            if (siteNameComboBox.Text != "" &&
                siteURLComboBox.Text != "" &&
                searchWordTextBox.Text != "")
            {
                Uri uriResult;
                if (Uri.TryCreate(siteURLComboBox.Text, UriKind.Absolute, out uriResult)
                  && (uriResult.Scheme == Uri.UriSchemeHttp))
                {
                    String siteURL = null;
                    if (siteURLComboBox.Text.StartsWith("http://"))
                    {
                        //siteURL = siteURLComboBox.Text.Substring("http://".Length);
                        Uri u1 = new Uri(siteURLComboBox.Text);
                        siteURL = u1.DnsSafeHost;
                    }

                    int rtnval = this.mainForm.addNewItem(siteNameComboBox.Text, siteURL, searchWordTextBox.Text);
                    if (rtnval == Wise.MainForm.SUCCESS)
                    {
                        this.Dispose();
                    }
                    else if (rtnval == Wise.MainForm.SameNameExist)
                    {
                        DialogResult result = MessageBox.Show(Wise.Properties.Resources.MessageBoxSiteNameExist,
                            "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    }
                    else if (rtnval == Wise.MainForm.SameSearchWordExist)
                    {
                        DialogResult result = MessageBox.Show(Wise.Properties.Resources.MessageBoxSameSearchWordExist,
                            "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    }
                    else if (rtnval == Wise.MainForm.SameURLExist)
                    {
                        DialogResult result = MessageBox.Show(Wise.Properties.Resources.MessageBoxSameURLExist,
                            "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(Wise.Properties.Resources.MessageBoxURL,
                        "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(Wise.Properties.Resources.MessageBoxEmpty,
                    "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }

        private void siteNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int resultIndex = siteNameComboBox.FindStringExact((string)siteNameComboBox.SelectedItem);
            siteURLComboBox.SelectedIndex = resultIndex;
        }

        private void siteURLComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int resultIndex = siteURLComboBox.FindStringExact((string)siteURLComboBox.SelectedItem);
            siteNameComboBox.SelectedIndex = resultIndex;
        }
    }
}
