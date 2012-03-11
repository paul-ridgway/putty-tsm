using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.IO;

namespace AppInForm
{
    public class SessionManager
    {
        private String databaseFile;
        private SQLiteConnection connection;
        public SessionManager(String databaseFile)
        {
            this.databaseFile = databaseFile;
            connection = new SQLiteConnection("Data Source=" + databaseFile);
        }

        public void Open()
        {
            bool initializeAfterOpen = !File.Exists(databaseFile);
            connection.Open();
            if (initializeAfterOpen)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            //Creation queries
            SQLiteCommand systemCreate = new SQLiteCommand("CREATE TABLE [system] ([id] INTEGER  NOT NULL PRIMARY KEY,[name] VARCHAR(128)  NOT NULL,[value] VARCHAR(128)  NOT NULL)", connection);
            SQLiteCommand sessionFoldersCreate = new SQLiteCommand("CREATE TABLE [session_folders] ([id] INTEGER  NOT NULL PRIMARY KEY,[name] VARCHAR(128)  NOT NULL)", connection);
            SQLiteCommand sessionCreate = new SQLiteCommand("CREATE TABLE [sessions] ([id] INTEGER  NOT NULL PRIMARY KEY,[name] VARCHAR(128)  NOT NULL,[host] VARCHAR(128)  NOT NULL,[username] VARCHAR(128)  NOT NULL,[port] INTEGER  NOT NULL,[folder_id] INTEGER  NOT NULL)", connection);
            
            //Execute queries
            systemCreate.ExecuteNonQuery();
            sessionFoldersCreate.ExecuteNonQuery();
            sessionCreate.ExecuteNonQuery();
        }

        public Dictionary<int, string> ListFolders()
        {
            Dictionary<int, string> folders = new Dictionary<int, string>();
            SQLiteCommand folderCommand = new SQLiteCommand("SELECT id,name FROM session_folders", connection);
            SQLiteDataReader reader = folderCommand.ExecuteReader();
            while (reader.Read())
            {
                folders.Add(reader.GetInt32(0), reader.GetString(1));
            }
            reader.Close();
            return folders;
        }

        public void ListSessions(int folderID)
        {

        }

        public int CreateFolder(String name)
        {
            SQLiteCommand createCommand = new SQLiteCommand("INSERT INTO session_folders (name) VALUES('" + name + "');", connection);
            createCommand.ExecuteNonQuery();
            return LastInsertID();
        }

        public int CreateSSHSession(int folderID, String name, String host, String username, int port)
        {
            SQLiteCommand createCommand = new SQLiteCommand("INSERT INTO sessions (folder_id, name,host,username,port) VALUES(" + folderID + ",'" + name + "','" + host + "','" + username + "'," + port + ");", connection);
            createCommand.ExecuteNonQuery();
            return LastInsertID();
        }

        private int LastInsertID()
        {
            SQLiteCommand lastIDCommand = new SQLiteCommand(@"SELECT last_insert_rowid()", connection);
            object result = lastIDCommand.ExecuteScalar();
            return int.Parse(result.ToString());
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
