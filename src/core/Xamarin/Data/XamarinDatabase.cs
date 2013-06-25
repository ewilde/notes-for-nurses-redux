namespace Edward.Wilde.Note.For.Nurses.Core.DL
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using SQLite;

    public abstract class XamarinDatabase : SQLiteConnection
    {
        protected static XamarinDatabase database = null;

        protected static object locker = new object ();

        protected XamarinDatabase(string databasePath, bool storeDateTimeAsTicks = false)
            : base(databasePath, storeDateTimeAsTicks)
        {
        }

        public static string DatabaseFilePath
        {
            get
            {
                return database.DatabasePath;
            }
        }

        public static IEnumerable<T> GetItems<T> () where T : IBusinessEntity, new ()
        {
            lock (locker) {
                return (from i in database.Table<T> () select i).ToList ();
            }
        }

        public static T GetItem<T> (int id) where T : IBusinessEntity, new ()
        {
            lock (locker) {
                
                // ---
                //return (from i in database.Table<T> ()
                //        where i.Id == id
                //        select i).FirstOrDefault ();

                // +++ To properly use Generic version and eliminate NotSupportedException
                // ("Cannot compile: " + expr.NodeType.ToString ()); in SQLite.cs
                return database.Table<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public static int SaveItem<T> (T item) where T : IBusinessEntity
        {
            lock (locker) {
                if (item.Id != 0) {
                    database.Update (item);
                    return item.Id;
                } else {
                    return database.Insert (item);
                }
            }
        }

        public static void SaveItems<T> (IEnumerable<T> items) where T : IBusinessEntity
        {
            lock (locker) {
                database.BeginTransaction ();

                foreach (T item in items) {
                    SaveItem<T> (item);
                }

                database.Commit ();
            }
        }

        public static int DeleteItem<T>(int id) where T : IBusinessEntity, new ()
        {
            lock (locker) {
                return database.Delete<T> (new T () { Id = id });
            }
        }

        public static void ClearTable<T>() where T : IBusinessEntity, new ()
        {
            lock (locker) {
                database.Execute (string.Format ("delete from \"{0}\"", typeof (T).Name));
            }
        }

        public static int CountTable<T>() where T : IBusinessEntity, new ()
        {
            lock (locker) {
                if (database == null)
                {
                    return 0;
                }

                string sql = string.Format ("select count (*) from \"{0}\"", typeof (T).Name);
                var c = database.CreateCommand (sql, new object[0]);
                return c.ExecuteScalar<int>();
            }
        }

        public static string GetDatabaseFilePath(string fileName)
        {
#if SILVERLIGHT
            var path = fileName;
#else

    #if __ANDROID__
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    #elif TESTRUNNER
            string libraryPath = AppDomain.CurrentDomain.BaseDirectory;
#else
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "../Library/");
    #endif
    
    var path = Path.Combine(libraryPath, fileName);
#endif
    return path;            
        }
    }
}