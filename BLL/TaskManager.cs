using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TCIv3.Models;
using TCIv3.DAL;

namespace TCIv3.BLL
{
    public class TaskManager
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void Run()
        {
            logger.Info("Begin Run...");

            ArchiveFiles archiveFiles = new ArchiveFiles();
            RemoteFiles remoteFiles = new RemoteFiles();
            DownloadFiles downloadFiles = new DownloadFiles();

            DataAccess dataAccess = new DataAccess();
            AgencyInfo currentAgencyInfo = new AgencyInfo();

            List<string> listOfRemoteFiles = new List<string>();

            List<AgencyInfo> agencyInfoList = new List<AgencyInfo>();

            try
            {
                agencyInfoList = dataAccess.GetAgencyInfoList();
            }
            catch (Exception e)
            {
                logger.Error(e, "An error has occurred retrieving Agencies");
            }

            if (agencyInfoList.Count > 0)
            {
                logger.Info("[" + agencyInfoList.Count + "] active Agencies found.");

                foreach (AgencyInfo activeAgencyInfo in agencyInfoList)
                {
                    currentAgencyInfo = activeAgencyInfo;

                    logger.Debug("Current agency: [" + currentAgencyInfo.AgencyName + "]");

                    // Archive old files
                    logger.Debug("Archiving old files...");
                    logger.Debug("Local path: [" + currentAgencyInfo.LocalPath + "]");
                    archiveFiles.ArchiveOldFiles(currentAgencyInfo);

                    // Transfer files from vendor
                    logger.Debug("Getting list of remote files...");
                    currentAgencyInfo.RemoteFileList = remoteFiles.GetRemoteFilesList(currentAgencyInfo);
                    
                    //**********************************************//
                    logger.Debug("Transferring remote files...");
                    downloadFiles.DownloadRemoteFilesFTP(currentAgencyInfo);
                    //**********************************************//

                    // Unzip files
                    logger.Debug("Unzipping files...");
                    downloadFiles.DownloadRemoteFilesFTP(currentAgencyInfo);

                    // Rename citation images

                    // Stamp citation images

                    // Get data file

                    // Update ImportFileLog

                    // Save old files to archive and processed folders

                    // Delete old files

                    // Mark images

                    // Process files


                }
            }
            else
            {
                logger.Info("No active Agencies found.");
            }

            logger.Info("End Run...");
        }
    }
}
