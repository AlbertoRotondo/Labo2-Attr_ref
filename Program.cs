using System;
using System.Reflection;
using MyAttribute;

namespace Labo2_Attr_ref
{

    class Programs
    {
        static void Main(string[] args)
        {
            var a = Assembly.LoadFrom("MyLibrary.dll");

            foreach (var type in a.GetTypes())
            {
                Object o = Activator.CreateInstance(type, null);


                foreach (var m in type.GetMethods(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly))
                {
                    System.Console.Write("\n");

                    MethodInfo methodInfo = type.GetMethod(m.Name);
                    ExecuteMe[] authorAttributes =
                    (ExecuteMe[]) methodInfo.GetCustomAttributes<ExecuteMe>(false);
                    
                    foreach (var aa in authorAttributes)
                    {
                        try
                        {
                            switch (methodInfo.GetParameters().Length)
                            {
                                case 0:
                                    m.Invoke(o, null);
                                    break;
                                case 1:
                                    m.Invoke(o, new object[] {aa.age});
                                    break;
                                case 2:
                                    m.Invoke(o, new object[] {aa.Name, aa.surname});
                                    break;
                            }
                        }
                        catch (TargetParameterCountException M)
                        {
                            System.Console.WriteLine( M.Message);
                        }
                    }


                }


            }
        }
    }
}
