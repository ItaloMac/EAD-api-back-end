Sistema EAD - Back-end

Este repositório contém o back-end do sistema de Ensino a Distância (EAD), desenvolvido em .NET 8.0.

Tecnologias utilizadas

- .NET 8.0
- Entity Framework Core
- ASP.NET Core Web API
- Swagger (documentação da API)
- JWT (autenticação)
- MySql

Estrutura do Projeto:

Application/      
Domain/           
Infrastructure/  
InvictusAPI/
 └── .sln  
 └── Controllers.sln
 └── Program.cs
 ....

⚡ Funcionalidades principais

1. Cadastro e autenticação de usuários (alunos e administradores)
2. Gestão de cursos, modulos, aulas, matriculas, turmas e alunos.
3. Área administrativa (acesso autenticado)
4. Gateway de pagamento (atualmente configurado com Sandbox Asaas)
5. Portal do aluno (em desenvolvimento)
6. Upload de arquivos, fotos e videos.
7. Site institucional

Serviços Externos
1. Resend (envio de email)
2. Upload de fotos (Cloudnary)
3. Upload de videos (Vimeo)
4. Gateway de pagamento (Asaas)
5. Viacep
