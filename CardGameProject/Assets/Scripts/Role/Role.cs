using System.Collections;
using System.Collections.Generic;
using UFramework.FrameUtil;
using UnityEngine;

public class Role : MonoBehaviour {
    private RoleData roleData;

    private List<Card> cards;

    private RectTransform cardParentTrans;

    public void Init (RoleType roleType) {
        roleData = new RoleData (roleType);
        cards = new List<Card> ();

        float halfAngle = Mathf.Asin (arcRadius / radius);
        this.totalCardCount = halfAngle / angleInterval * 2 + 1;
    }

    public void SetCardParent (RectTransform cardParentTrans) {
        this.cardParentTrans = cardParentTrans;
    }

    #region  卡牌周期
    public void DrawStage () {
        foreach (var card in cards) {
            card.DrawStage ();
        }
    }

    public void MainStage () {
        foreach (var card in cards) {
            card.MainStage ();
        }
    }

    public void EndStage () {
        foreach (var card in cards) {
            card.EndStage ();
        }
    }
    #endregion

    public void Damage (int value) {
        this.roleData.hp -= value;
        // Debug.Log ($"curHp{this.roleData.hp}");
    }

    public void AddCard (Card card) {
        card.rectTransform.SetParent (this.cardParentTrans, false);
        this.cards.Add (card);
        this.OrderCards ();
    }

    private float radius = 800f;
    private readonly float arcRadius = 500f;
    private float angleInterval = 15f;
    private float totalCardCount;

    public void OrderCards (float _radius = 0, float _angleInterval = 0) {
        if (_radius > 0) {
            radius = _radius;
        }
        if (_angleInterval > 0) {
            angleInterval = _angleInterval;
        }
        int cardsCount = cards.Count;
        int leftIndex = cardsCount / 2;
        float curAngleInterval = this.angleInterval;

        bool isOddNumber = CommonUtil.isOddNumber (cardsCount);
        float angle = 0;
        int cardIndex = 0;
        Vector3 targetAngle = Vector3.zero;
        Vector3 targetPos = Vector3.zero;
        // Debug.Log ($"是否是奇数: {isOddNumber}, count: {cardsCount}");
        for (int i = -leftIndex; i <= leftIndex; i++) {
            if (i == 0 && !isOddNumber) {
                continue;
            }

            if (!isOddNumber && i < 0) {
                angle = curAngleInterval * i + (curAngleInterval / 2);
            }

            if (!isOddNumber && i > 0) {
                angle = curAngleInterval * i - (curAngleInterval / 2);
            }

            if (isOddNumber) {
                angle = curAngleInterval * i;
            }

            // 设置角度
            targetAngle.z = -angle;

            Card card = cards[cardIndex++];
            card.rectTransform.localEulerAngles = targetAngle;

            float radiansValue = Mathf.PI / 180f * angle;

            // 设置位置
            float x = Mathf.Sin (radiansValue) * radius;
            float y = radius * (Mathf.Cos (radiansValue) - 1);
            targetPos.x = x;
            targetPos.y = y;
            card.rectTransform.localPosition = targetPos;

            // Debug.Log ($"angle: {targetAngle} x: {x}, y:{y}");
        }

        // Debug.Log ("===========");
    }

}