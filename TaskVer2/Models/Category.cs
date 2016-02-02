using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskVer2.Models
{
    public class Category//в итоге добавил таблицу категорий
    {
        public int CategoryID { get; set; }
        public string category{ get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}