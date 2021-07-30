using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using TCIv3.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace TCIv3.DAL
{
    public class DataAccess
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static OdyClerkInternalEntities odyClerkInternalDB = new OdyClerkInternalEntities();

        public List<AgencyInfo> GetAgencyInfoList()
        {
            logger.Debug("Begin GetAgencyInfoList...");

            List<AgencyInfo> agencyInfoList = new List<AgencyInfo>();

            ObjectResult<TCIv3_GetAgencyInfo_Result> getAgencyInfo_Results = odyClerkInternalDB.TCIv3_GetAgencyInfo();

            try
            {
                foreach (TCIv3_GetAgencyInfo_Result agencyInfo_Result in getAgencyInfo_Results)
                {
                    AgencyInfo agencyInfo = new AgencyInfo();

                    agencyInfo.RecordID = agencyInfo_Result.RecordId;
                    agencyInfo.VendorAgencyID = agencyInfo_Result.VendorAgencyId;
                    agencyInfo.CitationType = agencyInfo_Result.CitationType;
                    agencyInfo.VendorName = agencyInfo_Result.VendorName;
                    agencyInfo.AgencyName = agencyInfo_Result.AgencyName;
                    agencyInfo.ConnectionType = agencyInfo_Result.ConnectionType;
                    agencyInfo.ServerName = agencyInfo_Result.ServerName;
                    agencyInfo.ServerUserName = agencyInfo_Result.ServerUserName;
                    agencyInfo.ServerPassword = agencyInfo_Result.ServerPassword;
                    agencyInfo.ServerPort = agencyInfo_Result.ServerPort;
                    agencyInfo.LocalPath = agencyInfo_Result.LocalPath;
                    agencyInfo.RemotePath = agencyInfo_Result.RemotePath;
                    agencyInfo.SSHKey = agencyInfo_Result.SSHKey;
                    agencyInfo.Description = agencyInfo_Result.Description;
                    agencyInfo.Active = agencyInfo_Result.Active;
                    agencyInfo.BCPFormatFile = agencyInfo_Result.BCPFormatFile;
                    agencyInfo.AgencyCode = agencyInfo_Result.AgencyCode;
                    agencyInfo.NodeID = agencyInfo_Result.NodeID;
                    agencyInfo.SLA = agencyInfo_Result.SLA;

                    agencyInfoList.Add(agencyInfo);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Agency Info retrieval error.");
            }            

            logger.Debug("End GetAgencyInfoList...");

            return agencyInfoList;
        }
    }
}
