using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace TaskVer2.Models
{
    public class Feed
    {
        static public List<string> AddFeed()
        {
            dataContext db = new dataContext();  // конектимся к базе 
            List<string>answer=new List<string>();

            for (int i = 1; i <= db.Chanel.Count(); i++) // запускает цикл обнавления каналов по количеству каналов
            {
                answer.Add(LoadFeed(i));// собираем ответы с каналов
            }
            return answer;
        }
        static public string LoadFeed(int ChID)
        {
        dataContext db = new dataContext();  // конектимся к базе 
            int countLoad = 0;//счилает кол-во обработаных записей
            int countRec = 0;// переменная считает кол-во добавленных
            Chanel Chanel = db.Chanel.Find(ChID); // выбираем канал
            try
            {
                string Url = Chanel.link;            // поучаем адресс канала

                // string Url = "http://lenta.ru/rss";  // пробная строка если аза не работает

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);  // получение xml файла с RSS лентой
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader RssStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);


                XmlDocument TempXml = new XmlDocument(); // загружаем поток 
                TempXml.Load(RssStream);
                RssStream.Close(); //закрываем поток

                List<News> NewsList = new List<News>();//создаем список новостей

                List<Category> Category = new List<Category>();// создаем список категорий
                
                XmlNodeList List = TempXml.GetElementsByTagName("item");// создаем список элечентов из XML файла

                
                foreach (XmlNode Item in List) //обрабатываем каждый элемент
                {
                    countLoad++;
                    NewsList = db.News.ToList();
                    Category = db.Category.ToList();
                    News TempNews = new News();// создаем место для хранения записей

                    TempNews.category = "Новости";       // любая запись изначально будет иметь категорию новости т.к. бывают пустые категории
                    TempNews.ChanelID = Chanel.ChanelID;
                    TempNews.CategoryID = 1;
                    foreach (XmlElement element in Item)
                    {
                        switch (element.Name)
                        {
                            case "title":
                                TempNews.title = element.InnerText;
                                continue;
                            case "link":
                                TempNews.link = element.InnerText;
                                continue;
                            case "description":
                                TempNews.Description = element.InnerText;
                                continue;
                            case "pubDate":
                                TempNews.pubDate = DateTime.Parse(element.InnerText);
                                continue;
                            case "category":
                                {
                                    if ((element.InnerText == "") || (element.InnerText == " "))
                                    {
                                    }
                                    else
                                    {
                                        bool Chek = true;// маркер существования категории записи
                                        foreach (Category cat in Category)
                                        {
                                            if (cat.category == element.InnerText)//если такая категория существует устанавливаем ID этой категории и меняем маркер
                                            {
                                                TempNews.CategoryID = cat.CategoryID;
                                                Chek = false;
                                                continue;
                                            }
                                        }
                                        if (Chek) //если маркер не изменился записываем категорию 
                                        {
                                            db.Category.Add(new Category { category = element.InnerText });
                                            db.SaveChanges();
                                            TempNews.CategoryID = db.Category.Count();
                                        }
                                        TempNews.category = element.InnerText;
                                        
                                    }
                                    continue;
                                }
                        }
                    }
                   
                    

                    bool Check = true; // устанавливаем маркер добавления записи 

                        foreach (News node in NewsList) //проверка существования такой новости в базе 
                        {
                            if (node.title == TempNews.title)// проверяем новости по заголовку
                            {
                                if (node.Description == TempNews.Description)// проверяем соответствие содержания
                                {
                                    Check = false;// устанавливаем маркер
                                    continue; // покидаем цикл проверки
                                }
                                db.News.Remove(node); // если текст новости не совпадает удаляем старую запись
                                db.News.Add(TempNews);   //добавляем новую
                                db.SaveChanges();
                                Check = false; // устанавливаем маркер
                                continue; // покидаем цикл проверки
                            }
                        }


                    if (Check)// проверяем маркер если условие верно добавляем запись
                    {
                        countRec++; // счетчик добавленных записей
                        db.News.Add(TempNews);
                        db.SaveChanges();
                    }
                       


                

                      
                }
            }

            catch (Exception)
            {
                
                return "Failed"; // возвращаем при получении любой ошибки

            }
            
           
            String Mess = "From "+Chanel.name+" chanel "+countLoad+" was processed, "+countRec+" new records was added.";
            return Mess;    
            
                
        }
    }
}