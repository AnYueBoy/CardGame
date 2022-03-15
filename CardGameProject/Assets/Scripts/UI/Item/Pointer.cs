using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Sprite pointerBottom;
    [SerializeField] private Sprite pointerTop;

    private readonly int pointerLength = 9;
    private readonly float bottomItemInterval = 64;
    private readonly float topItemInterval = 128;
    private List<RectTransform> pointerItemList;
    private RectTransform parentRectTransform;

    private void OnEnable()
    {
        parentRectTransform ??= GetComponent<RectTransform>();
        BuildPointerItem();
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
            pointerRectTransform.SetParent(parentRectTransform, false);
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

        pointerPos.y += topItemInterval;
    }
}