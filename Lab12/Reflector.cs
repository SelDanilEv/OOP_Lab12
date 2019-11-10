using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Lab12
{
    static class Reflector
    {
        private static string _dataway = @"data.txt";
        private static string _parameterway = @"param.txt";

        public static string addNamespace(string str) => "Lab12." + str;

        public static void InfoAboutClassToFile(string className)       //запись в файл по имени
        {
            Console.WriteLine($"Info about class was written in file. Class: {className}\n");
            Type myType = Type.GetType(addNamespace(className));
            using (StreamWriter sw = new StreamWriter(_dataway, false, System.Text.Encoding.Default))
            {
                foreach (MemberInfo item in myType.GetMembers())
                {
                    sw.WriteLine($"{item.DeclaringType} {item.MemberType} {item.Name}");
                }
                sw.Close();
            }
        }

        public static void ShowPublicMethods(string className)          // публичные методы
        {
            Console.WriteLine($"(Class: {className})Show public methods:");
            Type myType = Type.GetType(addNamespace(className));
            MethodInfo[] methods = myType.GetMethods();    //все методы
            foreach (MethodInfo method in methods)
            {
                string outstring = "";
                outstring += $"public {method.ReturnType.Name} {method.Name} (";
                ParameterInfo[] parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    outstring += $"{parameters[i].ParameterType.Name} {parameters[i].Name}";
                    if (i + 1 < parameters.Length) outstring += ", ";
                }
                outstring += ')';
                Console.WriteLine(outstring);
            }
        }

        public static void ShowFieldsAndProperties(string className)
        {
            Type myType = Type.GetType(addNamespace(className));
            string outstring = $"\n(Class: {className})Show fiels and properties:\n";

            outstring += "Поля:";
            foreach (FieldInfo field in myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static))
            {
                outstring += $"\n{field.FieldType} {field.Name}";
            }

            outstring += "\nСвойства:";
            foreach (PropertyInfo prop in myType.GetProperties())
            {
                outstring += $"\n{prop.PropertyType} {prop.Name}";
            }
            Console.WriteLine(outstring);
        }

        public static void ShowInterfaces(string className)
        {
            Type myType = Type.GetType(addNamespace(className));
            string outstring = $"\n(Class: {className})Realized interfaces:\n";
            foreach (Type i in myType.GetInterfaces())
            {
                outstring += i.Name + '\n';
            }
            Console.WriteLine(outstring);
        }

        public static void ShowMethodsWithParameter(string className, Type parm)
        {
            Type myType = Type.GetType(addNamespace(className));
            string outsrting = $"\n(Class: {className})Show methods with parameters:\n";
            foreach (MethodInfo method in myType.GetMethods())
            {
                foreach (ParameterInfo param in method.GetParameters())
                    if (param.ParameterType == parm)
                    {
                        outsrting += $"{method.ReturnType.Name} {method.Name} (";
                        ParameterInfo[] parameters = method.GetParameters();
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            outsrting += $"{parameters[i].ParameterType.Name} {parameters[i].Name}";
                            if (i + 1 < parameters.Length) outsrting += ", ";
                        }
                        outsrting += ")\n";
                    }
            }
            Console.WriteLine(outsrting);
        }

        public static void CallMethod(string className, string MethodName)
        {
            Console.WriteLine($"\nInvoke method {MethodName} in {className} class");

            Assembly asm = Assembly.LoadFrom("Lab12.exe");
            Type t = asm.GetType(addNamespace(className));

            object obj = Activator.CreateInstance(t);

            MethodInfo method = t.GetMethod(MethodName);

            int[] parm;
            using (FileStream fstream = File.OpenRead(_parameterway))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                char[] separators = { ' ' };
                string[] temp;
                temp = textFromFile.Split(separators);
                parm = new int[temp.Length];
                for(int i = 0; i < temp.Length;i++)
                {
                    int.TryParse(temp[i],out parm[i]);
                }
                fstream.Close();
            }
            switch (parm.Length)
            {
                case 0:
                    method.Invoke(obj, new object[] { });
                    break;
                case 1:
                    method.Invoke(obj, new object[] { parm[0] });
                    break;
                case 2:
                    method.Invoke(obj, new object[] { parm[0] , parm[1] });
                    break;
            }
        }
    }
}
