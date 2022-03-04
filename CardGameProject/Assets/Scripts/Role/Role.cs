using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour {
    private RoleData roleData;

    private List<Card> cards;

    public void Init () {
        roleData = new RoleData ();
        cards = new List<Card> ();
    }

    public void DrawStage () {
        foreach (var card in cards) {
            card.DrawStage (this);
        }
    }

    public void ReadyStage () {
        foreach (var card in cards) {
            card.ReadyStage (this);
        }
    }

    public void EndStage () {
        foreach (var card in cards) {
            card.EndStage (this);
        }
    }

    public void Damage (int value) {
        this.roleData.hp -= value;
    }

}