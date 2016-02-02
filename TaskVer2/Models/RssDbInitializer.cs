using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace TaskVer2.Models
{
    public class RssDbInitializer : DropCreateDatabaseIfModelChanges<dataContext>
    //public class RssDbInitializer : DropCreateDatabaseAlways<dataContext>
    {
        protected override void Seed(dataContext Base)
        {
            Base.News.Add(new News
            {
                NewsID = 0,
                ChanelID = 0,
                CategoryID = 0,
                title = "Китайская компания выдала премии резиновыми женщинами",
                link = "http://www.dailymail.co.uk/news/peoplesdaily/article-3418855/Is-bizarre-annual-bonus-Chinese-firm-hands-SEX-DOLLS-instead-cash-male-employees.html",
                Description = "24 января в городе Гуанчжоу компания Lianlian провела ежегодную вечеринку." +
            "На ней сотрудники получили премиальные в виде резиновых женщин, шлепанцев," +
            "свинины и купонов на посещение караоке.Директор компании Ван Южу сказал," +
            "что резиновых женщин купили для одиноких сотрудников," +
            "а шлепанцы удобны для ношения в Гуанчжоу," +
            "c жаркой и влажной погодой",
                pubDate = DateTime.Parse("2016-01-28"),
            });

            Base.Category.Add(new Category { CategoryID = 0, category = "Новости" });
            Base.Chanel.Add(new Chanel { ChanelID = 0, name = "Lenta.Ru", link = "http://lenta.ru/rss" });


            // base.Seed(Base);
            Base.SaveChanges();
        }
    }


}