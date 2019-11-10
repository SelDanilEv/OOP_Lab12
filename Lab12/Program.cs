using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            Reflector.InfoAboutClassToFile("Student");
            Reflector.ShowPublicMethods("Student");
            Reflector.ShowFieldsAndProperties("Student");
            Reflector.ShowInterfaces("Student");
            Reflector.ShowMethodsWithParameter("Student",typeof(int));
            Reflector.CallMethod("Student", "Study");
            

            Console.ReadKey();
        }
    }
}
