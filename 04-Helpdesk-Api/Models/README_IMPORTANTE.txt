ATENÇÃO!

Este projeto de API não compilará até que você gere os Modelos do seu banco de dados.
Os arquivos TUsuario.cs, TPerfil.cs, HelpdeskDBContext.cs, etc., precisam ser criados.

COMO FAZER:
1. Abra o Visual Studio.
2. Abra o "Console do Gerenciador de Pacotes" (Ferramentas > ...).
3. No console, verifique se o "Projeto padrão" está selecionado para "Helpdesk.Api".
4. Copie e cole o comando abaixo no console e pressione Enter:

Scaffold-DbContext "Server=localhost;Database=HelpdeskDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context HelpdeskDBContext -Force

5. Após o comando terminar, o projeto estará pronto para compilar!