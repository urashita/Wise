/*----------------------------------------*/
/*                                        */
/* MainForm.cs                            */
/*                                        */
/* WISE (C) Shoji Urashita                */
/*----------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Wise
{
    public partial class MainForm : Form
    {
        string dbName = "Wise.db";
        string dbConnectionString = null;
        public const int SUCCESS = 1;
        public const int FAILURE = -1;
        public const int NameURLMatch = 11;
        public const int NameURLNotMatch = 12;
        public const int SameNameExist = -11;
        public const int SameSearchWordExist = -12;
        public const int SameURLExist = -13;
        enum ChangeStatus { RANK_UP, RANK_STABLE, RANK_DOWN };

        #region Main Form
        public MainForm()
        {
            string wiseSetting = @"WiseSetting.txt";

            // File Open if exits
            if (File.Exists(wiseSetting))
            {
                StreamReader reader = File.OpenText(wiseSetting);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] items = line.Split('=');

                    if (items.Length == 2)
                    {
                        if (items[0].StartsWith("WISE_DB_PATH"))
                        {
                            dbName = items[1].Replace(" ", "");
                            break;

                        }
                    }
                }
            }

            if (File.Exists(dbName))
            {
            }
            else
            {
                MessageBox.Show(dbName + " does not exist", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            dbConnectionString = "Data Source=" + dbName;


            InitializeComponent();

            toolStripStatusLabel1.Text = "WISE started";
        }

        void Form1_MouseHover(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                toolStripStatusLabel1.Text = item.Text;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                backgroundShowTab.DoWork += new DoWorkEventHandler(BackgroundShowTab_DoWork);
                backgroundShowTab.ProgressChanged += new ProgressChangedEventHandler(BackgroundShowTab_ProgressChanged);
                backgroundShowTab.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundShowTab_RunWorkerCompleted);
                backgroundShowTab.WorkerReportsProgress = true;
                backgroundShowTab.WorkerSupportsCancellation = true;
                backgroundShowTab.RunWorkerAsync();


                backgroundRankCheck.DoWork += new DoWorkEventHandler(BackgroundRankCheck_DoWork);
                backgroundRankCheck.ProgressChanged += new ProgressChangedEventHandler(BackgroundRankCheck_ProgressChanged);
                backgroundRankCheck.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundRankCheck_RunWorkerCompleted);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackgroundShowTab_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            fileToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            browseToolStripMenuItem.Enabled = true;
            runToolStripMenuItem.Enabled = true;
            optionToolStripMenuItem.Enabled = true;
            helpToolStripMenuItem.Enabled = true;
        }

        private void BackgroundShowTab_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void BackgroundShowTab_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int columnCount = 0;
                showTabs(ref columnCount);
                toolStripStatusLabel1.Text = dataGridView1.Rows.Count + " items. ";
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Show Tabs
        // Show Tabs
        private void showTabs(ref int columnCount)
        {
            DateTime dtToday = DateTime.Today.AddYears(-1); // 1 Year Ago

            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    string Date1 = null;
                    string Id1 = null;
                    SQLiteCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT Id, strftime('%m', CheckDate), strftime('%d', CheckDate) FROM DateTable WHERE CheckDate > (@dtToday) order by CheckDate Desc";
                    cmd.Parameters.Add("dtToday", System.Data.DbType.Date);
                    cmd.Parameters["dtToday"].Value = dtToday;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Id1 = reader["Id"].ToString();
                            Date1 = (int.Parse(reader["strftime('%m', CheckDate)"].ToString())).ToString() + "/" + (int.Parse(reader["strftime('%d', CheckDate)"].ToString())).ToString();

                            DataGridViewTextBoxColumn textColumn2 = new DataGridViewTextBoxColumn();
                            textColumn2.DataPropertyName = "Date1";
                            textColumn2.Name = Id1;
                            textColumn2.HeaderText = Date1;
                            textColumn2.Width = 40;
                            textColumn2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView2.Columns.Add(textColumn2);

                            DataGridViewTextBoxColumn textColumn3 = new DataGridViewTextBoxColumn();
                            textColumn3.DataPropertyName = "Date1";
                            textColumn3.Name = Id1;
                            textColumn3.HeaderText = Date1;
                            textColumn3.Width = 40;
                            textColumn3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView3.Columns.Add(textColumn3);

                            DataGridViewTextBoxColumn textColumn4 = new DataGridViewTextBoxColumn();
                            textColumn4.DataPropertyName = "Date1";
                            textColumn4.Name = Id1;
                            textColumn4.HeaderText = Date1;
                            textColumn4.Width = 40;
                            textColumn4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView4.Columns.Add(textColumn4);
                        }
                    }


                    SQLiteCommand cmd2 = cn.CreateCommand();
                    cmd2.CommandText = "SELECT Id, SiteNameTable_Id, SearchWord FROM SearchWordTable order by SearchWordTableOrder_Id;";
                    columnCount = 0;
                    using (SQLiteDataReader reader2 = cmd2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            int searchWordTable_Id = int.Parse(reader2["Id"].ToString());
                            int siteNameTable_Id = int.Parse(reader2["SiteNameTable_Id"].ToString());
                            string searchWord = reader2["SearchWord"].ToString();
                            string siteName = null, siteURL = null;
                            string googlePosition = null, googleVolumn = null;
                            string yahooPosition = null, yahooVolumn = null;
                            string bingPosition = null, bingVolumn = null;

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
                            Invoke(new delegate_func1(func1), dataGridView2, siteName, siteURL, searchWord);
                            Invoke(new delegate_func1(func1), dataGridView3, siteName, siteURL, searchWord);
                            Invoke(new delegate_func1(func1), dataGridView4, siteName, siteURL, searchWord);

                            SQLiteCommand cmd4 = cn.CreateCommand();
                            cmd4.CommandText = "SELECT GooglePosition, GoogleVolumn, YahooPosition, YahooVolumn, BingPosition, BingVolumn FROM SearchWordPositionTable WHERE SearchWordTable_Id is (@SearchWordTable_Id) and DateTable_Id is (select Max(DateTable_Id) from SearchWordPositionTable where SearchWordTable_Id is (@SearchWordTable_Id))";
                            cmd4.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                            cmd4.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;

                            using (SQLiteDataReader reader4 = cmd4.ExecuteReader())
                            {
                                while (reader4.Read())
                                {
                                    googlePosition = reader4["GooglePosition"].ToString();
                                    googleVolumn = reader4["GoogleVolumn"].ToString();
                                    yahooPosition = reader4["YahooPosition"].ToString();
                                    yahooVolumn = reader4["YahooVolumn"].ToString();
                                    bingPosition = reader4["BingPosition"].ToString();
                                    bingVolumn = reader4["BingVolumn"].ToString();
                                }
                            }
                            Invoke(new delegate_func2(func2), dataGridView1, siteName, siteURL, searchWord, googlePosition, googleVolumn, yahooPosition, yahooVolumn, bingPosition, bingVolumn);

                            Invoke(new delegate_func3(func3), dataGridView5, siteName, siteURL, searchWord, googlePosition);
                            Invoke(new delegate_func3(func3), dataGridView6, siteName, siteURL, searchWord, yahooPosition);
                            Invoke(new delegate_func3(func3), dataGridView7, siteName, siteURL, searchWord, bingPosition);

                            // Position History
                            SQLiteCommand cmd5 = cn.CreateCommand();
                            cmd5.CommandText = "SELECT GooglePosition, YahooPosition, BingPosition, DateTable_Id FROM SearchWordPositionTable WHERE SearchWordTable_Id is (@SearchWordTable_Id)";
                            cmd5.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                            cmd5.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                            string googlePositionHistory = null, yahooPositionHistory = null, bingPositionHistory = null;
                            string dateTable_Id = null;

                            using (SQLiteDataReader reader5 = cmd5.ExecuteReader())
                            {
                                while (reader5.Read())
                                {
                                    googlePositionHistory = reader5["GooglePosition"].ToString();
                                    yahooPositionHistory = reader5["YahooPosition"].ToString();
                                    bingPositionHistory = reader5["BingPosition"].ToString();
                                    dateTable_Id = reader5["DateTable_Id"].ToString();

                                    try
                                    {
                                        dataGridView2.Rows[columnCount].Cells[dateTable_Id].Value = googlePositionHistory;
                                        dataGridView3.Rows[columnCount].Cells[dateTable_Id].Value = yahooPositionHistory;
                                        dataGridView4.Rows[columnCount].Cells[dateTable_Id].Value = bingPositionHistory;
                                    }
                                    catch (Exception)
                                    {
                                        // If there is data without correct date, ignore it.
                                    }
                                }
                            }

                            // Search Result Detail
                            SQLiteCommand cmd6 = cn.CreateCommand();
                            cmd6.CommandText = "SELECT GoogleURL, GoogleTitle, GoogleChangeText, GoogleChangeStatus, YahooURL, YahooTitle, YahooChangeText, YahooChangeStatus, BingURL, BingTitle, BingChangeText, BingChangeStatus FROM SearchResultDetailTable WHERE SearchWordTable_Id is (@SearchWordTable_Id)";
                            cmd6.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                            cmd6.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                            string googleURL = null, googleTitle = null, googleChangeText = null, googleChangeStatus = null;
                            string yahooURL = null, yahooTitle = null, yahooChangeText = null, yahooChangeStatus = null;
                            string bingURL = null, bingTitle = null, bingChangeText = null, bingChangeStatus = null;

                            using (SQLiteDataReader reader6 = cmd6.ExecuteReader())
                            {
                                while (reader6.Read())
                                {
                                    googleURL = reader6["GoogleURL"].ToString();
                                    googleTitle = reader6["GoogleTitle"].ToString();
                                    googleChangeText = reader6["GoogleChangeText"].ToString();
                                    googleChangeStatus = reader6["GoogleChangeStatus"].ToString();
                                    yahooURL = reader6["YahooURL"].ToString();
                                    yahooTitle = reader6["YahooTitle"].ToString();
                                    yahooChangeText = reader6["YahooChangeText"].ToString();
                                    yahooChangeStatus = reader6["YahooChangeStatus"].ToString();
                                    bingURL = reader6["BingURL"].ToString();
                                    bingTitle = reader6["BingTitle"].ToString();
                                    bingChangeText = reader6["BingChangeText"].ToString();
                                    bingChangeStatus = reader6["BingChangeStatus"].ToString();

                                    try
                                    {
                                        setSellForChangeText(columnCount, 4, googleChangeText, googleChangeStatus);
                                        setSellForChangeText(columnCount, 7, yahooChangeText, yahooChangeStatus);
                                        setSellForChangeText(columnCount, 10, bingChangeText, bingChangeStatus);

                                        dataGridView5.Rows[columnCount].Cells[4].Value = googleURL;
                                        dataGridView5.Rows[columnCount].Cells[5].Value = googleTitle;
                                        dataGridView6.Rows[columnCount].Cells[4].Value = yahooURL;
                                        dataGridView6.Rows[columnCount].Cells[5].Value = yahooTitle;
                                        dataGridView7.Rows[columnCount].Cells[4].Value = bingURL;
                                        dataGridView7.Rows[columnCount].Cells[5].Value = bingTitle;
                                    }
                                    catch (Exception)
                                    {
                                        // If there is data without correct date, ignore it.
                                    }
                                }
                            }

                            columnCount++;
                        }
                    }
                }
            }
        }

        delegate void delegate_func1(DataGridView dataGridView, string siteName, string siteURL, string searchWord);
        void func1(DataGridView dataGridView, string siteName, string siteURL, string searchWord)
        {
            dataGridView.Rows.Add(siteName, siteURL, searchWord);
        }

        delegate void delegate_func2(DataGridView dataGridView, string siteName, string siteURL, string searchWord, string googlePosition, string googleVolumn, string yahooPosition, string yahooVolumn, string bingPosition, string bingVolumn);
        void func2(DataGridView dataGridView, string siteName, string siteURL, string searchWord, string googlePosition, string googleVolumn, string yahooPosition, string yahooVolumn, string bingPosition, string bingVolumn)
        {
            dataGridView.Rows.Add(siteName, siteURL, searchWord, googlePosition, "", googleVolumn, yahooPosition, "", yahooVolumn, bingPosition, "", bingVolumn);
        }

        delegate void delegate_func3(DataGridView dataGridView, string siteName, string siteURL, string searchWord, string position);
        void func3(DataGridView dataGridView, string siteName, string siteURL, string searchWord, string position)
        {
            dataGridView.Rows.Add(siteName, siteURL, searchWord, position);
        }

        private void setSellForChangeText(int columnCount, int p, string changeText, string changeStatus)
        {
            if (changeStatus == "0") //UP
            {
                dataGridView1.Rows[columnCount].Cells[p].Style.ForeColor = Color.Blue;
            }
            else if (changeStatus == "2") //Down
            {
                dataGridView1.Rows[columnCount].Cells[p].Style.ForeColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[columnCount].Cells[p].Style.ForeColor = Color.Black;
            }

            dataGridView1.Rows[columnCount].Cells[p].Value = changeText;
        }
        #endregion

        #region Miscellaneous Functions
        private void getOptions(ref int googleCount, ref int yahooCount)
        {
            googleCount = 100;
            yahooCount = 100;
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT GoogleCount, YahooCount FROM OptionTable;";

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        googleCount = Convert.ToInt32(reader["GoogleCount"]);
                        yahooCount = Convert.ToInt32(reader["YahooCount"]);
                    }
                }
            }
        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Header Cell?
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    int idx = dataGridView1.CurrentCell.RowIndex;
                    int rowindex = e.RowIndex;

                    //Show context menu
                    this.contextMenuStrip1.Show();

                    //Get mouse cursor position
                    Point p = Control.MousePosition;
                    this.contextMenuStrip1.Top = p.Y;
                    this.contextMenuStrip1.Left = p.X;
                }
            }
        }

        private int checkSiteNameURL(string siteName, string siteURL)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    int count;
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT count(*) FROM SiteNameTable WHERE SiteName is (@SiteName) and SiteURL is (@SiteURL)";
                    cmd.Parameters.Add("SiteName", System.Data.DbType.String);
                    cmd.Parameters.Add("SiteURL", System.Data.DbType.String);
                    cmd.Parameters["SiteName"].Value = siteName;
                    cmd.Parameters["SiteURL"].Value = siteURL;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = int.Parse(reader["count(*)"].ToString());
                            if (count >= 1)
                            {
                                return NameURLMatch;
                            }
                        }
                    }

                    SQLiteCommand cmd2 = cn.CreateCommand();
                    cmd2.CommandText = "SELECT count(*) FROM SiteNameTable WHERE SiteName is (@SiteName)";
                    cmd2.Parameters.Add("SiteName", System.Data.DbType.String);
                    cmd2.Parameters["SiteName"].Value = siteName;
                    using (SQLiteDataReader reader2 = cmd2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            count = int.Parse(reader2["count(*)"].ToString());
                            if (count >= 1)
                            {
                                return SameNameExist;
                            }
                        }
                    }

                    SQLiteCommand cmd3 = cn.CreateCommand();
                    cmd3.CommandText = "SELECT count(*) FROM SiteNameTable WHERE SiteURL is (@SiteURL)";
                    cmd3.Parameters.Add("SiteURL", System.Data.DbType.String);
                    cmd3.Parameters["SiteURL"].Value = siteURL;
                    using (SQLiteDataReader reader3 = cmd3.ExecuteReader())
                    {
                        while (reader3.Read())
                        {
                            count = int.Parse(reader3["count(*)"].ToString());
                            if (count >= 1)
                            {
                                return SameURLExist;
                            }
                        }
                    }

                    return NameURLNotMatch;
                }
            }
        }

        private int getSiteNameTable_Id(string siteName, string siteURL)
        {
            int siteNameTable_Id = -1;
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT Id FROM SiteNameTable WHERE SiteName is (@SiteName) and SiteURL is (@SiteURL)";
                    cmd.Parameters.Add("SiteName", System.Data.DbType.String);
                    cmd.Parameters.Add("SiteURL", System.Data.DbType.String);
                    cmd.Parameters["SiteName"].Value = siteName;
                    cmd.Parameters["SiteURL"].Value = siteURL;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sid = reader["Id"].ToString();
                            siteNameTable_Id = int.Parse(sid);
                        }
                    }
                }
            }
            return siteNameTable_Id;
        }

        private int getSearchWordTable_Id(int siteNameTable_Id, string searchWord)
        {
            int searchWordTable_Id = -1;
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT Id FROM SearchWordTable WHERE SiteNameTable_Id is (@SiteNameTable_Id) and SearchWord is (@SearchWord)";
                    cmd.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id;
                    cmd.Parameters["SearchWord"].Value = searchWord;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sid = reader["Id"].ToString();
                            searchWordTable_Id = int.Parse(sid);
                        }
                    }
                }
            }

            return searchWordTable_Id;
        }
        #endregion

        #region Start Rank Check
        private void BackgroundRankCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = (BackgroundWorker)sender;

            //Get loop count
            int maxLoops = (int)e.Argument;
            int i = 0;
            try
            {
                //int googleCount = -1, yahooCount = -1;
                //getOptions(ref googleCount, ref yahooCount);
                for (i = 0; i < maxLoops; i++)
                {
                    //Is cancelled
                    if (bgWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    //Progress
                    bgWorker.ReportProgress(i + 1);


                    DateTime dtToday = DateTime.Today;
                    String siteName = (String)dataGridView1.Rows[i].Cells[0].Value;
                    String siteURL = "http://" + (String)dataGridView1.Rows[i].Cells[1].Value;
                    String kensakuword = (String)dataGridView1.Rows[i].Cells[2].Value;

                    int siteNameTable_Id = getSiteNameTable_Id(siteName, (String)dataGridView1.Rows[i].Cells[1].Value);
                    int searchWordTable_Id = getSearchWordTable_Id(siteNameTable_Id, kensakuword);
                    int dateTable_Id = getDateTable_Id(dtToday);
                    if (dateTable_Id != -1)
                    {
                        int searchWordPositionTable_Id = getSearchWordPositionTable_Id(searchWordTable_Id, dateTable_Id);
                        if (searchWordPositionTable_Id != -1)
                        {
                            dataGridView1.Rows[i].Cells[12].Value = "";
                            dataGridView1.Rows[i].Cells[12].Value = "Skipped";

                            continue;
                        }
                    }

                    dataGridView1.Rows[i].Cells[12].Value = "Checking";
                    // Search Google
                    String res = Search.Helper.GetGoogleSearchResultHtlm(kensakuword);
                    String keywordPosition = "-";
                    String searchVolumn = null;
                    String googleURL = null;
                    String googleTitle = null;

                    List<String> result = Search.Helper.ParseGoogleSearchResultHtml(res, siteURL, ref keywordPosition, ref searchVolumn, ref googleURL, ref googleTitle);
                    String googlePosition = keywordPosition;
                    ChangeStatus googleChangeStatus = ChangeStatus.RANK_STABLE;
                    String googleChangeText = createChangeString(keywordPosition, dataGridView1.Rows[i].Cells[3].Value, ref googleChangeStatus);
                    String googleVolumn = searchVolumn;
                    setSellOnTopTab(i, 3, keywordPosition, googleChangeText, searchVolumn, googleChangeStatus);

                    // Search Yahoo
                    //
                    // Not supported now
                    /*
                    res = Search.Helper.GetYahooSearchResultHtlm(kensakuword);
                    keywordPosition = "-";
                    searchVolumn = null;
                    String yahooURL = null;
                    String yahooTitle = null;
                    result = Search.Helper.ParseYahooSearchResultHtml(res, siteURL, ref keywordPosition, ref searchVolumn, ref yahooURL, ref yahooTitle);
                    String yahooPosition = keywordPosition;
                    ChangeStatus yahooChangeStatus = ChangeStatus.RANK_STABLE;
                    String yahooChangeText = createChangeString(keywordPosition, dataGridView1.Rows[i].Cells[6].Value, ref yahooChangeStatus);
                    String yahooVolumn = searchVolumn;
                    setSellOnTopTab(i, 6, keywordPosition, yahooChangeText, searchVolumn, yahooChangeStatus);
                    */
                    String yahooURL = null;
                    String yahooTitle = null;
                    String yahooPosition = null;
                    String yahooVolumn = null;
                    String yahooChangeText = null;
                    ChangeStatus yahooChangeStatus = ChangeStatus.RANK_STABLE;

                    // Search Bing
                    res = Search.Helper.GetBingSearchResultHtlm(kensakuword);
                    keywordPosition = "-";
                    searchVolumn = null;
                    String bingURL = null;
                    String bingTitle = null;
                    result = Search.Helper.ParseBingSearchResultHtml(res, siteURL, ref keywordPosition, ref searchVolumn, ref bingURL, ref bingTitle);
                    String bingPosition = keywordPosition;
                    ChangeStatus bingChangeStatus = ChangeStatus.RANK_STABLE;
                    String bingChangeText = createChangeString(keywordPosition, dataGridView1.Rows[i].Cells[9].Value, ref bingChangeStatus);
                    String bingVolumn = searchVolumn;
                    setSellOnTopTab(i, 9, keywordPosition, bingChangeText, searchVolumn, bingChangeStatus);

                    dataGridView1.Rows[i].Cells[12].Value = "Done";


                    if (dateTable_Id == -1)
                    {
                        saveDateTable(dtToday);
                        dateTable_Id = getDateTable_Id(dtToday);

                        addDateColumnOnHistoryTab(dtToday);

                    }
                    saveSearchWordPositionTable(searchWordTable_Id, dateTable_Id, googlePosition, googleVolumn,
                        yahooPosition, yahooVolumn, bingPosition, bingVolumn);
                    saveSearchResultDetailTable(searchWordTable_Id, dateTable_Id, googleURL, googleTitle, googleChangeText, googleChangeStatus,
                        yahooURL, yahooTitle, yahooChangeText, yahooChangeStatus, bingURL, bingTitle, bingChangeText, bingChangeStatus);

                    dataGridView2.Rows[i].Cells[3].Value = googlePosition;
                    dataGridView3.Rows[i].Cells[3].Value = yahooPosition;
                    dataGridView4.Rows[i].Cells[3].Value = bingPosition;

                    dataGridView5.Rows[i].Cells[3].Value = googlePosition;
                    dataGridView5.Rows[i].Cells[4].Value = googleURL;
                    dataGridView5.Rows[i].Cells[5].Value = googleTitle;

                    dataGridView6.Rows[i].Cells[3].Value = yahooPosition;
                    dataGridView6.Rows[i].Cells[4].Value = yahooURL;
                    dataGridView6.Rows[i].Cells[5].Value = yahooTitle;

                    dataGridView7.Rows[i].Cells[3].Value = bingPosition;
                    dataGridView7.Rows[i].Cells[4].Value = bingURL;
                    dataGridView7.Rows[i].Cells[5].Value = bingTitle;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message + Environment.NewLine + "Cannot search Google more than 50 at once." + Environment.NewLine + "Try one hour later.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //e.Result = maxLoops;
            e.Result = i;
        }


        private void setSellOnTopTab(int row, int column, string keywordPosition, string changeText, string searchVolumn, ChangeStatus rankStatus)
        {
            dataGridView1.Rows[row].Cells[column].Value = keywordPosition;
            dataGridView1.Rows[row].Cells[column + 1].Value = changeText;
            if (rankStatus == ChangeStatus.RANK_UP)
            {
                dataGridView1.Rows[row].Cells[column + 1].Style.ForeColor = Color.Blue;
            }
            else if (rankStatus == ChangeStatus.RANK_DOWN)
            {
                dataGridView1.Rows[row].Cells[column + 1].Style.ForeColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[row].Cells[column + 1].Style.ForeColor = Color.Black;
            }

            if (searchVolumn == null)
            {
                dataGridView1.Rows[row].Cells[column + 2].Value = "";
            }
            else
            {
                dataGridView1.Rows[row].Cells[column + 2].Value = searchVolumn;
            }
        }

        private void saveDateTable(DateTime dtToday)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "INSERT INTO DateTable (CheckDate) VALUES (@CheckDate)";
                    cmd.Parameters.Add("CheckDate", System.Data.DbType.Date);
                    cmd.Parameters["CheckDate"].Value = dtToday;
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
            }
        }

        private int getDateTable_Id(DateTime dtToday)
        {
            int dateTable_Id = -1;

            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT Id FROM DateTable WHERE CheckDate is (@CheckDate)";
                    cmd.Parameters.Add("CheckDate", System.Data.DbType.Date);
                    cmd.Parameters["CheckDate"].Value = dtToday;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sid = reader["Id"].ToString();
                            dateTable_Id = int.Parse(sid);
                        }
                    }
                }
            }

            return dateTable_Id;
        }

        private int getSearchWordPositionTable_Id(int searchWordTable_Id, int dateTable_Id)
        {
            int searchWordPositionTable_Id = -1;

            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT Id FROM SearchWordPositionTable WHERE SearchWordTable_Id is (@SearchWordTable_Id) and DateTable_Id is (@DateTable_Id)";
                    cmd.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters.Add("DateTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                    cmd.Parameters["DateTable_Id"].Value = dateTable_Id;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sid = reader["Id"].ToString();
                            searchWordPositionTable_Id = int.Parse(sid);
                        }
                    }
                }
            }

            return searchWordPositionTable_Id;
        }

        private void saveSearchWordPositionTable(int searchWordTable_Id, int dateTable_Id, string googlePosition, string googleVolumn,
            string yahooPosition, string yahooVolumn, string bingPosition, string bingVolumn)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "INSERT INTO SearchWordPositionTable (SearchWordTable_Id, DateTable_Id, GooglePosition, GoogleVolumn, YahooPosition, YahooVolumn, BingPosition, BingVolumn) VALUES (@SearchWordTable_Id, @DateTable_Id, @GooglePosition, @GoogleVolumn, @YahooPosition, @YahooVolumn, @BingPosition, @BingVolumn)";
                    cmd.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters.Add("DateTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters.Add("GooglePosition", System.Data.DbType.String);
                    cmd.Parameters.Add("GoogleVolumn", System.Data.DbType.String);
                    cmd.Parameters.Add("YahooPosition", System.Data.DbType.String);
                    cmd.Parameters.Add("YahooVolumn", System.Data.DbType.String);
                    cmd.Parameters.Add("BingPosition", System.Data.DbType.String);
                    cmd.Parameters.Add("BingVolumn", System.Data.DbType.String);
                    cmd.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                    cmd.Parameters["DateTable_Id"].Value = dateTable_Id;
                    cmd.Parameters["GooglePosition"].Value = googlePosition;
                    cmd.Parameters["GoogleVolumn"].Value = googleVolumn;
                    cmd.Parameters["YahooPosition"].Value = yahooPosition;
                    cmd.Parameters["YahooVolumn"].Value = yahooVolumn;
                    cmd.Parameters["BingPosition"].Value = bingPosition;
                    cmd.Parameters["BingVolumn"].Value = bingVolumn;
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
            }
        }

        private void saveSearchResultDetailTable(int searchWordTable_Id, int dateTable_Id, string googleURL, string googleTitle, string googleChangeText, ChangeStatus googleChangeStatus,
            string yahooURL, string yahooTitle, string yahooChangeText, ChangeStatus yahooChangeStatus, string bingURL, string bingTitle, string bingChangeText, ChangeStatus bingChangeStatus)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT SearchWordTable_Id FROM SearchResultDetailTable WHERE SearchWordTable_Id is (@SearchWordTable_Id)";
                    cmd.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                    String sId = null;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sId = reader["SearchWordTable_Id"].ToString();
                        }
                    }
                    if (sId != null)
                    {
                        cmd.CommandText = "UPDATE SearchResultDetailTable SET SearchWordTable_Id=(@SearchWordTable_Id), DateTable_Id=(@DateTable_Id), GoogleURL=(@GoogleURL), GoogleTitle=(@GoogleTitle), GoogleChangeText=(@GoogleChangeText), GoogleChangeStatus=(@GoogleChangeStatus), YahooURL=(@YahooURL), YahooTitle=(@YahooTitle), YahooChangeText=(@YahooChangeText), YahooChangeStatus=(@YahooChangeStatus), BingURL=(@BingURL), BingTitle=(@BingTitle), BingChangeText=(@BingChangeText), BingChangeStatus=(@BingChangeStatus) WHERE SearchWordTable_Id is (@SearchWordTable_Id)";
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO SearchResultDetailTable (SearchWordTable_Id, DateTable_Id, GoogleURL, GoogleTitle, GoogleChangeText, GoogleChangeStatus, YahooURL, YahooTitle, YahooChangeText, YahooChangeStatus, BingURL, BingTitle, BingChangeText, BingChangeStatus) VALUES (@SearchWordTable_Id, @DateTable_Id, @GoogleURL, @GoogleTitle, @GoogleChangeText, @GoogleChangeStatus, @YahooURL, @YahooTitle, @YahooChangeText, @YahooChangeStatus, @BingURL, @BingTitle, @BingChangeText, @BingChangeStatus)";
                    }
                    cmd.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters.Add("DateTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters.Add("GoogleURL", System.Data.DbType.String);
                    cmd.Parameters.Add("GoogleTitle", System.Data.DbType.String);
                    cmd.Parameters.Add("GoogleChangeText", System.Data.DbType.String);
                    cmd.Parameters.Add("GoogleChangeStatus", System.Data.DbType.Int64);
                    cmd.Parameters.Add("YahooURL", System.Data.DbType.String);
                    cmd.Parameters.Add("YahooTitle", System.Data.DbType.String);
                    cmd.Parameters.Add("YahooChangeText", System.Data.DbType.String);
                    cmd.Parameters.Add("YahooChangeStatus", System.Data.DbType.Int64);
                    cmd.Parameters.Add("BingURL", System.Data.DbType.String);
                    cmd.Parameters.Add("BingTitle", System.Data.DbType.String);
                    cmd.Parameters.Add("BingChangeText", System.Data.DbType.String);
                    cmd.Parameters.Add("BingChangeStatus", System.Data.DbType.Int64);
                    cmd.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                    cmd.Parameters["DateTable_Id"].Value = dateTable_Id;
                    cmd.Parameters["GoogleURL"].Value = googleURL;
                    cmd.Parameters["GoogleTitle"].Value = googleTitle;
                    cmd.Parameters["GoogleChangeText"].Value = googleChangeText;
                    cmd.Parameters["GoogleChangeStatus"].Value = googleChangeStatus;
                    cmd.Parameters["YahooURL"].Value = yahooURL;
                    cmd.Parameters["YahooTitle"].Value = yahooTitle;
                    cmd.Parameters["YahooChangeText"].Value = yahooChangeText;
                    cmd.Parameters["YahooChangeStatus"].Value = yahooChangeStatus;
                    cmd.Parameters["BingURL"].Value = bingURL;
                    cmd.Parameters["BingTitle"].Value = bingTitle;
                    cmd.Parameters["BingChangeText"].Value = bingChangeText;
                    cmd.Parameters["BingChangeStatus"].Value = bingChangeStatus;
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
            }
        }

        private string createChangeString(string newPosition, object currentPosition, ref ChangeStatus changeStatus)
        {
            String changeString = null;
            int newInt, currentInt, changeInt = 0;

            if (newPosition == null)
            {
                newPosition = "n/a";
            }
            if (newPosition == "n/a" || newPosition == "-")
            {
                newInt = -1;
            }
            else
            {
                newInt = int.Parse(newPosition);
            }

            if (currentPosition == null)
            {
                currentPosition = "n/a";
            }
            if (currentPosition.ToString() == "n/a" || currentPosition.ToString() == "-")
            {
                currentInt = -1;
            }
            else
            {
                currentInt = Convert.ToInt32(currentPosition);
            }

            if (newInt != -1 && currentInt != -1)
            {
                changeInt = newInt - currentInt;
            }
            if (newInt == -1 && currentInt == -1)
            {
                changeInt = 0;
            }
            if (newInt == -1 && currentInt != -1)
            {
                changeInt = 1;
            }
            if (newInt != -1 && currentInt == -1)
            {
                changeInt = -1;
            }


            if (changeInt > 0)
            {
                changeString = currentPosition.ToString() + " ↘ " + newPosition;
                changeStatus = ChangeStatus.RANK_DOWN;
            }
            else if (changeInt == 0)
            {
                changeString = currentPosition.ToString() + " → " + newPosition;
                changeStatus = ChangeStatus.RANK_STABLE;
            }
            else
            {
                changeString = currentPosition.ToString() + " ↗ " + newPosition;
                changeStatus = ChangeStatus.RANK_UP;
            }

            return changeString;
        }

        private void BackgroundRankCheck_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = "Checking  " + e.ProgressPercentage.ToString() + "/" + toolStripProgressBar1.Maximum;
        }

        private void BackgroundRankCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                toolStripStatusLabel1.Text = dataGridView1.Rows.Count + " items. " + "Error:" + e.Error.Message;
            }
            else if (e.Cancelled)
            {
                toolStripStatusLabel1.Text = dataGridView1.Rows.Count + " items. " + "Cancelled.";
            }
            else
            {
                int result = (int)e.Result;
                toolStripStatusLabel1.Text = dataGridView1.Rows.Count + " items. " + result  + " items checked.";
            }

            fileToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            browseToolStripMenuItem.Enabled = true;
            optionToolStripMenuItem.Enabled = true;
            helpToolStripMenuItem.Enabled = true;

            runWiseToolStripMenuItem.Enabled = true;
            cancelWiseToolStripMenuItem.Enabled = false;
            toolStripProgressBar1.Visible = false;
        }

        private void execSEORankRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backgroundRankCheck.IsBusy)
            {
                return;
            }

            fileToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Enabled = false;
            browseToolStripMenuItem.Enabled = false;
            optionToolStripMenuItem.Enabled = false;
            helpToolStripMenuItem.Enabled = false;

            runWiseToolStripMenuItem.Enabled = false;
            cancelWiseToolStripMenuItem.Enabled = true;

            //Initialize contorl
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = dataGridView1.Rows.Count;
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel1.Text = "0";

            backgroundRankCheck.WorkerReportsProgress = true;
            backgroundRankCheck.WorkerSupportsCancellation = true;
            backgroundRankCheck.RunWorkerAsync(dataGridView1.Rows.Count);
        }

        void InsertColumn2(DateTime dtToday)
        {
            int column = 3;
            DataGridViewTextBoxColumn textColumn2 = new DataGridViewTextBoxColumn();
            textColumn2.DataPropertyName = "Date1";
            textColumn2.Name = "Column1";
            textColumn2.Width = 40;
            textColumn2.HeaderText = dtToday.Month.ToString() + "/" + dtToday.Day.ToString();

            dataGridView2.Columns.Insert(column, textColumn2);
        }
        void InsertColumn3(DateTime dtToday)
        {
            int column = 3;
            DataGridViewTextBoxColumn textColumn3 = new DataGridViewTextBoxColumn();
            textColumn3.DataPropertyName = "Date1";
            textColumn3.Name = "Column1";
            textColumn3.Width = 40;
            textColumn3.HeaderText = dtToday.Month.ToString() + "/" + dtToday.Day.ToString();

            dataGridView3.Columns.Insert(column, textColumn3);
        }
        void InsertColumn4(DateTime dtToday)
        {
            int column = 3;
            DataGridViewTextBoxColumn textColumn4 = new DataGridViewTextBoxColumn();
            textColumn4.DataPropertyName = "Date1";
            textColumn4.Name = "Column1";
            textColumn4.Width = 40;
            textColumn4.HeaderText = dtToday.Month.ToString() + "/" + dtToday.Day.ToString();

            dataGridView4.Columns.Insert(column, textColumn4);
        }

        delegate void delegate1(DateTime dtToday);
        private void addDateColumnOnHistoryTab(DateTime dtToday)
        {
            Invoke(new delegate1(InsertColumn2), dtToday);
            Invoke(new delegate1(InsertColumn3), dtToday);
            Invoke(new delegate1(InsertColumn4), dtToday);
        }
        #endregion

        #region Cancel Rank Check
        private void cancelWISEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cancelWiseToolStripMenuItem.Enabled = false;
            backgroundRankCheck.CancelAsync();
        }
        #endregion

        #region Exit
        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Add Item
        private void itemNewAddAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemForm f = new AddItemForm(this);
            f.ShowDialog(this);
        }

        internal int addNewItem(String siteName, String siteURL, String searchWords)
        {

            StringReader sr = new StringReader(searchWords);
            String searchWord;

            int rtn = checkSiteNameURL(siteName, siteURL);
            switch (rtn)
            {
                case SameNameExist:
                    return SameNameExist;
                case SameURLExist:
                    return SameURLExist;
                case NameURLMatch:
                    // Add search word in exisiting site name and site URL
                    while ((searchWord = sr.ReadLine()) != null)
                    {
                        int siteNameTalbe_Id = getSiteNameTable_Id(siteName, siteURL);
                        if (searchWord.Length == 0)
                        {
                            continue;
                        }
                        if (addSearchWordTable(siteNameTalbe_Id, searchWord))
                        {
                            dataGridView1.Rows.Add(siteName, siteURL, searchWord);
                            toolStripStatusLabel1.Text = dataGridView1.Rows.Count + " items. ";
                            dataGridView2.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView3.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView4.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView5.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView6.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView7.Rows.Add(siteName, siteURL, searchWord);
                        }
                    }
                    break;
                case NameURLNotMatch:
                    while ((searchWord = sr.ReadLine()) != null)
                    {
                        int siteNameTalbe_Id = getSiteNameTable_Id(siteName, siteURL);
                        if (siteNameTalbe_Id == -1)
                        {
                            addSiteNameTable(siteName, siteURL);
                            siteNameTalbe_Id = getSiteNameTable_Id(siteName, siteURL);
                        }
                        if (addSearchWordTable(siteNameTalbe_Id, searchWord))
                        {
                            dataGridView1.Rows.Add(siteName, siteURL, searchWord);
                            toolStripStatusLabel1.Text = dataGridView1.Rows.Count + " items. ";
                            dataGridView2.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView3.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView4.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView5.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView6.Rows.Add(siteName, siteURL, searchWord);
                            dataGridView7.Rows.Add(siteName, siteURL, searchWord);
                        }
                    }
                    break;
            }

            dataGridView1.CurrentCell = null;
            return SUCCESS;
        }

        private void addSiteNameTable(string siteName, string siteURL)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "INSERT INTO SiteNameTable (SiteName, SiteURL) VALUES (@SiteName, @SiteURL)";
                    cmd.Parameters.Add("SiteName", System.Data.DbType.String);
                    cmd.Parameters.Add("SiteURL", System.Data.DbType.String);
                    cmd.Parameters["SiteName"].Value = siteName;
                    cmd.Parameters["SiteURL"].Value = siteURL;
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
            }
        }

        private Boolean addSearchWordTable(int siteNameTable_Id, string searchWord)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT count(*) FROM SearchWordTable WHERE SiteNameTable_Id is (@SiteNameTable_Id) and SearchWord is (@SearchWord)";
                    cmd.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id;
                    cmd.Parameters["SearchWord"].Value = searchWord;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int count = int.Parse(reader["count(*)"].ToString());
                            if (count >= 1)
                            {
                                return false;
                            }
                        }
                    }


                    SQLiteCommand cmd2 = cn.CreateCommand();
                    cmd2.CommandText = "INSERT INTO SearchWordTable (SearchWordTableOrder_Id, SiteNameTable_Id, SearchWord) VALUES ((select max(searchwordTableorder_Id)+1 from searchwordTable), @SiteNameTable_Id, @SearchWord)";
                    cmd2.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd2.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd2.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id;
                    cmd2.Parameters["SearchWord"].Value = searchWord;
                    cmd2.ExecuteNonQuery();

                    trans.Commit();
                }
            }
            return true;
        }

        internal void AddItemForm(ComboBox siteNameComboBox, ComboBox siteURLComboBox)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT SiteName, SiteURL FROM SiteNameTable";
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string siteName = reader["SiteName"].ToString();
                            string siteURL = reader["SiteURL"].ToString();

                            siteNameComboBox.Items.Add(siteName);
                            siteURLComboBox.Items.Add("http://" + siteURL);
                        }
                    }
                }
            }
        }
        #endregion

        # region Edit Item
        private void itemEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView dataGridView;
                dataGridView = getFocusDataGridView();
                if (dataGridView == null)
                {
                    return;
                }

                int y = dataGridView.CurrentCellAddress.Y;
                String siteName = Convert.ToString(dataGridView.Rows[y].Cells[0].Value);
                String siteURL = Convert.ToString(dataGridView.Rows[y].Cells[1].Value);
                String searchWord = Convert.ToString(dataGridView.Rows[y].Cells[2].Value);

                EditItemForm f = new EditItemForm(this, siteName, siteURL, searchWord);
                f.ShowDialog(this);
            }
            catch (Exception)
            {
            }
        }

        private DataGridView getFocusDataGridView()
        {
            int x = tabControl1.SelectedIndex;
            switch (x)
            {
                case 0:
                    return dataGridView1;
                case 1:
                    return dataGridView2;
                case 2:
                    return dataGridView3;
                case 3:
                    return dataGridView4;
                case 4:
                    return dataGridView5;
                case 5:
                    return dataGridView6;
                case 6:
                    return dataGridView7;
            }
            return null;
        }

        internal int changeSiteName(String siteName, String siteURL)
        {
            int rtn = checkSiteNameURL(siteName, siteURL);
            if (rtn == NameURLMatch || rtn == SameNameExist)
            {
                return rtn;
            }
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "UPDATE SiteNameTable Set SiteName = (@SiteName) WHERE SiteURL is (@SiteURL)";
                    cmd.Parameters.Add("SiteName", System.Data.DbType.String);
                    cmd.Parameters.Add("SiteURL", System.Data.DbType.String);
                    cmd.Parameters["SiteName"].Value = siteName;
                    cmd.Parameters["SiteURL"].Value = siteURL;
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1[1, i].Value.Equals(siteURL))
                {
                    dataGridView1[0, i].Value = siteName;
                    dataGridView2[0, i].Value = siteName;
                    dataGridView3[0, i].Value = siteName;
                    dataGridView4[0, i].Value = siteName;
                    dataGridView5[0, i].Value = siteName;
                    dataGridView6[0, i].Value = siteName;
                    dataGridView7[0, i].Value = siteName;
                }
            }

            return SUCCESS;
        }
        #endregion

        # region Delete Item
        private void removeSearchWord(int siteNameTable_Id, int searchWordTable_Id)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "DELETE FROM SearchWordTable WHERE Id is (@Id)";
                    cmd.Parameters.Add("Id", System.Data.DbType.Int64);
                    cmd.Parameters["Id"].Value = searchWordTable_Id;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM SearchWordPositionTable WHERE SearchWordTable_Id is (@SearchWordTable_Id)";
                    cmd.Parameters.Add("SearchWordTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SearchWordTable_Id"].Value = searchWordTable_Id;
                    cmd.ExecuteNonQuery();


                    SQLiteCommand cmd2 = cn.CreateCommand();
                    cmd2.CommandText = "SELECT count(*) FROM SearchWordTable WHERE SiteNameTable_Id is (@SiteNameTable_Id)";
                    cmd2.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd2.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id;
                    using (SQLiteDataReader reader = cmd2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int count = int.Parse(reader["count(*)"].ToString());

                            if (count == 0)
                            {
                                SQLiteCommand cmd3 = cn.CreateCommand();
                                cmd3.CommandText = "DELETE FROM SiteNameTable WHERE Id is (@Id)";
                                cmd3.Parameters.Add("Id", System.Data.DbType.Int64);
                                cmd3.Parameters["Id"].Value = siteNameTable_Id;
                                cmd3.ExecuteNonQuery();

                            }
                        }
                    }

                    trans.Commit();
                }
            }
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            itemDeleteToolStripMenuItem_Click(sender, e);
            e.Cancel = true;
        }
        private void dataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            itemDeleteToolStripMenuItem_Click(sender, e);
            e.Cancel = true;
        }

        private void dataGridView3_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            itemDeleteToolStripMenuItem_Click(sender, e);
            e.Cancel = true;
        }

        private void dataGridView4_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            itemDeleteToolStripMenuItem_Click(sender, e);
            e.Cancel = true;
        }

        private void dataGridView5_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            itemDeleteToolStripMenuItem_Click(sender, e);
            e.Cancel = true;
        }

        private void dataGridView6_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            itemDeleteToolStripMenuItem_Click(sender, e);
            e.Cancel = true;
        }

        private void dataGridView7_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            itemDeleteToolStripMenuItem_Click(sender, e);
            e.Cancel = true;
        }

        private void itemDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView dataGridView;
                dataGridView = getFocusDataGridView();
                if (dataGridView == null)
                {
                    return;
                }

                int y = dataGridView.CurrentCellAddress.Y;
                String siteName = Convert.ToString(dataGridView.Rows[y].Cells[0].Value);
                String siteURL = Convert.ToString(dataGridView.Rows[y].Cells[1].Value);
                String searchWord = Convert.ToString(dataGridView.Rows[y].Cells[2].Value);


                DialogResult result = MessageBox.Show(Wise.Properties.Resources.MessageBoxDeleteItem,
                    "MessageBox", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    int siteNameTable_Id = getSiteNameTable_Id(siteName, siteURL);
                    int searchWordTable_Id = getSearchWordTable_Id(siteNameTable_Id, searchWord);
                    removeSearchWord(siteNameTable_Id, searchWordTable_Id);

                    dataGridView1.Rows.RemoveAt(y);
                    toolStripStatusLabel1.Text = dataGridView1.Rows.Count + " items. ";
                    dataGridView2.Rows.RemoveAt(y);
                    dataGridView3.Rows.RemoveAt(y);
                    dataGridView4.Rows.RemoveAt(y);
                    dataGridView5.Rows.RemoveAt(y);
                    dataGridView6.Rows.RemoveAt(y);
                    dataGridView7.Rows.RemoveAt(y);
                }
                else
                {

                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region One Up
        private void oneUpUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView dataGridView;
                dataGridView = getFocusDataGridView();
                if (dataGridView == null) // Not selected
                {
                    return;
                }

                int y2 = dataGridView.CurrentCellAddress.Y;
                if (y2 == 0) // Top most
                {
                    return;
                }
                String siteName2 = Convert.ToString(dataGridView.Rows[y2].Cells[0].Value);
                String siteURL2 = Convert.ToString(dataGridView.Rows[y2].Cells[1].Value);
                String searchWord2 = Convert.ToString(dataGridView.Rows[y2].Cells[2].Value);
                int siteNameTable_Id2 = getSiteNameTable_Id(siteName2, siteURL2);

                int y1 = dataGridView.CurrentCellAddress.Y - 1;
                String siteName1 = Convert.ToString(dataGridView.Rows[y1].Cells[0].Value);
                String siteURL1 = Convert.ToString(dataGridView.Rows[y1].Cells[1].Value);
                String searchWord1 = Convert.ToString(dataGridView.Rows[y1].Cells[2].Value);
                int siteNameTable_Id1 = getSiteNameTable_Id(siteName1, siteURL1);

                exchangeSearchWordTableOrder_Id(siteNameTable_Id1, searchWord1, siteNameTable_Id2, searchWord2);

                exchangeRowU(dataGridView1, y1, y2);
                exchangeRowU(dataGridView2, y1, y2);
                exchangeRowU(dataGridView3, y1, y2);
                exchangeRowU(dataGridView4, y1, y2);
                exchangeRowU(dataGridView5, y1, y2);
                exchangeRowU(dataGridView6, y1, y2);
                exchangeRowU(dataGridView7, y1, y2);

            }
            catch (Exception)
            {

            }
        }

        private void exchangeSearchWordTableOrder_Id(int siteNameTable_Id1, string searchWord1, int siteNameTable_Id2, string searchWord2)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    cmd.CommandText = "SELECT SearchWordTableOrder_Id FROM SearchWordTable WHERE SiteNameTable_Id is (@SiteNameTable_Id) and SearchWord is (@SearchWord)";
                    cmd.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id1;
                    cmd.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd.Parameters["SearchWord"].Value = searchWord1;
                    int searchWordTableOrder_Id1 = -1;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            searchWordTableOrder_Id1 = int.Parse(reader["SearchWordTableOrder_Id"].ToString());
                        }
                    }

                    cmd.CommandText = "SELECT SearchWordTableOrder_Id FROM SearchWordTable WHERE SiteNameTable_Id is (@SiteNameTable_Id) and SearchWord is (@SearchWord)";
                    cmd.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id2;
                    cmd.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd.Parameters["SearchWord"].Value = searchWord2;
                    int searchWordTableOrder_Id2 = -1;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            searchWordTableOrder_Id2 = int.Parse(reader["SearchWordTableOrder_Id"].ToString());
                        }
                    }

                    if (searchWordTableOrder_Id1 == -1 || searchWordTableOrder_Id2 == -1)
                    {
                        return;
                    }

                    // Set Null
                    cmd.CommandText = "UPDATE SearchWordTable SET SearchWordTableOrder_Id=(@SearchWordTableOrder_Id) WHERE SiteNameTable_Id is (@SiteNameTable_Id) and SearchWord is (@SearchWord)";
                    cmd.Parameters.Add("SearchWordTableOrder_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SearchWordTableOrder_Id"].Value = null;
                    cmd.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id1;
                    cmd.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd.Parameters["SearchWord"].Value = searchWord1;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "UPDATE SearchWordTable SET SearchWordTableOrder_Id=(@SearchWordTableOrder_Id) WHERE SiteNameTable_Id is (@SiteNameTable_Id) and SearchWord is (@SearchWord)";
                    cmd.Parameters.Add("SearchWordTableOrder_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SearchWordTableOrder_Id"].Value = null;
                    cmd.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id2;
                    cmd.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd.Parameters["SearchWord"].Value = searchWord2;
                    cmd.ExecuteNonQuery();

                    // Exchange searchWordTableOrder_Id1 and searchWordTableOrder_Id2
                    cmd.CommandText = "UPDATE SearchWordTable SET SearchWordTableOrder_Id=(@SearchWordTableOrder_Id) WHERE SiteNameTable_Id is (@SiteNameTable_Id) and SearchWord is (@SearchWord)";
                    cmd.Parameters.Add("SearchWordTableOrder_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SearchWordTableOrder_Id"].Value = searchWordTableOrder_Id2;
                    cmd.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id1;
                    cmd.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd.Parameters["SearchWord"].Value = searchWord1;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "UPDATE SearchWordTable SET SearchWordTableOrder_Id=(@SearchWordTableOrder_Id) WHERE SiteNameTable_Id is (@SiteNameTable_Id) and SearchWord is (@SearchWord)";
                    cmd.Parameters.Add("SearchWordTableOrder_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SearchWordTableOrder_Id"].Value = searchWordTableOrder_Id1;
                    cmd.Parameters.Add("SiteNameTable_Id", System.Data.DbType.Int64);
                    cmd.Parameters["SiteNameTable_Id"].Value = siteNameTable_Id2;
                    cmd.Parameters.Add("SearchWord", System.Data.DbType.String);
                    cmd.Parameters["SearchWord"].Value = searchWord2;
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
            }
        }

        private void exchangeRowU(DataGridView dataGridView, int y1, int y2)
        {
            DataGridViewRowCollection rows = dataGridView.Rows;
            DataGridViewRow row = rows[y1];
            rows.Remove(row);
            rows.Insert(y2, row);

        }

        #endregion

        #region One Down
        private void oneDownNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView dataGridView;
                dataGridView = getFocusDataGridView();
                if (dataGridView == null) // Not selected
                {
                    return;
                }

                int y1 = dataGridView.CurrentCellAddress.Y;
                if (y1 >= (dataGridView.Rows.Count - 1))
                {
                    return;
                }

                String siteName1 = Convert.ToString(dataGridView.Rows[y1].Cells[0].Value);
                String siteURL1 = Convert.ToString(dataGridView.Rows[y1].Cells[1].Value);
                String searchWord1 = Convert.ToString(dataGridView.Rows[y1].Cells[2].Value);
                int siteNameTable_Id1 = getSiteNameTable_Id(siteName1, siteURL1);

                int y2 = dataGridView.CurrentCellAddress.Y + 1;
                String siteName2 = Convert.ToString(dataGridView.Rows[y2].Cells[0].Value);
                String siteURL2 = Convert.ToString(dataGridView.Rows[y2].Cells[1].Value);
                String searchWord2 = Convert.ToString(dataGridView.Rows[y2].Cells[2].Value);
                int siteNameTable_Id2 = getSiteNameTable_Id(siteName2, siteURL2);

                exchangeSearchWordTableOrder_Id(siteNameTable_Id1, searchWord1, siteNameTable_Id2, searchWord2);

                exchangeRowD(dataGridView1, y1, y2);
                exchangeRowD(dataGridView2, y1, y2);
                exchangeRowD(dataGridView3, y1, y2);
                exchangeRowD(dataGridView4, y1, y2);
                exchangeRowD(dataGridView5, y1, y2);
                exchangeRowD(dataGridView6, y1, y2);
                exchangeRowD(dataGridView7, y1, y2);
            }
            catch (Exception)
            {

            }
        }

        private void exchangeRowD(DataGridView dataGridView, int y1, int y2)
        {
            DataGridViewRowCollection rows = dataGridView.Rows;
            DataGridViewRow row = rows[y2];
            rows.Remove(row);
            rows.Insert(y1, row);
        }
        #endregion

        #region About
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.ShowDialog(this);
        }
        #endregion

        #region CSV
        private void exportToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportCSVForm f = new ExportCSVForm(dbConnectionString);
            f.ShowDialog(this);
        }
        #endregion

        #region Browse
        // Open URL with default browser
        private void openOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                String siteURL = "http://" + (String)dataGridView1.Rows[r.Index].Cells[1].Value;
                System.Diagnostics.Process.Start(siteURL);
            }
        }


        // Search in Google
        private void googleSearchGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView;
            dataGridView = getFocusDataGridView();
            if (dataGridView == null)
            {
                return;
            }

            foreach (DataGridViewRow r in dataGridView.SelectedRows)
            {
                String kensakuword = (String)dataGridView.Rows[r.Index].Cells[2].Value;
                System.Diagnostics.Process.Start("http://www.google.co.jp/#q=" + kensakuword);
            }
        }

        // Search in Yahoo
        private void yahooSearchYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView;
            dataGridView = getFocusDataGridView();
            if (dataGridView == null)
            {
                return;
            }

            foreach (DataGridViewRow r in dataGridView.SelectedRows)
            {
                String kensakuword = (String)dataGridView.Rows[r.Index].Cells[2].Value;
                System.Diagnostics.Process.Start("http://search.yahoo.co.jp/search?p=" + kensakuword);
            }
        }

        // Search in Bing
        private void bingSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView;
            dataGridView = getFocusDataGridView();
            if (dataGridView == null)
            {
                return;
            }

            foreach (DataGridViewRow r in dataGridView.SelectedRows)
            {
                String kensakuword = (String)dataGridView.Rows[r.Index].Cells[2].Value;
                System.Diagnostics.Process.Start("http://www.bing.com/search?q=" + kensakuword);
            }
        }
        #endregion

        #region Option
        private void optionOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionForm f = new OptionForm(dbConnectionString);
            f.ShowDialog(this);
        }
        #endregion

    }
}
