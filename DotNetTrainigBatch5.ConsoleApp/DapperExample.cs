using Dapper;
using DotNetTrainigBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    }
}

