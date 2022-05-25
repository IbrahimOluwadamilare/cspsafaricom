using System.Collections.Generic;
using cspv3.Models;
using System.Threading.Tasks;
using System.Linq;

namespace cspv3.Services
{
    public interface IProductOffering
    {
        Task<IEnumerable<ProductOffering>> GetProductOfferingsAsync();

        IQueryable<ProductOffering> ProductOfferings(string productCategory);

        IEnumerable<SubProductOffering> SubProductOfferings(string meterCategory);
    }
}
