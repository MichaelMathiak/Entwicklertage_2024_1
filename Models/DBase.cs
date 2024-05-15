﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;

namespace Entwicklertage_2024_1.Models
{
    public class DBase
    {


        public List<String> Haltestellen()
        {
            List<String> list = new List<String>();
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string cmd_txt;
           ///* string con_str = @"D*/ata Source=(localdb)\mssqllocaldb;AttachDbFilename=C:\Entwicklertage\2024_1\entwicklertage24-main\database.db;Integrated Security=True;Connect Timeout=30";
            //string con_str = @"Server=10.2.39.225\HCAPPS;Database=CT-Auspacker;user id=hcit;password=hcit;Integrated Security=false; persist security info=false;";
           

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(baseDir, "database.db");
            var sqlite_connectionString = $@"Data Source={dbPath}; Version = 3;";
            
            
            sqlite_conn = new SQLiteConnection(sqlite_connectionString);
            
            // Open the connection:
        
                sqlite_conn.Open();

                cmd_txt = "SELECT * from stops";
            
                //con = new SqlConnection(con_str);
                //con.Open();
                cmd = new SQLiteCommand(cmd_txt, sqlite_conn);
                dr = cmd.ExecuteReader();
                while (dr.Read()) { list.Add(dr.GetValue(1).ToString());};
            return list;

        }
    }
}
