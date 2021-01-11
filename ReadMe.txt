1. Запускаем- \СurrencyExchangePublish\СurrencyExchange\bin\Release\netcoreapp3.1\publish\СurrencyExchange.exe
либо через CMD \СurrencyExchangePublish\СurrencyExchange\bin\Release\netcoreapp3.1\СurrencyExchange.dll
2. В любом браузере (на старых версиях Internet Explorer может не запустится) вбиваем адрес 
https://localhost:5001/getExchangedData?onDate=2020-12-24&periodicity=0
3. Получаем данные.
Если onDate корректный но на завтрашнюю дату - получим сведения на сегодняшнее число.
Если onDate не корректный получим НИЧЕГО и уведомление о некорректной дате.
Если periodicity не 1 или 0 - получим сведения по значению periodicity  == 0




