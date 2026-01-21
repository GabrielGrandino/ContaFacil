# 💰 ContaFácil – Sistema de Controle de Gastos Residenciais

Sistema completo para controle de gastos residenciais, composto por **Backend em .NET** e **Frontend em React + TypeScript**, desenvolvido com foco em regras de negócio, clareza de código e boas práticas.

---

## 📌 Visão Geral

O **ContaFácil** permite:

- Cadastro de pessoas
- Cadastro de categorias (despesa / receita / ambas)
- Cadastro de transações financeiras
- Aplicação de regras de negócio (ex.: menor de idade não pode ter receita)
- Visualização de totais por pessoa e total geral
- Dashboard com visão consolidada dos dados

Backend e frontend são **totalmente desacoplados**.

---

## 🛠 Tecnologias – Backend

- .NET 8
- C#
- Entity Framework Core
- PostgreSQL (Supabase)
- MediatR
- Serilog
- Swagger / OpenAPI

---

## 🗄 Banco de Dados

- PostgreSQL (Supabase)
- Estrutura criada manualmente
- EF Core utilizado apenas para mapeamento e consultas
- Views utilizadas:
  - `vw_person_totals`
  - `vw_global_totals`
  - `vw_category_totals` (opcional)

---

## ▶️ Executando o Backend

### 1️⃣ Executar

```bash
dotnet run
```

### 2️⃣ Swagger

```bash
https://localhost:{porta}/swagger
```

## 🛠 Tecnologias – Front-End

- React
- TypeScript
- Vite
- Axios
- CSS puro (sem frameworks)

---

## ▶️ Executando o Frontend

### 1️⃣ Instalar dependencias

```bash
npm install
```

### 2️⃣ Executar

```bash
npm run dev
```

### 3️⃣ Acessar
```bash
http://localhost:5173
```
