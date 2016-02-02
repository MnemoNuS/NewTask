using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.Web;

namespace CreateRSS
{
    class Parce
    {
    //    //static void Main(string[] args)
    //    //{
            
    //    //    print("http://lenta.ru/rss");

    //    //    Console.WriteLine();

    //    //}


  
        static public void RSS(string url)
        {
            HttpWebRequest NewReq = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse NewRes = (HttpWebResponse)NewReq.GetResponse();
            
            StreamReader NewRssStream = new StreamReader(NewRes.GetResponseStream(), Encoding.UTF8);

            StreamWriter NewWriter = new StreamWriter("demo.xml");
            
            NewWriter.WriteLine(NewRssStream.ReadToEnd());

            NewWriter.Close();
            NewRssStream.Close();
           
        }

        
     }
}




