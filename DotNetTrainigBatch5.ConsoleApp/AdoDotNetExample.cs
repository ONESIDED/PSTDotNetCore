using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainigBatch5.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source= MSI\\MSSQLSERVER2019; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa;";
        public void Read()
        {
            Console.WriteLine("Connection String: " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            //Console.WriteLine("Connection Opening...");
            connection.Open();
            //Console.WriteLine("Connection Opened.");

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";

            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
            }

            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    //Console.WriteLine(dr["DeleteFlag"]);
            //}

            //Console.WriteLine("Connection Closing...");
            connection.Close();
            //Console.WriteLine("Connection Closed.");

            // DataSet
            // DataTable
            // DataRow
            // DataColumn


            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    //Console.WriteLine(dr["DeleteFlag"]);
            //}

        }

        public void Create()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            Console.WriteLine("Please Blog Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Please Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Please Blog Content: ");
            string content = Console.ReadLine();

            //string queryInsert = $@"INSERT INTO [dbo].[Tbl_BLog]
            //           ([BlogTitle]
            //           ,[BlogAuthor]
            //           ,[BlogContent]
            //           ,[DeleteFlag])
            //     VALUES
            //           ('{title}'
            //           ,'{author}'
            //           ,'{contect}'
            //           ,'0')";

            string query = $@"INSERT INTO [dbo].[Tbl_BLog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,'0')";


            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            if (string.IsNullOrEmpty(title))
            {
                return;
            }

            if (string.IsNullOrEmpty(author))
            {
                return;
            }

            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            Console.WriteLine(result == 1 ? "Insert Successful." : "Inesert Failed.");

            connection.Close();
        }

        public void Edit()
        {
            //Console.WriteLine("Blog Id: ");
            //Console.WriteLine(); နဲရေးမယ်ဆိုရင် ကိုယ်ရေးတဲံ့စာကို အောက်တစ်ကြောင်းဆင်းပြီးပြနေပါလိမ့်မယ်
            Console.Write("Blog Id: ");
            string id = Console.ReadLine();

            if (string.IsNullOrEmpty(id)) { return; }

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = $@"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_BLog] where BlogId = @BlogId and [DeleteFlag] = 0";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            connection.Close(); // data ကိုယူတဲ့ ကိစ္စပြီးတာနဲ connection ပိတ်တာကကောင်းပါတယ်။

            if (dt.Rows.Count is 0) // row ရဲ့ count က ၀ နဲညီနေရင် data မပါလာလို့ပါ။
            {
                Console.WriteLine("No data found...");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);

            #region လုပ်ခဲ့တာအဆင့်ဆင့်

            // ၁ id တောင်းတယ် သူကမပေးခဲ့ရင် ရပ်ရမယ်။
            // ၂ connection ကိုဆောက်တယ် ပြီးရင် connection ဖွင့်ခဲ့ရမယ်
            // ၃ query ရေးရမယ်
            // ၄ cmd ကိုဆောက်ရမယ် သူ့ထဲကို query နဲ con ကိုထည့်ရမယ်
            // ၅ i/o operation ကိုလုပ်ရမယ် 
            // adapter ကိုသုံးရင် adapter ကိုဆောက် (or) reader ကို ဖတ်မယ်ဆို reader ကိုဆောက်ရမယ်
            // ၆ data ကိုယူတဲ့ ကိစ္စပြီးတာနဲ connection ပိတ်တာက အကောင်းဆုံးပါပဲ။
            // ၇ row ရဲ့ count က ၀ နဲညီနေရင် data မပါလာလို့ပါ။ သူ့အတွက်စစ်ပေးရမယ်။

            #endregion
        }

        public void Update() 
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            Console.WriteLine("Please Blog ID: ");
            string id = Console.ReadLine();

            Console.WriteLine("Please Blog Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Please Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Please Blog Content: ");
            string content = Console.ReadLine();

            string query = @"UPDATE [dbo].[Tbl_BLog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE [BlogId] = @BlogId and [DeleteFlag] != 1";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue ("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Update Successful." : "Update Failed.");
        }
    }
}
