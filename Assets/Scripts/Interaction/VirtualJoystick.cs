using UnityEngine;
using UnityEngine.EventSystems;
using Moba.Events;

namespace Moba.Interaction
{
    public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {

        public RectTransform thumb;

        [SerializeField]
        private float deadZone = 0;
        [SerializeField]
        private float handleRange = 1;

        private Canvas canvas;
        private Camera cam;
        private RectTransform joystick;

        private Vector2 originalPosition;
        private Vector2 originalThumbPosition;

        public Vector2 input;

        private DecimalVector3 speedScale = new DecimalVector3();
        private bool speedChange = false;

        private int updateTimes = 0;

        // Start is called before the first frame update
        private void Start()
        {
            canvas = this.GetComponentInParent<Canvas>();
            cam = canvas.worldCamera;
            joystick = this.GetComponent<RectTransform>();

            originalPosition = joystick.localPosition;
            originalThumbPosition = thumb.localPosition;

            input = Vector2.zero;
        }

        private void FixedUpdate()
        {
            if (updateTimes < 3)
            {
                updateTimes++;
                return;
            }

            if (speedChange)
            {
                EventInteraction.Instance.Invoke(new EventInteractionTick(EventInteractionTick.TYPE.MOVE, speedScale));
                speedChange = false;
            }
            updateTimes = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            joystick.position = worldPoint(eventData);

            thumb.localPosition = originalThumbPosition;

            InputTouch.LOCK_TOUCH = true;
        }

        public void OnDrag(PointerEventData eventData)
        {

            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, joystick.position);
            Vector2 radius = joystick.sizeDelta / 2;
            input = (eventData.position - position) / (radius * canvas.scaleFactor);

            HandleInput(input.magnitude, input.normalized, radius, cam);
            thumb.anchoredPosition = input * radius * handleRange;

            Vector2 pos = thumb.localPosition;
            Vector2 tmp = (pos - originalThumbPosition).normalized;

            DecimalVector3 curScale = new DecimalVector3(new decimal(tmp.x), 0, new decimal(tmp.y));
            speedChange = curScale != speedScale;
            if (speedChange)
                speedScale = curScale;

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            joystick.localPosition = originalPosition;
            thumb.localPosition = originalThumbPosition;

            speedChange = true;
            speedScale = new DecimalVector3();

            InputTouch.LOCK_TOUCH = false;
        }

        protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                {
                    input = normalised;
                }
            }
            else
                input = Vector2.zero;
        }

        private Vector3 worldPoint(PointerEventData eventData)
        {
            Vector3 wp = new Vector3();
            RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, cam, out wp);
            return wp;
        }
    }
}
