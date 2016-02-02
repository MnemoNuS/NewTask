using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using TaskVer2.Models;

namespace TaskVer2.Util
{
    public class NewsUtil
    {

        static public void UpdateNews() // обновляет новости?
        {
            dataContext db = new dataContext();
            foreach (Chanel Chanel in db.Chanel.ToList())
            {
                int countLoad = 0;//счилает кол-во обработаных записей
                int countRecords = 0;// переменная считает кол-во добавленных новостей
                int countCategories = 0;// переменная считает кол-во добавленных категорий
                string Url = Chanel.link;            // поучаем адресс канала
                try
                {

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
                        NewsList = db.News.ToList();//при прохождении каждой новости перезаполняем списки
                        Category = db.Category.ToList();
                        News TempNews = new News();// создаем место для хранения записей

                        TempNews.ChanelID = Chanel.ChanelID;
                        TempNews.title = Item.SelectSingleNode("title").InnerText;
                        TempNews.link = Item.SelectSingleNode("link").InnerText;
                        TempNews.Description = Item.SelectSingleNode("description").InnerText;
                        TempNews.pubDate = DateTime.Parse(Item.SelectSingleNode("pubDate").InnerText);
                        try
                        {
                            if (!((Item.SelectSingleNode("category").InnerText == "") || (Item.SelectSingleNode("category").InnerText == " ")))
                            {
                                bool Chek = true;// маркер существования категории записи
                                foreach (Category cat in Category)
                                {
                                    if (cat.category == Item.SelectSingleNode("category").InnerText)//если такая категория существует устанавливаем ID этой категории и меняем маркер
                                    {
                                        TempNews.CategoryID = cat.CategoryID;
                                        Chek = false;
                                        continue;
                                    }
                                }
                                if (Chek) //если маркер не изменился записываем категорию 
                                {
                                    db.Category.Add(new Category { category = Item.SelectSingleNode("category").InnerText });
                                    db.SaveChanges();
                                    countCategories++;
                                    TempNews.CategoryID = db.Category.ToList().Last().CategoryID;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            TempNews.CategoryID = 1;       // любая запись без категории будет иметь категорию новости т.к. бывают пустые категории
                        }

                        bool Check = true; // устанавливаем маркер добавления записи 

                        foreach (News news in NewsList) //проверка существования такой новости в базе 
                        {
                            if (news.title == TempNews.title)// проверяем новости по заголовку
                            {
                                if (news.Description == TempNews.Description)// проверяем соответствие содержания
                                {
                                    Check = false;// устанавливаем маркер
                                    continue; // покидаем цикл проверки
                                }
                                db.News.Find(news.NewsID).Description = TempNews.Description;
                                //db.News.Remove(news); // если текст новости не совпадает удаляем старую запись
                                //db.News.Add(TempNews);   //добавляем новую
                                db.SaveChanges();
                                Check = false; // устанавливаем маркер
                                continue; // покидаем цикл проверки
                            }
                        }

                        if (Check)// проверяем маркер если условие верно добавляем запись
                        {
                            countRecords++; // счетчик добавленных записей
                            db.News.Add(TempNews);
                            db.SaveChanges();
                        }
                    }
                }
                catch (OutOfMemoryException)
                {

                    return; // возвращаем при получении любой ошибки

                }
                //ведется лог обновлений
                String Mess = DateTime.Now + " from " + Chanel.name + " chanel " + countLoad + " items was processed, " + countRecords + " new records was added, " + countCategories + "categories was added.\n";
                System.IO.File.AppendAllText("../NewsLog.txt", Mess);

            }

            return;


        }
    }
}