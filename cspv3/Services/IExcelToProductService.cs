using System.Threading.Tasks;

namespace cspv3.Services
{
    public interface IExcelToProductService
    {
        Task ConvertFileToProductString(string filePath);
    }
}