using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace EF.Model
{
    public class SourthwindContext : DbContext
    {
        public SourthwindContext()
            :base("SourthwindContext")//config链接
            //: base("Data Source=(local); initial catalog=eftestsouth; User ID=sa; Password=" + Config.password + "; MultipleActiveResultSets=True")
        {
        }
        public DbSet<SourthCategory> SourthCategories
        {
            get;
            set;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().Property(p => p.Order).HasPrecision(18, 2);
            modelBuilder.Configurations.Add(new SouthCategoryConfig());
        }
    }


    public class SourthCategory
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryID
        {
            get;
            set;
        }

        //public int ID { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required, MaxLength(50)]
        public string CategoryName
        {
            get;
            set;
        }       

        public string Size
        {
            get;
            set;
        }

        public decimal Order
        {
            get;
            set;
        }
    }

    public class SouthCategoryConfig : EntityTypeConfiguration<SourthCategory>
    {
        public SouthCategoryConfig()
        {
            this.HasKey(c => c.CategoryID);
            this.ToTable("EF_SourthCategory");
        }
    }

}
