# Пример инструкции C#
> [!WARNING]
> Необходимо иметь на запускаемой машине Pg4Admin.
1. Открыть appsettings.json
2. В 'SimbirGoDbConnection' указать свою строку подключения
3. Открыть диспетчер пакетов, выбрать Simbir.Go.Api запускаемым проектом, Simbir.Go.DataAcess проектом по умолчанию
4. выполнить в диспетчере пакетов команду 'update-database'
## URL: https://localhost:7057/swagger/index.html

## Изменения
- Вместо того чтобы создавать для админа контроллеры отдельно я сделал разделение по ролям