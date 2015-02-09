using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebApplicationPractice
{
    public partial class Tasks : System.Web.UI.Page
    {

        //SELECT * FROM database WHERE [condition]
        //DELETE FROM database WHERE [condition] [AND, OR] [condition]...
        //SELECT * FROM database ORDER BY [data] [ASC, DESC]
        //INSERT INTO database (data, data...) VALUES (values. values...)
        static string server = "10.238.56.195",
            user = "master",
            password = "master",
            database = "tasks",
            table = "taskslist",
            SELECT = "SELECT",
            ALL = "*",
            FROM = "FROM",
            WHERE = "WHERE",
            DELETE = "DELETE",
            AND = "AND",
            OR = "OR",
            ORDER = "ORDER BY",
            ASC = "ASC",
            DESC = "DESC",
            INSERT = "INSERT INTO",
            VALUES = "VALUES";
        static string[] columnNames = { "Name", "Duration", "Privacy", "Priority" };
        static string tableColumns = "(name, duration, privacy, priority)";
        static string selectAll = String.Format("{0} {1} {2} {3}", SELECT, ALL, FROM, table);
        static string delete = String.Format("{0} {1} {2} {3}", DELETE, FROM, table, WHERE);

        MySql.Data.MySqlClient.MySqlConnection msqlConnection;

        protected void Page_Load(object sender, EventArgs e)
        {
            msqlConnection = new MySqlConnection(GetConnectionString());
            if (msqlConnection.State != ConnectionState.Open)
                try
                {
                    msqlConnection.Open();
                    loadDatabaseIntoTable(selectAll);
                }
                catch (MySqlException exception)
                {
                    lAddTask.Text = exception.ToString();
                }
        }

        private static string GetConnectionString()
        {
            return String.Format("server={0}; user id={1}; password={2}; database={3}; pooling=false", server, user, password, database);
        }

        private void loadDatabaseIntoTable(string commandString)
        {
            MySqlDataReader msqlReader = createReader(commandString);
            DataTable dataTable = new DataTable();
            dataTable.Load(msqlReader);
            clearTable();
            fillTable(dataTable);
            msqlReader.Close();
        }

        private MySqlDataReader createReader(string commandString)
        {
            MySqlCommand msqlCommand = new MySqlCommand(commandString, msqlConnection);
            msqlCommand.CommandType = CommandType.Text;
            return msqlCommand.ExecuteReader();
        }

        private void clearTable()
        {
            tableTasks.Rows.Clear();
        }

        private void fillTable(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                addTableKey();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    insertRow(dataRow);
                }
            }
            else
            {
                TableRow r = new TableRow();
                TableCell c = new TableCell();
                c.Text = "EMPTY DATABASE";
                r.Cells.Add(c);
                tableTasks.Rows.Add(r);
            }
        }

        private void addTableKey()
        {
            TableRow rowTitle = new TableRow();
            TableRow rowButtons = new TableRow();
            for (int i = 0; i < columnNames.Length; i++)
            {
                //Column Name
                TableCell cellN = new TableCell();
                cellN.Text = columnNames[i];
                rowTitle.Cells.Add(cellN);

                //buttons for sorting
                TableCell cellB = new TableCell();

                Button buttonSortUp = new Button();
                buttonSortUp.Text = "^";
                buttonSortUp.ID = string.Format("{0} {1}", columnNames[i], ASC);
                buttonSortUp.Click += new EventHandler(submitSortTable);
                cellB.Controls.Add(buttonSortUp);

                Button buttonSortDown = new Button();
                buttonSortDown.Text = "v";
                buttonSortDown.ID = string.Format("{0} {1}", columnNames[i], DESC);
                buttonSortDown.Click += new EventHandler(submitSortTable);
                cellB.Controls.Add(buttonSortDown);

                rowButtons.Cells.Add(cellB);
            }
            tableTasks.Rows.Add(rowTitle);
            tableTasks.Rows.Add(rowButtons);
        }

        private void insertRow(DataRow dataRow)
        {
            TableRow row = new TableRow();
            string buttonID = "";
            for (int i = 0; i < dataRow.ItemArray.Length; i++)
            {
                TableCell cell = new TableCell();
                cell.Text = dataRow[columnNames[i]].ToString();
                row.Cells.Add(cell);
                buttonID += string.Format("{0}='{1}'", columnNames[i], dataRow[columnNames[i]].ToString());
                if (i < dataRow.ItemArray.Length - 1)
                {
                    buttonID += string.Format(" {0} ", AND);
                }
            }
            //add delete button for row
            TableCell cellForDeleteButton = new TableCell();
            Button b = new Button();
            b.Text = "DELETE";
            b.ID = b.ID = string.Format(" name='{0}' AND privacy={2} AND priority={3}", dataRow["name"].ToString(), dataRow["duration"].ToString(), dataRow["privacy"].ToString(), dataRow["priority"].ToString());
            //b.ID = buttonID;
            b.Click += new EventHandler(submitRemoveFromTableRow);
            cellForDeleteButton.Controls.Add(b);
            row.Cells.Add(cellForDeleteButton);

            tableTasks.Rows.Add(row);
        }

        protected void exacuteCommand(string command)
        {
            MySqlCommand msqlCommand = new MySqlCommand(command, msqlConnection);
            msqlCommand.CommandType = CommandType.Text;
            try
            {
                msqlCommand.ExecuteNonQuery();
                lAddTask.Text = "";
            }
            catch (Exception e)
            {
                lAddTask.Text = e.ToString();
            }
        }

        //called from aspx button (addToTable)
        protected void submitAddToTable(object sender, EventArgs e)
        {
            string values = string.Format("('{0}', '{1}', {2}, {3})", tbAddName.Text, tbAddDuration.Text, tbAddPrivacy.Text, tbAddPriority.Text);
            exacuteCommand(string.Format("{0} {1} {2} {3} {4}", INSERT, table, tableColumns, VALUES, values));
            loadDatabaseIntoTable(selectAll);
        }

        protected void submitRemoveFromTableRow(object sender, EventArgs e)
        {
            exacuteCommand(delete + ((Button)sender).ID);
            loadDatabaseIntoTable(selectAll);
        }

        protected void submitSortTable(object sender, EventArgs e)
        {
            loadDatabaseIntoTable(string.Format("{0} {1} {2}", selectAll, ORDER, ((Button)sender).ID));
        }

        protected void submitSpolierAddTasks(object sender, EventArgs e)
        {
            if (divSpolier.Style.Value == "display:none")
            {
                divSpolier.Style.Value = "display:normal";
            } 
            else 
            {
                divSpolier.Style.Value = "display:none";
            }
        }

    }
}