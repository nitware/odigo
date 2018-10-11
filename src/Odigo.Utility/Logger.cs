using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Utility.Interfaces;

namespace Odigo.Utility
{
    public class Logger : ILogger
    {
        private IFileManager _file;

        public Logger(IFileManager file)
        {
            _file = file;
        }

        public void LogError(Exception ex)
        {
            try
            {
                DateTime currentDate = DateTime.Now;

                string year = SysUtil.PaddNumber(currentDate.Year, 4);
                string month = SysUtil.PaddNumber(currentDate.Month, 2);
                string day = SysUtil.PaddNumber(currentDate.Month, 2);
                string hour = SysUtil.PaddNumber(currentDate.Hour, 2);
                string minute = SysUtil.PaddNumber(currentDate.Minute, 2);
                string second = SysUtil.PaddNumber(currentDate.Second, 2);
                string millisecond = SysUtil.PaddNumber(currentDate.Millisecond, 3);
                                
                string folderPath = _file.BasePath + @"Content\" + _file.LogFolderPath;
                string date = year + "_" + month + "_" + day + "_" + hour + "_" + minute + "_" + second + "_" + millisecond;

                //string file = folderPath + @"\log_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".txt";

                string file = folderPath + @"\EL " + date + ".txt";

                _file.CreateFolderIfNeeded(folderPath);

                _file.CreateLog(file, ex.ToString());
            }
            catch(Exception)
            {
                throw;
            }
        }






       
    }


}
