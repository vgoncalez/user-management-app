# 📘 User Management App

Este projeto consiste em uma aplicação fullstack para gerenciamento de usuários, com autenticação JWT, arquitetura limpa, CQRS e frontend em Angular. O ambiente é totalmente conteinerizado com Docker.

---

## 🧰 Tecnologias Utilizadas

- **Backend:** .NET 8, ASP.NET Core, EF Core, MediatR, JWT
- **Frontend:** Angular 20 (Standalone Components)
- **Banco de Dados:** SQL Server
- **Containerização:** Docker e Docker Compose

---

## ⚙️ Como rodar o projeto localmente

> Pré-requisitos:
> - Docker e Docker Compose instalados
> - .NET 8 SDK (se quiser rodar backend fora do Docker)
> - Node.js e Angular CLI (se quiser rodar frontend fora do Docker)

### 📦 1. Clonar o repositório

```bash
git clone https://github.com/vgoncalez/user-management-app.git
cd UserManagementApp
```

### 🐳 2. Subir tudo com Docker

```bash
docker-compose up --build
```

Aguarde até que:
- O SQL Server esteja totalmente inicializado (pode demorar alguns segundos).
- O backend suba na porta `5000`.
- O frontend esteja disponível na porta `4200`.

### 📋 3. Endpoints disponíveis

| Serviço     | URL                                      |
|-------------|------------------------------------------|
| Swagger     | http://localhost:5000/swagger            |
| API (backend) | http://localhost:5000/api               |
| Frontend (Angular) | http://localhost:4200                  |

### 🧪 4. Usuário padrão para testes

Durante o startup, é criado automaticamente um usuário administrador:

```bash
Email: admin@admin.com
Senha: AdminSenha@123
```

> ⚠️ Este usuário é inserido apenas se não existir no banco.

---

## ⚙️ Detalhes Técnicos

### 🔐 Autenticação

- JWT gerado no login e armazenado no `localStorage` (no front).
- Proteção de rotas com guards no Angular.
- Políticas de autorização no ASP.NET Core.

### 🧱 Arquitetura

- **Clean Architecture**
- **CQRS** com `MediatR`
- `FluentValidation` para validações

---

## 🧑‍💻 Rodando manualmente (sem Docker)

### Backend:

```bash
cd backend/src/UserManagement.Api
dotnet ef database update
dotnet run
```

Acesse: `http://localhost:5041/swagger`

### Frontend:

```bash
cd frontend
npm install
ng serve
```

Acesse: `http://localhost:4200`

> Lembre-se de ajustar o `environment.ts` com a URL da API.