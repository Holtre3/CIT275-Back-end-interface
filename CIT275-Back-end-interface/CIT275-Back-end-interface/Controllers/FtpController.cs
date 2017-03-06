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
        public ActionResult Index(FormCollection formCollection)
        {
            ProcessFiles();

            //FEEDBACK + COMPARISON

            return RedirectToAction("Index");
        }

        [NonAction]
        public void ProcessFiles()
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

            FileStream file = new FileStream(@"C:\Users\jacobs33\" + fileName, FileMode.Create);
            responseStream.CopyTo(file);
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