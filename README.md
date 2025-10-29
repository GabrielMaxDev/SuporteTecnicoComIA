# 🎫 Sistema de Chamados de Suporte Técnico com Inteligência Artificial

Sistema completo de helpdesk dividido em **3 aplicações** integradas via API REST, seguindo padrões MVC, boas práticas modernas e com integração de IA para sugestões automáticas.

---

## 🌟 Visão Geral

- ✅ **Web Client (ASP.NET Core MVC)**: Interface web responsiva para clientes
- ✅ **Desktop Manager (Windows Forms)**: Aplicação desktop para gestores
- ✅ **Mobile Técnico (Android Java)**: App móvel para técnicos
- ✅ **API REST centralizada**: Backend em .NET 8 em `http://localhost:5000/api/`
- ✅ **Integração com IA**: Sugestões automáticas usando OpenAI/ChatGPT
- ✅ **Autenticação segura**: JWT para API e Cookies para Web
- ✅ **Notificações push**: Firebase Cloud Messaging

---

## 📁 Estrutura do Projeto

```
/HelpdeskCompleto/
├── README.md                          # Este arquivo
├── .gitignore                         # Arquivos ignorados
├── INSTALACAO.md                      # Guia de instalação
├── 01-WebClient-ASPNET/              # 🌐 Web Client MVC
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── Services/
│   ├── wwwroot/
│   ├── Program.cs
│   ├── WebClient.csproj
│   └── README.md
├── 02-DesktopManager-WinForms/       # 🖥️ Desktop Manager
│   ├── Forms/
│   ├── Services/
│   ├── Models/
│   ├── Program.cs
│   ├── DesktopManager.csproj
│   └── README.md
└── 03-MobileTecnico-Android/         # 📱 Mobile Android
    ├── app/
    ├── build.gradle
    └── README.md
```

---

## 🚀 Início Rápido

### Pré-requisitos

1. **.NET 8 SDK** - https://dotnet.microsoft.com/download/dotnet/8.0
2. **SQL Server** (LocalDB ou Express)
3. **Visual Studio 2022** ou **VS Code**
4. **Android Studio** (para mobile) - https://developer.android.com/studio
5. **OpenAI API Key** - https://platform.openai.com/api-keys

### 1. Configurar Banco de Dados

```sql
CREATE DATABASE HelpdeskDB;
```

### 2. Criar e Configurar API Backend

```bash
dotnet new webapi -n HelpdeskAPI
cd HelpdeskAPI

# Adicionar pacotes
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

Configure `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HelpdeskDB;Trusted_Connection=true;"
  },
  "Jwt": {
    "Key": "SuaChaveSuperSecretaAqui123456789012345678901234567890",
    "Issuer": "HelpdeskAPI",
    "Audience": "HelpdeskClients",
    "ExpirationHours": 8
  },
  "OpenAI": {
    "ApiKey": "sk-proj-sua-chave-aqui"
  }
}
```

### 3. Executar Web Client

```bash
cd 01-WebClient-ASPNET
dotnet restore
dotnet run --urls "http://localhost:5001"
```

Acesse: `http://localhost:5001`

### 4. Executar Desktop Manager

```bash
cd 02-DesktopManager-WinForms
dotnet restore
dotnet run
```

### 5. Executar Mobile (Android)

1. Abra o projeto no Android Studio
2. Configure o arquivo `ApiClient.java` com o IP correto
3. Adicione `google-services.json` do Firebase
4. Sync Gradle
5. Run 'app'

---

## 🎯 Usuários de Teste

| Email | Senha | Perfil |
|-------|-------|--------|
| admin@helpdesk.com | Admin@123 | Administrador |
| gestor@helpdesk.com | Gestor@123 | Gestor |
| tecnico@helpdesk.com | Tecnico@123 | Técnico |
| cliente@helpdesk.com | Cliente@123 | Cliente |

---

## 📚 Documentação Detalhada

- [Instalação Completa](INSTALACAO.md)
- [Web Client - README](01-WebClient-ASPNET/README.md)
- [Desktop Manager - README](02-DesktopManager-WinForms/README.md)
- [Mobile Técnico - README](03-MobileTecnico-Android/README.md)

---

## 🔌 API Endpoints

### Autenticação
```
POST /api/auth/login
POST /api/auth/register
```

### Chamados
```
GET    /api/chamados
GET    /api/chamados/{id}
POST   /api/chamados
PUT    /api/chamados/{id}
DELETE /api/chamados/{id}
GET    /api/chamados/meus
GET    /api/chamados/atribuidos
```

### IA
```
POST /api/ia/sugestao
```

### Comentários
```
GET  /api/comentarios/chamado/{id}
POST /api/comentarios
```

---

## 🛠️ Tecnologias Utilizadas

### Web Client
- ASP.NET Core 8 MVC
- Bootstrap 5
- jQuery 3.7
- Font Awesome 6

### Desktop Manager
- .NET 8 Windows Forms
- HttpClient
- JWT Authentication

### Mobile
- Android SDK (API 24+)
- Java 8+
- Retrofit2
- Room Database
- Firebase FCM

---

## 🔒 Segurança

- ✅ JWT Bearer Tokens
- ✅ Cookie-based Authentication
- ✅ BCrypt password hashing
- ✅ DataAnnotations validation
- ✅ CSRF protection
- ✅ SQL Injection prevention
- ✅ XSS protection

---

## 📧 Suporte

Para dúvidas ou problemas:
- Abra uma issue no GitHub
- Email: suporte@helpdesk.com

---

## 📄 Licença

MIT License - Livre para uso comercial e pessoal.

---

**⭐ Desenvolvido com .NET 8, Android Java e integração com OpenAI!**
