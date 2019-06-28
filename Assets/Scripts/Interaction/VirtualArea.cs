using UnityEngine;
using UnityEngine.EventSystems;

namespace Moba.Interaction
{
    public class VirtualArea : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public RectTransform thumb;

        private Vector2 originalPosition;
        private Vector2 originalThumbPosition;

        private Vector2 delta;

        // Start is called before the first frame update
        void Start()
        {
            originalPosition = this.GetComponent<RectTransform>().localPosition;
            originalThumbPosition = thumb.localPosition;

            thumb.gameObject.SetActive(false);

            delta = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            thumb.gameObject.SetActive(true);

            this.GetComponent<RectTransform>().position = worldPoint(eventData);
            thumb.localPosition = originalThumbPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            thumb.position = worldPoint(eventData);

            var size = this.GetComponent<RectTransform>().rect.size;

            delta = thumb.localPosition;

            delta.x /= size.x / 2.0f;
            delta.y /= size.y / 2.0f;

            delta.x = Mathf.Clamp(delta.x, -1.0f, 1.0f);
            delta.y = Mathf.Clamp(delta.y, -1.0f, 1.0f);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.GetComponent<RectTransform>().localPosition = originalPosition;

            delta = Vector2.zero;

            thumb.gameObject.SetActive(false);
        }

        private Vector3 worldPoint(PointerEventData eventData)
        {
            Vector3 wp = new Vector3();
            RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, eventData.enterEventCamera, out wp);
            return wp;
        }
    }
}
