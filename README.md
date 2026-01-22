# Кактус

Современный файлообменник с поддержкой HTTP/3.0 и шифрования файлов.

## О проекте

**Кактус** — веб-приложение для безопасного хранения и обмена файлами с использованием современных технологий передачи данных. Проект демонстрирует преимущества протокола HTTP/3.0 (QUIC) для российского рынка.

**Автор:** Колеров Артём

**Руководитель:** Ермихин Алексей Дмитриевич, учитель информатики

## Цель проекта

Создать файлообменник, работающий по протоколу HTTP/3.0, чтобы продемонстрировать преимущества данной технологии. Российские файлообменники (Облако Mail, Yandex Disk) пока не используют HTTP/3.0, в то время как зарубежные аналоги активно внедряют этот протокол.

## Технологии

| Компонент | Технология |
|-----------|------------|
| Платформа | ASP.NET Core 8.0 |
| Язык | C# |
| База данных | PostgreSQL + Entity Framework Core |
| Аутентификация | ASP.NET Core Identity |
| Шифрование файлов | AES |
| Хеширование паролей | BCrypt |
| Веб-сервер | Caddy (HTTP/3.0, QUIC, TLS 1.3) |
| Уведомления | AspNetCoreHero.ToastNotification |

## Возможности

- Регистрация и авторизация пользователей
- Загрузка файлов с опциональной защитой паролем
- AES-шифрование для защищённых файлов
- Скачивание файлов с проверкой пароля
- Управление файлами (просмотр, удаление)
- Поддержка HTTP/3.0 для ускоренной передачи данных

## Структура проекта

```
Kaktus/
├── Controllers/
│   ├── AccountController.cs    # Авторизация и регистрация
│   ├── HomeController.cs       # Управление файлами
│   └── TestController.cs       # Тестирование
├── Models/
│   ├── User.cs                 # Модель пользователя
│   ├── FileModel.cs            # Модель файла
│   ├── LoginModel.cs           # Модель входа
│   ├── RegistrationModel.cs    # Модель регистрации
│   └── ViewModel/
│       └── FileViewModel.cs    # ViewModel для загрузки
├── Views/
│   ├── Account/                # Страницы авторизации
│   ├── Home/                   # Главные страницы
│   └── Shared/                 # Общие компоненты
├── Services/
│   ├── FileManagerService.cs   # Логика работы с файлами
│   ├── Repository.cs           # Базовый репозиторий
│   └── Interfaces/             # Интерфейсы сервисов
├── Data/
│   └── Context.cs              # DbContext
├── Classes/
│   └── NotifyClasses/          # Уведомления
├── Migrations/                 # Миграции БД
├── UploadFiles/                # Хранилище файлов
└── wwwroot/                    # Статические файлы
```

## Запуск

### Требования

- .NET 8.0 SDK
- PostgreSQL

### Установка

1. Клонируйте репозиторий:
```bash
git clone https://github.com/rawer470/Kaktus.git
cd Kaktus
```

2. Настройте строку подключения в `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "MyDatabase": "Host=<host>;Port=5432;Username=<user>;Password=<password>;Database=kaktus"
  }
}
```

3. Примените миграции:
```bash
cd Kaktus
dotnet ef database update
```

4. Запустите приложение:
```bash
dotnet run
```

Приложение будет доступно по адресу `http://localhost:5500`

## Конфигурация Caddy

Для работы с HTTP/3.0 используется веб-сервер Caddy:

```caddyfile
kaktus.example.com {
    reverse_proxy localhost:5500

    tls {
        protocols tls1.3
    }

    header {
        Strict-Transport-Security "max-age=31536000; includeSubDomains"
        X-Content-Type-Options "nosniff"
        X-Frame-Options "DENY"
    }
}
```

### Установка Caddy (Ubuntu/Debian)

```bash
sudo apt install -y debian-keyring debian-archive-keyring apt-transport-https
curl -1sLf 'https://dl.cloudsmith.io/public/caddy/stable/gpg.key' | sudo gpg --dearmor -o /usr/share/keyrings/caddy-stable-archive-keyring.gpg
curl -1sLf 'https://dl.cloudsmith.io/public/caddy/stable/debian.deb.txt' | sudo tee /etc/apt/sources.list.d/caddy-stable.list
sudo apt update && sudo apt install caddy
sudo systemctl enable --now caddy
```

### Проверка HTTP/3

- В браузере Chrome: DevTools → Network → Protocol (должен показывать `h3`)
- Онлайн: [http3check.net](https://http3check.net)

## Архитектура

- **MVC Pattern** — разделение на Model, View, Controller
- **Repository Pattern** — абстракция доступа к данным
- **Dependency Injection** — внедрение зависимостей
- **Interface-based Design** — программирование на основе интерфейсов

## Безопасность

- Пароли пользователей хешируются с помощью BCrypt
- Файлы шифруются алгоритмом AES
- Авторизация через ASP.NET Core Identity
- TLS 1.3 через Caddy

## Почему HTTP/3.0?

HTTP/3.0 использует протокол QUIC поверх UDP вместо TCP:

- **0-RTT** — быстрое установление соединения
- **Устойчивость** — к потере пакетов
- **Встроенное шифрование** — безопасность по умолчанию
- **Мультиплексирование** — без блокировки head-of-line

## Этапы разработки

| Этап | Период |
|------|--------|
| Подготовительный | Сентябрь 2024 |
| Поисково-исследовательский | Октябрь 2024 |
| Конструкторско-технологический | Ноябрь 2024 |
| Оформительский | Декабрь 2024 |
| Апробация | Январь 2025 |
| Заключительный | Февраль 2025 |

## Лицензия

Учебный проект, 2024-2025

---

## Авторы

**Разработчик:** Колеров Артём
- GitHub: [@rawer470](https://github.com/rawer470)
- Gmail: rawer470@gmail.com



