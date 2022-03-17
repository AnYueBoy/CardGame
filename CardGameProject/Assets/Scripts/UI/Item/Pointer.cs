using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Sprite pointerBottom;
    [SerializeField] private Sprite pointerTop;

    private readonly int pointerLength = 9;
    private readonly float bottomItemInterval = 64;
    private readonly float topItemInterval = 128;
    private List<RectTransform> pointerItemList;
    private RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform ??= GetComponent<RectTransform>();
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

    private Vector3 originPoint = Vector3.zero;
    private Vector3 controlPoint = new Vector3(0, 630, 0);
    private Vector3 endPoint;

    public void MovePointer(Vector2 localPos)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        endPoint = new Vector3(localPos.x, localPos.y, 1);
        List<Vector3> drawPointList = BeizerUtil.GetBeizerPointList(originPoint, controlPoint, endPoint, pointerLength);

        refreshItem(drawPointList);
    }

    private void refreshItem(List<Vector3> itemPosList)
    {
        for (int i = 0; i < itemPosList.Count; i++)
        {
            Vector3 itemPos = itemPosList[i];
            pointerItemList[i].localPosition = itemPos;

            Vector3 tangentDir = BeizerUtil.GetTangent(i, originPoint, controlPoint, endPoint);
            tangentDir.Normalize();
            Debug.Log($"tangentDir: {tangentDir}");

            float angle = 0;
            if (tangentDir.y != 0)
            {
                angle = Mathf.Atan(tangentDir.x / tangentDir.y);
                angle *= 180 / Mathf.PI;
            }

            Debug.Log($"angle： {angle}");
            pointerItemList[i].localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}