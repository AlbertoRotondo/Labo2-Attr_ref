using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyAttribute;
using MyLibrary;

namespace Labo2_Attr_ref
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Assembly.LoadFrom("MyLibrary.dll");
            //Console.WriteLine(a.GetCustomAttributes(true).GetValue(10));

            foreach (var type in a.GetTypes())
            {
                Object o = Activator.CreateInstance(type, null);
                var obj = new Object[] { };


                if (type.IsClass)
                    Console.WriteLine(type.FullName);
                // Console.ReadLine();

                foreach (var m in type.GetMethods())
                {
                    //Console.Write(m);
                    //Console.Write("\n");


                    MethodInfo methodInfo = type.GetMethod(m.Name);
                    ExecuteMe[] authorAttributes =
                        (ExecuteMe[]) methodInfo.GetCustomAttributes<ExecuteMe>(false);

                    foreach (var aa in authorAttributes)
                    {

                        Array.Resize(ref obj, obj.Length + authorAttributes.Length);
                        for (int i = 0; i < authorAttributes.Length; i++)
                        {
                            if (aa.age != null)
                                obj[i] = aa.age; Console.WriteLine("{0}({1})", aa, aa.age);
                            if (aa.Name != "")
                                obj[i] = aa.Name; Console.WriteLine("{0}({1})", aa, aa.Name);
                        }





                        MethodInfo iMethod = type.GetMethod(methodInfo.ToString());
                        // object invMethod = iMethod.Invoke(o, new object[] { 100 });


                    }




                }








                //Aggiungete al Main del codice che, sfruttando la reflection, invochi tutti i metodi pubblici(di 
                //tutte le classi trovate nella DLL)
                //che siano stati annotati con[ExecuteMe], passando come argomenti gli argomenti dell'annotazione.

                //Facendo riferimento alla classe Foo mostrata sopra, M1 dovrà essere invocato senza parametri,
                //il metodo M2 dovrà essere invocato tre volte(con argomenti: 3, 0 e 45) e M3 dovrà essere invocato una 
                //volta con argomenti s1 = "hello" e s2 = "reflection".

            }
        }
    }
}
