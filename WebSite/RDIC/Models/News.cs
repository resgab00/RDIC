using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RDIC.Models
{
    public class News
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string TitoloIT { get; set; }
        public string TitoloEN { get; set; }
        public string TitoloFR { get; set; }
        public string DescriptionIT { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionFR { get; set; }
        public Immagine Immagine { get; set; }
        public bool Visible { get; set; }
    }
}