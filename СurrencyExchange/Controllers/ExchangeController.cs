using Contracts;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using СurrencyExchange.Filters;

namespace СurrencyExchange.Controllers
{
    public class ExchangeController : ControllerBase
    {
        public IExchangeService _service;

        public ExchangeController(IExchangeService service)
        {
            _service = service;
        }

        //для Get не работает, чтобы применить фильтры всех видов нам нужны модели, а при таком запросе их нет.
        //в сам фильтр то мы зайдем, но валидация модели не отработает. Исключительно для демонстрации.
        [ServiceFilter(typeof(FilterAttribute))]
        [HttpGet("getExchangedData")]
        public async Task<ActionResult<IEnumerable<CurrencyDto>>> GetExchangedData(DateTime? onDate, int? periodicity)
        {
            var rezult = await _service.GetCurrencyExchangeAsync(onDate, periodicity);
            if (rezult == null)
                return BadRequest("Осибка сервиса. Полная информация в логах.C:/Projects/СurrencyExchange/Project/logs");
            return Ok(rezult);
        }
    }
}
