using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;

namespace CIT275_Back_end_interface.Controllers
{
    public class FtpController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            // REMOVE AT LATER TIME
            // REMOVE AT LATER TIME
            string ftpAddress = "home200935066.1and1-data.host";
            string user = "u44756264-NMC";
            string password = "NMCdr0ne";
            // REMOVE AT LATER TIME
            // REMOVE AT LATER TIME

            NetworkCredential credential = new NetworkCredential(user, password);

            ViewBag.FileList = ListDirectory(ftpAddress, credential);

            //CREATE SESSION VARIABLES

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.ContentLength > 0 &&
                (Path.GetExtension(uploadedFile.FileName) == ".log" || Path.GetExtension(uploadedFile.FileName) == ".txt"))
            {
                string fileName = Path.GetFileName(uploadedFile.FileName);
                //string path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                string path = Path.Combine(@"C:\TempData\", fileName);
                uploadedFile.SaveAs(path);
            }

            //TODO: Give feedback to page
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessFiles(FormCollection collection)
        {
            // REMOVE AT LATER TIME
            // REMOVE AT LATER TIME
            string ftpAddress = "home200935066.1and1-data.host";
            string user = "u44756264-NMC";
            string password = "NMCdr0ne";
            // REMOVE AT LATER TIME
            // REMOVE AT LATER TIME

            var request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpAddress}/");
            NetworkCredential credential = new NetworkCredential(user, password);

            List<string> filesToProcess = ListDirectory(ftpAddress, credential);

            foreach (string file in filesToProcess)
            {
                DownloadFile(ftpAddress, credential, file);
                MoveFile(ftpAddress, credential, file);
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        static void DownloadFile(string ftpAddress, NetworkCredential credential, string fileName)
        {
            var request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpAddress}/{fileName}");
            request.Credentials = credential;
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            StreamReader sr = new StreamReader(responseStream);
            
            try
            {
                FileStream file = new FileStream(@"C:\TempData\" + fileName, FileMode.Create);
                responseStream.CopyTo(file);
            }
            catch (DirectoryNotFoundException de)
            {
                Directory.CreateDirectory(@"C:\TempData\");
                FileStream file = new FileStream(@"C:\TempData\" + fileName, FileMode.Create);
                responseStream.CopyTo(file);
            }            
            
        }

        [NonAction]
        static void MoveFile(string ftpAddress, NetworkCredential credential, string fileName)
        {
            var request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpAddress}/{fileName}");
            request.Credentials = credential;
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = $"../Logs/{fileName}";
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        }

        [NonAction]
        public List<string> ListDirectory(string ftpAddress, NetworkCredential credential)
        {
             var files = new List<string>();

            var request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpAddress}/");
            request.Credentials = credential;
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line.Contains(".log"))
                                files.Add(line);
                        }
                    }
                }
            }

            ViewBag.FileList = files;
            return files;
        }
    }
}