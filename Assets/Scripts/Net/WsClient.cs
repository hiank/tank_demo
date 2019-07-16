using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading.Tasks;
using UnityEngine.Events;
using Moba.Events;

namespace Moba.Net
{

    public class WsConn
    {   
        public ulong ID { get; set; }
        private ulong id;               //NOTE: 战斗中的当前玩家id, 从War_Match中获取
        private ClientWebSocket client;
        private Uri uri;
        private System.Threading.CancellationToken cancelActionToken;
        private UnityAction<EventInteractionTick> interactionCall;
        public WsConn()
        {
            client = new ClientWebSocket();
            uri = new Uri("ws://192.168.137.222:30250/ws");
            client.Options.SetRequestHeader("Token", "2002");
            cancelActionToken = new System.Threading.CancellationToken();
            interactionCall = new UnityAction<EventInteractionTick>(handleInteractionTick);
        }

        async public Task ConnectAsync()
        {
            switch (client.State)
            {
                case WebSocketState.None:
                case WebSocketState.Closed:
                case WebSocketState.Aborted:
                    Debug.Log("before connect");
                    await client.ConnectAsync(uri, cancelActionToken);
                    EventInteraction.Instance.AddListener(interactionCall);
                    Debug.Log("after connect and before recv");
                    recv();
                    Debug.Log("after recv");
                    break;
            }
        }

        async public void SendMessageAsync(Google.Protobuf.IMessage message)
        {

            var any = Google.Protobuf.WellKnownTypes.Any.Pack(message);
            await send(any);
        }

        async public Task send(Google.Protobuf.WellKnownTypes.Any any)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Google.Protobuf.CodedOutputStream s = new Google.Protobuf.CodedOutputStream(stream);
                any.WriteTo(s);
                s.Flush();
                ArraySegment<byte> buffer = new ArraySegment<byte>(stream.ToArray());
                await client.SendAsync(buffer, WebSocketMessageType.Binary, true, cancelActionToken);
            }
        }

        async private void recv()
        {

            var arr = new ArraySegment<byte>(new byte[512]);
            while (true)
            {
                arr.Array.Initialize();
                WebSocketReceiveResult rlt = await client.ReceiveAsync(arr, cancelActionToken);
                if (rlt.CloseStatus != null && rlt.CloseStatus != WebSocketCloseStatus.Empty)
                    break;


                //NOTE: 此处缺乏错误处理
                var any = Google.Protobuf.WellKnownTypes.Any.Parser.ParseFrom(arr.Array, 0, rlt.Count);
                string typeName = Google.Protobuf.WellKnownTypes.Any.GetTypeName(any.TypeUrl);
                Debug.Log("recv:" + typeName);
                EventNet.Instance.Get(typeName).Invoke(any);
            }
        }

        private void handleInteractionTick(EventInteractionTick tick)
        {
            WarPb.S_War_Do msg = toProtoDo(tick);
            //Google.Protobuf.WellKnownTypes.Any any = Google.Protobuf.WellKnownTypes.Any.Pack(msg);
            //client.SendAsync(new ArraySegment<byte>(any.Value.ToByteArray()), WebSocketMessageType.Binary, true, cancelActionToken);
            SendMessageAsync(msg);
        }

        private WarPb.S_War_Do toProtoDo(EventInteractionTick tick)
        {

            WarPb.S_War_Do msg = new WarPb.S_War_Do();
            msg.Id = ID;
            //msg.Action = global::Google.Protobuf.WellKnownTypes.Any.Pack(action);
            msg.Action = PBCode.InteractionToAny(tick);
            return msg;
        }
    }

    public class WsClient : NormalSingleton<WsConn> { }
}
