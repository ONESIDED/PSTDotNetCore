// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;


const string connectionString = "Data Source= MSI\\MSSQLSERVER2019; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa;";

Console.WriteLine("Hello, World!");
//Console.Read();
Console.ReadKey();

#region
//Console.ReadLine();

//markdown

//C# <=> Database

// ADO.Net
// Dapper (ORM) Object Relational Mapper [C# က Obj နဲ Databsee က Table နဲ ညီမျှပါတယ်ဆိုတာမျိုးကိုလုပ်ချင်တာပါ ]
// EFCore / Entity Framework

// C# => Sql query

// Nuget

// Ctrl + .

// max connection = 100
// 100 = 99

//101
#endregion

Console.WriteLine("Connection String: "+ connectionString);
SqlConnection connection = new SqlConnection(connectionString);

Console.WriteLine("Connection Opening...");
connection.Open();
Console.WriteLine("Connection Opened.");

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

// ExecuteReaderAsnyc အကြောင်းကိုမေးရန်
// dr နေရာမှာ reader ကိုပဲရွေးပြီး select မှတ်တာကိုမေးရန် ?1
// မှားပြီး commit all လုပ်လိုက်တာကို push မလုပ်ခင် message ပြန်ပြင်ချင်ရင် ဘယ်လိုပြန်လုပ်လို့ရလဲ နာမည်ပြန်ခွဲပြီးပေးချင်တာမျိုးပါ ?2

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

Console.WriteLine("Connection Closing...");
connection.Close();
Console.WriteLine("Connection Closed.");

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

connection.Open();

Console.WriteLine("Please Blog Title: ");
string title = Console.ReadLine();

Console.WriteLine("Please Blog Author: ");
string author = Console.ReadLine();

Console.WriteLine("Please Blog Content: ");
string contect = Console.ReadLine();

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

string queryInsert = $@"INSERT INTO [dbo].[Tbl_BLog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,'0')";


SqlCommand cmd2 = new SqlCommand(queryInsert, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
//DataTable dt = new DataTable();
//adapter.Fill(dt);

cmd2.Parameters.AddWithValue("@BlogTitle", title);
cmd2.Parameters.AddWithValue("@BlogAuthor", title);
cmd2.Parameters.AddWithValue("@BlogContent", title);

int result = cmd2.ExecuteNonQuery();

Console.WriteLine(result == 1 ? "Insert Successful." : "Inesert Failed.");

connection.Close();

Console.ReadKey();


