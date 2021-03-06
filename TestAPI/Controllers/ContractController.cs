﻿using System;
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

        }

        // GET api/values/1
        [HttpGet(Name = "GetContract")]
        public IActionResult GetAllContracts()
        {
            var item = _context.Contracts.Select(t=> t.Status =="Approved");
            try
            {

                if (item == null)
                {
                    return NotFound();
                }
            
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            }
            catch (Exception ex)
            {
                var logdetails = new EventLog()
                {
                    ContractId = 0,
                    LogMessage = ex.Message,
                    VersionUser = "testuser",
                    VersionDate = DateTime.Now
                };
                var errorlog = _context.Add(new EventLog());
                errorlog.CurrentValues.SetValues(logdetails);
                _context.SaveChangesAsync();

            }
            return new ObjectResult(item);
        }

        [HttpGet("{Id}")]
        public IActionResult GetByContractId(long Id)
        {
            var item = _context.Contracts.FirstOrDefault(t => t.ContractId == Id);
            try
            {

                if (item == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                var logdetails = new EventLog()
                {
                    ContractId = 0,
                    LogMessage = ex.Message,
                    VersionUser = "testuser",
                    VersionDate = DateTime.Now
                };
                var errorlog = _context.Add(new EventLog());
                errorlog.CurrentValues.SetValues(logdetails);
                _context.SaveChangesAsync();

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
                if (contract.LoanAmount > 0 && contract.LoanAmount <= 500000)
                {
                    contract.ContractType = "ExpressContract";
                    entry.CurrentValues.SetValues(contract);
                    await _context.SaveChangesAsync();
                }
                else if(contract.LoanAmount >0)
                {
                    contract.ContractType = "SalesContract";
                    entry.CurrentValues.SetValues(contract);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var logdetails = new EventLog()
                    {
                        ContractId = contract.ContractId,
                        LogMessage = "Loan Amount is less than 0, cannot create Contract for:" + contract.DealerName,
                        VersionUser = "testuser",
                        VersionDate = DateTime.Now
                    };
                    var errorlog = _context.Add(new EventLog());
                    errorlog.CurrentValues.SetValues(logdetails);
                    await _context.SaveChangesAsync();
                    return StatusCode(417);
                }
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
                await _context.SaveChangesAsync();
            }
            return CreatedAtRoute("GetContract", contract);
        }

        // PUT api/values/5.
        [HttpPut]
        public IActionResult Update([FromBody] Contracts item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var contractitem = _context.Contracts.FirstOrDefault(t => t.ContractId == item.ContractId);
            if (contractitem == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                contractitem.LoanAmount = item.LoanAmount;
                contractitem.DealerName = item.DealerName;
                if (item.LoanAmount > 0 && item.LoanAmount <= 500000)
                {
                    contractitem.ContractType = "ExpressContract";
                    _context.Contracts.Update(contractitem);
                    _context.SaveChanges();
                }
                else if(item.LoanAmount > 0)
                {
                    contractitem.ContractType = "SalesContract";
                    _context.Contracts.Update(contractitem);
                    _context.SaveChanges();
                }
                else
                {
                    var logdetails = new EventLog()
                    {
                        ContractId = contractitem.ContractId,
                        LogMessage = "Loan Amount is less than 0, cannot update Contract for:" + contractitem.DealerName,
                        VersionUser = "testuser",
                        VersionDate = DateTime.Now
                    };
                    var errorlog = _context.Add(new EventLog());
                    errorlog.CurrentValues.SetValues(logdetails);
                    _context.SaveChangesAsync();
                    return StatusCode(417);
                }
                
            }
            catch (Exception ex)
            {
                var logdetails = new EventLog()
                {
                    ContractId = contractitem.ContractId,
                    LogMessage = ex.Message,
                    VersionUser = "testuser",
                    VersionDate = DateTime.Now
                };
                var errorlog = _context.Add(new EventLog());
                errorlog.CurrentValues.SetValues(logdetails);
                _context.SaveChangesAsync();
            }
            return CreatedAtRoute("GetContract", new { id = contractitem.ContractId }, contractitem);
            // return new NoContentResult();
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
            return Ok("Contract Deleted");
        }
    }
}
