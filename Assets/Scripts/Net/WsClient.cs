using UnityEngine;
using UnityEditor;
using System;
using System.Net.WebSockets;
using UnityEngine.Events;
using Moba.Events;

namespace Moba.Net
{

    public class WsConn
    {
        private ulong id;               //NOTE: 战斗中的当前玩家id, 从War_Match中获取
        private ClientWebSocket client;
        private Uri uri;
        private System.Threading.CancellationToken cancelActionToken;
        private UnityAction<EventInteractionTick> interactionCall;
        public WsConn()
        {
            client = new ClientWebSocket();
            uri = new Uri("ws://192.168.137.222:30250/ws");
            cancelActionToken = new System.Threading.CancellationToken();
            interactionCall = new UnityAction<EventInteractionTick>(handleInteractionTick);
        }

        async public void ConnectAsync()
        {
            switch (client.State)
            {
                case WebSocketState.None:
                case WebSocketState.Closed:
                case WebSocketState.Aborted:
                    await client.ConnectAsync(uri, cancelActionToken);
                    EventInteraction.Instance.AddListener(interactionCall);
                    //client.ReceiveAsync()
                    //recv();
                    var arr = new ArraySegment<byte>(new byte[512]);
                    while (true)
                    {

                        WebSocketReceiveResult rlt = await client.ReceiveAsync(arr, cancelActionToken);
                        if (rlt.CloseStatus != null && rlt.CloseStatus != WebSocketCloseStatus.Empty)
                            break;

                        //NOTE: 此处缺乏错误处理
                        var any = Google.Protobuf.WellKnownTypes.Any.Parser.ParseFrom(arr.Array);
                        EventNet.Instance.Get(Google.Protobuf.WellKnownTypes.Any.GetTypeName(any.TypeUrl)).Invoke(any);
                    }
                    break;
            }
        }

        async private void recv()
        {

            var arr = new ArraySegment<byte>(new byte[512]);
            while (true)
            {

                WebSocketReceiveResult rlt = await client.ReceiveAsync(arr, cancelActionToken);

                //NOTE: 此处缺乏错误处理
                var any = Google.Protobuf.WellKnownTypes.Any.Parser.ParseFrom(arr.Array);
                EventNet.Instance.Get(Google.Protobuf.WellKnownTypes.Any.GetTypeName(any.TypeUrl)).Invoke(any);
            }
        }

        private void handleInteractionTick(EventInteractionTick tick)
        {
            WarPb.S_War_Do msg = toProtoDo(tick);
            Google.Protobuf.WellKnownTypes.Any any = Google.Protobuf.WellKnownTypes.Any.Pack(msg);
            client.SendAsync(new ArraySegment<byte>(any.Value.ToByteArray()), WebSocketMessageType.Binary, true, cancelActionToken);
        }

        private WarPb.S_War_Do toProtoDo(EventInteractionTick tick)
        {

            WarPb.S_War_Do msg = new WarPb.S_War_Do();
            msg.Id = id;
            //msg.Action = global::Google.Protobuf.WellKnownTypes.Any.Pack(action);
            msg.Action = PBCode.InteractionToAny(tick);
            return msg;
        }
    }

    public class WsClient : NormalSingleton<WsConn> { }
}
