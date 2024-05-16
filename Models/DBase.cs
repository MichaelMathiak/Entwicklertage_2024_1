using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Web.WebSockets;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;

namespace Entwicklertage_2024_1.Models
{
    public class DBase
    {
        
        public List<Anzeigedatan> AnzeigeDatan_Transfers { get; set; }
        
        private List<Transfers> transferList { get; set; }
        private List<Transfers> availableStops { get; set; }
        

        private static string baseDIr = AppDomain.CurrentDomain.BaseDirectory;
        private static string dbPath = Path.Combine(baseDIr, "database.db");
        private string sqLiteConnection = $@"Data Source={dbPath}; Version = 3;";
        private string _selectedEnd { get; set; }
        
        public Dictionary<string, string> Haltestellen()
        {
            var dictionary = new Dictionary<string, string>();
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string cmd_txt;


            SQLiteConnection sqlite_conn;
            // Create a new database connection:

            sqlite_conn = new SQLiteConnection(sqLiteConnection);

            // Open the connection:

            sqlite_conn.Open();

            cmd_txt = "SELECT DISTINCT stop_id, stop_name from stops order by stop_name";

            //con = new SqlConnection(con_str);
            //con.Open();
            cmd = new SQLiteCommand(cmd_txt, sqlite_conn);
            
            try
            {
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dictionary.Add(dr.GetValue(0).ToString(), dr.GetValue(1).ToString());
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return dictionary;
        }


        public void HandleStartZiel(IEnumerable<KeyValuePair<string, string>> selectedStart, IEnumerable<KeyValuePair<string, string>> selectedEnd)
        {
            transferList = new List<Transfers>();
            availableStops = new List<Transfers>();
            _selectedEnd = selectedEnd.First().Key;
            
            #region SQL
            SQLiteCommand sqLiteCommand;
            SQLiteDataReader dr;
            string sqlCommand;
            
            SQLiteConnection sqlite_conn;
            
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(baseDir, "database.db");
            var sqlite_connectionString = $@"Data Source={dbPath}; Version = 3;";


            sqlite_conn = new SQLiteConnection(sqlite_connectionString);
            
            sqlite_conn.Open();

            sqlCommand = "select from_stop_id, to_stop_id, min_transfer_time from transfers where from_stop_id = '" +
                         selectedStart.First().Key + "' order by min_transfer_time";
            
            sqLiteCommand = new SQLiteCommand(sqlCommand, sqlite_conn);
            
            try
            {
                dr = sqLiteCommand.ExecuteReader();
                while (dr.Read())
                {
                    var item = new Transfers();

                    item.FromStopId = dr.GetValue(0).ToString();
                    item.ToStopId = dr.GetValue(1).ToString();
                    item.MinTransferTime = dr.GetValue(2).ToString();
                                        
                    transferList.Add(item);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            #endregion
            
            if(transferList.Count == 0)
            {
                // keine Verbindung gefunden
                return;
            }
            
            Lade_Anzeigedaten_Transfers();
            
            foreach (var stop in transferList)
            {
                RekursiverAufruf(stop);
            }
            
        }

        private string RekursiverAufruf(Transfers transfer)
        {
            var newTransfers = new List<Transfers>();

            if (transfer.ToStopId.Equals(_selectedEnd))
            {
                // do stuff
            }
            
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string query = "select from_stop_id, to_stop_id, min_transfer_time from transfers where from_stop_id = '" +
                           transfer.ToStopId + "' order by min_transfer_time";

            
            var sqlite_conn = new SQLiteConnection(sqLiteConnection);

            sqlite_conn.Open();
            
            cmd = new SQLiteCommand(query, sqlite_conn);
            
            try
            {
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var item = new Transfers();
                    item.FromStopId = dr.GetValue(0).ToString();
                    item.ToStopId = dr.GetValue(1).ToString();
                    item.MinTransferTime = dr.GetValue(2).ToString();
                    
                    newTransfers.Add(item);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (newTransfers.Count > 0)
            {
                foreach (var trans in newTransfers)
                {
                    RekursiverAufruf(trans);
                }
            }

            return null;
        }

        public void Lade_Anzeigedaten_Transfers()
        {
            AnzeigeDatan_Transfers = new List<Anzeigedatan>();

            foreach (var transfer in transferList)
            {
                SQLiteCommand cmd;
                SQLiteDataReader dr;
                var query = "select (select stop_name from stops where stop_id = '"+transfer.FromStopId+"') as VonHaltestelle, (select stop_name from stops where stop_id = '"+transfer.ToStopId+"') as BisHaltestelle from stops";

            
                var sqlite_conn = new SQLiteConnection(sqLiteConnection);

                sqlite_conn.Open();
            
                cmd = new SQLiteCommand(query, sqlite_conn);
            
                try
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var item = new Anzeigedatan();
                        item.VonHaltestelle = dr.GetValue(0).ToString();
                        item.BisHaltestelle = dr.GetValue(1).ToString();
                        item.ZeitInMinuten = transfer.MinTransferTime;
                        
                        AnzeigeDatan_Transfers.Add(item);
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }

    public class Transfers
    {
        public string FromStopId { get; set; }
        public string ToStopId { get; set; }
        public string MinTransferTime { get; set; }
    }

    public class Anzeigedatan
    {
        public string VonHaltestelle { get; set; }
        public string BisHaltestelle { get; set; }
        public string ZeitInMinuten { get; set; }
    }
}
