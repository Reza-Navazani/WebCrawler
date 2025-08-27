# WebCrawler

A **multi-threaded web crawler** implemented as a **.NET Core 8 Web API** using the **Producer–Consumer pattern**.  
The crawler fetches web pages, parses content, and stores results in a **SQLite database**.

---

## Features

- **Multi-threaded crawling** using `BlockingCollection<Uri>`  
- **Producer–Consumer pattern** for efficient URL fetching and processing  
- Stores **raw HTML pages + metadata** in **SQLite** (`crawler.db`)  
- Built as a **RESTful Web API**  

---

## Tech Stack

- **Backend:** ASP.NET Core 8 Web API  
- **Database:** SQLite  
- **Libraries:**  
  - `HtmlAgilityPack` for HTML parsing  
  - `Entity Framework Core` for database access  

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- Visual Studio 2022 or higher  
- SQLite or DB Browser for SQLite (optional, to view the database)  

### Run Locally

1. Clone the repository:

```bash
git clone https://github.com/YourUserName/WebCrawler.git
cd WebCrawler

---
```
## Project Structure	


- WebCrawler.sln (Solution file)
- WebCrawler/ (Project folder)
  - Controllers/ (API endpoints)
  - Services/ (Crawler logic & Producer–Consumer)
  - Program.cs
  - WebCrawler.csproj


## Future Improvements

Add RAG embeddings for AI-powered content processing

Add authentication & API key management

Add crawling rules & filters (robots.txt compliance, domain restrictions)