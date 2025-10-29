# 🖥️ Desktop Manager - Windows Forms

Aplicação desktop para gestores administrarem o sistema de helpdesk.

## 🚀 Execução

```bash
dotnet restore
dotnet run
```

Ou abrir no Visual Studio 2022 e pressionar F5.

## 👤 Usuário de Teste

- Email: `gestor@helpdesk.com`
- Senha: `Gestor@123`

## 📁 Estrutura

- **Forms/**: FormLogin, FormPrincipal, FormChamados, FormUsuarios
- **Services/**: ApiService
- **Models/**: Chamado, Usuario
- **Utils/**: SessionManager

## ✨ Funcionalidades

✅ Login com JWT  
✅ CRUD de chamados  
✅ Atribuir técnicos  
✅ Gerenciar usuários  
✅ Relatórios com DataGridView  

## 📝 Nota

Use o **Visual Studio Designer** para criar as interfaces gráficas dos Forms.
Os arquivos .Designer.cs serão gerados automaticamente.
