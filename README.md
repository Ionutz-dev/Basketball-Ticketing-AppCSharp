# 🏀 Basketball Ticket Sales System (C# + WinForms + Networking)

This project is a **C# WinForms application** connected to a **multi-client server** via **Sockets and gRPC**.
It manages basketball ticket sales, live seat updates across clients, and persistent storage in SQLite.

## 📁 Project Structure

```
CSharpApp/
│
├── identifier.sqlite                    # SQLite database file
│
├── Client/                              # WinForms Client Application
│   ├── Client.csproj
│   ├── BasketballClientCtrl.cs
│   ├── BasketballUserEventArgs.cs
│   ├── LoginWindow.cs
│   ├── LoginWindow.Designer.cs
│   ├── MainWindow.cs
│   ├── MainWindow.Designer.cs
│   └── StartBasketballClient.cs
│
├── Server/                              # Socket Server Application
│   ├── Server.csproj
│   ├── App.config
│   ├── BasketballServerImpl.cs
│   └── StartServer.cs
│
├── GrpcServer/                          # Standalone gRPC Server
│   ├── GrpcServer.csproj
│   ├── App.config
│   ├── GrpcBasketballServerImpl.cs
│   ├── GrpcStartServer.cs
│   └── proto/                           # gRPC Protobuf Definitions
│       └── ticket.proto
│
├── Model/                               # Shared Model Classes
│   ├── Model.csproj
│   ├── Match.cs
│   ├── Ticket.cs
│   └── User.cs
│
├── Persistence/                         # Repository Layer (Database Access)
│   ├── Persistence.csproj
│   ├── DbUtils.cs
│   ├── IMatchRepository.cs
│   ├── ITicketRepository.cs
│   ├── IUserRepository.cs
│   ├── MatchRepositoryDb.cs
│   ├── TicketRepositoryDb.cs
│   └── UserRepositoryDb.cs
│
├── Networking/                          # Networking Layer (RPC Communication)
│   ├── Networking.csproj
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
└── Services/                            # Service Layer (Interfaces)
    ├── Services.csproj
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
* **Dual Communication Protocols**:

  * **Socket-based Protocol**: Using custom Request/Response objects for C# WinForms clients.
  * **gRPC API**: Using Protobuf definitions and server-streaming for Java clients.
* **Cross-Platform Client Support**:

  * C# WinForms clients connect via Sockets
  * Java clients connect via gRPC
* **WinForms GUI**: Friendly interface to view matches and sell tickets.
* **Separate Server Applications**:

  * Socket Server for C# clients
  * Standalone gRPC Server for Java clients

## 🚀 How to Run the System

### 1. Start the Socket Server (WinForms Clients)

* Open the `Server` project.
* Ensure `identifier.sqlite` is in the working directory.
* Run `StartServer.cs`.

### 2. Start the gRPC Server (Java Clients)

* Open the `GrpcServer` project.
* Ensure `identifier.sqlite` is accessible.
* Run `GrpcStartServer.cs`.

### 3. Start the Client(s)

* **For C# WinForms Clients**:
  * Open the `Client` project.
  * Run the application.
  * Log in using a valid user from the database.
  * You can run multiple clients in parallel.

* **For Java Clients**:
  * Ensure the gRPC server is running.
  * Run the Java client application.
  * The Java client will connect to port 50051 by default.

## ⚙️ Technologies Used

* **C# / .NET Framework 4.8**
* **WinForms**
* **Sockets (TCP/IP Communication)**
* **gRPC + Protobuf**
* **SQLite** (via `Mono.Data.Sqlite`)
* **JetBrains Rider** / **Visual Studio 2022**

## ✅ Homework Requirements Implemented

* ✔️ **Client-Server Networking** using both Sockets and gRPC.
* ✔️ **Live Updates** for all connected clients (Socket Observer pattern and gRPC Streaming).
* ✔️ **Database Persistence** using SQLite.
* ✔️ **WinForms GUI** for Login and Match View.
* ✔️ **Cross-Platform Client Support** (C# and Java).
* ✔️ **Good Exception Handling** with detailed logging.
* ✔️ **Separation of Concerns** into `Model`, `Services`, `Persistence`, `Networking`, `Client`, `Server`, and `GrpcServer`.