using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskVer2.Models
{
    public class Chanel // класс который удет отвечать за создание каналов
    {
        public int ChanelID { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public virtual ICollection<News> News { get; set; }

    }
}