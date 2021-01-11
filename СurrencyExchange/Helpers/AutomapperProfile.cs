using AutoMapper;
using Entities;
using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace СurrencyExchange.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Currency, CurrencyDto>().ReverseMap();
            CreateMap<RateShort, CurrencyDto>().ReverseMap();
            CreateMap<CurrencyInfo, CurrencyDto>().ReverseMap();
        }
    }
}
