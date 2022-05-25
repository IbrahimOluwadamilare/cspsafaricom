using System;
using System.Threading.Tasks;
using cspv3.Models;

namespace cspv3.Services
{
    public interface IFirstCheckout
    {
        Task<FirstCheckoutResponse> TransactionVerification(string referenceId);
    }
}
