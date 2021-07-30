using NLog;
using System;
using System.Configuration;
using System.IO;
using TCIv3.Models;

namespace TCIv3.BLL
{
    public class ArchiveFiles
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void ArchiveOldFiles(AgencyInfo currentAgencyInfo)
        {
            //Moving old files from Processed folder to Processed_Archive

            logger.Info("Begin ArchiveOldFiles...");

            try
            {
                int daysToArchive = int.Parse(ConfigurationManager.AppSettings["DaysToArchive"]);
                string directoryPath = currentAgencyInfo.LocalPath + "\\Processed";
                string destinationPath = currentAgencyInfo.LocalPath + "\\Processed_Archive";

                System.IO.Directory.CreateDirectory(directoryPath);
                System.IO.Directory.CreateDirectory(destinationPath);

                logger.Debug("Days to Archive: [" + daysToArchive + "]");
                logger.Debug("Directory Path: [" + directoryPath + "]");
                logger.Debug("Destination Path: [" + destinationPath + "]");

                string[] listOfFiles = Directory.GetFiles(directoryPath);

                logger.Debug("# of files in [" + directoryPath + "] = [" + listOfFiles.Length + "]");

                foreach (string currentFile in listOfFiles)
                {
                    FileInfo currentFileInfo = new FileInfo(currentFile);
                    string currentFileName = Path.GetFileName(currentFile);

                    if (currentFileInfo.LastAccessTime < DateTime.Now.AddDays(daysToArchive))
                    {
                        logger.Debug("Archiving file: [" + currentFileName + "]");
                        currentFileInfo.MoveTo(destinationPath + "\\" + currentFileName);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "An error has occurred during ArchiveOldFiles");
            }

            logger.Info("End ArchiveFiles");

        }
    }
}
