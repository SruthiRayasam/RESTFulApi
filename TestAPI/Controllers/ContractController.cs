using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using NLog;

namespace TestAPI.Controllers
{
    [Route("api/contracts")]
    public class ContractController : Controller
    {
        private readonly masterContext _context;
        /// <summary>
        /// NLog logger instance used for logging.
        /// </summary>
        protected Logger logger;
        public ContractController(masterContext context)
        {
            _context = context;

            //if (_context.Count() == 0)
            //{
            //    _context.Contracts.Add(new Contract
            //    {
            //        dealerName = "TestDealer1",
            //        businessNumber = "317-242-4339",
            //        contractActivationDate = DateTime.Today,
            //        LoanAmount = 250000.00,
            //        status = "Approved"
            //    });
            //    _context.SaveChanges();
            //}
        }
        /// GET api/values
        //[HttpGet]
        //public IEnumerable<Contract> GetAll()
        //{
        //     return _context.Contracts
        //    //{ "value1", "value2" };
        //}

        // GET api/values/1
        [HttpGet(Name = "GetContract")]
        public IActionResult GetAllContracts()
        {
            var item = _context.Contracts;
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContractAsync([FromBody] Contracts contract)
        {
            if (contract == null)
            {
                return BadRequest("");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var entry = _context.Add(new Contracts());
                entry.CurrentValues.SetValues(contract);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var logdetails = new EventLog()
                {
                    ContractId = contract.ContractId,
                    LogMessage=ex.Message,
                    VersionUser="testuser",
                    VersionDate=DateTime.Now
                };
                var errorlog = _context.Add(new EventLog());
                errorlog.CurrentValues.SetValues(logdetails);
                await _context.SaveChangesAsync();
            }
            return CreatedAtRoute("GetContract", contract);
        }

        // PUT api/values/5.
        [HttpPut]
        public IActionResult Update([FromBody] Contract item)
        {
            if (item == null)
            {
                return BadRequest();
            }

          //  var contractitem = _context.ContractDetails.FirstOrDefault(t => t.contractId == item.contractId);
            //if (contractitem == null)
            //{
            //    return NotFound();
            //}
            try
            { 
            //contractitem.LoanAmount = item.LoanAmount;
            //contractitem.dealerName = item.dealerName;

            //_context.ContractDetails.Update(contractitem);
            //_context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);
            }
            // return CreatedAtRoute("GetContract", new { id = contractitem.contractId }, contractitem);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] Contracts item)
        {
            var contract = _context.Contracts.FirstOrDefault(t => t.ContractId == item.ContractId);
            if (contract == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Contracts.Remove(contract);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var logdetails = new EventLog()
                {
                    ContractId = contract.ContractId,
                    LogMessage = ex.Message,
                    VersionUser = "testuser",
                    VersionDate = DateTime.Now
                };
                var errorlog = _context.Add(new EventLog());
                errorlog.CurrentValues.SetValues(logdetails);
                _context.SaveChangesAsync();
                return StatusCode(417);
            }
            return new NoContentResult();
        }
    }
}
