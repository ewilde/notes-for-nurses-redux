namespace Edward.Wilde.Note.For.Nurses.Core.Xamarin.Data
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using SQLite;

    /// <summary>
    /// Represents the behaviour supported by our database.
    /// </summary>
    public interface IXamarinDatabase
    {
        string DatabaseFilePath { get; }

        IEnumerable<T> GetItems<T> () where T : IBusinessEntity, new ();

        T GetItem<T> (int id) where T : IBusinessEntity, new ();

        int SaveItem<T> (T item) where T : IBusinessEntity;

        void SaveItems<T> (IEnumerable<T> items) where T : IBusinessEntity;

        int DeleteItem<T>(int id) where T : IBusinessEntity, new ();

        void ClearTable<T>() where T : IBusinessEntity, new ();

        int CountTable<T>() where T : IBusinessEntity, new ();

        /// <summary>
        /// Executes a "drop table" on the database.  This is non-recoverable.
        /// </summary>
        int DropTable<T>();

        /// <summary>
        /// Executes a "create table if not exists" on the database. It also
        /// creates any specified indexes on the columns of the table. It uses
        /// a schema automatically generated from the specified type. You can
        /// later access this schema by calling GetMapping.
        /// </summary>
        /// <returns>
        /// The number of entries added to the database schema.
        /// </returns>
        int CreateTable<T>(CreateFlags createFlags = CreateFlags.None);
    }
}