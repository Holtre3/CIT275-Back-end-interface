using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIT275_Back_end_interface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Documentation()
        {
            

            return View();
        }
        public ActionResult HowTo()
        {


            return View();
        }
        public FileResult Download()
        {
            string path = ControllerContext.HttpContext.Server.MapPath("~/Documentation/Documentation.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Documentation.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}