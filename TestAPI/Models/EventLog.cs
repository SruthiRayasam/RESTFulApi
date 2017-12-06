using System;
using System.Collections.Generic;

namespace TestAPI.Models
{
    public partial class EventLog
    {
        public int LogId { get; set; }
        public int? ContractId { get; set; }
        public string LogMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string VersionUser { get; set; }
        public DateTime? VersionDate { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowCreatedDate { get; set; }
    }
}
