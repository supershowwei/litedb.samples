using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiteDB;
using Newtonsoft.Json;
using RestSharp;

namespace LiteDBSamples
{
    public partial class Form1 : Form
    {
        private static readonly string ConnectionString = @"Filename=D:\test.db";

        public Form1()
        {
            this.InitializeComponent();
        }

        private static string GetRawSamples()
        {
            var client = new RestClient("https://www.generatedata.com/ajax.php");
            var request = new RestRequest(Method.POST);
            request.AddHeader(
                "Cookie",
                "PHPSESSID=hspMh6LJ6gEI7t8G%2CTYSm0; __utma=160707499.1059016374.1524369768.1524369768.1524369768.1; __utmc=160707499; __utmz=160707499.1524369768.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)");
            request.AddHeader("Accept-Language", "zh-TW,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Referer", "https://www.generatedata.com/");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            request.AddHeader(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.117 Safari/537.36");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            request.AddHeader("Origin", "https://www.generatedata.com");
            request.AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            request.AddHeader("Host", "www.generatedata.com");
            request.AddParameter(
                "undefined",
                "gdRowOrder=1%2C2%2C3%2C4%2C5%2C6&gdExportType=CSV&gdNumCols=6&gdExportFormat=&configurationID=&gdTitle_1=Id&gdDataType_1=data-type-GUID&gdTitle_2=Name&gdDataType_2=data-type-Names&dtExample_2=Name+Surname&dtOption_2=Name+Surname&gdTitle_3=Phone&gdDataType_3=data-type-Phone&dtExample_3=1-Xxx-Xxx-xxxx&dtOption_3=1-Xxx-Xxx-xxxx&gdTitle_4=Address&gdDataType_4=data-type-StreetAddress&gdTitle_5=Birthday&gdDataType_5=data-type-Date&dtFromDate_5=04%2F21%2F1911&dtToDate_5=04%2F21%2F2019&dtOption_5=Y-m-d&gdTitle_6=Code&gdDataType_6=data-type-NumberRange&dtNumRangeMin_6=1&dtNumRangeMax_6=100000&gdNumRowsToAdd=1&etCSV_delimiter=%7C&etCSV_lineEndings=Windows&etHTMLExportFormat=table&etHTMLCustomHTMLSource=%7Bif+%24isFirstBatch%7D%0D%0A%3C!DOCTYPE+html%3E%0D%0A%3Chtml%3E%0D%0A%3Chead%3E%0D%0A%09%3Cmeta+charset%3D%22utf-8%22%3E%0D%0A%09%3Cstyle+type%3D%22text%2Fcss%22%3E%0D%0A%09body+%7B+margin%3A+10px%3B+%7D%0D%0A%09table%2C+th%2C+td%2C+li%2C+dl+%7B+font-family%3A+%22lucida+grande%22%2C+arial%3B+font-size%3A+8pt%3B+%7D%0D%0A%09dt+%7B+font-weight%3A+bold%3B+%7D%0D%0A%09table+%7B+background-color%3A+%23efefef%3B+border%3A+2px+solid+%23dddddd%3B+width%3A+100%25%3B+%7D%0D%0A%09th+%7B+background-color%3A+%23efefef%3B+%7D%0D%0A%09td+%7B+background-color%3A+%23ffffff%3B+%7D%0D%0A%09%3C%2Fstyle%3E%0D%0A%3C%2Fhead%3E%0D%0A%3Cbody%3E%0D%0A%0D%0A%3Ctable+cellspacing%3D%220%22+cellpadding%3D%221%22%3E%0D%0A%3Ctr%3E%0D%0A%7Bforeach+%24colData+as+%24col%7D%0D%0A%09%3Cth%3E%7B%24col%7D%3C%2Fth%3E%0D%0A%7B%2Fforeach%7D%0D%0A%3C%2Ftr%3E%0D%0A%7B%2Fif%7D%0D%0A%7Bforeach+%24rowData+as+%24row%7D%0D%0A%3Ctr%3E%0D%0A%7Bforeach+%24row+as+%24r%7D%09%3Ctd%3E%7B%24r%7D%3C%2Ftd%3E%0D%0A%7B%2Fforeach%7D%0D%0A%3C%2Ftr%3E%0D%0A%7B%2Fforeach%7D%0D%0A%0D%0A%7Bif+%24isLastBatch%7D%0D%0A%3C%2Ftable%3E%0D%0A%0D%0A%3C%2Fbody%3E%0D%0A%3C%2Fhtml%3E%0D%0A%7B%2Fif%7D&etHTMLCustomSmarty=%7Bif+%24isFirstBatch%7D%0D%0A%3C!DOCTYPE+html%3E%0D%0A%3Chtml%3E%0D%0A%3Chead%3E%0D%0A%09%3Cmeta+charset%3D%22utf-8%22%3E%0D%0A%09%3Cstyle+type%3D%22text%2Fcss%22%3E%0D%0A%09body+%7B+margin%3A+10px%3B+%7D%0D%0A%09table%2C+th%2C+td%2C+li%2C+dl+%7B+font-family%3A+%22lucida+grande%22%2C+arial%3B+font-size%3A+8pt%3B+%7D%0D%0A%09dt+%7B+font-weight%3A+bold%3B+%7D%0D%0A%09table+%7B+background-color%3A+%23efefef%3B+border%3A+2px+solid+%23dddddd%3B+width%3A+100%25%3B+%7D%0D%0A%09th+%7B+background-color%3A+%23efefef%3B+%7D%0D%0A%09td+%7B+background-color%3A+%23ffffff%3B+%7D%0D%0A%09%3C%2Fstyle%3E%0D%0A%3C%2Fhead%3E%0D%0A%3Cbody%3E%0D%0A%0D%0A%3Ctable+cellspacing%3D%220%22+cellpadding%3D%221%22%3E%0D%0A%3Ctr%3E%0D%0A%7Bforeach+%24colData+as+%24col%7D%0D%0A%09%3Cth%3E%7B%24col%7D%3C%2Fth%3E%0D%0A%7B%2Fforeach%7D%0D%0A%3C%2Ftr%3E%0D%0A%7B%2Fif%7D%0D%0A%7Bforeach+%24rowData+as+%24row%7D%0D%0A%3Ctr%3E%0D%0A%7Bforeach+%24row+as+%24r%7D%09%3Ctd%3E%7B%24r%7D%3C%2Ftd%3E%0D%0A%7B%2Fforeach%7D%0D%0A%3C%2Ftr%3E%0D%0A%7B%2Fforeach%7D%0D%0A%0D%0A%7Bif+%24isLastBatch%7D%0D%0A%3C%2Ftable%3E%0D%0A%0D%0A%3C%2Fbody%3E%0D%0A%3C%2Fhtml%3E%0D%0A%7B%2Fif%7D&etJSON_dataStructureFormat=complex&etProgrammingLanguage_language=JavaScript&etSQL_tableName=myTable&etSQL_databaseType=MySQL&etSQL_createTable=on&etSQL_dropTable=on&etSQL_encloseWithBackquotes=on&etSQL_statementType=insert&etSQL_insertBatchSize=10&etSQL_primaryKey=default&etXMLRootNodeName=records&etXMLRecordNodeName=record&etXMLCustomHTMLSource=%7Bif+%24isFirstBatch%7D%0D%0A%3C%3Fxml+version%3D%221.0%22+encoding%3D%22UTF-8%22+%3F%3E%0D%0A%3Crecords%3E%0D%0A%7B%2Fif%7D%0D%0A%7Bforeach+%24rowData+as+%24row%7D%0D%0A%09%3Crecord%3E%0D%0A%7Bforeach+from%3D%24colData+item%3Dcol+name%3Dc%7D%0D%0A%09%09%3C%7B%24col%7D%3E%7B%24row%5B%24smarty.foreach.c.index%5D%7D%3C%2F%7B%24col%7D%3E%0D%0A%7B%2Fforeach%7D%0D%0A%09%3C%2Frecord%3E%0D%0A%7B%2Fforeach%7D%0D%0A%7Bif+%24isLastBatch%7D%0D%0A%3C%2Frecords%3E%0D%0A%7B%2Fif%7D&etXMLCustomSmarty=%7Bif+%24isFirstBatch%7D%0D%0A%3C%3Fxml+version%3D%221.0%22+encoding%3D%22UTF-8%22+%3F%3E%0D%0A%3Crecords%3E%0D%0A%7B%2Fif%7D%0D%0A%7Bforeach+%24rowData+as+%24row%7D%0D%0A%09%3Crecord%3E%0D%0A%7Bforeach+from%3D%24colData+item%3Dcol+name%3Dc%7D%0D%0A%09%09%3C%7B%24col%7D%3E%7B%24row%5B%24smarty.foreach.c.index%5D%7D%3C%2F%7B%24col%7D%3E%0D%0A%7B%2Fforeach%7D%0D%0A%09%3C%2Frecord%3E%0D%0A%7B%2Fforeach%7D%0D%0A%7Bif+%24isLastBatch%7D%0D%0A%3C%2Frecords%3E%0D%0A%7B%2Fif%7D&gdNumRowsToGenerate=100&gdExportTarget=inPage&action=generateInPage&gdBatchSize=100&gdCurrentBatchNum=1",
                ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeAnonymousType(
                response.Content,
                new { Success = default(bool), Content = default(string), IsComplete = default(bool) }).Content;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // 取得假資料
            var count = File.ReadAllLines(@"D:\sample.txt").Length;
            var rand = new Random(Guid.NewGuid().GetHashCode());

            while (true)
            {
                IEnumerable<string> rawSamples = null;

                await Task.Run(
                    () =>
                        {
                            try
                            {
                                rawSamples = GetRawSamples().Split(
                                    new[] { "\r\n" },
                                    StringSplitOptions.RemoveEmptyEntries).Skip(1);

                                if (rawSamples != null && rawSamples.Any())
                                {
                                    File.AppendAllLines(@"D:\sample.txt", rawSamples);
                                }
                            }
                            catch
                            {
                                Thread.Sleep(5000);
                            }
                        });

                count += rawSamples?.Count() ?? 0;

                this.textBox1.Text = count.ToString();

                if (count >= 150000) break;

                await Task.Delay(rand.Next(1000, 2000));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                // 直接用自訂型別當 Collection Name
                var collection1 = db.GetCollection<Sample>();

                // 額外定義一個 Collection Name
                var collection2 = db.GetCollection<Sample>("customer");

                // 沒有自訂型別也可以建立 Collection
                var collection3 = db.GetCollection("customer");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Sample>();

                // 新增單筆資料
                collection.Insert(new Sample());

                // 新增多筆資料
                collection.Insert(new List<Sample>());
                collection.InsertBulk(new List<Sample>());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var id = Guid.NewGuid();

            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Sample>();

                // 刪除單筆資料
                collection.Delete(id);

                // 條件式刪除資料
                collection.Delete(x => x.Birthday > new DateTime(2000, 1, 1));
                collection.Delete(Query.GT("Birthday", new DateTime(2000, 1, 1)));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var id = Guid.NewGuid();

            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Sample>();

                var doc = collection.FindById(id);

                doc.Name = "Hello World";

                // 更新單筆資料
                collection.Update(doc);
                collection.Update(id, doc);

                var docs = collection.Find(x => x.Birthday > new DateTime(2000, 1, 1));

                foreach (var sample in docs)
                {
                    sample.Birthday = new DateTime(2000, 1, 1);
                }

                // 更新多筆資料
                collection.Update(docs);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var id = Guid.NewGuid();

            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Sample>();

                var doc = new Sample
                              {
                                  Id = id,
                                  Name = "Hello World",
                                  Phone = "+886912345678",
                                  Address = "中和",
                                  Birthday = new DateTime(2000, 1, 1),
                                  Code = 12345
                              };

                // Insert Or Update 單筆資料
                collection.Upsert(doc);
                collection.Upsert(id, doc);

                // Insert Or Update 多筆資料
                collection.Upsert(new List<Sample> { doc });
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Sample>();

                // 給入 Lambda Expression 當條件
                var results1 = collection.Find(x => x.Name.Equals("Johnny Smith"));

                // 給入 Query Method 當條件
                var results2 = collection.Find(Query.EQ(nameof(Sample.Name), "Johnny Smith"));
            }
        }
    }
}