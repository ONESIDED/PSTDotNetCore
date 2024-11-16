using Dapper;
using DotNetTrainigBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainigBatch5.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source= MSI\\MSSQLSERVER2019; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa;";

        public void Read()
        {
            #region Dapper ကိုဟာကို ဖြည့်ပြီး Bind တာပါ
            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = @"SELECT * FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";
            //    var lst = db.Query(query).ToList();

            //    foreach (var item in lst) 
            //    {
            //        Console.WriteLine(item.BlogId);
            //        Console.WriteLine(item.BlogTitle);
            //        Console.WriteLine(item.BlogAuthor);
            //        Console.WriteLine(item.BlogContent);
            //    }

            //}
            #endregion

            // DTO => Data Transfer Obejct ကိုသုံးပြီးရေးပါမယ် 

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";
                var lst = db.Query<BlogDataModel>(query).ToList();

                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }

            }
        }

        public void Create(string title, string author, string content)
        {
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");

            }


        }

        public void Edit(int Id)
        {
            string query = $@"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_BLog] where BlogId = @BlogId and [DeleteFlag] = 0";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var item = db.Query<BlogDataModel>(query, new BlogDataModel
                {
                    BlogId = Id
                }).FirstOrDefault();

                if (item is null)
                {
                    Console.WriteLine("No data found.");
                    return;
                }

                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

            }


        }
    }
}

