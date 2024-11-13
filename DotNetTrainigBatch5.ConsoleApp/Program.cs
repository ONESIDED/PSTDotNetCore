// See https://aka.ms/new-console-template for more information

using DotNetTrainigBatch5.ConsoleApp;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;


//Console.WriteLine("Hello, World!");
//Console.Read();
//Console.ReadKey();

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

// ExecuteReaderAsnyc အကြောင်းကိုမေးရန် ျ await သိရန်လိုတယ် 
// dr နေရာမှာ reader ကိုပဲရွေးပြီး select မှတ်တာကိုမေးရန် ?1 ိdone (Alt + left click drag)
// မှားပြီး commit all လုပ်လိုက်တာကို push မလုပ်ခင် message ပြန်ပြင်ချင်ရင် ဘယ်လိုပြန်လုပ်လို့ရလဲ နာမည်ပြန်ခွဲပြီးပေးချင်တာမျိုးပါ ?2

#endregion


AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
adoDotNetExample.Delete();



Console.ReadKey();




