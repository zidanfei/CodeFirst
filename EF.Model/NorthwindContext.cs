using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Model
{
    public class Config
    {
        //public static string password = "1q@";
        public static string password = "pass@word1";
        //public static string password = "iws@prod1";
    }
    public class NorthwindContext : DbContext
    {

        public NorthwindContext()
            :base("NorthwindContext")//config链接
            //: base("Data Source=(local); initial catalog=eftest; User ID=sa; Password=" + Config.password + "; MultipleActiveResultSets=True")
        {
        }
        public DbSet<Category> Categories
        {
            get;
            set;
        }
        public DbSet<Table> Tables
        {
            get;
            set;
        }
        //public DbSet<Activity> Acitvitys
        //{
        //    get;
        //    set;
        //}

        //public DbSet<Trip> Trips
        //{
        //    get;
        //    set;
        //}

        //public DbSet<VTable> VTables
        //{
        //    get;
        //    set;
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().Property(p => p.Order).HasPrecision(18, 2);
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new TableConfig());
            //modelBuilder.Entity<Category>().ToTable("");
            //modelBuilder.Configurations.Add(new VTableConfig());


            ///轻量级约定Lightweight Conventions默认优先使用TableAttribute指定的表名
            ///CategoryConfig 中totable设置后，这里配置不起作用
            //modelBuilder.Types().Configure(f => f.ToTable("cms_" + f.ClrType));

            ///Configuration Conventions（配置型约定）
            ///未完成
            modelBuilder.Conventions.Add<StringMaxLengthConvertion>();
        }
    }

    public class StringMaxLengthConvertion : Convention
    {
        public StringMaxLengthConvertion()
        {
            this.Properties().Having(p => (ColumnAttribute)p.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault()).Configure((c, a) => c.HasColumnName(a.Name).HasMaxLength(50));
            ///未找到ColumnLengthAttribute命名空间
            //this.Properties().Having(p=>(ColumnLengthAttribute)p.GetCustomAttributes(typeof(ColumnLengthAttribute),true).FirstOrDefault()).Configure((c,a)=>c.HasColumnName(a.ColumnName).HasMaxLength(a.ColumnMaxLength));
        }
    }
  //  public class DefaultTableConvention :
  //IConfigurationConvention<Type, EntityTypeConfiguration>
  //  {
  //      public void Apply(
  //          Type type,
  //          Func<EntityTypeConfiguration> configuration)
  //      {
  //          TableAttribute[] tableAttributes = (TableAttribute[])type.GetCustomAttributes(typeof(TableAttribute), false);

  //          if (tableAttributes.Length == 0)
  //          {
  //              configuration().ToTable("EF_" + type.Name);
  //          }
  //      }
  //  }


    public class NorthwindContextJoin : DbContext
    {
        public NorthwindContextJoin()
            : base("NorthwindContextJoin")
        {
        }
        public DbSet<Category> Categoriesjoin
        {
            get;
            set;
        }

        public DbSet<Activity> Acitvitysjoin
        {
            get;
            set;
        }

        public DbSet<Trip> Tripsjoin
        {
            get;
            set;
        }

        public DbSet<Table> Tablesjoin
        {
            get;
            set;
        }
        public DbSet<VTable> VTablesjoin
        {
            get;
            set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().Property(p => p.Order).HasPrecision(18, 2);
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new TableConfig());
            modelBuilder.Configurations.Add(new VTableConfig());
        }
    }


}
