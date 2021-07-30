using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCIv3.Models
{
    public class AgencyInfo
    {
        public int RecordID { get; set; }
        public int VendorAgencyID { get; set; }
        public string CitationType { get; set; }
        public string VendorName { get; set; }
        public string AgencyName { get; set; }
        public string ConnectionType { get; set; }
        public string ServerName { get; set; }
        public string ServerUserName { get; set; }
        public string ServerPassword { get; set; }
        public Nullable<int> ServerPort { get; set; }
        public string LocalPath { get; set; }
        public string RemotePath { get; set; }
        public string SSHKey { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Active { get; set; }
        public string BCPFormatFile { get; set; }
        public string NodeID { get; set; }
        public string AgencyCode { get; set; }
        public Nullable<int> SLA { get; set; }
        public List<string> RemoteFileList { get; set; }
    }
}
