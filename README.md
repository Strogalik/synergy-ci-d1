# Расчёт тарифов

Для запуска модульных тестов с покрытием выполните `dotnet test Tariffs.Tests -c Release --collect "XPlat Code Coverage"`.

Для измерения производительности выполните `dotnet run --project Bench -c Release` из корня проекта.

В GitHub Actions задача `test` является обязательной проверкой для ветки `main`.
