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
    public class AdminController : Controller
    {
        private bool CheckAuth()
        {
            if (Session["User"] == null || Session["User"] == "")
            {
                return false;
            }
            return true;
        }
        public ActionResult Index()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }
            List<Categoria> Categoria = Data.LoadCategorias().OrderBy(ord => ord.Order).ToList();
            ViewData["Categoria"] = Categoria;

            return View();
        }
        public ActionResult LogIn(bool? LogInError)
        {
            Session["Menu"] = "HOME";

            ViewData["LogInError"] = LogInError ?? false;

            return View();
        }
        public ActionResult Logout()
        {
            Session["User"] = null;

            return RedirectToAction("LogIn","Admin");
        }
        public ActionResult AllProduct(int? IdCat, int? IdSub, string text)
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Prodotto> Prodotti = Data.LoadProdottos().OrderBy(ord => ord.TitoloIT).ToList();
            if ((IdCat ?? 0) != 0)
            {
                Prodotti = Prodotti.Where(cat => cat.IdCategoria == IdCat).ToList();
            }
            if ((IdSub ?? 0) != 0)
            {
                Prodotti = Prodotti.Where(cat => cat.IdSubCategoria == IdSub).ToList();
            }
            if (text != null && text != "")
            {
                Prodotti = Prodotti.Where(cat => cat.DescriptionIT.Contains(text)).ToList();
            }
            ViewData["Prodotti"] = Prodotti;

            List<Categoria> Categoria = Data.LoadCategorias().OrderBy(ord => ord.Order).ToList();
            ViewData["Categoria"] = Categoria;

            return PartialView();
        }
        public ActionResult AllCategoria()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categoria = Data.LoadCategorias().OrderBy(ord => ord.DescriptionIT).ToList();
            List<Prodotto> Prodotto = Data.LoadProdottos();

            ViewData["Prodotto"] = Prodotto;

            return PartialView(Categoria);
        }
        public ActionResult AllSubCategoria()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categoria = Data.LoadCategorias().OrderBy(ord => ord.DescriptionIT).ToList();
            List<Prodotto> Prodotto = Data.LoadProdottos();

            ViewData["Prodotto"] = Prodotto;

            return PartialView(Categoria);
        }
        public ActionResult AllNews()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<News> News = Data.LoadNewss().OrderByDescending(ord => ord.Data).ToList();

            return PartialView(News);
        }
        public ActionResult ProductDetail(int IdProdotto)
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Prodotto Prodotto = Data.LoadProdottos().Where(prd=>prd.Id== IdProdotto).FirstOrDefault();
            List<Categoria> Categoria = Data.LoadCategorias();
            ViewData["Categoria"] = Categoria;
            ViewData["Prodotto"] = Prodotto;

            return PartialView(Prodotto);
        }
        public ActionResult CategoriaDetail(int IdCategoria)
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Categoria Categoria = Data.LoadCategorias().Where(cat=>cat.Id== IdCategoria).FirstOrDefault();

            return PartialView(Categoria);
        }
        public ActionResult SubCategoriaDetail(int IdCategoria, int IdSubCategoria)
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categorias = Data.LoadCategorias();
            Categoria Categoria = Data.LoadCategorias().Where(cat => cat.Id == IdCategoria).FirstOrDefault();
            SubCategoria SubCategoria = Categoria.SubCategorias.Where(sub => sub.Id == IdSubCategoria).FirstOrDefault();
            List<Prodotto> Prodottos = Data.GetProdottos().Where(prd => prd.IdCategoria == IdCategoria).Where(prd => prd.IdSubCategoria == IdSubCategoria).ToList();

            ViewData["Prodottos"] = Prodottos;
            ViewData["Categoria"] = Categoria;
            ViewData["Categorias"] = Categorias;

            return PartialView(SubCategoria);
        }
        public ActionResult NewsDetail(int IdNews)
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            News News = Data.LoadNewss().Where(cat => cat.Id == IdNews).FirstOrDefault();

            return PartialView(News);
        }
        public ActionResult DatiBase()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            MasterData DatiBase = Data.GetMasterData();

            return PartialView(DatiBase);
        }
        public ActionResult CambiaPassword()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            MasterData DatiBase = Data.GetMasterData();

            return PartialView(DatiBase);
        }
        public ActionResult ProductNew()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categoria = Data.LoadCategorias();
            ViewData["Categoria"] = Categoria;

            return PartialView(Categoria);
        }
        public ActionResult CategoriaNew()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categoria = Data.LoadCategorias();

            return PartialView(Categoria);
        }
        public ActionResult SubCategoriaNew()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categoria = Data.LoadCategorias();

            return PartialView(Categoria);
        }
        public ActionResult NewsNew()
        {
            Session["Menu"] = "HOME";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<News> News = Data.LoadNewss();

            return PartialView(News);
        }
        public ActionResult OrdinaCategoria()
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categoria = Data.LoadCategorias().OrderBy(ord => ord.Order).ToList();

            return PartialView(Categoria);
        }
        public ActionResult OrdinaSubCategoria(int? idCategoria)
        {
            int IdCat = idCategoria ?? 0;
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categoria = Data.LoadCategorias().OrderBy(cat => cat.DescriptionIT).ToList();
            List<SubCategoria> SubCategoria = new List<SubCategoria>();
            if (IdCat == 0 && Categoria != null && Categoria.Count>0)
            {
                IdCat = Categoria[0].Id;
            }

            if (Categoria != null && Categoria.Count>0)
            {
                SubCategoria = Data.LoadCategorias().Where(cat => cat.Id == IdCat).FirstOrDefault().SubCategorias.OrderBy(ord => ord.Order).ToList();
            }

            ViewData["Categoria"] = Categoria;
            ViewData["idCategoria"] = IdCat;

            return PartialView(SubCategoria);
        }
        public ActionResult OrdinaProdotto(int? idCategoria, int? idSubCategoria)
        {
            int IdCat = idCategoria ?? 0;
            int IdSubCat = idSubCategoria ?? 0;

            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categoria = Data.LoadCategorias().OrderBy(cat => cat.DescriptionIT).ToList();
            List<SubCategoria> SubCategoria = new List<SubCategoria>();
            List<Prodotto> Prodotto = new List<Prodotto>();

            if (IdCat == 0 && Categoria!=null && Categoria.Count>0)
            {
                IdCat = Categoria[0].Id;
            }

            if (Categoria!=null && Categoria.Count>0)
            {
                SubCategoria = Data.LoadCategorias().Where(cat => cat.Id == IdCat).FirstOrDefault().SubCategorias.OrderBy(ord => ord.Order).ToList();
                if (IdSubCat == 0 && SubCategoria.Count > 0)
                {
                    IdSubCat = SubCategoria[0].Id;
                }

                if (IdSubCat != 0)
                {
                    Prodotto = Data.LoadProdottos().Where(cat => cat.IdCategoria == IdCat).Where(cat => cat.IdSubCategoria == IdSubCat).ToList();
                }
            }
            ViewData["Categoria"] = Categoria;
            ViewData["SubCategoria"] = SubCategoria;
            ViewData["idCategoria"] = IdCat;
            ViewData["idSubCategoria"] = IdSubCat;

            return PartialView(Prodotto);
        }
        public ActionResult OrdinaGlobalProdotto()
        {

            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Prodotto> Prodotto = new List<Prodotto>();

            Prodotto = Data.LoadProdottos().Where(cat => cat.Visible).OrderBy(cat => cat.GlobalOrder).ToList();

            return PartialView(Prodotto);
        }
        public ActionResult UpdateProduct(int idProdotto, int categoria, int subCategoria, string titleIT, string titleEN, string titleFR, string descriptionIT, string descriptionEN, string descriptionFR, string visible, string imgFileDeleted, string allFileDeleted)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Prodotto Prodotto = Data.LoadProdottos().Where(prd => prd.Id == idProdotto).FirstOrDefault();
            if (imgFileDeleted != "")
            {
                string[] ImgToDelete = imgFileDeleted.Split('-');
                foreach (string item in ImgToDelete)
                {
                    Immagine imgDelete = Prodotto.Immagini.Where(img => img.Id == Convert.ToInt32(item)).FirstOrDefault();
                    if (imgDelete != null)
                    {
                        Prodotto.Immagini.Remove(imgDelete);
                    }
                }
            }
            if (allFileDeleted != "")
            {
                string[] allToDelete = allFileDeleted.Split('-');
                foreach (string item in allToDelete)
                {
                    Allegato allDelete = Prodotto.Allegati.Where(all => all.Id == Convert.ToInt32(item)).FirstOrDefault();
                    if (allDelete != null)
                    {
                        Prodotto.Allegati.Remove(allDelete);
                    }
                }
            }

            Prodotto.IdCategoria = categoria;
            Prodotto.IdSubCategoria = subCategoria;
            Prodotto.TitoloIT = titleIT;
            Prodotto.TitoloEN = titleEN;
            Prodotto.TitoloFR = titleFR;
            Prodotto.DescriptionIT = descriptionIT;
            Prodotto.DescriptionEN = descriptionEN;
            Prodotto.DescriptionFR = descriptionFR;
            Prodotto.Visible = visible!=null?true:false;

            //Todo gestire Prodotto.Order
            //Todo gestire Prodotto.Allegati

            Data.UpdateProdotto(Prodotto);

            ViewData["Prodotto"] = Prodotto;

            return RedirectToAction("ProductDetail", "Admin", new { IdProdotto = idProdotto });
        }
        public ActionResult UpdateCategoria(int idCategoria, string titleIT, string titleEN, string titleFR)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Categoria Categoria = Data.LoadCategorias().Where(prd => prd.Id == idCategoria).FirstOrDefault();

            Categoria.Id = idCategoria;
            Categoria.DescriptionIT = titleIT;
            Categoria.DescriptionEN = titleEN;
            Categoria.DescriptionFR = titleFR;

            Data.UpdateCategoria(Categoria);

            ViewData["Categoria"] = Categoria;

            return RedirectToAction("CategoriaDetail", "Admin", new { IdCategoria = idCategoria });
        }
        public ActionResult UpdateSubCategoria(int idCategoria, int idSubCategoria, int categoria, string titleIT, string titleEN, string titleFR)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Categoria Categoria = Data.LoadCategorias().Where(prd => prd.Id == idCategoria).FirstOrDefault();
            SubCategoria SubCategoria = Categoria.SubCategorias.Where(sub => sub.Id == idSubCategoria).FirstOrDefault();
            SubCategoria.DescriptionIT = titleIT;
            SubCategoria.DescriptionEN = titleEN;
            SubCategoria.DescriptionFR = titleFR;
            Data.UpdateCategoria(Categoria);

            int SubCategoriaId = idSubCategoria;
            int CategoriaId = idCategoria;


            if (idCategoria != categoria)
            {
                Categoria NewCategoria = Data.LoadCategorias().Where(prd => prd.Id == categoria).FirstOrDefault();
                SubCategoria = Categoria.SubCategorias.Where(sub => sub.Id == idSubCategoria).FirstOrDefault();

                CategoriaId = NewCategoria.Id;
                SubCategoriaId = 1;

                if(NewCategoria.SubCategorias != null || NewCategoria.SubCategorias.Count > 0)
                {
                    int lastId = NewCategoria.SubCategorias.OrderByDescending(ord => ord.Id).FirstOrDefault().Id;
                    SubCategoriaId = lastId + 1;
                    SubCategoria.Id = SubCategoriaId;
                }

                NewCategoria.SubCategorias.Add(SubCategoria);
                Data.UpdateCategoria(NewCategoria);

                Categoria.SubCategorias.Remove(SubCategoria);

                Data.UpdateCategoria(Categoria);
            }

            ViewData["Categoria"] = Categoria;

            return RedirectToAction("SubCategoriaDetail", "Admin", new { IdCategoria = CategoriaId, IdSubCategoria = SubCategoriaId });
        }
        public ActionResult AddProduct(int categoria, int subCategoria, string titleIT, string titleEN, string titleFR, string descriptionIT, string descriptionEN, string descriptionFR, string visible)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Prodotto Prodotto = new Prodotto();

            Prodotto.IdCategoria = categoria;
            Prodotto.IdSubCategoria = subCategoria;
            Prodotto.TitoloIT = titleIT;
            Prodotto.TitoloEN = titleEN;
            Prodotto.TitoloFR = titleFR;
            Prodotto.DescriptionIT = descriptionIT;
            Prodotto.DescriptionEN = descriptionEN;
            Prodotto.DescriptionFR = descriptionFR;
            Prodotto.Visible = visible != null ? true : false;

            int IdProdotto = Data.AddProdotto(Prodotto);

            string sPath = "";

            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/Prodotto/" + IdProdotto.ToString());
            DirectoryInfo dr = new DirectoryInfo(sPath);
            if (!dr.Exists)
            {
                dr.Create();
            }

            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/Prodotto/" + IdProdotto.ToString()+"/Immagine");
            dr = new DirectoryInfo(sPath);
            if (!dr.Exists)
            {
                dr.Create();
            }

            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/Prodotto/" + IdProdotto.ToString() + "/Allegato");
            dr = new DirectoryInfo(sPath);
            if (!dr.Exists)
            {
                dr.Create();
            }



            ViewData["Prodotto"] = Prodotto;

            return RedirectToAction("ProductDetail", "Admin", new { IdProdotto = Prodotto.Id });
        }
        public ActionResult AddCategoria(string titleIT, string titleEN, string titleFR)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Categoria Categoria = new Categoria();

            Categoria.DescriptionIT = titleIT;
            Categoria.DescriptionEN = titleEN;
            Categoria.DescriptionFR = titleFR;
            Categoria.SubCategorias = new List<SubCategoria>();

            int IdCategoria = Data.AddCategoria(Categoria);

            return RedirectToAction("CategoriaDetail", "Admin", new { IdCategoria = Categoria.Id });
        }
        public ActionResult AddSubCategoria(int categoria, string titleIT, string titleEN, string titleFR)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Categoria Categoria = Data.LoadCategorias().Where(prd => prd.Id == categoria).FirstOrDefault();

            SubCategoria SubCategoria = new SubCategoria();

            SubCategoria.DescriptionIT = titleIT;
            SubCategoria.DescriptionEN = titleEN;
            SubCategoria.DescriptionFR = titleFR;

            SubCategoria.Id = 1;

            if (Categoria.SubCategorias != null && Categoria.SubCategorias.Count > 0)
            {
                int lastId = Categoria.SubCategorias.OrderByDescending(ord => ord.Id).FirstOrDefault().Id;
                SubCategoria.Id = lastId + 1;
            }

            Categoria.SubCategorias.Add(SubCategoria);

            Data.UpdateCategoria(Categoria);

            return RedirectToAction("SubCategoriaDetail", "Admin", new { IdCategoria = categoria, IdSubCategoria= SubCategoria.Id });
        }
        public ActionResult AddNews(DateTime dataNews, string titleIT, string titleEN, string titleFR, string descriptionIT, string descriptionEN, string descriptionFR, string visible)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            News News = new News();

            News.Data = dataNews;
            News.TitoloIT = titleIT;
            News.TitoloEN = titleEN;
            News.TitoloFR = titleFR;
            News.DescriptionIT = descriptionIT;
            News.DescriptionEN = descriptionEN;
            News.DescriptionFR = descriptionFR;
            News.Visible = visible != null ? true : false;

            News.Immagine = new Immagine();
            News.Immagine.FileName = "~/img/news_no_img.jpg";
            News.Immagine.Id = 1;

            int IdNews = Data.AddNews(News);

            string sPath = "";

            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/News/" + IdNews.ToString());
            DirectoryInfo dr = new DirectoryInfo(sPath);
            if (!dr.Exists)
            {
                dr.Create();
            }

            ViewData["News"] = News;

            return RedirectToAction("NewsDetail", "Admin", new { IdNews = News.Id });
        }
        public ActionResult DeleteProduct(int idProdotto)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            //Prodotto Prodotto = Data.LoadProdottos().Where(prd => prd.Id == idProdotto).FirstOrDefault();

            Data.DeleteProdotto(idProdotto);
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/Prodotto/" + idProdotto.ToString() );

            DirectoryInfo dr = new DirectoryInfo(sPath);
            if (dr.Exists)
            {
                dr.Delete(true);
            }
            JsonResult jr = new JsonResult();
            jr.Data = new { status = "OK" };

            return jr;
        }
        public ActionResult DeleteCategoria(int idCategoria)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Data.DeleteCategoria(idCategoria);

            JsonResult jr = new JsonResult();
            jr.Data = new { status = "OK" };

            return jr;
        }
        public ActionResult DeleteSubCategoria(int idCategoria, int idSubCategoria)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Categoria Categoria = Data.GetCategorias().Where(cat => cat.Id == idCategoria).FirstOrDefault();

            SubCategoria SubCat = Categoria.SubCategorias.Where(sub => sub.Id == idSubCategoria).FirstOrDefault();

            Categoria.SubCategorias.Remove(SubCat);

            Data.UpdateCategoria(Categoria);

            JsonResult jr = new JsonResult();
            jr.Data = new { status = "OK" };

            return jr;
        }
        public ActionResult ShowHideProduct(int idProdotto, bool visible)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            Prodotto Prodotto = Data.LoadProdottos().Where(prd => prd.Id == idProdotto).FirstOrDefault();

            Prodotto.Visible = visible;

            Data.UpdateProdotto(Prodotto);

            ViewData["Prodotto"] = Prodotto;
            JsonResult jr = new JsonResult();
            jr.Data = new { status= "OK" };

            return jr;

            //return PartialView(Prodotto);
        }
        public ActionResult GetSubCategory(int idCategoria)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<SubCategoria> SubCategoria = Data.LoadCategorias().Where(prd => prd.Id == idCategoria).FirstOrDefault().SubCategorias;
            JsonResult jr = new JsonResult();
            jr.Data = SubCategoria;
            return jr;

            //return PartialView(Prodotto);
        }
        public ActionResult uploadImageProduct(int idProdotto)
        {
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            JsonResult jr = new JsonResult();

            Prodotto prod = Data.GetProdottos().Where(prd => prd.Id == idProdotto).FirstOrDefault();

            //se request.files è vuoto non fare nulla
            //todo salvare i file su disco
            string sPath = "";
            string FileName = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/Prodotto/" + idProdotto.ToString() + "/Immagine/");

            if (System.Web.HttpContext.Current.Request.Files != null && System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                for (int i=0;i< System.Web.HttpContext.Current.Request.Files.Count;i++)
                {
                    System.Web.HttpPostedFile hpf = System.Web.HttpContext.Current.Request.Files[i];
                    if (hpf.ContentLength > 0)
                    {
                        if (System.IO.File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            System.IO.File.Delete(sPath + Path.GetFileName(hpf.FileName));
                        }
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        FileName = "~/Public/Prodotto/" + idProdotto.ToString() + "/Immagine/" + Path.GetFileName(hpf.FileName);
                        Immagine imgProd = new Immagine();
                        imgProd.AltEN = hpf.FileName;
                        imgProd.AltFR = hpf.FileName;
                        imgProd.AltIT = hpf.FileName;
                        imgProd.FileName = FileName;

                        Immagine LastImmagine = prod.Immagini.OrderByDescending(ord=>ord.Id).FirstOrDefault();
                        if(LastImmagine!= null)
                        {
                            imgProd.Id = LastImmagine.Id+1;
                        }
                        else
                        {
                            imgProd.Id = 1;
                        }

                        prod.Immagini.Add(imgProd);
                    }
                }
            }

            Data.UpdateProdotto(prod);

            List<string> InitialPreview = new List<string>();
            List<dynamic> InitialPreviewConfig = new List<dynamic>();

            foreach (RDIC.Models.Immagine img in prod.Immagini)
            {
                dynamic ipc = new { width= "200px",
                    url= Url.Action("deleteImageProduct"), 
                    key= img.Id,
                    extra= new { idProduct = idProdotto }
                };
                InitialPreview.Add("<img class='kv-preview-data file-preview-image' src='" + img.FileName + "'>");
                InitialPreviewConfig.Add(ipc);
            }

            dynamic rntFileUpload = new
            {
                error = "",
                initialPreview = InitialPreview,
                initialPreviewConfig = InitialPreviewConfig,
                initialPreviewThumbTags = "",
                append = false
            };

            jr.Data = rntFileUpload;
            return jr;
        }
        public ActionResult uploadAllProduct(int? idProdotto, string allDes, string language)
        {
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            JsonResult jr = new JsonResult();

            Prodotto prod = Data.GetProdottos().Where(prd => prd.Id == idProdotto).FirstOrDefault();
            string sPath = "";
            string FileName = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/Prodotto/" + idProdotto.ToString() + "/Allegato/");

            if (System.Web.HttpContext.Current.Request.Files != null && System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                for (int i = 0; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
                {
                    System.Web.HttpPostedFile hpf = System.Web.HttpContext.Current.Request.Files[i];
                    if (hpf.ContentLength > 0)
                    {
                        if (System.IO.File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            System.IO.File.Delete(sPath + Path.GetFileName(hpf.FileName));
                        }
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        FileName = "~/Public/Prodotto/" + idProdotto.ToString() + "/Allegato/" + Path.GetFileName(hpf.FileName);
                        Allegato allProd = new Allegato();
                        allProd.Description = allDes;
                        allProd.FileName = FileName;
                        allProd.Language = language;

                        allProd.Id = 1;

                        Allegato LastAllegato = prod.Allegati.OrderByDescending(ord => ord.Id).FirstOrDefault();
                        if (LastAllegato != null)
                        {
                            allProd.Id = LastAllegato.Id + 1;
                        }

                        prod.Allegati.Add(allProd);
                    }
                }
            }

            Data.UpdateProdotto(prod);

            List<string> InitialPreview = new List<string>();
            List<dynamic> InitialPreviewConfig = new List<dynamic>();

            foreach (RDIC.Models.Immagine img in prod.Immagini)
            {
                dynamic ipc = new
                {
                    width = "200px",
                    url = Url.Action("deleteImageProduct"),
                    key = img.Id,
                    extra = new { idProduct = idProdotto }
                };
                InitialPreview.Add("<img class='kv-preview-data file-preview-image' src='" + img.FileName + "'>");
                InitialPreviewConfig.Add(ipc);
            }

            dynamic rntFileUpload = new
            {
                error = "",
                initialPreview = InitialPreview,
                initialPreviewConfig = InitialPreviewConfig,
                initialPreviewThumbTags = "",
                append = false
            };

            jr.Data = rntFileUpload;
            return jr;
        }
        public ActionResult deleteImageProduct(int key, int idProduct)
        {
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            //Non fare nulla, viene fatto tutto sul salva
            JsonResult jr = new JsonResult();
            jr.Data = new { error = "", initialPreview = "", initialPreviewConfig = "", initialPreviewThumbTags = "", append = true };

            return jr;
        }
        public ActionResult UpdateOrdineCategoria(string strOrder)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categorias = Data.LoadCategorias();
            int Conta = 1;
            foreach (string order in strOrder.Split(',').ToList())
            {
                if (!string.IsNullOrEmpty(order))
                {
                    Categoria Categoria = Categorias.Where(cat => cat.Id == Convert.ToInt32(order)).FirstOrDefault();
                    Categoria.Order = Conta;
                    Data.UpdateCategoria(Categoria);
                    Conta++;
                }
            }

            return RedirectToAction("OrdinaCategoria", "Admin");
        }
        public ActionResult UpdateOrdineSubCategoria(string strOrder, int idCat)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Categoria> Categorias = Data.LoadCategorias();
            int Conta = 1;
            Categoria Categoria = Categorias.Where(cat => cat.Id == Convert.ToInt32(idCat)).FirstOrDefault();
            foreach (string order in strOrder.Split(',').ToList())
            {
                if (!string.IsNullOrEmpty(order))
                {
                    SubCategoria SubCategoria = Categoria.SubCategorias.Where(cat => cat.Id == Convert.ToInt32(order)).FirstOrDefault();
                    SubCategoria.Order = Conta;
                    Conta++;
                }
            }
            Data.UpdateCategoria(Categoria);

            return RedirectToAction("OrdinaSubCategoria", "Admin", new { idCategoria= idCat });
        }
        public ActionResult UpdateOrdineProdotto(string strOrder, int idCat, int idSubCat)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Prodotto> Prodottos = Data.LoadProdottos();
            int Conta = 1;
            foreach (string order in strOrder.Split(',').ToList())
            {
                if (!string.IsNullOrEmpty(order))
                {
                    Prodotto Prodotto = Prodottos.Where(cat => cat.Id == Convert.ToInt64(order)).FirstOrDefault();
                    Prodotto.Order = Conta;
                    Data.UpdateProdotto(Prodotto);
                    Conta++;
                }
            }

            return RedirectToAction("OrdinaProdotto", "Admin", new { idCategoria = idCat, idSubCategoria = idSubCat });
        }
        public ActionResult UpdateGlobalOrdineProdotto(string strOrder)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            List<Prodotto> Prodottos = Data.LoadProdottos();
            int Conta = 1;
            foreach (string order in strOrder.Split(',').ToList())
            {
                if (!string.IsNullOrEmpty(order))
                {
                    Prodotto Prodotto = Prodottos.Where(cat => cat.Id == Convert.ToInt64(order)).FirstOrDefault();
                    Prodotto.GlobalOrder = Conta;
                    Data.UpdateProdotto(Prodotto);
                    Conta++;
                }
            }

            return RedirectToAction("OrdinaGlobalProdotto", "Admin",new { _v=DateTime.Now});
        }
        public ActionResult ShowHideNews(int idNews, bool visible)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            News News = Data.LoadNewss().Where(prd => prd.Id == idNews).FirstOrDefault();

            News.Visible = visible;

            Data.UpdateNews(News);

            ViewData["News"] = News;
            JsonResult jr = new JsonResult();
            jr.Data = new { status = "OK" };

            return jr;

            //return PartialView(Prodotto);
        }
        public ActionResult DeleteNews(int idNews)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            //Prodotto Prodotto = Data.LoadProdottos().Where(prd => prd.Id == idProdotto).FirstOrDefault();

            Data.DeleteNews(idNews);
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/News/" + idNews.ToString());

            DirectoryInfo dr = new DirectoryInfo(sPath);
            if (dr.Exists)
            {
                dr.Delete(true);
            }
            JsonResult jr = new JsonResult();
            jr.Data = new { status = "OK" };

            return jr;
        }
        public ActionResult uploadImageNews(int idNews)
        {
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            JsonResult jr = new JsonResult();

            News news = Data.GetNewss().Where(prd => prd.Id == idNews).FirstOrDefault();

            //se request.files è vuoto non fare nulla
            //todo salvare i file su disco
            string sPath = "";
            string FileName = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Public/News/" + idNews.ToString() + "/");

            if (System.Web.HttpContext.Current.Request.Files != null && System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                for (int i = 0; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
                {
                    System.Web.HttpPostedFile hpf = System.Web.HttpContext.Current.Request.Files[i];
                    if (hpf.ContentLength > 0)
                    {
                        if (System.IO.File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            System.IO.File.Delete(sPath + Path.GetFileName(hpf.FileName));
                        }
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        FileName = "~/Public/News/" + idNews.ToString() + "/" + Path.GetFileName(hpf.FileName);
                        Immagine imgProd = new Immagine();
                        imgProd.AltEN = hpf.FileName;
                        imgProd.AltFR = hpf.FileName;
                        imgProd.AltIT = hpf.FileName;
                        imgProd.FileName = FileName;
                        imgProd.Id = news.Immagine.Id+1;

                        news.Immagine = imgProd;
                    }
                }
            }

            Data.UpdateNews(news);

            List<string> InitialPreview = new List<string>();
            List<dynamic> InitialPreviewConfig = new List<dynamic>();

            dynamic ipc = new
            {
                width = "200px",
                url = Url.Action("deleteImageProduct"),
                key = news.Immagine.Id,
                extra = new { idNewsPar = idNews }
            };
            InitialPreview.Add("<img class='kv-preview-data file-preview-image' src='" + news.Immagine.FileName + "'>");
            InitialPreviewConfig.Add(ipc);

            dynamic rntFileUpload = new
            {
                error = "",
                initialPreview = InitialPreview,
                initialPreviewConfig = InitialPreviewConfig,
                initialPreviewThumbTags = "",
                append = false
            };

            jr.Data = rntFileUpload;
            return jr;
        }
        public ActionResult deleteImageNews(int key, int idNewsPar)
        {
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            //Non fare nulla, viene fatto tutto sul salva
            JsonResult jr = new JsonResult();
            jr.Data = new { error = "", initialPreview = "", initialPreviewConfig = "", initialPreviewThumbTags = "", append = true };

            return jr;
        }
        public ActionResult UpdateNews(int idNews, DateTime dataNews, string titleIT, string titleEN, string titleFR, string descriptionIT, string descriptionEN, string descriptionFR, string visible, string imgFileDeleted)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            News News = Data.LoadNewss().Where(prd => prd.Id == idNews).FirstOrDefault();
            if (imgFileDeleted != "")
            {
                string[] ImgToDelete = imgFileDeleted.Split('-');
                foreach (string item in ImgToDelete)
                {
                    if (News.Immagine.Id == Convert.ToInt32(item))
                    {
                        News.Immagine = new Immagine();
                        News.Immagine.FileName = "~/img/news_no_img.jpg";
                        News.Immagine.Id = 1;
                    }
                }
            }

            News.Data = dataNews;
            News.TitoloIT = titleIT;
            News.TitoloEN = titleEN;
            News.TitoloFR = titleFR;
            News.DescriptionIT = descriptionIT;
            News.DescriptionEN = descriptionEN;
            News.DescriptionFR = descriptionFR;
            News.Visible = visible != null ? true : false;
            
            if(News.Immagine == null | string.IsNullOrEmpty(News.Immagine.FileName))
            {
                News.Immagine = new Immagine();
                News.Immagine.FileName = "../img/news_no_img.jpg";
                News.Immagine.Id = 1;
            }

            Data.UpdateNews(News);

            ViewData["News"] = News;

            return RedirectToAction("NewsDetail", "Admin", new { IdNews = idNews });
        }
        public ActionResult UpdateMasterData(int idMasterData, string userName, string smtpAddress, string smtpUser, string smtpPassword, string emailContatti, int numNewProdotti)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            MasterData DatiBase = Data.GetMasterData();

            DatiBase.AdminUser = userName;
            DatiBase.SmtpAddress = smtpAddress;
            DatiBase.SmtpUser = smtpUser;
            if (DatiBase.SmtpPassword != smtpPassword)
            {
                DatiBase.SmtpPassword = EncoderHelper.Encod(smtpPassword);
            }
            DatiBase.EmailContatti = emailContatti;
            DatiBase.NumNewProdotti = numNewProdotti;

            Data.UpdateMasterData(DatiBase);

            return RedirectToAction("DatiBase", "Admin");
        }
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            Session["Menu"] = "ADMIN";
            if (!CheckAuth()) { return RedirectToAction("LogIn", "Admin"); }

            MasterData DatiBase = Data.GetMasterData();

            DatiBase.AdminPassword = Util.GetHashSha256(newPassword);

            Data.UpdateMasterData(DatiBase);

            return RedirectToAction("CambiaPassword", "Admin");
        }
        public ActionResult GetLogIn(string userName, string password)
        {
            Session["Menu"] = "ADMIN";

            MasterData DatiBase = Data.GetMasterData();

            var loginPassword = Util.GetHashSha256(password);

#if DEBUG
            Session["User"] = DatiBase.AdminUser;
            return RedirectToAction("Index", "Admin");
#endif

            if (loginPassword == DatiBase.AdminPassword && userName.ToLower() == DatiBase.AdminUser.ToLower())
            {
                Session["User"] = DatiBase.AdminUser;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("LogIn", "Admin", new { LogInError= true });
            }
        }

    }
}