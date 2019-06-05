using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CSBGlobal.Data;
using CSBGlobal.Models;
using CSBGlobal.Models.Products;
using CSBGlobal.Services;

namespace CSBGlobal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    // [Authorize]

    public class DomainController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        private readonly IDomain _domainservice;

        public DomainController(ApplicationDbContext context, IDomain domain)
        {
            _context = context;
            _domainservice = domain;
        }
        [HttpGet]
        public async Task<ActionResult<GenericResponse<bool>>> CheckDomainExistence(string domain)
        {
            try
            {
                var dom = await _domainservice.CheckDomainExistence(domain);
                if (dom.Message == null)
                {
                    return Ok(dom);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, dom);

                }


            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new GenericResponse<bool>
                {
                    Message = e.Message,
                    Data  = false,
                    Success = false
                });
            }

        }
        }

    }


