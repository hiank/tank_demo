using UnityEngine.Events;

namespace Moba.Events
{
    public class EventInteractionTick
    {

        public TYPE T { get; }
        public DecimalVector3 DV { get; }

        public enum TYPE
        {
            MOVE,       //NOTE: 移动
            SHOOT       //NOTE: 射击
        }
        public EventInteractionTick(TYPE t, DecimalVector3 dv)
        {
            T = t;
            DV = dv;
        }
    }

    public class EventInteractionObj : UnityEvent<EventInteractionTick> { }
    //NOTE: 根据本地交互创建的Tick 消息监听
    public class EventInteraction : NormalSingleton<EventInteractionObj> { }

    //public class EventLogicTick
    //{
    //    public DecimalVector3 v3;   //NOTE: 移动方向速度
    //    public int actionTag;       //NOTE: 动作tag
    //    public int tag;             //NOTE: 单位tag

    //    public EventLogicTick()
    //    {
    //        v3 = new DecimalVector3();
    //        actionTag = 0;
    //        tag = (1 << 13) ^ (1 << 9) ^ (1 << 5);      //NOTE: 主角的tag
    //    }
    //}

    //public class EventLogicTickAction : List<EventLogicTick> { }
    //public class EventLogicObj : UnityEvent<EventLogicTickAction> { }
    //public class EventLogic : NormalSingleton<EventLogicObj> { }

}
