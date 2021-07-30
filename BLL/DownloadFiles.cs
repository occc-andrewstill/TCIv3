using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using NLog;
using TCIv3.Models;

namespace TCIv3.BLL
{
	public class DownloadFiles
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public void DownloadRemoteFilesFTP(AgencyInfo currentAgencyInfo)
		{

			logger.Info("Begin DownloadRemoteFilesFTP...");

			try
			{
				List<string> remoteFileList = currentAgencyInfo.RemoteFileList;

				foreach (string file in remoteFileList)
				{
					logger.Debug("Transferring file: [" + file + "]");

					if (file.Substring(file.Length - 3) == "zip" || file.Substring(file.Length - 3) == "ZIP" || file.Substring(file.Length - 3) == "Zip"
						|| file.Substring(file.Length - 3) == "cit" || file.Substring(file.Length - 3) == "dat")
					{
						string serverUri = "ftp://" + currentAgencyInfo.ServerName + currentAgencyInfo.RemotePath + file;

						FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
						request.Credentials = new NetworkCredential(currentAgencyInfo.ServerUserName, currentAgencyInfo.ServerPassword);

						FtpWebResponse response = (FtpWebResponse)request.GetResponse();
						Stream responseStream = response.GetResponseStream();
						StreamReader reader = new StreamReader(responseStream);

						byte[] buffer = new byte[16 * 16384];
						int len = 0;
						FileStream objFS = new FileStream(currentAgencyInfo.LocalPath + "\\" + file, FileMode.Create, FileAccess.Write, FileShare.Read);

						logger.Debug("Now downloading file: " + currentAgencyInfo.LocalPath + "\\" + file + " for agency: " + currentAgencyInfo.AgencyName);

						while ((len = reader.BaseStream.Read(buffer, 0, buffer.Length)) != 0)
						{
							objFS.Write(buffer, 0, len);
						}

						objFS.Close();
						response.Close();
					}
					else if (File.Exists(currentAgencyInfo.LocalPath + "\\" + file))
					{
						logger.Debug("File: " + currentAgencyInfo.LocalPath + "\\" + file + " already exist at local path");
						continue;
					}
					else
					{
					}
				}

			}
			catch (Exception ex)
			{
				logger.Error(ex, "Error during file download.");
			}


			logger.Info("End DownloadRemoteFilesFTP");
		}
	}
}

