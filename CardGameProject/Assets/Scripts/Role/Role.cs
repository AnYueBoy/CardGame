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
        Debug.Log ($"curHp{this.roleData.hp}");
    }

    private void AddCard (Card card) {
        this.cards.Add (card);
        this.OrderCards ();
    }

    private readonly float radius = 10f;
    private readonly float arcRadius = 500f;
    private readonly float angleInterval = 10f;
    private float totalCardCount;

    private void OrderCards () {
        int cardsCount = cards.Count;
        int midIndex = cardsCount / 2;
        float curAngleInterval = this.angleInterval;

        bool isOddNumber = CommonUtil.isOddNumber (cardsCount);
        for (int i = 0; i < cards.Count; i++) {

        }
    }

}