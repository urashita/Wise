/*----------------------------------------*/
/*                                        */
/* OptionForm.cs                          */
/*                                        */
/* WISE (C) Shoji Urashita                */
/*----------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wise
{
    public partial class OptionForm : Form
    {
        private string dbConnectionString;

        public OptionForm()
        {
            InitializeComponent();
        }

        public OptionForm(string dbConnectionString)
        {
            InitializeComponent();

            this.dbConnectionString = dbConnectionString;

            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT GoogleCount, YahooCount FROM OptionTable;";

                int googleCount = -1;
                int yahooCount = -1;
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        googleCount = Convert.ToInt32(reader["GoogleCount"]);
                        yahooCount = Convert.ToInt32(reader["YahooCount"]);
                    }
                }

                if (googleCount == 100)
                {
                    google100RadioButton.Checked = true;

                }
                else if (googleCount == 200)
                {
                    google200RadioButton.Checked = true;
                }
                else
                {
                    google300RadioButton.Checked = true;
                }

                if (yahooCount == 100)
                {
                    yahoo100RadioButton.Checked = true;

                }
                else if (yahooCount == 200)
                {
                    yahoo200RadioButton.Checked = true;
                }
                else
                {
                    yahoo300RadioButton.Checked = true;
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int googleCount, yahooCount;
            if (google100RadioButton.Checked)
            {
                googleCount = 100;
            }
            else if (google200RadioButton.Checked)
            {
                googleCount = 200;
            }
            else
            {
                googleCount = 300;
            }

            if (yahoo100RadioButton.Checked)
            {
                yahooCount = 100;
            }
            else if (yahoo200RadioButton.Checked)
            {
                yahooCount = 200;
            }
            else
            {
                yahooCount = 300;
            }

            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "UPDATE OptionTable Set GoogleCount = (@GoogleCount), YahooCount = (@YahooCount)";
                    cmd.Parameters.Add("GoogleCount", System.Data.DbType.Int64);
                    cmd.Parameters.Add("YahooCount", System.Data.DbType.Int64);
                    cmd.Parameters["GoogleCount"].Value = googleCount;
                    cmd.Parameters["YahooCount"].Value = yahooCount;
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
            }

            this.Dispose();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
