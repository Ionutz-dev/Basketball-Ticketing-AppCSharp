// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: proto/ticket.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Ticketing {
  public static partial class TicketService
  {
    static readonly string __ServiceName = "ticketing.TicketService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Ticketing.LoginRequest> __Marshaller_ticketing_LoginRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Ticketing.LoginRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Ticketing.LoginResponse> __Marshaller_ticketing_LoginResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Ticketing.LoginResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Ticketing.Empty> __Marshaller_ticketing_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Ticketing.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Ticketing.MatchList> __Marshaller_ticketing_MatchList = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Ticketing.MatchList.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Ticketing.SellTicketRequest> __Marshaller_ticketing_SellTicketRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Ticketing.SellTicketRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Ticketing.SellTicketResponse> __Marshaller_ticketing_SellTicketResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Ticketing.SellTicketResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Ticketing.LoginRequest, global::Ticketing.LoginResponse> __Method_Login = new grpc::Method<global::Ticketing.LoginRequest, global::Ticketing.LoginResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Login",
        __Marshaller_ticketing_LoginRequest,
        __Marshaller_ticketing_LoginResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Ticketing.Empty, global::Ticketing.MatchList> __Method_GetAllMatches = new grpc::Method<global::Ticketing.Empty, global::Ticketing.MatchList>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllMatches",
        __Marshaller_ticketing_Empty,
        __Marshaller_ticketing_MatchList);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Ticketing.SellTicketRequest, global::Ticketing.SellTicketResponse> __Method_SellTicket = new grpc::Method<global::Ticketing.SellTicketRequest, global::Ticketing.SellTicketResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SellTicket",
        __Marshaller_ticketing_SellTicketRequest,
        __Marshaller_ticketing_SellTicketResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Ticketing.Empty, global::Ticketing.MatchList> __Method_WatchMatches = new grpc::Method<global::Ticketing.Empty, global::Ticketing.MatchList>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "WatchMatches",
        __Marshaller_ticketing_Empty,
        __Marshaller_ticketing_MatchList);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Ticketing.TicketReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of TicketService</summary>
    [grpc::BindServiceMethod(typeof(TicketService), "BindService")]
    public abstract partial class TicketServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Ticketing.LoginResponse> Login(global::Ticketing.LoginRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Ticketing.MatchList> GetAllMatches(global::Ticketing.Empty request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Ticketing.SellTicketResponse> SellTicket(global::Ticketing.SellTicketRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task WatchMatches(global::Ticketing.Empty request, grpc::IServerStreamWriter<global::Ticketing.MatchList> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(TicketServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Login, serviceImpl.Login)
          .AddMethod(__Method_GetAllMatches, serviceImpl.GetAllMatches)
          .AddMethod(__Method_SellTicket, serviceImpl.SellTicket)
          .AddMethod(__Method_WatchMatches, serviceImpl.WatchMatches).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, TicketServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Login, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Ticketing.LoginRequest, global::Ticketing.LoginResponse>(serviceImpl.Login));
      serviceBinder.AddMethod(__Method_GetAllMatches, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Ticketing.Empty, global::Ticketing.MatchList>(serviceImpl.GetAllMatches));
      serviceBinder.AddMethod(__Method_SellTicket, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Ticketing.SellTicketRequest, global::Ticketing.SellTicketResponse>(serviceImpl.SellTicket));
      serviceBinder.AddMethod(__Method_WatchMatches, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Ticketing.Empty, global::Ticketing.MatchList>(serviceImpl.WatchMatches));
    }

  }
}
#endregion
