using UnityEngine;
using System.Collections;
using Moba.Events;

namespace Moba.Net
{
    public class PBCode
    {

        public static Google.Protobuf.WellKnownTypes.Any InteractionToAny(EventInteractionTick iTick)
        {
            WarPb.DecimalVector3 v = new WarPb.DecimalVector3();
            v.X = iTick.DV.x.ToString();
            v.Y = iTick.DV.y.ToString();
            v.Z = iTick.DV.z.ToString();

            Google.Protobuf.IMessage action;
            switch (iTick.T)
            {
                case EventInteractionTick.TYPE.MOVE:
                    WarPb.Move move = new WarPb.Move();
                    move.Uid = 2002;
                    move.Speed = v;
                    action = move;
                    break;
                case EventInteractionTick.TYPE.SHOOT:
                    WarPb.Shoot shoot = new WarPb.Shoot();
                    shoot.Uid = 2002;
                    shoot.Super = false;
                    shoot.Point = v;
                    action = shoot;
                    break;
                default:
                    return null;
            }

            var any = Google.Protobuf.WellKnownTypes.Any.Pack(action);
            return any;
        }
    }
}
