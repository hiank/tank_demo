using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Moba.Events
{
    public class UnityEventAny : UnityEvent<Google.Protobuf.WellKnownTypes.Any> { }

    public class EventNetHub
    {
        private Dictionary<string, UnityEventAny> hashMap;
        
        public EventNetHub()
        {
            hashMap = new Dictionary<string, UnityEventAny>();
        }

        public UnityEventAny Get(string anyDes)
        {
            UnityEventAny ue;
            if (!hashMap.TryGetValue(anyDes, out ue))
            {
                ue = new UnityEventAny();
                hashMap.Add(anyDes, ue);
            }
            //ue.AddListener(call);
            return ue;
        }

        //public void AddListener(string anyDes, UnityAction<Google.Protobuf.WellKnownTypes.Any> call)
        //{

        //    UnityEventAny ue;
        //    if (!hashMap.TryGetValue(anyDes, out ue))
        //    {
        //        ue = new UnityEventAny();
        //        hashMap.Add(anyDes, ue);
        //    }
        //    ue.AddListener(call);
        //}

        //public void 
    }

    //NOTE: 远端Tick消息监听
    public class EventNet : NormalSingleton<EventNetHub> { }
}
