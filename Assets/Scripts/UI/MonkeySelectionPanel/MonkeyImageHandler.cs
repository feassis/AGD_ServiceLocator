using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        private RectTransform rectTransform;
        private Vector3 originalPos;
        private Vector3 originalAnchoredPos;

        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
        }

        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;

            rectTransform = GetComponent<RectTransform>();
            originalPos = rectTransform.position; ;
            originalAnchoredPos = rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // rectTransform.anchoredPosition += eventData.delta; not working the mouse was moving more then the image due to canvas scale

            rectTransform.anchoredPosition += eventData.delta / UIService.Instance.rectTransform.localScale;
            owner.MonkeyDraggedAt(rectTransform.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            owner.MonkeyDroppedAt(eventData.position);
            ResetMonkey();
        }

        private void ResetMonkey()
        {
            rectTransform.position = originalPos;
            rectTransform.anchoredPosition = originalAnchoredPos;
            GetComponent<LayoutElement>().enabled = false;
            GetComponent<LayoutElement>().enabled = true;
            monkeyImage.color = new Color(1, 1, 1, 1f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            monkeyImage.color = new Color(1, 1, 1, 0.6f);
        }
    }
}