# 🏀 Basketball Ticket Sales System (C# + WinForms + Networking)

This project is a **C# WinForms application** connected to a **multi-client server** via **Sockets and gRPC**.
It manages basketball ticket sales, live seat updates across clients, and persistent storage in SQLite.

## 📁 Project Structure

```
CSharpApp/
│
├── identifier.sqlite                    # SQLite database file
│
├── client/                              # WinForms Client Application
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
├── server/                              # Server Application (Sockets + gRPC)
│   ├── server.csproj
│   ├── App.config
│   ├── BasketballServerImpl.cs
│   ├── StartServer.cs
│   ├── proto/                           # gRPC Protobuf Definitions
│   │   └── ticket.proto
│   └── grpc/                            # gRPC Server Implementation
│       ├── GrpcBasketballServiceImpl.cs
│       └── GrpcStartServer.cs
│
├── GrpcServer/                          # gRPC build output (generated files & exe)
│   ├── GrpcServer.csproj
│   └── bin/obj/debug/...                # Compiled executables and generated classes
│
├── model/                               # Shared Model Classes
│   ├── model.csproj
│   ├── Match.cs
│   ├── Ticket.cs
│   └── User.cs
│
├── persistence/                         # Repository Layer (Database Access)
│   ├── persistence.csproj
│   ├── DbUtils.cs
│   ├── IMatchRepository.cs
│   ├── ITicketRepository.cs
│   ├── IUserRepository.cs
│   ├── MatchRepositoryDb.cs
│   ├── TicketRepositoryDb.cs
│   └── UserRepositoryDb.cs
│
├── networking/                          # Networking Layer (RPC Communication)
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
└── services/                            # Service Layer (Interfaces)
    ├── services.csproj
    ├── BasketballException.cs
    ├── IBasketballObserver.cs
    └── IBasketballServices.cs
```

## ✨ Features

* **Multi-client Server**: Multiple clients can connect and interact simultaneously.
* **Login/Logout System**: Secure user authentication.
* **Ticket Purchase Workflow**: Select match → Enter customer → Buy tickets.
* **Live Seat Updates**: When one client sells a ticket, all connected clients automatically refresh.
* **Persistent SQLite Database**: Matches, tickets, and users stored persistently.
* **Error Handling**: Graceful handling of server disconnections, bad inputs, and network issues.
* **Networking Communication**:

  * Socket-based protocol using custom Request/Response objects.
  * gRPC API using Protobuf definitions and server-streaming for updates.
  * Server uses `BasketballClientObjectWorker` (sockets) and `IServerStreamWriter` (gRPC).
* **WinForms GUI**: Friendly interface to view matches and sell tickets.
* **gRPC Server Features**:

  * Added to `server/grpc/` directory.
  * Compatible with Java clients.
  * Implements streaming updates via `WatchMatches()`.

## 🚀 How to Run the System

### 1. Start the Socket Server (WinForms Clients)

* Open the `server` project.
* Ensure `identifier.sqlite` is in the working directory.
* Run `StartServer.cs`.

### 2. Start the gRPC Server (Java Clients or other gRPC clients)

* Open `server/grpc/GrpcStartServer.cs`.
* Make sure `identifier.sqlite` is accessible.
* Run `GrpcStartServer`.

### 3. Start the Client(s)

* Open the `client` project.
* Run the application.
* Log in using a valid user from the database.
* You can run multiple clients in parallel.

## ⚙️ Technologies Used

* **C# 12 / .NET 9**
* **WinForms**
* **Sockets (TCP/IP Communication)**
* **gRPC + Protobuf**
* **SQLite** (via `System.Data.SQLite`)
* **JetBrains Rider** / **Visual Studio 2022**

## ✅ Homework Requirements Implemented

* ✔️ **Client-Server Networking** using both Sockets and gRPC.
* ✔️ **Live Updates** for all connected clients (Sockets or gRPC).
* ✔️ **Database Persistence** using SQLite.
* ✔️ **WinForms GUI** for Login and Match View.
* ✔️ **Good Exception Handling** and modular structure.
* ✔️ **Separation of Concerns** into `model`, `services`, `persistence`, `networking`, `client`, and `server`.


