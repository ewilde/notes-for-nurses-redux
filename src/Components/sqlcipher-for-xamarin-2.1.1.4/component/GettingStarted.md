##Getting Started with SQLCipher 

### Prerequisite

Start by installing the SQLCipher component and verifying Mono.Data.Sqlcipher.dll is listed as project assembly reference.

SQLCipher for Xamarin provides two popular APIs options for interacting with an encrypted database:

1. Mono.Data.Sqlcipher - Compatible with the [Mono.Data.Sqlite provider](http://docs.xamarin.com/guides/ios/advanced_topics/system.data) API.

2. SQLite - Compatible with the popular [sqlite-net project](https://github.com/praeclarum/sqlite-net) API.

_Tip: Before converting a project to use SQLCipher, remove any existing assembly references to Mono.Data.Sqlite, sqlite-net, or sqlite-net source code (i.e. SQLite.c)._

### sqlite-net

    using SQLite;
    
    ...

    public class Model
    {
      [PrimaryKey,AutoIncrement]
      public int Id { get; set; }
      public string Content { get; set; }
    }
    
    ...
    
    using(var conn = new SQLiteConnection (FilePath, Password))
    {
      conn.CreateTable<Model>();
    
      conn.InsertOrReplace( 
        new Model() {Id = 0, Content = content});
    
      var models = conn.Query<Model> (
        "SELECT * FROM Model WHERE Id = ?", 0);
    }

### Mono.Data.Sqlcipher

    using Mono.Data.Sqlcipher;

    ...

    using(var conn = new SqliteConnection(string.Format("Data Source={0}", FilePath)))
    {
      conn.SetPassword(Password);
      conn.Open(); 
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandText = “CREATE TABLE Model(“ +
          “Id INTEGER PRIMARY KEY AUTOINCREMENT, Content TEXT)";
        cmd.ExecuteNonQuery();
    
        cmd.CommandText = 
          "INSERT INTO Model (Content) VALUES (@content)";
        var p = cmd.CreateParameter();
        p.ParameterName = "@content";
        p.Value = content;
        cmd.Parameters.Add(p);
    
        cmd.CommandText = 
          “SELECT * FROM Model Where Id = 0";
        var reader = command.ExecuteReader ();
        while (reader.Read()) {
          // process results
        }
      }
    }

