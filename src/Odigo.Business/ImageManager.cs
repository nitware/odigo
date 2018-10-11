using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using Odigo.Utility.Interfaces;
using Odigo.Entities;

namespace Odigo.Business
{
    public class ImageManager : IImageManager
    {
        private readonly IRepository _da;
        private readonly IFileManager _file;

        public ImageManager(IRepository da, IFileManager file)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            _da = da;
            _file = file;
        }

        public string Store(Teacher teacher)
        {
            try
            {
                string junkFileUrl = teacher.ImageFileUrl;

                if (junkFileUrl.Contains(_file.JunkFolderPath))
                {
                    string junkFileSearchString = teacher.Person.Id.ToString() + "__";
                    string path = _file.GetFileDestinationRelativePath(junkFileUrl, junkFileSearchString, _file.JunkFolderPath, _file.DestinationFolderPath);

                    TEACHER entity = _da.GetSingleBy<TEACHER>(s => s.Person_Id == teacher.Person.Id);
                    entity.Image_File_Url = path;

                    if (_da.Update(entity))
                    {
                        _file.Move(junkFileUrl, path, junkFileSearchString);
                        return path;
                    }
                }
                else if (junkFileUrl.Contains(_file.DestinationFolderPath))
                {
                    return junkFileUrl;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }

}
