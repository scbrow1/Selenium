using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Configuration;
using System.IO;
using System.Diagnostics;

public class Database
{
    public static MySqlDataReader ExecuteQuery(MySqlCommand cmd)
    {
        MySqlDataReader dr = null;
        dr = cmd.ExecuteReader();

        return dr;
    }

    public static void UpdateQuery(string query)
    {
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["DbConn"].ToString());
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = query;
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Dispose();
    }

    public static MySqlCommand InsertQuery(string query, dynamic obj)
    {
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["DbConn"].ToString());
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = query;
        cmd.Prepare();
        cmd.Parameters.AddWithValue("?DateCreated", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        cmd.Parameters.AddWithValue("?DateModified", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        cmd = obj.SetParameters(cmd);
        cmd.ExecuteNonQuery();

        conn.Dispose();
        return (cmd);
    }

    public static void DeleteQuery(string query)
    {
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["DbConn"].ToString());
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = query;
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Dispose();
    }

    public static int GetIdOfLastRecord(string tableName)
    {
        int lastID = 0;

        MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["DbConn"].ToString());
        conn.Open(); MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * From " + tableName + " ORDER BY id desc limit 1";
        MySqlDataReader dr = Database.ExecuteQuery(cmd);
        if (dr.Read())
            lastID = Convert.ToInt16(dr["ID"]);

        dr.Dispose(); cmd.Dispose(); conn.Dispose();

        return lastID;
    }

    public static bool ColumnExists(MySqlDataReader reader, string columnName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
            if (reader.GetName(i).ToLower() == columnName.ToLower())
                return true;

        return false;
    }

}

