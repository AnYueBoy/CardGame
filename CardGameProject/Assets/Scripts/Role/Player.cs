using System.Collections;
using System.Collections.Generic;
using UFramework.FrameUtil;
using UnityEngine;

public class Player : MonoBehaviour, IRole {
    private RoleData roleData;

    private List<Card> cards;

    private RectTransform cardParentTrans;

    private int normalMaxCardCount;
    private float totalAngle;
    public void Init () {
        // TODO: 根据玩家数据读取角色类型
        roleData = new RoleData (RoleType.Warrior);
        cards = new List<Card> ();

        float halfRadians = Mathf.Asin (arcRadius / 2 / radius);
        totalAngle = 180 / Mathf.PI * halfRadians * 2;
        normalMaxCardCount = (int) (totalAngle / angleInterval) + 1;
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
        Debug.Log ($"curHp{this.roleData.hp}");
    }

    public void AddCard (Card card) {
        card.rectTransform.SetParent (this.cardParentTrans, false);
        this.cards.Add (card);
        this.OrderCards ();
    }

    private readonly float radius = 800f;
    private readonly float arcRadius = 700f;
    private readonly float angleInterval = 15f;

    public void OrderCards () {
        int cardsCount = cards.Count;
        int leftIndex = cardsCount / 2;
        float curAngleInterval = this.angleInterval;

        bool isOddNumber = CommonUtil.isOddNumber (cardsCount);
        if (cardsCount > this.normalMaxCardCount) {
            // 奇数转偶数
            if (isOddNumber) {
                cardsCount++;
            }

            // 重计算卡牌间距角度
            curAngleInterval = totalAngle / cardsCount;
        }

        float angle = 0;
        int cardIndex = 0;
        Vector3 targetAngle = Vector3.zero;
        Vector3 targetPos = Vector3.zero;
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
        }
    }
}