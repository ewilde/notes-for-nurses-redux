namespace Edward.Wilde.Note.For.Nurses.Core.Xamarin.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using SQLite;

    public abstract class XamarinDatabase : SQLiteConnection, IXamarinDatabase
    {
        protected static object staticLock = new object ();

        protected XamarinDatabase(string databasePath, bool storeDateTimeAsTicks = false)
            : base(databasePath, storeDateTimeAsTicks)
        {
        }

        public string DatabaseFilePath
        {
            get
            {
                return this.DatabasePath;
            }
        }

        public IEnumerable<T> GetItems<T> () where T : IBusinessEntity, new ()
        {
            lock (staticLock) {
                return (this.Table<T>().Select(i => i)).ToList();
            }
        }

        public T GetItem<T> (int id) where T : IBusinessEntity, new ()
        {
            lock (staticLock) {
                
                // ---
                //return (from i in database.Table<T> ()
                //        where i.Id == id
                //        select i).FirstOrDefault ();

                // +++ To properly use Generic version and eliminate NotSupportedException
                // ("Cannot compile: " + expr.NodeType.ToString ()); in SQLite.cs
                return this.Table<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem<T> (T item) where T : IBusinessEntity
        {
            lock (staticLock)
            {
                if (item.Id != 0) {
                    this.Update(item);
                    return item.Id;
                }


                return this.Insert(item);
            }
        }

        public void SaveItems<T> (IEnumerable<T> items) where T : IBusinessEntity
        {
            lock (staticLock) {
                this.BeginTransaction();

                foreach (T item in items) {
                    this.SaveItem<T> (item);
                }

                this.Commit();
            }
        }

        public int DeleteItem<T>(int id) where T : IBusinessEntity, new ()
        {
            lock (staticLock) {
                return this.Delete<T>(new T() { Id = id });
            }
        }

        public void ClearTable<T>() where T : IBusinessEntity, new ()
        {
            lock (staticLock) {
                this.Execute(string.Format("delete from \"{0}\"", typeof(T).Name));
            }
        }

        public int CountTable<T>() where T : IBusinessEntity, new ()
        {
            lock (staticLock) {
                string sql = string.Format ("select count (*) from \"{0}\"", typeof (T).Name);
                var c = this.CreateCommand(sql, new object[0]);
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