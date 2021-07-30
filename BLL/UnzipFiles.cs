using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TCIv3.Models;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace TCIv3.BLL
{
    public class UnzipFiles
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void UnzipSourceFiles (AgencyInfo currentAgencyInfo)
        {

            logger.Info("Begin UnzipSourceFiles...");

            try
            {
                
            }
            catch (Exception e)
            {
                logger.Error(e, "An error has occurred during UnzipSourceFiles");
            }

            logger.Info("End UnzipSourceFiles");

        }
    }
    
}
