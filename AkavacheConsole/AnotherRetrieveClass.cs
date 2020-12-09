using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using System.Security.Cryptography;
using System.Reactive.Linq;

namespace AkavacheConsole
{
    public class AnotherRetrieveClass
    {
        public async void RetrieveValues()
        {
            var final = await BlobCache.LocalMachine.GetObject<MyInfo>("Oliver");
            Console.WriteLine("Student: " + final.Name + "ID number: " + final.Id + " Of School: " + final.School + "\n \n \n");
        }
    }
}
