using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour {
    private RoleData roleData;

    private List<Card> cards;

    private RectTransform cardParentTrans;

    public void Init (RoleType roleType) {
        roleData = new RoleData (roleType);
        cards = new List<Card> ();
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

    private void OrderCards () {

    }

}