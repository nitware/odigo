using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Web.Mvc;

namespace Odigo.Utility.Interfaces
{
    public interface IFileManager
    {
        string JunkFolderPath { get; }
        string DestinationFolderPath { get; }
        string LogFolderPath { get; }
        string BasePath { get; }

        JsonResult Upload(HttpPostedFileBase file, string id, string folderPath, string url);
        string GetDestinationFilePath(string junkFilePath, string junkFolderPath, string destinationFolderPath);
        string GetFileDestinationRelativePath(string junkFileUrl, string junkFileSearchString, string junkFolderPath, string destinationFolderPath);
        void Move(string junkFileUrl, string destinationFileUrl, string junkFileSearchString);
        string GetRelativeFilePathFrom(string absolutefilePath, string startAt);
        string ChangeFileName(FileInfo file, string newFileNameStartString);
        void DeleteFileIfExist(string folderPath, string fileName);
        bool CreateFolderIfNeeded(string path);
        void CreateLog(string filePath, string content);
        bool Exist(string fileUrl);

    }





}
