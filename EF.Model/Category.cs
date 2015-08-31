using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Model
{
    //http://blog.163.com/m13864039250_1/blog/static/21386524820152141548357/ EF 提供了一系列属性用于描述模型
    public partial class Category
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
        
        public string CategoryType
        {
            get;
            set;
        }
        public List<Table> Tables
        {
            get;
            set;
        }

        public List<Table> Tables2
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
        [NotMapped]
        public decimal Order2
        {
            get;
            set;
        }
        public decimal Order3
        {
            get;
            set;
        }
        //public decimal Order4
        //{
        //    get;
        //    set;
        //}
        //public decimal? Order5
        //{
        //    get;
        //    set;
        //}
    }

    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            this.HasKey(c => c.CategoryID);
            //this.ToTable("EF_Category");
        }
    }

    public partial class Table
    {
        public int TableID
        {
            get;
            set;
        }

        [Required, MaxLength(50)]
        public string TableName
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public string IsDrop
        {
            get;
            set;
        }
        //[ForeignKey("Category")]
        //public int CategoryID { get; set; }

        //public int CategoryID2 { get; set; }

        //[ForeignKey("CategoryID")]
        //[InverseProperty("Tables")]
        public Category Category
        {
            get;
            set;
        }

        //[InverseProperty("Tables2")]
        public Category Category2
        {
            get;
            set;
        }
    }

    public class TableConfig : EntityTypeConfiguration<Table>
    {
        public TableConfig()
        {
            this.HasKey(c => c.TableID);
            this.Property(t => t.TableName).HasColumnName("EF_TableName");
            this.HasOptional(p => p.Category2).WithMany(l => l.Tables2);
            this.HasOptional(p => p.Category).WithMany(l => l.Tables);
            //this.HasRequired(c => c.Category);
            //this.HasRequired(c => c.Category2);
            this.ToTable("EF_Table");
        }
    }

    public class VTable
    {

        public int TableID
        {
            get;
            set;
        }
        public int TableID2
        {
            get;
            set;
        }

        public Nullable<int> CategoryID
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }

        public string TableName
        {
            get;
            set;
        }

        //public DateTime CreateTime { get; set; }

    }

    public class VTableConfig : EntityTypeConfiguration<VTable>
    {
        public VTableConfig()
        {
            this.HasKey(c => new
            {
                c.TableID,
                c.TableID2
            });
            //this.Property(t => t.TableID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); 
            this.Property(t => t.TableName).HasColumnName("EF_TableName");
            this.Property(t => t.TableID).HasColumnName("TableID");

            //this.HasRequired(c => c.Category);
            //this.HasRequired(c => c.Category2);
            //this.ToTable("VTable");
        }
    }

    public partial class Activity
    {
        public int ActivityId
        {
            get;
            set;
        }
        [Required, MaxLength(50)]
        public string Name
        {
            get;
            set;
        }
        public List<Trip> Trips
        {
            get;
            set;
        }
    }

    public class ActivityConfig : EntityTypeConfiguration<Activity>
    {
        public ActivityConfig()
        {
            this.HasMany(a => a.Trips).WithMany(t => t.Activities).Map(m =>
                 {
                     m.ToTable("TripActivities");
                     m.MapLeftKey("ActivityId");//对应Activity的主键
                     m.MapRightKey("TripIdentifier");
                 });
            this.ToTable("EF_Activity");
        }
    }

    public partial class Trip
    {
        public int TripId
        {
            get;
            set;
        }
        public DateTime StartDate
        {
            get;
            set;
        }
        public DateTime EndDate
        {
            get;
            set;
        }
        public decimal CostUSD
        {
            get;
            set;
        }
        public byte[] RowVersion
        {
            get;
            set;
        }
        public List<Activity> Activities
        {
            get;
            set;
        }
    }
}
