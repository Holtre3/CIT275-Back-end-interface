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
        const int HASH_SIZE = 32;
        static string _Pwd = "NMCdr0neIA";
        static byte[] _Salt = new byte[] { 0x45, 0xF1, 0x61, 0x6e, 0x20, 0x00, 0x65, 0x64, 0x76, 0x65, 0x64, 0x03, 0x76 };

        [HttpGet]
        public ActionResult Index()
        {
            // REMOVE AT LATER TIME
            // REMOVE AT LATER TIME
            string[] ftpInfo = GetCredentials().Split('|');

            string ftpAddress = ftpInfo[0];
            string user = ftpInfo[1];
            string password = ftpInfo[2];

            /*string ftpAddress = "home200935066.1and1-data.host";
            string user = "u44756264-NMC";
            string password = "NMCdr0ne";*/
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
        public async Task<JsonResult> ProcessFile(string fileName)
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

            try
            {
                DownloadFile(ftpAddress, credential, fileName);
                MoveFile(ftpAddress, credential, fileName);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
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
                FileStream file = new FileStream(@"~\App_Data\LogFiles\" + fileName, FileMode.Create);
                responseStream.CopyTo(file);
            }
            catch (DirectoryNotFoundException de)
            {
                Directory.CreateDirectory(@"~\App_Data\LogFiles\");
                FileStream file = new FileStream(@"~\App_Data\LogFiles\" + fileName, FileMode.Create);
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

                byte[] cipherText = Encoding.ASCII.GetBytes(ftpInformation);
                //byte[] decryptedInfo = Decrypt(_Pwd, _Salt, cipherText);

                //ftpInfoArray = Encoding.ASCII.GetString(decryptedInfo).Split('|');
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
            /*
            byte[] saveString = Encoding.ASCII.GetBytes(hostname + "|" + username + "|" + password);
            byte[] ftpInformation = Encrypt(_Pwd, _Salt, saveString);
            */
            //save encrypted ftp information
            try
            {
                using (StreamWriter sw = new StreamWriter(Server.MapPath(@"~\FtpConfig.config")))
                {
                    //sw.Write(Encoding.ASCII.GetString(ftpInformation));
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
            string ftpInfo;

            try
            {
                using (StreamReader sr = new StreamReader(Server.MapPath(@"~\FtpConfig.Config")))
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

        [NonAction]
        public static byte[] Encrypt(string password, byte[] passwordSalt, byte[] plainText)
        {
            // Construct message with hash
            var msg = new byte[HASH_SIZE + plainText.Length];
            var hash = ComputeHash(plainText, 0, plainText.Length);
            Buffer.BlockCopy(hash, 0, msg, 0, HASH_SIZE);
            Buffer.BlockCopy(plainText, 0, msg, HASH_SIZE, plainText.Length);

            // Encrypt
            using (var aes = CreateAes(password, passwordSalt))
            {
                aes.GenerateIV();
                using (var enc = aes.CreateEncryptor())
                {

                    var encBytes = enc.TransformFinalBlock(msg, 0, msg.Length);
                    // Prepend IV to result
                    var res = new byte[aes.IV.Length + encBytes.Length];
                    Buffer.BlockCopy(aes.IV, 0, res, 0, aes.IV.Length);
                    Buffer.BlockCopy(encBytes, 0, res, aes.IV.Length, encBytes.Length);
                    return res;
                }
            }
        }
        [NonAction]
        public static byte[] Decrypt(string password, byte[] passwordSalt, byte[] cipherText)
        {
            using (var aes = CreateAes(password, passwordSalt))
            {
                var iv = new byte[aes.IV.Length];
                Buffer.BlockCopy(cipherText, 0, iv, 0, iv.Length);
                aes.IV = iv; // Probably could copy right to the byte array, but that's not guaranteed

                using (var dec = aes.CreateDecryptor())
                {
                    var decBytes = dec.TransformFinalBlock(cipherText, iv.Length, cipherText.Length - iv.Length);

                    // Verify hash
                    var hash = ComputeHash(decBytes, HASH_SIZE, decBytes.Length - HASH_SIZE);
                    var existingHash = new byte[HASH_SIZE];
                    Buffer.BlockCopy(decBytes, 0, existingHash, 0, HASH_SIZE);
                    if (!CompareBytes(existingHash, hash))
                    {
                        throw new CryptographicException("Message hash incorrect.");
                    }

                    // Hash is valid, we're done
                    var res = new byte[decBytes.Length - HASH_SIZE];
                    Buffer.BlockCopy(decBytes, HASH_SIZE, res, 0, res.Length);
                    return res;
                }
            }
        }
        [NonAction]
        static bool CompareBytes(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length) return false;
            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i]) return false;
            }
            return true;
        }
        [NonAction]
        static Aes CreateAes(string password, byte[] salt)
        {
            // Salt may not be needed if password is safe
            if (password.Length < 8) throw new ArgumentException("Password must be at least 8 characters.", "password");
            if (salt.Length < 8) throw new ArgumentException("Salt must be at least 8 bytes.", "salt");
            var pdb = new PasswordDeriveBytes(password, salt, "SHA512", 129);
            var key = pdb.GetBytes(16);

            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            return aes;
        }
        [NonAction]
        static byte[] ComputeHash(byte[] data, int offset, int count)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(data, offset, count);
            }
        }








        //TODO: Create a file log record
    }
}