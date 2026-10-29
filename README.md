
# 🏢 BergerCRM — Real Estate Management System

### Enterprise-Oriented Backend Development with C# & .NET

![C#](https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![.NET](https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge)

---

## 🌟 Overview

**BergerCRM** is a real estate CRM system being independently developed for a business client.

The project is designed to support real estate developers and sales teams by organizing property projects, customer relationships, leads, sales processes, and daily operational activities.

The backend is built using **C#, ASP.NET Core, .NET 10, Entity Framework Core, and SQL Server**, with an emphasis on modular architecture, relational database design, and maintainability.

Unlike a simple CRUD demonstration, the project involves designing a comprehensive business domain with interconnected entities, reference data, and workflows.

**Project status:** Active development. The database foundation, entity models, migrations, and initial reference-data setup have been implemented.

---

## 🎯 Project Goals

- Centralize real estate project and customer information
- Support lead tracking and sales pipeline management
- Model buildings, units, and related property information
- Organize customer interactions and follow-up activities
- Provide a foundation for a future back-office and customer portal
- Enable future AI-assisted sales workflows

---

## 🛠️ Technology Stack

| Layer | Technologies |
|---|---|
| Language | C# |
| Framework | .NET 10, ASP.NET Core Web API |
| ORM | Entity Framework Core 10 |
| Database | Microsoft SQL Server |
| Architecture | Layered architecture |
| Data Modeling | Relational entities and relationships |
| Database Versioning | EF Core Migrations |
| Development Tools | Visual Studio, SQL Server Management Studio, Git & GitHub |

---

## 🏗️ Architecture

The solution separates data-access responsibilities from the API application.

```text
BergerCRM
│
├── CRM.Api
│   └── ASP.NET Core Web API
│
└── CRM.DAL
    ├── Data
    │   └── CrmDbContext.cs
    │
    ├── Entities
    │   └── Domain models
    │
    ├── Migrations
    │   └── EF Core database migrations
    │
    └── Seed Data
        └── Initial reference data
```

### Architectural Principles

- Separation of concerns
- Object-oriented domain modeling
- Relational database integrity
- Centralized database access through EF Core
- Maintainable and extensible solution structure
- Consistent entity metadata and timestamps

The architecture is intended to support additional services, business workflows, authentication, and API endpoints as development progresses.

---

## 🧩 Business Modules

The data model covers the main areas of a real estate CRM.

### 🏗️ Projects & Properties

Structured management of real estate projects and their related entities.

Includes domain models for:

- Projects
- Buildings
- Property units
- Unit types and statuses
- Asset types and statuses
- Project and building reference data

### 👥 Customers

Data structures for managing customer information and classifications.

Includes:

- Customer records
- Customer types
- Customer statuses
- Related customer information

### 📈 Leads & Sales

A foundation for organizing sales opportunities and customer acquisition.

Includes:

- Lead records
- Lead sources
- Lead statuses
- Lost reasons
- Sales-related classifications

### 📞 Activities

Domain models for tracking customer interactions and business activities.

### ✅ Tasks

Task-management data structures, including task states and priorities.

### 🤝 Deals

Deal-related entities and pipeline stages.

### 🔐 Identity & Audit

Data structures supporting user identity, authorization modeling, and auditing.

---

## 🗄️ Database Design

One of the central engineering challenges in BergerCRM is the design of a comprehensive relational data model.

The solution includes **47 EF Core DbSets** across multiple business domains.

### Implemented Database Foundations

- Entity classes and relational mappings
- Primary and foreign key relationships
- Shared base entity metadata
- Creation and update timestamps
- Entity Framework Core DbContext
- Database migrations
- Initial reference-data seeders
- SQL Server database initialization

### Database Modules

| Module | DbSets |
|---|---:|
| Identity | 9 |
| Projects | 16 |
| Customers | 5 |
| Leads | 8 |
| Activities | 2 |
| Tasks | 3 |
| Deals | 3 |
| Audit | 1 |
| **Total** | **47** |

The database schema was created and applied using Entity Framework Core migrations.

---

## ⚙️ Entity Framework Core

Entity Framework Core is used for relational mapping, database management, and schema evolution.

### Main DbContext

```text
CRM.DAL/Data/CrmDbContext.cs
```

### Database Migration

The initial migration was created and applied successfully.

```text
InitialCreate
```

### Reference Data

The project includes seeders for business classifications such as:

- Project types and statuses
- Building statuses
- Unit types and statuses
- Asset types and statuses
- Customer types and statuses
- Lead sources, statuses, and lost reasons
- Activity types
- Task states and priorities
- Deal stages

This provides a structured foundation for consistent business data.

---

## 🚀 Getting Started

### Prerequisites

- .NET 10 SDK
- Microsoft SQL Server
- Visual Studio or another .NET-compatible IDE

### 1. Clone the repository

```bash
git clone https://github.com/RacheliWasserman/BergerCRM-Backend.git
cd BergerCRM-Backend
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure the database

Configure the SQL Server connection string in the API application's local configuration.

Do not commit database passwords or other secrets to Git.

### 4. Build the solution

```bash
dotnet build
```

### 5. Apply database migrations

After configuring the database connection, apply the EF Core migrations using the correct startup and data-access projects.

For example, if the projects are located at the repository root:

```bash
dotnet ef database update --project CRM.DAL --startup-project CRM.Api
```

Adjust the paths to match the repository structure.

### 6. Run the API application

```bash
dotnet run --project CRM.Api
```

The currently available endpoints depend on the development stage.

---

## 🧠 Planned AI Capabilities

A future development phase is intended to introduce AI-assisted sales workflows.

### AI-Powered Call Processing

Planned functionality includes:

- Processing sales-call recordings
- Speech-to-text transcription
- Automatic call summaries
- Extraction of customer preferences and objections
- Suggested follow-up tasks
- Human approval before sensitive business data is updated

**Status:** Planned functionality — not presented as an implemented feature.

The goal is to combine AI assistance with controlled business workflows and reliable application logic.

---

## 📍 Development Roadmap

| Area | Status |
|---|---|
| Domain entities and relationships | Implemented |
| EF Core DbContext | Implemented |
| Initial database migration | Implemented |
| SQL Server database setup | Implemented |
| Reference-data seeders | Implemented |
| Repository and service layers | Planned / In progress |
| Business API endpoints | Planned / In progress |
| Authentication and authorization | Planned / In progress |
| Frontend back-office | Planned |
| Customer portal | Planned |
| AI-assisted call workflows | Planned |

---

## 💡 Engineering Focus

This project demonstrates experience in:

- Designing a business-oriented backend from scratch
- Translating real-world requirements into a relational data model
- Working with complex entity relationships
- Structuring a modular .NET solution
- Managing database schema evolution
- Building a foundation for scalable business workflows

---

## 👩‍💻 Developer

**Rachel Wasserman**

Full Stack & AI Developer

Graduate of the Ultra Code Excellence Program by KamaTech.

Passionate about backend architecture, database design, Full Stack development, and building practical software solutions for real business needs.

[![GitHub](https://img.shields.io/badge/GitHub-RacheliWasserman-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/RacheliWasserman)

---

*Building software with purpose, curiosity, and attention to detail.*
