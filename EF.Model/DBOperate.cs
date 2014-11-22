using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Model
{
    public class DBOperate
    {

        static readonly ILog logFile = LogManager.GetLogger("logFile");
        static readonly ILog logConsole = LogManager.GetLogger("logConsole");

        public static void Init()
        {
          
            //    Database.SetInitializer<NorthwindContext>(new DropCreateDatabaseAlways<NorthwindContext>());
            //    Database.SetInitializer<SourthwindContext>(new DropCreateDatabaseAlways<SourthwindContext>());
            //IObjectContextAdapter
            NorthwindContext context = new NorthwindContext();
            if (!context.Database.Exists())
            {
                //context.Database.Initialize(false);
                context.Database.Create();
                logConsole.Debug("数据库创建成功");
            }

            //builder.Entity<Category>().HasKey(b => b.CategoryID);
            //builder.Entity<Category>().HasMany(p => p.Tables).WithMany(l => l.Lodgings).HasForeignKey(p => p.TarDestinationId);
            DBOperate.InsertData();

            //var result = (from dc in dbndata
            //              join sc in dbs.SourthCategories on dc.CategoryID equals sc.CategoryID
            //              select new
            //              {
            //                  CategoryID = dc.CategoryID,
            //                  CategoryName = dc.CategoryName
            //              }).ToList();
            logFile.Info("Finish");
            logConsole.Info("Finish");
            Console.ReadLine();
        }

        /// <summary>
        /// 重复打开链接
        /// </summary>
        public static void InsertDataReOpen()
        {
            #region Insert Data

            for (int i = 0; i < 1000; i++)
            {

                Category c = new Category()
                {
                    CategoryName = "电子数码" + i.ToString()
                };
                Activity a = new Activity()
                {
                    Name = "ac" + i.ToString()
                };
                Trip tr = new Trip()
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                };
                using (NorthwindContext db = new NorthwindContext())
                {
                    db.Categories.Add(c);
                    c.Tables = new List<Table>();
                    Table t = new Table()
                    {
                        TableName = "Test" + i.ToString(),
                        CreateTime = DateTime.Now
                    };
                    c.Tables.Add(t);

                    //db.Acitvitys.Add(a);
                    //a.Trips = new List<Trip>();
                    //a.Trips.Add(tr);

                    db.SaveChanges();

                    var ts = db.Tables;

                    Console.WriteLine(ts.ToString());

                }
                logFile.Info(i);

                using (SourthwindContext db = new SourthwindContext())
                {
                    SourthCategory sc = new SourthCategory()
                    {
                        CategoryName = "电子数码"
                    };
                    db.SourthCategories.Add(sc);
                    db.SaveChanges();

                }

            }
            #endregion
        }

        /// <summary>
        /// 重复打开链接
        /// </summary>
        public static void InsertData()
        {
            #region Insert Data
            using (NorthwindContext db = new NorthwindContext())
            {
                for (int i = 0; i < 100; i++)
                {

                    Category c = new Category()
                    {
                        CategoryName = "电子数码" + i.ToString(),
                        Order2 = i,
                        //ExtendProp=i.ToString()
                    };

                    db.Categories.Add(c);
                    c.Tables = new List<Table>();
                    Table t = new Table()
                    {
                        TableName = "Test" + i.ToString(),
                        CreateTime = DateTime.Now
                    };
                    Table t2 = new Table()
                    {
                        TableName = "Test" + i.ToString() + i.ToString(),
                        CreateTime = DateTime.Now
                    };
                    c.Tables.Add(t);
                    c.Tables.Add(t2);
                    logFile.Info("Category" + i);
                    logConsole.Info("Category" + i);
                }
                db.SaveChanges();

                //var ts = db.Tables;

                //Console.WriteLine(ts.ToString());



            }
            using (SourthwindContext db = new SourthwindContext())
            {
                for (int i = 0; i < 1000; i++)
                {
                    SourthCategory sc = new SourthCategory()
                    {
                        CategoryName = "电子数码" + i.ToString()
                    };
                    db.SourthCategories.Add(sc);

                }
                db.SaveChanges();
            }
            #endregion
        }

        public static void SelectData()
        {
            NorthwindContext dbn = new NorthwindContext();
            SourthwindContext dbs = new SourthwindContext();
            var dbndata = dbn.Categories.Select(k => k.CategoryID).ToList();
            var result = (from sc in dbs.SourthCategories
                          //  join dc in dbndata on sc.CategoryID equals dc.CategoryID
                          where dbndata.Contains(sc.CategoryID)
                          select new
                          {
                              CategoryID = sc.CategoryID,
                              CategoryName = sc.CategoryName
                          }).ToList();
        }
    }
}
