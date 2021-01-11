using Entities;
using Entities.DataTransferObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
   public interface IExchangeService
    {
        Task<IEnumerable<CurrencyDto>> GetCurrencyExchangeAsync(DateTime? dateTime, int? periodicity);
    }
}
