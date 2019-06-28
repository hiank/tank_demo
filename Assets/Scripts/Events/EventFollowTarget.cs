using UnityEngine;
using UnityEngine.Events;

namespace Moba.Events
{
    public class EventFollowTarget : UnityEvent<Transform>
    {

    }

    public class EventFollow : NormalSingleton<EventFollowTarget>
    {
    }
}
