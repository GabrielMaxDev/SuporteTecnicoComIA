# 📦 Guia de Instalação Completo

Este guia detalha passo a passo como instalar e configurar todo o sistema de helpdesk.

---

## 1️⃣ Pré-requisitos

### Ferramentas Necessárias

- **.NET 8 SDK**
  - Download: https://dotnet.microsoft.com/download/dotnet/8.0
  - Verificar instalação: `dotnet --version`

- **SQL Server**
  - SQL Server Express: https://www.microsoft.com/sql-server/sql-server-downloads
  - Ou SQL Server LocalDB (incluído no Visual Studio)

- **Visual Studio 2022** (recomendado) ou **VS Code**
  - Download VS 2022: https://visualstudio.microsoft.com/downloads/
  - Workloads necessários:
    - ASP.NET and web development
    - .NET desktop development

- **Android Studio** (apenas para mobile)
  - Download: https://developer.android.com/studio
  - SDK Platform API 24+

- **Git**
  - Download: https://git-scm.com/downloads

---

## 2️⃣ Configuração do Banco de Dados

### Opção A: SQL Server LocalDB

```sql
-- Conectar via SQL Server Management Studio (SSMS) ou Azure Data Studio
-- Server name: (localdb)\mssqllocaldb

-- Criar banco de dados
CREATE DATABASE HelpdeskDB;
GO

USE HelpdeskDB;
GO

-- As tabelas serão criadas automaticamente pelo Entity Framework Migrations
```

### Opção B: SQL Server Express

```sql
-- Server name: localhost\SQLEXPRESS

CREATE DATABASE HelpdeskDB;
GO
```

### String de Conexão

LocalDB:
```
Server=(localdb)\mssqllocaldb;Database=HelpdeskDB;Trusted_Connection=true;
```

SQL Express:
```
Server=localhost\SQLEXPRESS;Database=HelpdeskDB;Trusted_Connection=true;
```

SQL Server Remoto:
```
Server=seu-servidor.com;Database=HelpdeskDB;User Id=usuario;Password=senha;
```

---

## 3️⃣ Criar API Backend

### 3.1 Criar Projeto

```bash
# Navegar para pasta do projeto
cd HelpdeskCompleto

# Criar projeto API
dotnet new webapi -n HelpdeskAPI
cd HelpdeskAPI
```

### 3.2 Adicionar Pacotes NuGet

```bash
# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0

# Identity (Autenticação)
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0

# Swagger
dotnet add package Swashbuckle.AspNetCore --version 6.5.0

# OpenAI (opcional)
dotnet add package Azure.AI.OpenAI --version 1.0.0-beta.12
```

### 3.3 Configurar appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HelpdeskDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Key": "SuaChaveSuperSecretaComMaisDe32Caracteres12345678901234567890",
    "Issuer": "HelpdeskAPI",
    "Audience": "HelpdeskClients",
    "ExpirationHours": 8
  },
  "OpenAI": {
    "ApiKey": "sk-proj-sua-chave-openai-aqui",
    "Model": "gpt-4"
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:5001",
      "http://localhost:3000"
    ]
  }
}
```

### 3.4 Executar Migrations

```bash
# Criar migration inicial
dotnet ef migrations add InitialCreate

# Aplicar ao banco de dados
dotnet ef database update
```

### 3.5 Executar API

```bash
dotnet run --urls "http://localhost:5000"
```

Acesse Swagger: `http://localhost:5000/swagger`

---

## 4️⃣ Configurar Web Client

### 4.1 Restaurar Dependências

```bash
cd ../01-WebClient-ASPNET
dotnet restore
```

### 4.2 Configurar appsettings.json

Edite `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ApiSettings": {
    "BaseUrl": "http://localhost:5000/api/",
    "Timeout": 30
  },
  "Authentication": {
    "CookieName": "HelpdeskAuth",
    "ExpirationMinutes": 480,
    "LoginPath": "/Account/Login"
  }
}
```

### 4.3 Instalar Dependências Frontend

```bash
# Criar wwwroot/lib se não existir
mkdir -p wwwroot/lib

# Bootstrap 5 (via CDN nas Views, não precisa instalar localmente)
# jQuery (via CDN nas Views)
# Font Awesome (via CDN nas Views)
```

### 4.4 Executar Web Client

```bash
dotnet run --urls "http://localhost:5001"
```

Acesse: `http://localhost:5001`

---

## 5️⃣ Configurar Desktop Manager

### 5.1 Restaurar Dependências

```bash
cd ../02-DesktopManager-WinForms
dotnet restore
```

### 5.2 Configurar API URL

Edite `Services/ApiService.cs`:

```csharp
private const string BASE_URL = "http://localhost:5000/api/";
```

### 5.3 Executar

```bash
dotnet run
```

Ou abrir no Visual Studio e pressionar F5.

---

## 6️⃣ Configurar Mobile Técnico (Android)

### 6.1 Abrir no Android Studio

1. Abrir Android Studio
2. File > Open
3. Selecionar pasta `03-MobileTecnico-Android`
4. Aguardar Gradle Sync

### 6.2 Configurar API URL

Edite `app/src/main/java/com/helpdesk/tecnico/network/ApiClient.java`:

```java
// Para emulador Android
private static final String BASE_URL = "http://10.0.2.2:5000/api/";

// Para dispositivo físico na mesma rede
// private static final String BASE_URL = "http://SEU-IP-LOCAL:5000/api/";
// Exemplo: "http://192.168.1.100:5000/api/";
```

### 6.3 Configurar Firebase (Opcional - para Push Notifications)

1. Acesse: https://console.firebase.google.com
2. Crie um novo projeto
3. Adicione um app Android
4. Package name: `com.helpdesk.tecnico`
5. Download `google-services.json`
6. Copie para `app/google-services.json`

### 6.4 Executar no Emulador

1. Tools > Device Manager
2. Criar AVD (Android Virtual Device)
3. Run > Run 'app'

### 6.5 Executar em Dispositivo Físico

1. Habilitar "Opções do desenvolvedor" no celular
2. Habilitar "Depuração USB"
3. Conectar via USB
4. Run > Run 'app'

---

## 7️⃣ Testar o Sistema

### 7.1 Verificar API

```bash
curl http://localhost:5000/api/health
```

### 7.2 Testar Login Web

1. Acesse: http://localhost:5001
2. Email: `cliente@helpdesk.com`
3. Senha: `Cliente@123`

### 7.3 Testar Desktop

1. Executar DesktopManager
2. Email: `gestor@helpdesk.com`
3. Senha: `Gestor@123`

### 7.4 Testar Mobile

1. Executar app Android
2. Email: `tecnico@helpdesk.com`
3. Senha: `Tecnico@123`

---

## 8️⃣ Configurar OpenAI (Opcional)

### 8.1 Obter API Key

1. Acesse: https://platform.openai.com/api-keys
2. Crie uma conta (se não tiver)
3. Generate new secret key
4. Copie a chave (começa com `sk-proj-...`)

### 8.2 Configurar na API

Edite `HelpdeskAPI/appsettings.json`:

```json
{
  "OpenAI": {
    "ApiKey": "sk-proj-SUA-CHAVE-AQUI",
    "Model": "gpt-4"
  }
}
```

### 8.3 Testar Sugestão da IA

1. Acesse Web Client
2. Criar Novo Chamado
3. Preencha título e descrição
4. Clique em "Obter Sugestão da IA"

---

## 9️⃣ Troubleshooting

### Erro: "A conexão foi recusada"

**Solução**: Verifique se a API está rodando em `http://localhost:5000`

```bash
netstat -ano | findstr :5000
```

### Erro: "Não foi possível conectar ao SQL Server"

**Solução**: Verifique se o SQL Server está rodando

```bash
# PowerShell
Get-Service | Where-Object {$_.Name -like "*SQL*"}

# Iniciar SQL Server LocalDB
sqllocaldb start mssqllocaldb
```

### Erro: "CORS policy blocked"

**Solução**: Configure CORS na API

```csharp
// Program.cs da API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5001")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// ...
app.UseCors();
```

### Android: "Failed to connect to /10.0.2.2:5000"

**Solução**: Use o IP correto

```bash
# Descobrir IP local (Windows)
ipconfig

# Linux/Mac
ifconfig

# Atualizar ApiClient.java com seu IP local
private static final String BASE_URL = "http://192.168.X.X:5000/api/";
```

---

## 🔟 Deploy em Produção

### 10.1 Publicar API

```bash
cd HelpdeskAPI
dotnet publish -c Release -o ./publish

# IIS: Copiar conteúdo de ./publish para pasta do IIS
# Docker: Criar Dockerfile
```

### 10.2 Publicar Web Client

```bash
cd 01-WebClient-ASPNET
dotnet publish -c Release -o ./publish
```

### 10.3 Publicar Desktop

```bash
cd 02-DesktopManager-WinForms
dotnet publish -c Release -r win-x64 --self-contained
```

### 10.4 Gerar APK Android

```bash
cd 03-MobileTecnico-Android
./gradlew assembleRelease

# APK em: app/build/outputs/apk/release/app-release.apk
```

---

## ✅ Checklist Final

- [ ] .NET 8 SDK instalado
- [ ] SQL Server instalado e rodando
- [ ] Banco HelpdeskDB criado
- [ ] API rodando em localhost:5000
- [ ] Web Client rodando em localhost:5001
- [ ] Desktop Manager executando
- [ ] Mobile Android executando
- [ ] Usuários de teste criados
- [ ] OpenAI API configurada (opcional)
- [ ] Firebase configurado (opcional)

---

**🎉 Instalação concluída! Sistema pronto para uso!**
