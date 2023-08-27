using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RDIC.Models
{
    public class MasterData
    {
        public int Id { get; set; }
        public string AdminUser { get; set; }
        public string AdminPassword {get; set;}
        public string SmtpAddress { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string EmailAddress { get; set; }
        public string EmailContatti { get; set; }
        public int NumNewProdotti { get; set; }

    }
}