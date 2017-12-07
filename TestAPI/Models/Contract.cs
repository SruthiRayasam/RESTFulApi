using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class Contract
    {
        public long contractId { get; set; }
        public string dealerName { get; set; }
        public string businessNumber { get; set; }
        public DateTime contractActivationDate { get; set; }
        public double LoanAmount { get; set; }
        public string ContractType { get; set; }
        public string status { get; set; }
    }
}
