using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF.Model
{
    //public partial class Category
    //{
    //    public string ExtendProp { get; set; }
    //}

    public class CategoryExtend
    {
        public CategoryExtend()
        {
            Category = new Category();
        }
        Category Category;
        public static string ExtendProp { get; set; }

        public Type BuildExtendProp()
        {

            PropertyInfo[] pis = typeof(CategoryExtend).GetProperties();
            //ExtendProperty.RegisterProperty

            //dynamic user = user.AsDynamic(); 
            sample sam = new sample();
            return sam.BuildExtendType(typeof(Category), pis);
            //Category cat = new Category();
        }

        public void SetValue(string val,object value)
        {
            var prop = typeof(Category).GetProperty(val);
            if (prop != null)
            {
                prop.SetValue(Category, value);
            }
            
        }

        public object GetValue(string val)
        {
            var prop = typeof(Category).GetProperty(val);
            if (prop != null)
            {
               return prop.GetValue(Category);
            }
            return null;
        }
    }



    class sample
    {

        public Type BuildExtendType(Type sourceType, PropertyInfo[] extenders)
        {
            StringBuilder codeBuilder = new StringBuilder();
            CodeDomProvider comp = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            codeBuilder.Append("using System;");
            codeBuilder.Append("using Microsoft.JScript;");
            codeBuilder.Append("public class ExtendObject{");
            codeBuilder.Append(PrepareProperties(sourceType.GetProperties()));
            codeBuilder.Append(PrepareMappingProperties(extenders));
            codeBuilder.Append("}");
            cp.ReferencedAssemblies.Contains("System.dll");
            cp.ReferencedAssemblies.Add("Microsoft.JScript.dll");
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            string code = codeBuilder.ToString();
            CompilerResults cr = comp.CompileAssemblyFromSource(cp, code);
            if (cr.Errors.HasErrors)
            {
                string description = string.Empty;
                foreach (CompilerError e in cr.Errors)
                {
                    description += e.ErrorText;
                }
                throw new Exception(description);
            }
            else
            {
                Assembly a = cr.CompiledAssembly;
                return a.GetType("ExtendObject");
            }
        }

        protected string PrepareProperties(PropertyInfo[] ps)
        {
            StringBuilder sProperties = new StringBuilder();
            foreach (PropertyInfo p in ps)
            {
                sProperties.Append("protected ");
                sProperties.Append(p.PropertyType.FullName);
                sProperties.Append(" ");
                sProperties.Append("_" + p.Name);
                sProperties.Append(";");

                sProperties.Append("public ");
                sProperties.Append(p.PropertyType.FullName);
                sProperties.Append(" ");
                sProperties.Append(p.Name);
                sProperties.Append("{get{return " + "_" + p.Name + ";}set{" + "_" + p.Name + "= value;}}");
            }
            return sProperties.ToString();
        }

        protected string PrepareMappingProperties(PropertyInfo[] ps)
        {
            //Samples: ITAB_BILL_TEMP_NAME
            StringBuilder sProperties = new StringBuilder();
            foreach (PropertyInfo p in ps)
            {
                sProperties.Append("protected ");
                sProperties.Append(p.PropertyType.FullName);
                sProperties.Append(" ");
                sProperties.Append("_" + p.DeclaringType.Name.Substring(11) + "_" + p.Name);
                sProperties.Append(";");

                sProperties.Append("public ");
                sProperties.Append(p.PropertyType.FullName);
                sProperties.Append(" ");
                sProperties.Append(p.DeclaringType.Name.Substring(11) + "_" + p.Name);
                sProperties.Append("{get{return " + "_" + p.DeclaringType.Name.Substring(11) + "_" + p.Name + ";}set{" + "_" + p.DeclaringType.Name.Substring(11) + "_" + p.Name + " = value;}}");
            }
            return sProperties.ToString();
        }
    }
}
