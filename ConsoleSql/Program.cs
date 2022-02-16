using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleSql
{
	public class Program
	{
		static void Main(string[] args)
		{

			SelectUsingReader();
			SelectUsingAdapter();
			InsertUpdate();
		
		}

		private static void InsertUpdate()
		{
			string strConn = "Server=127.0.0.1;Database=dawoon;Uid=root;Pwd=ekdnsel";

			using (MySqlConnection conn = new MySqlConnection(strConn))
			{
				conn.Open();
				MySqlCommand cmd = new MySqlCommand("INSERT INTO Tab1 VALUES (5, 'Tom')", conn);
				cmd.ExecuteNonQuery();

				cmd.CommandText = "Update Tab1 SET Name='Tim' WHERE Id=2";
				cmd.ExecuteNonQuery();
			}
		}
		private static void SelectUsingAdapter()
		{
			DataSet ds = new DataSet();
			string connStr = "Server=127.0.0.1;Database=dawoon;Uid=root;Pwd=ekdnsel";

			using (MySqlConnection conn = new MySqlConnection(connStr))
			{
				//MySqlDataAdapter 클래스를 이ㅛㅇㅇ하여
				//비연결 모드로 데이터 가져오기
				string sql = "SELECT * FROM tab1 WHERE Id>=2";
				MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
				adapter.Fill(ds, "tab1");
			}
			foreach (DataRow r in ds.Tables[0].Rows)
			{
				Console.WriteLine(r["Name"]);
			}
		}

		private static void SelectUsingReader()
		{
			string connStr = "Server=127.0.0.1;Database=dawoon;Uid=root;Pwd=ekdnsel;";

			using (MySqlConnection conn = new MySqlConnection(connStr))
			{
				conn.Open();
				string sql = "SELECT * FROM tab1 WHERE Id>=2";

				//ExecuteReader를 이용하여
				//연결모드로 데이터 가져오기

				MySqlCommand cmd = new MySqlCommand(sql, conn);
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					Console.WriteLine("{0}: {1}", rdr["Id"], rdr["Name"]);
				}
				rdr.Close();

			}
		}

	}
}
