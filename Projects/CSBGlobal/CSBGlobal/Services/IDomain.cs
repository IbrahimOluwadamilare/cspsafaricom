using CSBGlobal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Services
{
    public interface IDomain
    {
        Task<GenericResponse<bool>> CheckDomainExistence(string domain);
    }
}
