using CSBGlobal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Services
{

    public class DomainService: IDomain
    {
        private readonly ICSPapi _cspApi;

        public DomainService(ICSPapi CSPapi)
        {
            _cspApi = CSPapi;

        }
        public async Task<GenericResponse<bool>> CheckDomainExistence(string domain)
        {
            var avail = await _cspApi.IsDomainAvailableAsync(domain);
            
            return avail;
          
            
        }
    }
}
