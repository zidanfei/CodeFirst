using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CFSqlserver
{
    class Program
    {
        static void Main(string[] args)
        {
            Category d = new Category();
            CategoryExtend ce = new CategoryExtend();
            ce.BuildExtendProp();
            ce.SetValue("ExtendProp", "a");
            var v = ce.GetValue("ExtendProp");
            DBOperate.Init();
        }
    }
}
