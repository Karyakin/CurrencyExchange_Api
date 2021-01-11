using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Класс взят с сайта нацбанка. Свойства не трогал. Закомитанный код - альтернативная запись.
    /// </summary>
    public class RateShort
    {
        public int Cur_ID { get; set; }
        // В этом случае атрибут не нужен и работать он не будет, т.к. мы не используем эту модель. Чисто для примера.
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        [Key]
        public System.DateTime Date { get; set; }

        public decimal? Cur_OfficialRate { get; set; }// == public Nullable<decimal> Cur_OfficialRate { get; set; }
    }
}
