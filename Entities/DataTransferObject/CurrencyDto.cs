using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.DataTransferObject
{
    /// <summary>
    /// Сущность Currency содержит кучу свойств, которые ответ от нацбанка не предоставляет(они Null) - 1е для чего в этом случае нуна Dtoшка
    /// Возвращаемый ответ содержит свойства из двух сущностей Currency и RateShort - 2е для чего в этом случае нуна Dtoшка
    /// Ну и вообще работа напрямую с сущностями - плохая идея. Если бы запрос был не Get и мог принимать Body, то и параметры в метод я бы передавал
    /// через Dto 
    /// </summary>
    public class CurrencyDto
    {
        [JsonPropertyName("Cur_ID")]
        public int CurID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public System.DateTime Date { get; set; }

        [JsonPropertyName("Cur_Abbreviation")]
        public string CurAbbreviation { get; set; }

        [JsonPropertyName("Cur_Scale")]
        public int CurScale { get; set; }

        [JsonPropertyName("Cur_Name")]
        public string CurName { get; set; }

        [JsonPropertyName("Cur_OfficialRate")]
        public decimal? CurOfficialRate { get; set; }
    }
}
