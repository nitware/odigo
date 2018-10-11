using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Odigo.Utility.Interfaces;
using Odigo.Utility;

namespace Odigo.Unity.Test
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestLogError()
        {
            IFileManager file = new FileManager(@"C:\Users\Dan\Documents\Visual Studio 2015\Projects\Odigo\Odigo.Web" + @"\Content");
            ILogger logger = new Logger(file);

            Exception ex = new Exception("Testing exception handling!!!");
            
            logger.LogError(ex);
        }




    }
}
