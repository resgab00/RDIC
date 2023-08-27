using System.Collections.Generic;
using System.Linq;
using RDIC.Models;
using RDIC.Controls;
using System.Xml;
using System.Web;
using System;
using System.Security.Cryptography;

namespace RDIC.Controls
{
    public static class Data
    {
        #region Categoria
        //private static List<User> Users = new List<User>();

        public static List<Categoria> LoadCategorias()
        {
            List<Categoria> Categorias = new List<Categoria>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath(@"~/App_Data/Categoria.xml"));

            Categorias = (List<Categoria>)Util.CreateObject(xmlDoc.InnerXml, Categorias);
            return Categorias;
        }
        public static List<Categoria> GetCategorias()
        {
            return LoadCategorias();
        }
        public static int AddCategoria(Categoria categoria)
        {
            Categoria LastCategoria = GetCategorias().OrderBy(ord => ord.Id).ToList().FindLast(us => us.Id > -1);

            categoria.Id = 1;
            if (LastCategoria != null)
            {
                categoria.Id = LastCategoria.Id + 1;
            }
            List<Categoria> Categorias = new List<Categoria>();
            Categorias = GetCategorias();
            Categorias.Add(categoria);
            WriteDataToFile(Categorias);
            return categoria.Id;
        }
        public static void UpdateCategoria(Categoria categoria)
        {
            List<Categoria> Categorias = new List<Categoria>();
            Categorias = GetCategorias();
            Categoria oldCategoria = Categorias.Find(us => us.Id == categoria.Id);
            Categorias.Remove(oldCategoria);
            Categorias.Add(categoria);
            WriteDataToFile(Categorias);
        }
        public static void DeleteCategoria(int IdCategoria)
        {
            List<Categoria> Categorias = new List<Categoria>();
            Categorias = GetCategorias();

            Categoria DeleteCat = Categorias.Where(cat => cat.Id == IdCategoria).FirstOrDefault();
            Categorias.Remove(DeleteCat);

            WriteDataToFile(Categorias);
        }
        #endregion

        #region Prodotto
        //private static List<User> Users = new List<User>();

        public static List<Prodotto> LoadProdottos()
        {
            List<Prodotto> Prodottos = new List<Prodotto>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath(@"~/App_Data/Prodotto.xml"));

            Prodottos = (List<Prodotto>)Util.CreateObject(xmlDoc.InnerXml, Prodottos);
            return Prodottos;
        }
        public static List<Prodotto> GetProdottos()
        {
            return LoadProdottos();
        }
        public static int AddProdotto(Prodotto prodotto)
        {
            Prodotto LastProdotto = GetProdottos().OrderByDescending(ord => ord.Id).FirstOrDefault();
            prodotto.Id = 1;
            if (LastProdotto != null)
            {
                prodotto.Id = LastProdotto.Id + 1;
            }
            List<Prodotto> Prodottos = new List<Prodotto>();
            Prodottos = GetProdottos();
            Prodottos.Add(prodotto);
            WriteDataToFile(Prodottos);
            return prodotto.Id;

        }
        public static void UpdateProdotto(Prodotto prodotto)
        {
            List<Prodotto> Prodottos = new List<Prodotto>();
            Prodottos = GetProdottos();
            Prodotto oldProdotto = Prodottos.Find(us => us.Id == prodotto.Id);
            Prodottos.Remove(oldProdotto);
            Prodottos.Add(prodotto);
            WriteDataToFile(Prodottos);
        }
        public static void DeleteProdotto(int idProdotto)
        {
            List<Prodotto> Prodottos = new List<Prodotto>();
            Prodottos = GetProdottos();
            Prodotto delProd = Prodottos.Where(prd => prd.Id == idProdotto).FirstOrDefault();

            Prodottos.Remove(delProd);
            WriteDataToFile(Prodottos);
        }
        #endregion

        #region News

        public static List<News> LoadNewss()
        {
            List<News> Newss = new List<News>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath(@"~/App_Data/News.xml"));

            Newss = (List<News>)Util.CreateObject(xmlDoc.InnerXml, Newss);
            return Newss;
        }
        public static List<News> GetNewss()
        {
            return LoadNewss();
        }
        public static int AddNews(News news)
        {
            News LastNews = GetNewss().OrderByDescending(ord => ord.Id).FirstOrDefault();

            news.Id = 1;
            if (LastNews != null)
            {
                news.Id = LastNews.Id + 1;
            }
            List<News> Newss = new List<News>();
            Newss = GetNewss();
            Newss.Add(news);
            WriteDataToFile(Newss);

            return news.Id;
        }
        public static void UpdateNews(News news)
        {
            List<News> Newss = new List<News>();
            Newss = GetNewss();
            News oldNews = Newss.Find(us => us.Id == news.Id);
            Newss.Remove(oldNews);
            Newss.Add(news);
            WriteDataToFile(Newss);
        }
        public static void DeleteNews(int idNews)
        {
            List<News> Newss = new List<News>();
            Newss = GetNewss();

            News delNews = Newss.Where(prd => prd.Id == idNews).FirstOrDefault();

            Newss.Remove(delNews);
            WriteDataToFile(Newss);
        }
        #endregion

        #region MasterData
        public static MasterData LoadMasterData()
        {
            List<MasterData> MasterDatas = new List<MasterData>();
            MasterData MasterData = new MasterData();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath(@"~/App_Data/MasterData.xml"));

            MasterDatas = (List<MasterData>)Util.CreateObject(xmlDoc.InnerXml, MasterDatas);

            if(MasterDatas!=null && MasterDatas.Count > 0)
            {
                MasterData = MasterDatas[0];
            }

            return MasterData;
        }
        public static MasterData GetMasterData()
        {
            return LoadMasterData();
        }
        public static void UpdateMasterData(MasterData masterData)
        {
            List<MasterData> MasterDatas = new List<MasterData>();
            MasterDatas.Add(masterData);
            WriteDataToFile(MasterDatas);
        }
        #endregion
        private static void WriteDataToFile(object myData)
        {
            string FileName = "";
            string strView = "";
            XmlDocument xmlDoc = new XmlDocument();
            if (myData is List<Categoria>)
            {
                strView = Util.CreateXML((List<Categoria>)myData);
                FileName = HttpContext.Current.Server.MapPath(@"~/App_Data/Categoria.xml");
            }
            if (myData is List<Prodotto>)
            {
                strView = Util.CreateXML((List<Prodotto>)myData);
                FileName = HttpContext.Current.Server.MapPath(@"~/App_Data/Prodotto.xml");
            }
            if (myData is List<News>)
            {
                strView = Util.CreateXML((List<News>)myData);
                FileName = HttpContext.Current.Server.MapPath(@"~/App_Data/News.xml");
            }
            if (myData is List<MasterData>)
            {
                strView = Util.CreateXML((List<MasterData>)myData);
                FileName = HttpContext.Current.Server.MapPath(@"~/App_Data/MasterData.xml");
            }
            xmlDoc.LoadXml(strView);
            xmlDoc.Save(FileName);

            //if (myData is List<User>)
            //{
            //    //Users.Clear();
            //    //LoadUsers();
            //}
            //if (myData is List<Gallery>)
            //{
            //    Galleries.Clear();
            //    LoadGalleries();
            //}
            //if (myData is List<Event>)
            //{
            //    Events.Clear();
            //    LoadEvents();
            //}
            //if (myData is List<News>)
            //{
            //    AllNews.Clear();
            //    LoadAllNews();
            //}
        }
    }
}
