using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Web.Mvc;
using System.Configuration;
using Odigo.Utility.Interfaces;

namespace Odigo.Utility
{
    public class FileManager : Controller, IFileManager
    {
        private const string JUNK_FOLDER_PATH = "Junk";
        private const string DESTINATION_FOLDER_PATH = "Person";
        private const string LOG_FOLDER_PATH = "Log";

        private string appRoot = ConfigurationManager.AppSettings["AppRoot"];
        public const string DEFAULT_AVATAR = "/Content/Images/default_avatar.png";
        public static string TILDA = "~";
        
        private readonly string _basePath;

        public FileManager(string basePath)
        {
            _basePath = basePath;
        }

        public string JunkFolderPath { get { return JUNK_FOLDER_PATH; } }
        public string DestinationFolderPath { get { return DESTINATION_FOLDER_PATH; } }
        public string LogFolderPath { get { return LOG_FOLDER_PATH; } }
        public string BasePath { get { return _basePath; } }

        public JsonResult Upload(HttpPostedFileBase file, string id, string folderPath, string url)
        {
            string path = null;
            string imageUrl = null;
            string displayImageUrl = null;
            string message = "File upload failed";
            bool isUploaded = false;

            try
            {
                if (file != null && file.ContentLength != 0)
                {
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        Guid guid = Guid.NewGuid();
                        id = guid.ToString().Replace("-", "");
                    }

                    FileInfo fileInfo = new FileInfo(file.FileName);
                    string fileExtension = fileInfo.Extension;
                    string newFile = id + "__";
                    string newFileName = newFile + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + fileExtension;

                    string invalidFileMessage = InvalidFile(file.ContentLength, fileExtension);
                    if (!string.IsNullOrEmpty(invalidFileMessage))
                    {
                        isUploaded = false;
                        TempData["imageUrl"] = null;
                        return Json(new { isUploaded = isUploaded, message = invalidFileMessage, imageUrl = url }, "text/html", JsonRequestBehavior.AllowGet);
                    }

                    string mapPathFilePath = TILDA + folderPath;
                    //string pathForSaving = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Junk");

                    string pathForSaving = System.Web.Hosting.HostingEnvironment.MapPath("~" + folderPath);
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        DeleteFileIfExist(pathForSaving, id);

                        file.SaveAs(Path.Combine(pathForSaving, newFileName));

                        isUploaded = true;
                        message = "File uploaded successfully!";

                        path = Path.Combine(pathForSaving, newFileName);
                        if (path != null)
                        {
                            imageUrl = folderPath + "/" + newFileName;
                            displayImageUrl = appRoot + imageUrl + "?t=" + DateTime.Now;
                        }
                    }
                }
                else
                {
                    isUploaded = false;
                    TempData["imageUrl"] = null;
                    return Json(new { isUploaded = isUploaded, message = "No file uploaded or uploaded file content lenght is zero! Please upload a file", imageUrl = url }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                message = string.Format("File upload failed: {0}", ex.Message);
            }

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("isUploaded", isUploaded);
            result.Add("message", message);
            result.Add("displayImageUrl", displayImageUrl);
            result.Add("imageUrl", imageUrl);

            return Json(result, "text/html", JsonRequestBehavior.AllowGet);

            //return Json(new { isUploaded = isUploaded.ToString(), message = message, displayImageUrl = displayImageUrl, imageUrl = imageUrl }, "text/html", JsonRequestBehavior.AllowGet);
        }

        private string InvalidFile(decimal uploadedFileSize, string fileExtension)
        {
            try
            {
                string message = null;
                decimal oneKiloByte = 1024;
                decimal maximumFileSize = 20 * oneKiloByte;

                decimal actualFileSizeToUpload = Math.Round(uploadedFileSize / oneKiloByte, 1);
                if (InvalidFileType(fileExtension))
                {
                    message = "File type '" + fileExtension + "' is invalid! File type must be any of the following: .jpg, .jpeg, .png or .jif ";
                }
                else if (actualFileSizeToUpload > (maximumFileSize / oneKiloByte))
                {
                    message = "Your file size of " + actualFileSizeToUpload.ToString("0.#") + " Kb is too large, maximum allowed size is " + (maximumFileSize / oneKiloByte) + " Kb";
                }

                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool InvalidFileType(string extension)
        {
            try
            {
                extension = extension.ToLower();
                switch (extension)
                {
                    case ".jpg":
                    case ".png":
                    case ".gif":
                    case ".jpeg":
                        return false;

                    default:
                        return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteFileIfExist(string folderPath, string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folderPath) || string.IsNullOrWhiteSpace(fileName))
                {
                    throw new Exception("Folder path or file search string cannot be null! Please contact your system administrator.");
                }

                string wildCard = fileName + "*.*";
                IEnumerable<string> files = Directory.EnumerateFiles(folderPath, wildCard, SearchOption.TopDirectoryOnly);

                if (files != null && files.Count() > 0)
                {
                    foreach (string file in files)
                    {
                        System.IO.File.Delete(file);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }

        public string GetDestinationFilePath(string junkFilePath, string junkFolderPath, string destinationFolderPath)
        {

            try
            {
                return junkFilePath.Replace(junkFolderPath, destinationFolderPath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Move(string junkFileUrl, string destinationFileUrl, string junkFileSearchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(destinationFileUrl))
                {
                    throw new Exception("Destination file path cannot be empty! Please try again. Contact your administrator after three unsuccessful trials.");
                }
                
                //string junkFilePath = System.Web.Hosting.HostingEnvironment.MapPath(TILDA + junkFileUrl);
                //string destinationFilePath = System.Web.Hosting.HostingEnvironment.MapPath(TILDA + destinationFileUrl);

                string junkFilePath = _basePath + junkFileUrl.Replace("/", @"\");
                string destinationFilePath = _basePath + destinationFileUrl.Replace("/", @"\");
                string folderPath = Path.GetDirectoryName(destinationFilePath);

                if (!CreateFolderIfNeeded(folderPath))
                {
                    throw new Exception("Destination file path failed during creation! Please try again!");
                }

                DeleteFileIfExist(folderPath, junkFileSearchString);
                if (System.IO.File.Exists(junkFilePath) && !string.IsNullOrWhiteSpace(destinationFilePath))
                {
                    System.IO.File.Move(junkFilePath, destinationFilePath);
                }
                else if (!string.IsNullOrWhiteSpace(destinationFilePath))
                {
                    System.IO.File.Create(destinationFilePath);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ChangeFileName(FileInfo file, string newFileNameStartString)
        {
            try
            {
                string fileName = file.Name;
                string fileExtension = file.Extension;
                string directory = Path.GetDirectoryName(file.FullName);
                string newFileName = newFileNameStartString + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + fileExtension;

                return directory + @"\" + newFileName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetRelativeFilePathFrom(string absolutefilePath, string startAt)
        {
            try
            {
                int stringLenght = absolutefilePath.Length;
                int startIndex = absolutefilePath.IndexOf(startAt);
                int endIndex = stringLenght - startIndex;

                string relativePath = absolutefilePath.Substring(startIndex, endIndex);
                if (!string.IsNullOrWhiteSpace(relativePath))
                {
                    relativePath = relativePath.Replace("\\", "/");
                }

                return relativePath;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exist(string fileUrl)
        {
            try
            {
                //string filePath = System.Web.Hosting.HostingEnvironment.MapPath(TILDA + fileUrl);

                string filePath = _basePath + fileUrl.Replace("/", @"\");
                return System.IO.File.Exists(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetFileDestinationRelativePath(string junkFileUrl, string junkFileSearchString, string junkFolderPath, string destinationFolderPath)
        {
            try
            {
                string startAt = @"\Content";
                string destinationFileUrl = GetDestinationFilePath(junkFileUrl, junkFolderPath, destinationFolderPath);
               
                string destinationFolder = _basePath + startAt + @"\" + destinationFolderPath;
                if (!CreateFolderIfNeeded(destinationFolder))
                {
                    throw new Exception("Destination folder does not exist or cannot be created!");
                }

                //System.IO.FileInfo file = new System.IO.FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(TILDA + destinationFileUrl));

                string fileUrl = _basePath + destinationFileUrl.Replace("/", @"\"); 
                FileInfo file = new FileInfo(fileUrl);
                string newAbsoluteFilePath = ChangeFileName(file, junkFileSearchString);
                
                return GetRelativeFilePathFrom(newAbsoluteFilePath, startAt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateLog(string filePath, string content)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

       



    }
}