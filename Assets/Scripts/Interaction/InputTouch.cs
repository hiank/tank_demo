using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moba.Events;

namespace Moba.Interaction
{
    public class InputTouch : MonoBehaviour
    {
        private Collider coll;

        public static bool LOCK_TOUCH { set; get; }

        private void Start()
        {
            coll = GetComponent<Collider>();
            LOCK_TOUCH = false;
        }

        private void LateUpdate()
        {
            if (LOCK_TOUCH || !Input.GetMouseButtonDown(0))
                return;

            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (coll.Raycast(ray, out hit, 100))
            {
                EventInteraction.Instance.Invoke(new EventInteractionTick(EventInteractionTick.TYPE.SHOOT, new DecimalVector3(new decimal(hit.point.x), new decimal(0), new decimal(hit.point.z))));
            }
        }
    }
}
