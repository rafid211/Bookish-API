using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_API.Models
{
    public class SearchModel
    {
        public string searchStr { get; set; }
        public string orderBy { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string publisher { get; set; }

        
    }
}