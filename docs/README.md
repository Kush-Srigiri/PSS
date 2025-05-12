# ⚙️ PSS — Production Steering System  
*Produktions‑Steuerungs‑System*

<p align="left">
  
  <a href="https://github.com/Kush-Srigiri/PSS/blob/main/LICENSE">
    <img alt="License: MIT" src="https://img.shields.io/badge/license-MIT-blue.svg">
  </a>
  <img alt="Made with C#" src="https://img.shields.io/badge/made%20with-C%23-239120?logo=c-sharp&logoColor=fff">
  <img alt="Version" src="https://img.shields.io/github/v/release/Kush-Srigiri/PSS?include_prereleases">
</p>

> **PSS** is your smart, modular system for **planning, steering, monitoring & optimising** production processes.  
> Einfach. Flexibel. Zuverlässig.

---

## 📑 Table of Contents
1. [Features](#features)
2. [Architecture](#architecture)
3. [Tech Stack](#tech-stack)
4. [Quick Start](#quick-start)
5. [Configuration](#configuration)
6. [Database Model](#database-model)
7. [Testing](#testing)
8. [Documentation](#documentation)
9. [Roadmap](#roadmap)
10. [Contributing](#contributing)
11. [Security](#security)
12. [License](#license)
13. [Contact](#contact)

---

## ✨ Features
- **Order & Job Scheduling** – GANTT‑based finite‑capacity planning  
- **Live Shop‑Floor Monitoring** – real‑time KPIs, OEE dashboards  
- **Adaptive Alerts** – rule‑based notifications for deviations  
- **Plug‑in API** – extend with custom data sources or logic  
- **Role‑based Access Control** – granular user & group permissions  
- **REST + gRPC** integration layer – talk to ERP/MES, PLCs & IIoT devices  
- **Multi‑tenant ready** – segregate data for plants or customers  

---

## 🏗️ Architecture
```mermaid
flowchart LR
    UI[Web / Desktop Clients]
    API[REST / gRPC API]
    CORE[Application Core<br/>Domain-Driven Design]
    DB[(PostgreSQL)]
    MSG[(RabbitMQ)]
    EXT[External Systems<br/>(ERP, PLC, IIoT)]
    UI -- HTTPS --> API
    API -- CQRS / Mediator --> CORE
    CORE -- EF Core --> DB
    CORE -- Pub/Sub --> MSG
    MSG -- Consumers --> CORE
    API <-- WebHooks --> EXT
```
> Full ER & relational models are available in  
> `PSS_ER_Modell.drawio` and `PSS_Relationale_Modell.drawio`.

---

## 🛠️ Tech Stack
| Layer | Technology |
|-------|------------|
| Language | **C# 12**, .NET 8 LTS |
| Persistence | PostgreSQL · Entity Framework Core |
| Messaging | RabbitMQ (AMQP 0‑9‑1) |
| Build & CI | GitHub Actions · `dotnet` CLI |
| Testing | xUnit · FluentAssertions |
| Docs | MkDocs Material |

---

## 🚀 Quick Start
```bash
# 1 Clone
git clone https://github.com/Kush-Srigiri/PSS.git
cd PSS

# 2 Configure connection string
cp config/appsettings.Development.json.example    src/PSS/appsettings.Development.json
# → update DB, MQ credentials

# 3 Restore + build
dotnet restore
dotnet build --configuration Release

# 4 Apply migrations & seed demo data
dotnet ef database update --project src/PSS

# 5 Run
dotnet run --project src/PSS
```
Navigate to **`https://localhost:5001`** and log in with the seeded *admin* account (`admin / admin123` by default).

---

## ⚙️ Configuration
All tunables live in **`config/appsettings.{Environment}.json`**.

| Key | Purpose | Default |
|-----|---------|---------|
| `ConnectionStrings:Postgres` | DB endpoint | `Host=localhost;Port=5432;...` |
| `MessageBus:Host` | RabbitMQ URL | `amqp://guest:guest@localhost:5672` |
| `Kpi:RefreshIntervalSec` | Dashboard poll rate | `10` |

> Environment variables override JSON keys: `PSS__ConnectionStrings__Postgres=...`.

---

## 🗄️ Database Model
![ER diagram](docs/images/er_model.png)

*See `/PSS_ER_Modell.drawio` for the editable diagram.*

---

## ✔️ Testing
```bash
dotnet test
```
The pipeline runs **unit + integration tests** on every PR via GitHub Actions and publishes code‑coverage to the *Tests* badge.

---

## 📚 Documentation
Full end‑user & developer docs live under **`/docs`** and are auto‑deployed to GitHub Pages at  
<https://kush-srigiri.github.io/PSS/>.

---

## 🛤️ Roadmap
- [ ] OPC UA driver for direct PLC connectivity  
- [ ] Mobile‑first responsive UI  
- [ ] AI‑assisted schedule optimisation (Genetic Algorithm)  
- [ ] Multi‑language support (EN/DE/ES)  

---

## 🤝 Contributing
1. Fork → Feature Branch → PR  
2. Follow the **Conventional Commits** spec.  
3. Run `dotnet format` before pushing.  
4. Write tests that cover your change.

Please read [`CONTRIBUTING.md`](CONTRIBUTING.md) for the full guide.

<a href="https://github.com/kush-srigiri/pss/graphs/contributors">
  <img src="https://contrib.rocks/image?&columns=25&max=10000&&repo=kush-srigiri/pss" />
</a>

---

## 🛡️ Security
If you discover a vulnerability, please review [`SECURITY.md`](SECURITY.md) and **responsibly disclose** it via the e‑mail listed there.

---

## 📄 License
Distributed under the **MIT License**.  
See [`LICENSE`](LICENSE) for details.

---

## 📬 Contact
[**@Kush Srigiri**](https://github.com/Kush-Srigiri) – *maintainer* 
[**@Jamie Poeffel**](https://github.com/Jamie-Poeffel) – *maintainer* 
[**@Alessio Huber**](https://github.com/Alessio-Huber) – *Contributor* 

---

> © 2025 Kush Srigiri, Jamie Poeffel, Alessio Huber



