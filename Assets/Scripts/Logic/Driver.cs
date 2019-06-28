using UnityEngine;
using UnityEngine.Events;
using Moba.Events;

namespace Moba.Logic
{
    //NOTE: 逻辑层不演示，定期生成一个tick，用于驱动View层
    public class Driver : MonoBehaviour
    {

        private int tickNum = 0;
        private WarPb.Tick localTick;       //NOTE: 本地操作tick

        public Meet M { set; get; }

        private void Awake()
        {
            EventInteraction.Instance.AddListener(new UnityAction<EventInteractionTick>(handleInteractionTick));     //NOTE: 监听本地tick，用于演示
            EventNet.Instance.Get(WarPb.Tick.Descriptor.FullName).AddListener(new UnityAction<Google.Protobuf.WellKnownTypes.Any>(handleNetTick));
        }

        // Start is called before the first frame update
        private void Start()
        {

            localTick = new WarPb.Tick();
            localTick.Index = 0;
        }

        //NOTE: 处理tick？0.02调用一次，适合定点数调用？tick 应该由此而来，三次调用，一个tick
        private void FixedUpdate()
        {
            M.Fixed(Time.deltaTime);
            tickNum++;
            if (tickNum % 3 == 0)       //NOTE: 此时需要发送logic tick
            {
                M.DoLocalTick(localTick);                //NOTE: 每tick结束后，响应下一个tick，改变各单位状态，下一个tick使用新的状态进行演示
                localTick.Actions.Clear();
                localTick.Index++;
            }
        }

        //监听交互tick消息
        private void handleInteractionTick(EventInteractionTick iTick)
        {

            var any = Net.PBCode.InteractionToAny(iTick);
            localTick.Actions.Add(any);
        }

        //监听网络tick消息
        private void handleNetTick(Google.Protobuf.WellKnownTypes.Any any)
        {

            M.DoNetTick(any.Unpack<WarPb.Tick>());
        }
    }
}
