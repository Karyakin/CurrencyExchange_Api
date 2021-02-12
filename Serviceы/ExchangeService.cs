using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObject;
using LoggerService;
using Microsoft.Extensions.Options;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Если бы было много сервисов, я бы создал класс типа "Wrapper" и через конструкторы засунул бы в тот Wrapper все сервисы до кучи, 
    /// чтобы в каждом сервисе не обращатся к HttpClientFactory и не дергать IOptions для получения url.
    /// </summary>
    public class ExchangeService : IExchangeService
    {
        public HttpClient _httpClient;
        private readonly IOptions<NationalBankSettings> _nationalBankConfig;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public ExchangeService(HttpClient httpClient, IOptions<NationalBankSettings> options, IMapper mapper, ILoggerManager logger)
        {
            _httpClient = httpClient;
            _nationalBankConfig = options;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CurrencyDto>> GetCurrencyExchangeAsync(DateTime? onDate, int? periodicity)
        {
            if (!onDate.HasValue || onDate.Value.AddDays(1) > onDate)//onDate.Value.AddDays(1) > onDate - такую проверку я бы не проводил, т.к. это может ввести в заблуждения пользователя(если он уже ввел данные на завтрашний день, то он и не будет смотреть, что ввел не то), но для нагладности проверил
                onDate = DateTime.Now;
            try
            {
                _logger.LogInfo($"Обращение к контроллеру ExchangeService, дата:{DateTime.Now}");
                if (!periodicity.HasValue || periodicity.Value < 0 || periodicity.Value > 1)
                    periodicity = 0;
                //|в appsettings.Development.json можно было положить адрес до вертикальной черты
                var str = $"{_nationalBankConfig.Value.BaseUrl}exrates/rates?ondate={onDate.Value.ToString("yyyy-MM-dd")}&periodicity={periodicity}";
                var responseString = await _httpClient.GetStringAsync(str);
                var catalogCurrencyDto = JsonSerializer.Deserialize<IEnumerable<CurrencyDto>>(responseString);

                #region ReadMe
                //два нижеприведенных объекта вдля поставленной задачи не нужны обсалютно. Они добавлено для отображения того, что 
                //что я сохранил все свойства из моделей, которые приходят нам по запросу из Нацбанки и могут использоватся дальше, но для
                //данного примера они нам не нужны. currency и rateShortEntity можно удалить
                var currency = _mapper.Map<IEnumerable<Currency>>(catalogCurrencyDto);// тут будут свойства Currency(согласно модельки с сайта)
                var rateShortEntity = _mapper.Map<IEnumerable<RateShort>>(catalogCurrencyDto);// тут будут свойства RateShort(согласно модельки с сайта)
                #endregion

                return catalogCurrencyDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"При попытке получения курсов валют произошла ошибка. Подробности " +
                   $"{ex.Message},\n {ex.Source}, \n {ex.HelpLink}");
                return null;
            }
        }
    }
}
