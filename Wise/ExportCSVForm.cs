/*----------------------------------------*/
/*                                        */
/* ExportCSVForm.cs                       */
/*                                        */
/* WISE (C) Shoji Urashita                */
/*----------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wise
{
    public partial class ExportCSVForm : Form
    {
        private string dbConnectionString;

        public ExportCSVForm()
        {
            InitializeComponent();
        }

        public ExportCSVForm(string dbConnectionString)
        {
            InitializeComponent();

            this.dbConnectionString = dbConnectionString;
        }

        private void ExportCSV_Load(object sender, EventArgs e)
        {

        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream stream;
                stream = saveFileDialog1.OpenFile();

                if (stream != null)
                {
                    //Write to File
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                    sw.Write("Site Name,Site URL, Search Word");


                    using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
                    {
                        cn.Open();
                        using (SQLiteTransaction trans = cn.BeginTransaction())
                        {
                            DateTime startDate = new DateTime(1900, 1, 1, 0, 00, 00);
                            DateTime endDate = new DateTime(2099, 12, 31, 0, 00, 00);
                            if (specifiedTermRadioButton.Checked == true)
                            {
                                startDate = startDateTimePicker.Value.Date;
                                endDate = endDateTimePicker.Value.Date;
                            }

                            string Date1 = null;
                            int Id1 = -1, max_id = -1, min_id = -1;
                            ArrayList dateArray = new ArrayList();
                            SQLiteCommand cmd = cn.CreateCommand();
                            cmd.CommandText = "SELECT Id, strftime('%m', CheckDate), strftime('%d', CheckDate) FROM DateTable where CheckDate BETWEEN (@startDate) AND (@endDate) order by CheckDate Desc;";
                            cmd.Parameters.Add("startDate", System.Data.DbType.Date);
                            cmd.Parameters.Add("endDate", System.Data.DbType.Date);
                            cmd.Parameters["startDate"].Value = startDate;
                            cmd.Parameters["endDate"].Value = endDate;
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Id1 = Convert.ToInt32(reader["Id"].ToString());
                                    Date1 = (int.Parse(reader["strftime('%m', CheckDate)"].ToString())).ToString() + "/" + (int.Parse(reader["strftime('%d', CheckDate)"].ToString())).ToString();

                                    if (max_id == -1)
                                    {
                                        max_id = Id1;
                                    }
                                    if (min_id == -1)
                                    {
                                        min_id = Id1;
                                    }

                                    sw.Write(",");
                                    sw.Write(Date1);
                                    max_id = Math.Max(Id1, max_id);
                                    min_id = Math.Min(Id1, min_id);
                                    dateArray.Add(Id1);
                                }
                                sw.Write(Environment.NewLine);

                                SQLiteCommand cmd2 = cn.CreateCommand();
                                cmd2.CommandText = "SELECT Id, SiteNameTable_Id, SearchWord FROM SearchWordTable order by SearchWordTableOrder_Id;";
                                int currentColumn = 0;
                                using (SQLiteDataReader reader2 = cmd2.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        int searchWordTable_Id = int.Parse(reader2["Id"].ToString());
                                        int siteNameTable_Id = int.Parse(reader2["SiteNameTable_Id"].ToString());
                                        string searchWord = reader2["SearchWord"].ToString();
                                        string siteName = null, siteURL = null;

                                        SQLiteCommand cmd3 = cn.CreateCommand();
                                        cmd3.CommandText = "SELECT SiteName, SiteURL FROM SiteNameTable WHERE Id is (@Id)";
                                        cmd3.Parameters.Add("Id", System.Data.DbType.Int64);
                                        cmd3.Parameters["Id"].Value = siteNameTable_Id;
                                        using (SQLiteDataReader reader3 = cmd3.ExecuteReader())
                                        {
                                            while (reader3.Read())
                                            {
                                                siteName = reader3["SiteName"].ToString();
                                                siteURL = reader3["SiteURL"].ToString();
                                            }
                                        }
                                        sw.Write(siteName);
                                        sw.Write(",");
                                        sw.Write(siteURL);
                                        sw.Write(",");
                                        sw.Write(searchWord);

                                        if (googleRadioButton.Checked)
                                        {
                                            // Position History
                                            SQLiteCommand cmd4 = cn.CreateCommand();
                                            cmd4.CommandText = "SELECT GooglePosition, DateTable_Id FROM SearchWordPositionTable WHERE SearchWordTable_Id is (@SearchWordTable_Id) and DateTable_Id BETWEEN (@min_id) AND (@max_id) order by DateTable_Id Desc;";

                                            cmd4.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                                            cmd4.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                                            cmd4.Parameters.Add("max_id", System.Data.DbType.Int64);
                                            cmd4.Parameters["max_id"].Value = max_id;
                                            cmd4.Parameters.Add("min_id", System.Data.DbType.Int64);
                                            cmd4.Parameters["min_id"].Value = min_id;
                                            string googlePositionHistory = null;
                                            int dateTable_Id = -1;
                                            int count = 0;
                                            using (SQLiteDataReader reader4 = cmd4.ExecuteReader())
                                            {
                                                while (reader4.Read())
                                                {
                                                    googlePositionHistory = reader4["GooglePosition"].ToString();
                                                    dateTable_Id = Convert.ToInt32(reader4["DateTable_Id"]);

                                                    if (!dateArray.Contains(dateTable_Id))
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        while (true)
                                                        {
                                                            if (Convert.ToInt32(dateArray[count]) == dateTable_Id)
                                                            {
                                                                sw.Write(",");
                                                                sw.Write(googlePositionHistory);
                                                                count++;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                sw.Write(",");
                                                                count++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            currentColumn++;
                                            sw.Write(Environment.NewLine);
                                        }
                                        else if (yahooRadioButton.Checked)
                                        {
                                            SQLiteCommand cmd4 = cn.CreateCommand();
                                            cmd4.CommandText = "SELECT YahooPosition, DateTable_Id FROM SearchWordPositionTable WHERE SearchWordTable_Id is (@SearchWordTable_Id) and DateTable_Id BETWEEN (@min_id) AND (@max_id) order by DateTable_Id Desc;";
                                            cmd4.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                                            cmd4.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                                            cmd4.Parameters.Add("max_id", System.Data.DbType.Int64);
                                            cmd4.Parameters["max_id"].Value = max_id;
                                            cmd4.Parameters.Add("min_id", System.Data.DbType.Int64);
                                            cmd4.Parameters["min_id"].Value = min_id;
                                            string yahooPositionHistory = null;
                                            int dateTable_Id = -1;
                                            int count = 0;
                                            using (SQLiteDataReader reader4 = cmd4.ExecuteReader())
                                            {
                                                while (reader4.Read())
                                                {
                                                    yahooPositionHistory = reader4["YahooPosition"].ToString();
                                                    dateTable_Id = Convert.ToInt32(reader4["DateTable_Id"].ToString());

                                                    if (!dateArray.Contains(dateTable_Id))
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        while (true)
                                                        {
                                                            if (Convert.ToInt32(dateArray[count]) == dateTable_Id)
                                                            {
                                                                sw.Write(",");
                                                                sw.Write(yahooPositionHistory);
                                                                count++;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                sw.Write(",");
                                                                count++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            currentColumn++;
                                            sw.Write(Environment.NewLine);
                                        }
                                        else if (bingRadioButton.Checked)
                                        {
                                            SQLiteCommand cmd4 = cn.CreateCommand();
                                            cmd4.CommandText = "SELECT BingPosition, DateTable_Id FROM SearchWordPositionTable WHERE SearchWordTable_Id is (@SearchWordTable_Id) and DateTable_Id BETWEEN (@min_id) AND (@max_id) order by DateTable_Id Desc;";
                                            cmd4.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                                            cmd4.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                                            cmd4.Parameters.Add("max_id", System.Data.DbType.Int64);
                                            cmd4.Parameters["max_id"].Value = max_id;
                                            cmd4.Parameters.Add("min_id", System.Data.DbType.Int64);
                                            cmd4.Parameters["min_id"].Value = min_id;
                                            string bingPositionHistory = null;
                                            int dateTable_Id = -1;
                                            int count = 0;
                                            using (SQLiteDataReader reader4 = cmd4.ExecuteReader())
                                            {
                                                while (reader4.Read())
                                                {
                                                    bingPositionHistory = reader4["BingPosition"].ToString();
                                                    dateTable_Id = Convert.ToInt32(reader4["DateTable_Id"].ToString());

                                                    if (!dateArray.Contains(dateTable_Id))
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        while (true)
                                                        {
                                                            if (Convert.ToInt32(dateArray[count]) == dateTable_Id)
                                                            {
                                                                sw.Write(",");
                                                                sw.Write(bingPositionHistory);
                                                                count++;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                sw.Write(",");
                                                                count++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            currentColumn++;
                                            sw.Write(Environment.NewLine);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Close
                    sw.Close();
                    stream.Close();
                }
            }

            this.Dispose();

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void startDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void endDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void specifiedTermRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            startDateTimePicker.Enabled = true;
            endDateTimePicker.Enabled = true;
        }

        private void allTermRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            startDateTimePicker.Enabled = false;
            endDateTimePicker.Enabled = false;
        }
    }
}
