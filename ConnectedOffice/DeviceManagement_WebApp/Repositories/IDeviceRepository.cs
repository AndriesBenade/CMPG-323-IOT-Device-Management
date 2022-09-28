using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repositories
{
    public class IDeviceRepository
    {
        public Guid DeviceID { get; set; }
        public string DeviceName { get; set; }
        public Guid CategoryID { get; set; }
        public Guid ZoneID { get; set; }
        public string Status { get; set; }
        public byte IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
