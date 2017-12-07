using System;
using System.Collections.Generic;

namespace TestAPI.Models
{
    public partial class Contracts
    {
        public int ContractId { get; set; }
        public string DealerName { get; set; }
        public string BusinessNumber { get; set; }
        public decimal? LoanAmount { get; set; }
        public string Status { get; set; }
        public DateTime? ContractActivationDate { get; set; }
        public string ContractType { get; set; }
        public string VersionUser { get; set; }
        public DateTime? VersionDate { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowCreatedDate { get; set; }

    }
}
