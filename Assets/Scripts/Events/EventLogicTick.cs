using UnityEngine.Events;
using System.Collections.Generic;

namespace Moba.Events
{
    public class EventLogicTick
    {
        public DecimalVector3 v3;   //NOTE: 移动方向速度
        public int actionTag;       //NOTE: 动作tag
        public int tag;             //NOTE: 单位tag

        public EventLogicTick()
        {
            v3 = new DecimalVector3();
            actionTag = 0;
            tag = (1 << 13) ^ (1 << 9) ^ (1 << 5);      //NOTE: 主角的tag
        }
    }

    public class EventLogicTickList : List<EventLogicTick> { }
    public class EventLogicObj : UnityEvent<EventLogicTickList> { }
    public class EventLogic : NormalSingleton<EventLogicObj> { }
}