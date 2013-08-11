using System;
using System.IO;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text.Method;

using Mono.Data.Sqlcipher;

namespace demo
{
	[Activity (Label = "demo", MainLauncher = true)]
	public class Activity1 : Activity
	{
		TextView displayOutput;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button runButton = FindViewById<Button> (Resource.Id.runButton);
			Button clearButton = FindViewById<Button>(Resource.Id.clearButton);
			displayOutput = FindViewById<TextView>(Resource.Id.displayOutput);
			displayOutput.MovementMethod = ScrollingMovementMethod.Instance;

			runButton.Click += (sender, e) => {
				RunSqlCipher();
			};
			clearButton.Click += (sender, e) => {
				ClearSqlCipher();	
			};
		}
		
		public void RunSqlCipher ()
		{
			var buffer = new StringBuilder();
			using (var connection = GetConnection("demo.db", "test")) {
				connection.Open ();
				using (var command = connection.CreateCommand()) {
					var createTable = "create table if not exists t1(a,b)";
					var insertData = "insert into t1(a,b) values('one for the money', 'two for the show')";
					var query = "select * from t1";
					command.CommandText = createTable;
					command.ExecuteNonQuery ();
					command.CommandText = insertData;
					command.ExecuteNonQuery ();
					command.CommandText = query;
					var reader = command.ExecuteReader ();
					while (reader.Read()) {
						var a = reader.GetString (0);
						var b = reader.GetString (1);
						buffer.Append(String.Format("a:{0} b:{1}{2}", a, b, System.Environment.NewLine));
					}
				}
				connection.Close ();
			}
			displayOutput.Text = buffer.ToString();
		}
		
		private void ClearSqlCipher()
		{
			using(var connection = GetConnection("demo.db", "test"))
			{
				connection.Open();
				using(var command = connection.CreateCommand())
				{
					command.CommandText = "delete from t1";
					command.ExecuteNonQuery();
				}
				connection.Close();
			}
			displayOutput.Text = "";
		}

		private SqliteConnection GetConnection(String databaseName, String password)
		{
			var databasePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), databaseName);
			var conn =  new SqliteConnection(String.Format("Data Source={0}", databasePath));
			conn.SetPassword(password);
			return conn;
		}

	}
}


