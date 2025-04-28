# 🏀 Basketball Ticket Sales System (C# + WinForms + Networking)

This project is a **C# WinForms application** connected to a **multi-client server** via **Sockets**.  
It manages basketball ticket sales, live seat updates across clients, and persistent storage in SQLite.

## 📁 Project Structure

```
CSharpApp/
│
├── identifier.sqlite                    # SQLite database file
│
├── client/                               # WinForms Client Application
│   ├── client.csproj
│   ├── Forms/
│   │   ├── LoginForm.cs
│   │   ├── LoginForm.Designer.cs
│   │   ├── MainForm.cs
│   │   ├── MainForm.Designer.cs
│   │   └── Program.cs
│   └── Properties/
│       └── Resources.resx
│
├── server/                               # Server Application
│   ├── server.csproj
│   ├── App.config
│   ├── BasketballServerImpl.cs
│   └── StartServer.cs
│
├── model/                                # Shared Model Classes
│   ├── model.csproj
│   ├── Match.cs
│   ├── Ticket.cs
│   └── User.cs
│
├── persistence/                          # Repository Layer (Database Access)
│   ├── persistence.csproj
│   ├── DbUtils.cs
│   ├── IMatchRepository.cs
│   ├── ITicketRepository.cs
│   ├── IUserRepository.cs
│   ├── MatchRepositoryDb.cs
│   ├── TicketRepositoryDb.cs
│   └── UserRepositoryDb.cs
│
├── networking/                            # Networking Layer (RPC Communication)
│   ├── networking.csproj
│   ├── BasketballClientObjectWorker.cs
│   ├── BasketballServerObjectProxy.cs
│   ├── ObjectRequestProtocol.cs
│   ├── ObjectResponseProtocol.cs
│   ├── DTOUtils.cs
│   ├── MatchDTO.cs
│   ├── TicketDTO.cs
│   ├── UserDTO.cs
│   └── ServerUtils.cs
│
└── services/                               # Service Layer (Interfaces)
    ├── services.csproj
    ├── BasketballException.cs
    ├── IBasketballObserver.cs
    └── IBasketballServices.cs
```

## ✨ Features

- **Multi-client Server**: Multiple clients can connect and interact simultaneously.
- **Login/Logout System**: Secure user authentication.
- **Ticket Purchase Workflow**: Select match → Enter customer → Buy tickets.
- **Live Seat Updates**: When one client sells a ticket, all connected clients automatically refresh.
- **Persistent SQLite Database**: Matches, tickets, and users stored persistently.
- **Error Handling**: Graceful handling of server disconnections, bad inputs, and network issues.
- **Networking Communication**:
   - Custom protocol using Request/Response objects.
   - Client uses `BasketballServerObjectProxy` to call server methods.
   - Server uses `BasketballClientObjectWorker` to handle each client connection.
- **WinForms GUI**: Friendly interface to view matches and sell tickets.

## 🚀 How to Run the System

### 1. Start the Server
- Open the `server` project in **Rider** or **Visual Studio**.
- Make sure the `identifier.sqlite` database is in the server's output directory.
- **Run the Server** (`StartServer.cs`).

### 2. Start the Client(s)
- Open the `client` project.
- **Run the Client**.
- Login using a valid username/password from the database.

- You can run **multiple clients** at once (e.g., from Rider or separate terminals).

## ⚙️ Technologies Used
- **C# 12 / .NET 9**
- **WinForms**
- **Sockets (TCP/IP Communication)**
- **SQLite** (via `System.Data.SQLite`)
- **JetBrains Rider** / **Visual Studio 2022**

## ✅ Homework Requirements Implemented

- ✔️ **Client-Server Networking** using Sockets and custom RPC Protocol.
- ✔️ **Live Updates** for all connected clients via Observer Pattern.
- ✔️ **Database Persistence** for Users, Matches, and Tickets.
- ✔️ **WinForms GUI** for Login and Main Interaction.
- ✔️ **Good Exception Handling** on both server and client sides.
- ✔️ **Separation of Concerns**: `model`, `persistence`, `networking`, `services`, `client`, `server` modules.
