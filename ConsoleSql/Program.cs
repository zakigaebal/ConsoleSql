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

		private static void InsertUpdate() // 삽입과 수정
		{
			// 연결 스트링
			string strConn = "Server=127.0.0.1;Database=dawoon;Uid=root;Pwd=ekdnsel";

			//MysqlConnection클래스에서 strConn을 conn이라는 변수에 저장하고 사용
			using (MySqlConnection conn = new MySqlConnection(strConn))
			{
				//연결
				conn.Open();
				//삽입쿼리 cmd에 저장
				MySqlCommand cmd = new MySqlCommand("INSERT INTO Tab1 VALUES (5, 'Tom')", conn);
				
				//쿼리실행
				cmd.ExecuteNonQuery();

				//cmd쿼리에 명령어를 수정쿼리로 저장시킴
				cmd.CommandText = "Update Tab1 SET Name='Tim' WHERE Id=2";

				//쿼리실행
				cmd.ExecuteNonQuery();
			}
		}
		private static void SelectUsingAdapter()
		{
			//데이터셋 ds 변수 선언
			DataSet ds = new DataSet();

			//ConnStr에 연결스트링을 저장
			string connStr = "Server=127.0.0.1;Database=dawoon;Uid=root;Pwd=ekdnsel";

			// conn이라는 변수에 연결스트링 저장한후 사용
			using (MySqlConnection conn = new MySqlConnection(connStr))
			{
				//MySqlDataAdapter 클래스를 이용하여
				//비연결 모드로 데이터 가져오기

				// sql 변수에 데이터 가져오기
				string sql = "SELECT * FROM tab1 WHERE Id>=2";

				//클래스 사용하여 sql과conn을 저장한클래스를 생성시킨 변수 adapter에 저장
				MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);

				//adapter내용을 데이터셋 tab1을 저장
				adapter.Fill(ds, "tab1");
			}

			//데이터테이블의 로우값들을 순차적으로 돌려라
			foreach (DataRow r in ds.Tables[0].Rows)
			{
				//r에 저장한 네임값을 나타내라
				Console.WriteLine(r["Name"]);
			}
		}

		private static void SelectUsingReader()
		{
			//연결 문자열
			string connStr = "Server=127.0.0.1;Database=dawoon;Uid=root;Pwd=ekdnsel;";

			//연결어
			using (MySqlConnection conn = new MySqlConnection(connStr))
			{
				//연결
				conn.Open();

				//sql 문자열 저장
				string sql = "SELECT * FROM tab1 WHERE Id>=2";

				//ExecuteReader를 이용하여
				//연결모드로 데이터 가져오기

				// 명령어
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				
				// 데이터 읽기
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					//데이터를 읽는동안 첫번째와 두번째 값을 id랑 name으로 보여줘라
					Console.WriteLine("{0}: {1}", rdr["Id"], rdr["Name"]);
				}
				// 데이터읽기 닫기
				rdr.Close();

			}
		}

	}
}
