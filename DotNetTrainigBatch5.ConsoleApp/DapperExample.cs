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

        #region Dapper ကို သုံးခြင်း

        //  ၁။  using ဆိုပြီးတော့အရင် ဆောက်ရမယ်။
        //  ၂။  IDbConnection ကို new sqlconnection နဲဆောက်ရပြီးတော့ _constring ကိုအဲ့ထည်ကိုထည့်ပေးရမယ်။
        //  ၃။  cmd ဆောက်သလိုမျိုး query နဲ _constring ကို ထည့်ပေးရမှာမဟုတ်ဘူး။ _constring ကိုပဲထည့်ရင်ရပြီ။
        //  ၄။  အထဲမှာ query လိုမယ်။
        //  ၅။  ပြီးရင် var နဲ query ကို run or execution လုပ်ဖို့လုပ်ရမယ်။
        //  ၆။  အထဲမှာ Models က column_name တွေယူပြီး foreach or smth နဲ data ထုတ်ဖို့လုပ်ရမယ်။
        //      ၆.၁။  parameter လိုရင် new model_name နဲ parameters တွေကိုတစ်ခါတည်းထည့်ပြီရင် 
        //            data ထည့်လို့ရပြီ or ထုတ်လို့ရပြီ။

        #endregion

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

        public void Update()
        {
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = int.Parse(id),
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");

            }
        }

        public void Delete()
        {
            Console.Write("Blog Id: ");
            string id = Console.ReadLine();

            if (string.IsNullOrEmpty(id)) { return; }

            string query = @"UPDATE [dbo].[Tbl_BLog]
   SET [DeleteFlag] = 1
 WHERE [BlogId] = @BlogId and [DeleteFlag] = 0";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = int.Parse(id)
                });
                Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");

            }
        }

        //Recovery day
        //Recovery day 2
    }
}

