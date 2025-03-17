namespace CSharpApp.DB;

using System.Configuration;
using System.Data.SQLite;

public class DBUtils
{
    public static SQLiteConnection GetConnection() {
        string connStr = ConfigurationManager.ConnectionStrings["Identifier"].ConnectionString;
        Console.WriteLine("Using DB file: " + connStr);
        return new SQLiteConnection(connStr);
    }
}