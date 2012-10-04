using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SQLInjection.Models
{
    public class DBManager
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter dataAdapter;
        private DataSet dataSet = new DataSet();
        private DataTable dataTable = new DataTable();
        public DBManager()
        {
            sql_con = new SQLiteConnection("Data Source=|DataDirectory|test.db;Version=3;New=False;Compress=True;"); 
        }

        public string GetUserByUserName(string username)
        {
            var sqlUser = string.Format("SELECT [Name], [Description] FROM [Users] WHERE [Name]='{0}'", username);
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = sqlUser;
            string result = ReadData(sql_cmd);
            sql_con.Close();
            return result;
        }

        public string GetUserByUserName2(string username)
        {
            var sqlUser = string.Format("SELECT [Name], [Description] FROM [Users] WHERE [Name]=@name");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = sqlUser;
            sql_cmd.Parameters.AddWithValue("name", username);
            string result = ReadData(sql_cmd);
            sql_con.Close();
            return result;
        }

        public string GetUsers()
        {
            var sqlSelectAllUsers = string.Format("SELECT * FROM [Users]");
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = sqlSelectAllUsers;
            sql_con.Open();
            string result = ReadData(sql_cmd);
            sql_con.Close();
            return result;
        }

        public void CreateAndSeedUsers()
        {
            sql_con.Open();
            // Drop Table
            var sqlDropTable = string.Format("DROP TABLE IF EXISTS [Users]");
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = sqlDropTable;
            sql_cmd.ExecuteNonQuery();

            // Create Table
            var sqlCreateTable = string.Format("CREATE TABLE [Users] ([ID] NVARCHAR(3) PRIMARY KEY, [NAME] NVARCHAR(255), [Description] NVARCHAR(255), [PASSWORD] NVARCHAR(255))");
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = sqlCreateTable;
            sql_cmd.ExecuteNonQuery();

            // Seed Data
            var sqlInsertUser1 = string.Format("INSERT INTO [Users] ([ID], [NAME], [DESCRIPTION], [PASSWORD]) VALUES ('1', 'bob', 'Likes fashion', 'secret1')");
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = sqlInsertUser1;
            sql_cmd.ExecuteNonQuery();
            var sqlInsertUser2 = string.Format("INSERT INTO [Users] ([ID], [NAME], [DESCRIPTION], [PASSWORD]) VALUES ('2', 'anne', 'Likes games', 'abc123!')");
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = sqlInsertUser2;
            sql_cmd.ExecuteNonQuery();
            var sqlInsertUser3 = string.Format("INSERT INTO [Users] ([ID], [NAME], [DESCRIPTION], [PASSWORD]) VALUES ('3', 'dan', 'Has no hobbies', '123password')");
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = sqlInsertUser3;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public string ReadData(SQLiteCommand cmd)
        {
            dataAdapter = new SQLiteDataAdapter(cmd);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            string result = "";
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (var field in row.ItemArray)
                    {
                        result += string.Format(" {0}", (string)field);
                    }
                    result += "</br>";
                }
            }
            SQLiteCommand sql_cmd = sql_con.CreateCommand();
            sql_con.Close();
            return result;
        }










// string.Format("SELECT [Name], [Description] " +
//                 "FROM [Users] " +
//                 "WHERE [Name]='{0}'", username);
//
// username = "bob' OR 1 = 1 --" =>
// SELECT * FROM [Users] 
//  WHERE username='bob' OR 1 = 1 --'
//
// username = "bob'; SELECT * FROM [Users]; --" =>
// SELECT * FROM [Users] WHERE username='bob'; 
// SELECT * FROM [Users]; --'
//
// username = "bob'; DROP TABLE [Users]; --" =>
// SELECT * FROM [Users] WHERE username='bob; 
// DROP TABLE [Users]; --'




    }
}