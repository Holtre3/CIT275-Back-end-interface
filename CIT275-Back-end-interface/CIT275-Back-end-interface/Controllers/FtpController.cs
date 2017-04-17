using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace CIT275_Back_end_interface.Controllers
{
    public class FtpController : Controller
    {
        static string _Pwd = "NMCdr0neIA67V@!";
        static string _IV = "nMcdr0n3157!!34V";

        [HttpGet]
        public ActionResult Index()
        {
            string[] ftpInfo = GetCredentials().Split('|');

            string ftpAddress = ftpInfo[0];
            string user = ftpInfo[1];
            string password = ftpInfo[2];

            this.Session["FTPServerAddress"] = ftpAddress;
            this.Session["FTPUserName"] = user;
            this.Session["FTPPassword"] = password;

            NetworkCredential credential = new NetworkCredential(user, password);

            ViewBag.FileList = ListDirectory(ftpAddress, credential);

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
                string path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                //string path = Path.Combine(@"C:\TempData\", fileName);
                try
                {
                    uploadedFile.SaveAs(path);
                }
                catch (Exception ex)
                {
                    
                }
            } 

            //TODO: Give feedback to page

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> ProcessFile(string fileName)
        {
            string[] ftpInfo = GetCredentials().Split('|');

            string ftpAddress = ftpInfo[0];
            string user = ftpInfo[1];
            string password = ftpInfo[2];

            var request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpAddress}/");
            NetworkCredential credential = new NetworkCredential(user, password);

            try
            {
                DownloadFile(ftpAddress, credential, fileName);
                MoveFile(ftpAddress, credential, fileName);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed. " + ex.Message);
            }

            return Json("File uploaded successfully");
        }

        [NonAction]
        static void DownloadFile(string ftpAddress, NetworkCredential credential, string fileName)
        {
            var request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpAddress}/Logs/{fileName}");
            request.Credentials = credential;
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            StreamReader sr = new StreamReader(responseStream);

            try
            {
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Uploads\\LogFiles\\" + fileName, FileMode.Create);
                //FileStream file = new FileStream("C:\\Users\\jacobs33\\Source\\Repos\\CIT275-Back-end-interface\\CIT275-Back-end-interface\\CIT275-Back-end-interface\\Uploads\\LogFiles\\" + fileName, FileMode.Create);
                responseStream.CopyTo(file);
            }
            catch (DirectoryNotFoundException de)
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Uploads\\LogFiles\\");
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Uploads\\LogFiles\\" + fileName, FileMode.Create);
                responseStream.CopyTo(file);
            }

        }

        [NonAction]
        static void MoveFile(string ftpAddress, NetworkCredential credential, string fileName)
        {
            var request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpAddress}/Logs/{fileName}");
            request.Credentials = credential;
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = $"../Logs-Archive/{fileName}";
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        }

        [NonAction]
        public List<string> ListDirectory(string ftpAddress, NetworkCredential credential)
        {
            var files = new List<string>();

            var request = (FtpWebRequest)WebRequest.Create($"ftp://{ftpAddress}/Logs/");
            request.Credentials = credential;
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            try
            {
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
            }
            catch (Exception e)
            {
                ViewBag.FileList = new List<string> { e.Message };
            }
            
            return files;
        }

        [HttpGet]
        public ActionResult Configuration(string message = "")
        {
            string ftpInformation;
            string[] ftpInfoArray = new String[3];

            ViewBag.Result = message;

            try
            {
                using (StreamReader sr = new StreamReader(Server.MapPath(@"~\FtpConfig.config"))){
                    ftpInformation = sr.ReadToEnd();                    
                }

                ftpInfoArray = ftpInformation.Split('|');

            }
            catch (FileNotFoundException)
            {
                ftpInfoArray[0] = "";
                ftpInfoArray[1] = "";
                ftpInfoArray[2] = "";
            }

            ViewBag.Hostname = ftpInfoArray[0];
            ViewBag.Username = ftpInfoArray[1];
            ViewBag.Password = ftpInfoArray[2];

            return View();
        }

        [HttpPost]
        public ActionResult Configuration(FormCollection collection)
        {
            string hostname = Convert.ToString(collection["hostname"]);
            string username = Convert.ToString(collection["username"]);
            string password = Convert.ToString(collection["password"]);
            string ftpInfo = hostname + "|" + username + "|" + password;
            string result = "";

            try
            {
                using (StreamWriter sw = new StreamWriter(Server.MapPath(@"~\FtpConfig.config")))
                {
                    sw.WriteLine(ftpInfo);
                }

                result = "Information saved!";
            }
            catch (DirectoryNotFoundException)
            {
                result = "Directory not found!";
            }
            catch (FileNotFoundException)
            {
                result = "File not found!";
            }

            return RedirectToAction("Configuration", "Ftp", new { message = result });
        }
        
        [NonAction]
        public string GetCredentials()
        {
            string ftpInfo = "";

            try
            {
                using (StreamReader sr = new StreamReader(Server.MapPath(@"~\FtpConfig.config")))
                {
                    ftpInfo = sr.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                return "File Not Found";
            }

            return ftpInfo;            
        }

        //TODO: Create a file log record
    }
}