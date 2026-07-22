
# Запуск приложения через терминал

    git clone https://github.com/LySpacy/InfotecsTest.git
    cd project
    docker compose up --build

Documentation Api (Swagger): http://localhost:5000/swagger

# Документация по методам апи
адрес апи http://localhost:5000/api/

## Endpoint /timescale
### /loadCSV
- Описание метода: Принимает на вход csv, после чего происходит обработка и сохранение данных файла в БД.
- Тип метода: Post
- Принимаемый тип: Файл
- Коды ответа: 200 (Ok), 400 (Bad request)

### /results
- Описание метода: Метод получения списка результатов, с возможность фильтрации
- Тип метода: Get
- Принимаемые параменты: string? FileName, DateTime? TimeStartFirstOperationStart, DateTime? TimeStartFirstOperationEnd, double? AverageValueeUp, double? AverageValueeDown, double? AverageExecutionTimeUp, double? AverageExecutionTimeDown 
(Все параментры имеют не обязательный характер)
- Отдаваемый тип тип: Коллекция из элементов ResultDTO
- Коды ответа: 200 (Ok), 400 (Bad request)

### /lastValueByFile
- Описание метода: Получения списка последних 10 значений, отсортированных по начальному времени запуска Date по имени заданного файла
- Тип метода: Get
- Принимаемые параменты: string fileName
- Отдаваемый тип: Коллекция из элементов ValueDTO
- Коды ответа: 200 (Ok), 400 (Bad request)

# Информация по проекту
- Платформа: .Net 10.0
- Архитектура: Луковая. 
- Слои: Domain (Сущности, бизнес логика сущностей), Application (Контракты, ViewModels, сервисная логика), Infrustraction (Парсинг файлов, взаимодействие с базой данных), API (Эндпоинты и методы)
- База данных: PostgreSQL
- Автоматическая документация API: Swagger
- Парсер CSV файлов: Библиотека CsvHelper
- Взаимодействие с базой данных: EFcore
