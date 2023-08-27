using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RDIC.Models
{
    public class Prodotto
    {
        public int Id { get; set; }
        public string TitoloIT { get; set; }
        public string TitoloEN { get; set; }
        public string TitoloFR { get; set; }
        public string DescriptionIT { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionFR { get; set; }
        public List<Immagine> Immagini { get; set; }
        public List<Allegato> Allegati { get; set; }
        public int IdCategoria { get; set; }
        public int IdSubCategoria { get; set; }
        public int Order { get; set; }
        public int GlobalOrder { get; set; }
        public bool Visible { get; set; }
    }
}