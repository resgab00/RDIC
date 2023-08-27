using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RDIC.Models
{
    public class SubCategoria
    {
        public int Id { get; set; }
        public string DescriptionIT { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionFR { get; set; }
        public int Order { get; set; }
        public bool Visible { get; set; }
    }
}