using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RDIC.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string DescriptionIT { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionFR { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        public List<SubCategoria> SubCategorias { get; set; }
        public bool Visible { get; set; }
    }
}