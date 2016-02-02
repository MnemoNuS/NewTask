using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using CreateRSS;


namespace SomeTestHowToWorkWithXML
{

    public class MyItem
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string pubDate { get; set; }
        public string category{ get; set; }
    public MyItem()
        {
            title = "";
            link = "";
            description = "";
            pubDate = "";
            category = "";
        }
        
    }

    
    class MyProgram
    {
        static void Main(string[] args)
        {
            
            string pathToXml = "demo.xml";
            string chanelURL = "";

            string[] listOfChanels = new string [3];
            listOfChanels[0] = "http://news.yandex.ru/hardware.rss";
            listOfChanels[1] = "http://lenta.ru/rss";
            listOfChanels[2] = "http://www.1tv.ru/rss/rss_all.xml";
            
            foreach (string tempText in listOfChanels)
            {
                Console.WriteLine(tempText);
            }

            string KeyPressed=Console.ReadLine();
            switch (KeyPressed)
            {
                case "1":
                chanelURL =listOfChanels[0];
                break;
                case "2":
                chanelURL = listOfChanels[1];
                break;
                case "3":
                chanelURL = listOfChanels[2];
                break;
            }


            Parce.RSS(chanelURL);

            //CreateXML(pathToXml);
            ReadXml(pathToXml);
            Console.ReadLine();
        }
        private static void CreateXML(string pathToXml)
        {

            XmlTextWriter textWritter = new XmlTextWriter(pathToXml, Encoding.UTF8);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("head");
            textWritter.WriteEndElement();
            textWritter.Close();


            XmlDocument doc = new XmlDocument();
            doc.Load(pathToXml);

            for (int i = 0; i < 3; i++)
            {

                XmlNode element = doc.CreateElement("User");
                doc.DocumentElement.AppendChild(element);

                XmlAttribute atribute = doc.CreateAttribute("number");
                atribute.Value = "1";

                element.Attributes.Append(atribute);

                XmlNode subElement1 = doc.CreateElement("FirstName");
                subElement1.InnerText = "Mike";
                element.AppendChild(subElement1);

                XmlNode subElement2 = doc.CreateElement("SecondName");
                subElement2.InnerText = "Ross";
                element.AppendChild(subElement2);

                XmlNode subElement3 = doc.CreateElement("Age");
                subElement3.InnerText = "30";
                element.AppendChild(subElement3);

                //XmlNode element = document.CreateElement("element");
                //document.DocumentElement.AppendChild(element); // указываем родителя
                //XmlAttribute attribute = document.CreateAttribute("number"); // создаём атрибут
                //attribute.Value = 1; // устанавливаем значение атрибута
                //element.Attributes.Append(attribute); // добавляем атрибут

                //Добавляем в запись данные:
                //XmlNode subElement1 = document.CreateElement("subElement1"); // даём имя
                //subElement1.InnerText = "Hello"; // и значение
                //element.AppendChild(subElement1); // и указываем кому принадлежит

                doc.Save(pathToXml);

            }

        }

        static void ReadXml(string pathToXml)
        {
            //{
            //    string name, pwd; // Новые переменные имени и пароля  

            //    // Объявляем и забиваем файл в документ  
            //    XmlDocument xd = new XmlDocument();
            //    FileStream fs = new FileStream(filepath, FileMode.Open);
            //    xd.Load(fs);

            XmlDocument doc = new XmlDocument();
            FileStream strem = new FileStream(pathToXml, FileMode.Open);
            doc.Load(strem);

        SomeTestHowToWorkWithXML.MyItem Table = new SomeTestHowToWorkWithXML.MyItem();
            XmlNodeList List = doc.SelectNodes("rss/chanel/item");

           //XmlNodeList List = doc.GetElementsByTagName("item");
            for (int i = 1; i < 3; i++)
            {
                Table.title = doc.GetElementsByTagName("title")[i].InnerText;
                Table.link = doc.GetElementsByTagName("link")[i].InnerText;
                Table.description = doc.GetElementsByTagName("description")[i].InnerText;
                Table.pubDate = doc.GetElementsByTagName("pubDate")[i].InnerText;
                try
                {
                    Table.category = doc.GetElementsByTagName("category")[i].InnerText;
                }
                catch (Exception)
                {
                    Table.category = " ";
                }

                //XmlElement Link = (XmlElement)doc.GetElementsByTagName("link")[i];
                //XmlElement Description = (XmlElement)doc.GetElementsByTagName("description")[i];
                Console.WriteLine(Table.title);
                Console.WriteLine(Table.link);
                Console.WriteLine(Table.description);
                Console.WriteLine(Table.pubDate);
                Console.WriteLine(Table.category);
            }
            strem.Close();
                
                
                
                //    XmlNodeList list = xd.GetElementsByTagName("user"); // Создаем и заполняем лист по тегу "user"  
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        XmlElement id = (XmlElement)xd.GetElementsByTagName("user")[i];         // Забиваем id в переменную  
            //        XmlElement user = (XmlElement)xd.GetElementsByTagName("login")[i];      // Забиваем login в переменную  
            //        XmlElement pass = (XmlElement)xd.GetElementsByTagName("password")[i];   // Забиваем password в переменную  

            //        if (id.GetAttribute("id") == pid) // Если наткнулся на нужный айдишник  
            //        {
            //            // Вставляем в переменные текст из тегов  
            //            name = user.InnerText;
            //            pwd = pass.InnerText;

            //            // Заполняем поля на форме  
            //            txtRLogin.Text = name;
            //            txtRPassword.Text = pwd;
            //            break;
            //        }
            //        else
            //        {
            //            // Чистим поля на форме  
            //            txtRLogin.Text = "";
            //            txtRPassword.Text = "";
            //        }
            //    }
            //    // Закрываем поток  
            //    fs.Close();
            //}
        }


    }
}
