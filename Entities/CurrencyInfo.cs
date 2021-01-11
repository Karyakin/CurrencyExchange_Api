using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    ///<summary>
    ///данный класс для поставленной задачи не нужны обсалютно. Он добавлен для отображения того, что 
    ///что я сохранил все свойства из моделей, которые приходят нам по запросу из Нацбанки и могут использоватся дальше, но для
    ///данного примера они нам не нужны. CurrencyInfo можно удалить для данного случая
    ///</summary>
    public class CurrencyInfo
    {
        public Currency  Currency { get; set; }
        public RateShort RateShort { get; set; } 
    }
}

