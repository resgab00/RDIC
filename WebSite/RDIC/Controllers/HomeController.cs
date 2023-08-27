using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDIC.Controls;
using RDIC.Models;

namespace RDIC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            Session["Menu"] = "HOME";
            MasterData md = Data.GetMasterData();
            List<Prodotto> NuoviProdotti = Data.LoadProdottos().Where(prd=>prd.Visible).OrderBy(ord => ord.GlobalOrder).Take(md.NumNewProdotti).ToList();
            ViewData["NuoviProdotti"] = NuoviProdotti;

            News HomeNews = Data.LoadNewss().Where(news=>news.Visible).OrderByDescending(ord => ord.Data).FirstOrDefault();
            ViewData["HomeNews"] = HomeNews;

            return View();
        }

        public ActionResult Azienda()
        {
            Session["Menu"] = "AZIENDA";

            return View();
        }
        
        public ActionResult Catalogo(int? idCategoria)
        {
            Session["Menu"] = "CATALOGO";
            List<Categoria>  Model = Data.LoadCategorias();

            if(idCategoria!= null && idCategoria != 0)
            {
                Model = Model.Where(cat => cat.Id == idCategoria).ToList();
            }

            List<Prodotto> prds = Data.GetProdottos().Where(prd => prd.Immagini.Count > 0).ToList();
            foreach (Categoria item in Model)
            {
                Prodotto prod = prds.Where(prd => prd.IdCategoria == item.Id).Where(prd => prd.Visible).FirstOrDefault();
                if(prod!=null && prod.Immagini.Count > 0)
                {
                    item.Image = prod.Immagini[0].FileName;
                    item.Visible = true;
                }
                else if (prod == null)
                {
                    item.Visible = false;
                }

                foreach (SubCategoria sub in item.SubCategorias)
                {
                    prod = prds.Where(prd => prd.IdCategoria == item.Id).Where(prd => prd.IdSubCategoria == sub.Id).Where(prd => prd.Visible).FirstOrDefault();
                    if (prod == null)
                    {
                        sub.Visible = false;
                    }
                    else
                    {
                        sub.Visible = true;
                    }
                }
            }

            Model = Model.OrderBy(ord => ord.Order).ToList();

            return View(Model);
        }
        
        public ActionResult CatalogoProdotto(string idCategoria, string idSubCategoria)
        {
            Session["Menu"] = "CATALOGO";

            Categoria categoria = Data.LoadCategorias().Where(cat => cat.Id == Convert.ToInt32(idCategoria)).FirstOrDefault();
            SubCategoria subCategoria = categoria.SubCategorias.Where(sub => sub.Id == Convert.ToInt32(idSubCategoria)).FirstOrDefault();
            ViewData["Categoria"] = categoria;
            ViewData["SubCategoria"] = subCategoria;

            return View(Data.LoadProdottos().Where(prd => prd.Visible).ToList());
        }
        
        public ActionResult ServiziOfferti()
        {
            Session["Menu"] = "SERVIZIOFFERTI";

            return View();
        }
        
        public ActionResult Contatti(bool? isInviato, bool? isError)
        {
            Session["Menu"] = "CONTATTI";
            bool Inviato = isInviato ?? false;
            ViewData["Inviato"] = Inviato;
            bool Error = isError ?? false;
            ViewData["Error"] = Error;
            return View();
        }
             
        public ActionResult InformazioniLegali()
        {
            Session["Menu"] = "INFOLEGALI";

            return View();
        }
        
        public ActionResult News()
        {
            Session["Menu"] = "NEWS";

            return View(Data.LoadNewss().Where(news=>news.Visible).OrderByDescending(ord => ord.Data).ToList());
        }
        public ActionResult ExplodedNews(int IdNews)
        {
            Session["Menu"] = "NEWS";
            News NewsDetail = Data.LoadNewss().Where(news => news.Id == IdNews).FirstOrDefault();
            return View(NewsDetail);
        }

        public ActionResult NuoviProdotti()
        {
            Session["Menu"] = "NUOVIPRODOTTI";
            MasterData md = Data.GetMasterData();
            List<Prodotto> NuoviProdotti = Data.LoadProdottos().Where(prd => prd.Visible).OrderBy(ord => ord.GlobalOrder).Take(md.NumNewProdotti).ToList();

            return View(NuoviProdotti);
        }
        
        public ActionResult PrivacyCookie()
        {
            Session["Menu"] = "PRIVACY";

            return View();
        }
        
        public ActionResult ChangeCulture(string Culture)
        {
            System.Globalization.CultureInfo currentInfo = new System.Globalization.CultureInfo(Culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = currentInfo;
            string urlRedirect = "~";

            if (Session["Culture"] == null)
                Session.Add("Culture", Culture);
            else
                Session["Culture"] = Culture;

            if(HttpContext.Request.UrlReferrer != null && !string.IsNullOrEmpty(HttpContext.Request.UrlReferrer.AbsoluteUri))
            {
                urlRedirect = HttpContext.Request.UrlReferrer.AbsoluteUri;
            }

            return Redirect(urlRedirect);
        }
        public ActionResult InviaRichiesta(string first_name, string last_name, string email, string phone, string message)
        {
            Session["Menu"] = "CONTATTI";

            bool Inviato = true;
            bool Error = false;

            MasterData MD = Data.GetMasterData();

            string Body = @"Richiesta inviata dal sito RDICsrl.com<br /><br />
                            <b>nome</b><br />" + first_name + @"<br />
                            <b>cognome</b><br />" + last_name + @"<br />
                            <b>e-mail</b><br />" + email + @"<br />
                            <b>Tel.</b><br />" + phone + @"<br />
                            <b>Messaggio</b><br />" + message + @"<br />";

            try
            {
                Util.SendMail(email, MD.EmailContatti, "Richiesta da RDICsrl.com", Body);
            }
            catch
            {
                Inviato = false;
                Error = true;
            }

            return RedirectToAction("Contatti", "Home", new { isInviato = Inviato, isError= Error });

        }

    }
}