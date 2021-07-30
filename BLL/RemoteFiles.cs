using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using NLog;
using TCIv3.Models;
using System.Configuration;
using System.IO;
using System.Linq;

namespace TCIv3.BLL
{
    public class RemoteFiles
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<string> GetRemoteFilesList(AgencyInfo currentAgencyInfo)
        {
            logger.Info("Begin GetRemoteFilesList...");

            string connectionType = currentAgencyInfo.ConnectionType;
            List<string> remoteFileList = new List<string>();
            string serverURI = "ftp://" + currentAgencyInfo.ServerName + currentAgencyInfo.RemotePath;
            string serverName = currentAgencyInfo.ServerName;
            int port = Convert.ToInt32(currentAgencyInfo.ServerPort);
            string serverUserName = currentAgencyInfo.ServerUserName;
            string serverPassword = currentAgencyInfo.ServerPassword;
            string remotePath = currentAgencyInfo.RemotePath;

            logger.Debug("Connection Type: [" + connectionType + "]");
            logger.Debug("ServerURI: [" + serverURI + "] / Port: [" + port + "]");
            logger.Debug("Server Name: [" + serverName + "]");
            logger.Debug("Server UserName: [" + serverUserName + "]");

            try
            {


                switch (connectionType)
                {
                    case "FTP":
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverURI);
                        request.Method = WebRequestMethods.Ftp.ListDirectory;

                        request.Credentials = new NetworkCredential(serverUserName, serverPassword);

                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            using (Stream responseStream = response.GetResponseStream())
                            {
                                using (StreamReader reader = new StreamReader(responseStream))
                                {
                                    while (responseStream.CanRead && !reader.EndOfStream)
                                    {
                                        remoteFileList.Add(reader.ReadLine());
                                    }

                                }
                            }
                        }
                        break;

                    case "SFTP":
                        using (Renci.SshNet.SftpClient sftpConn = new Renci.SshNet.SftpClient(serverName, port, serverUserName.Normalize(), serverPassword))
                        {
                            sftpConn.BufferSize = 1024 * 32 - 52;
                            sftpConn.Connect();

                            var Files = (from file in sftpConn.ListDirectory(currentAgencyInfo.RemotePath, null) select file).ToList();

                            foreach (var file in Files)
                            {
                                remoteFileList.Add(file.FullName);
                            }
                        }
                        break;

                    case "FILE":
                        if (Directory.Exists(remotePath))
                        {
                            remoteFileList = System.IO.Directory.GetFiles(remotePath).ToList();
                        }
                        else
                        {
                            logger.Debug("RemotePath not found [" + remotePath + "]");
                        }
                        break;

                    default:
                        break;
                }

                logger.Debug("[" + remoteFileList.Count + "] files found");

                foreach (string file in remoteFileList)
                {
                    logger.Debug("[" + file + "]");
                }

            }
            catch (Exception e)
            {
                logger.Error(e, "An error has occurred retrieving list of remote files");
            }

            logger.Info("End GetRemoteFilesList");

            return remoteFileList;

        }
    }
}
