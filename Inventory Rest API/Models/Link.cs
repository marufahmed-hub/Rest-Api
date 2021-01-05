using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_Rest_API.Models
{
    public class Link
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public string Relation { get; set; }
    }
}