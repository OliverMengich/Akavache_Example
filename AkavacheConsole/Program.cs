using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Akavache;
using System.Reactive.Linq;
using System.Xml.Serialization;
using System.IO;

namespace AkavacheConsole
{
    

    
    class Program
    {
        public static RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
        public static RSAParameters _privateKey;
        public static RSAParameters _publicKey;
        public static Dictionary<string, RSAParameters> keyValuePairs;
        
        public Program()
        {
            
            _publicKey = csp.ExportParameters(false);
            _privateKey = csp.ExportParameters(true);
        }
        static void Main(string[] args)
        {
            AkavacheExample();
            FetchingResults();
            Console.WriteLine("\n \n");
            AnotherRetrieveClass another = new AnotherRetrieveClass();
            another.RetrieveValues();
            Console.ReadKey();
        }
        public async static void AkavacheExample()
        {
            Akavache.BlobCache.ApplicationName = "AkavacheConsole";
            keyValuePairs = new Dictionary<string, RSAParameters>();
            keyValuePairs.Add("Public Key", _publicKey);
            keyValuePairs.Add("Private Key", _privateKey);
            MyInfo info = new MyInfo();
            info.Name = "Oliver Mengich";
            info.Id = 37546727;
            info.School = "School Of Engineering";
            Akavache.BlobCache.ApplicationName = "AkavacheExample";
            await BlobCache.LocalMachine.InsertObject<MyInfo>("Oliver", info);
            await BlobCache.LocalMachine.InsertObject("Values", keyValuePairs);

        }
        public string PublicKeyString(RSAParameters rsa)
        {
            // working with string function
            var sw = new StringWriter();
            // public key is produced as an XML file. to change it to a string 
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);

            return sw.ToString(); // produce the public key
        }
        public string PrivateKeyString(RSAParameters rsa)
        {
            // working with string function
            var sw = new StringWriter();
            // public key is produced as an XML file. to change it to a string 
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _privateKey);

            return sw.ToString(); // produce the public key
        }
        public async static void FetchingResults()
        {
            Program p = new Program();
            var final = await BlobCache.LocalMachine.GetObject<MyInfo>("Oliver");
            Console.WriteLine("Student: "+final.Name+"ID number: "+final.Id+" Of School: "+final.School+"\n \n \n");
            var retrived = await BlobCache.LocalMachine.GetObject<Dictionary<string, RSAParameters>>("Values");
            //Console.WriteLine(retrived["Public Key"]);
            Console.WriteLine("Private Key is: " +p.PrivateKeyString(retrived["Private Key"]));
            Console.WriteLine("Public Key is: " + p.PublicKeyString(retrived["Public Key"]));
        }
    }
    public class MyInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string School { get; set; }
    }
}
