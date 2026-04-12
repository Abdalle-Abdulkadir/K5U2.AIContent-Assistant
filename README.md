#  K4U2 – AI Content Assistant

##  Overview
This project is a **two-service Web API solution** built with ASP.NET Core.

The system allows users to:
- Create and manage emails (CRUD)
- Generate AI-powered email content using an external LLM

---

##  Architecture

###  Service A – Content API
Responsible for:
- CRUD operations for emails
- Business logic
- Calling Service B for AI generation

## Technologies:
- Entity Framework Core (InMemory)
- DTOs
- Repository & Service pattern

---

###  Service B – LLM Proxy API
Responsible for:
- Communicating with the external AI (Ollama)
- Processing AI requests
- Returning generated content to Service A

---

##  Communication

Service A communicates with Service B using:
- HttpClientFactory
- Typed Client

---

##  Features

-  Full CRUD (GET, POST, PUT, DELETE)
-  AI email generation (`/api/emails/generate`)
-  DTO-based architecture
-  Query filtering (e.g. by subject)
-  Custom validation filter
-  Proper HTTP status codes
-  API documentation with Scalar/Swagger
-  XML comments for endpoint descriptions

---

##  Example Request

```json
{
  "subject": "Test",
  "tone": "Friendly",
  "content": "Write a greeting email"
}

##  Example Response
Hej,
Jag ville bara höra hur du har det och tacka för allt stöd du har gett mig.
Med vänliga hälsningar

## How To Run
- Start both APIs (Multiple Startup Projects in Visual Studio)
- Ensure Ollama is running
  ollama run llama3
- Then run the solution
- Open Scalar/Swagger in browser
- Test endpoints

## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core (InMemory)
- HttpClientFactory (Typed Clients)
- Scalar / Swagger
- Ollama (LLM)

## Author 
-Abdalle Abdulkadir
