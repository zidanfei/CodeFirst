using EF.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CFLocalDB
{
    class Program
    {     
        static void Main(string[] args)
        {
            string app_data = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\";
            AppDomain.CurrentDomain.SetData("DataDirectory", app_data);
            DBOperate.Init();
        }

    }
}
