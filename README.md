# LLM-based-Agent-Service

An AI-powered backend service that enables a telecom carrier chatbot to understand user queries about mobile plans, recommend plans, or modify existing ones — all powered by OpenAI GPT and a real SQL Server database.

---

## Project Overview

This project aims to replace live representatives by enabling:
- Plan searching based on user needs
- AI-powered personalized plan recommendations
- Real-time plan change and upgrade support

---

## Tech Stack

| Component       | Tech                       |
|----------------|----------------------------|
| Backend API     | ASP.NET Core Web API (.NET 7) |
| LLM Integration | OpenAI GPT-3.5 Turbo       |
| Database        | SQL Server (Dockerized)    |
| Caching         | ASP.NET `MemoryCache`      |
| Frontend        | HTML + CSS + JS            |
| Infra Tools     | Docker, VS Code, Swagger   |

---

## Project Structure

```bash
LLM-based-Agent-Service/
├── AgentApiService/         # Backend API in C#
│   ├── Controllers/         # API endpoints
│   ├── Models/              # DTOs & response formats
│   ├── Services/            # Core logic: LLM, SQL exec, cache
│   ├── Repository/          # Raw SQL access
│   ├── Prompts/             # Prompt engineering templates
│   └── appsettings.json     # Config (ignored in .git)
├── chatbot-ui/              # Simple web chat UI (HTML + JS)
├── DB/                      # SQL schema and sample data
└── README.md
```

## Setup & Run Guide

### 1 Prerequisites
- .NET 7+ SDK  
- Docker  
- OpenAI API Key (GPT-3.5 or GPT-4)  
- VS Code (with C# & Live Server extensions)  
- Node.js (optional, for `npx serve`)

### 2 Start SQL Server in Docker
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong1!Pass" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

### 3 Initialize Database
Connect to `localhost,1433` as user `sa`/`YourStrong1!Pass`, then run DB/db_setup.sql

### 4 Configure App Settings
In `AgentApiService/appsettings.json`:
```json
{
  "OpenAI": { "ApiKey": "sk-xxxxxx" },
  "ConnectionStrings": {
    "SqlServer": "Server=localhost,1433;Database=carrier_ai_service;User Id=sa;Password=YourStrong1!Pass;Encrypt=False"
  }
}
```

### 5 Run Backend
```bash
cd AgentApiService
dotnet run
```
→ API at http://localhost:5120/swagger

### 6 Run Frontend
```bash
npx serve chatbot-ui
```
→ UI at http://127.0.0.1:5500

### 7 Test End-to-End
```bash
curl "http://localhost:5120/api/client/infoPass?sessionID=test&message=What%20is%20my%20plan"
```
Or in chat UI: “What’s the cheapest unlimited plan?”

## Demo Video
<https://youtu.be/soAnLER0mqc>





