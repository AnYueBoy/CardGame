using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Sprite pointerBottom;
    [SerializeField] private Sprite pointerTop;
    [SerializeField] private LineRenderer linerRender;

    private readonly int pointerLength = 9;
    private readonly float bottomItemInterval = 64;
    private readonly float topItemInterval = 128;
    private List<RectTransform> pointerItemList;
    private RectTransform parentRectTransform;
    private RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform ??= GetComponent<RectTransform>();
        BuildPointerItem();
    }

    private void Update()
    {
        if (drawPointList != null && drawPointList.Count > 0)
        {
            linerRender.SetPositions(drawPointList.ToArray());
        }
    }

    public void SetParentRectTransform(RectTransform parentRectTransform)
    {
        this.parentRectTransform = parentRectTransform;
    }

    private void BuildPointerItem()
    {
        if (pointerItemList != null && pointerItemList.Count > 0)
        {
            return;
        }

        pointerItemList = new List<RectTransform>();
        Vector3 pointerPos = new Vector3();

        for (int i = 0; i < pointerLength; i++)
        {
            GameObject pointerItemNode = new GameObject("PointerItem");

            Image pointerImage = pointerItemNode.AddComponent<Image>();
            pointerImage.sprite = i == pointerLength - 1 ? pointerTop : pointerBottom;
            pointerImage.SetNativeSize();

            RectTransform pointerRectTransform = pointerItemNode.GetComponent<RectTransform>();
            pointerRectTransform.SetParent(rectTransform, false);
            pointerItemList.Add(pointerRectTransform);

            pointerPos.x = 0;
            pointerPos.y = i * bottomItemInterval;
            pointerPos.z = 0;

            if (i == pointerLength - 1)
            {
                pointerPos.y = pointerPos.y - bottomItemInterval + topItemInterval;
            }

            pointerRectTransform.localPosition = pointerPos;
        }
    }

    private List<Vector3> drawPointList;

    public void MovePointer(PointerEventData eventData)
    {
        Debug.Log("Convert");
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out Vector2 localPos);

        Vector3 originPos = rectTransform.localPosition;
        drawPointList = BeizerUtil.GetBeizerPointList(originPos, originPos + Vector3.up * 50,
            new Vector3(localPos.x, localPos.y, 1), 15);
    }
}