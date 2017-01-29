using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Aplicação apenas para fins de avaliação do programa de seleção Stefanini.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Carlos Rogerio.";

            return View();
        }
    }
}