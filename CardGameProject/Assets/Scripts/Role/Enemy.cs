using UFramework.Core;
using UFramework.Promise;
using UnityEngine;
public class Enemy : MonoBehaviour, IRole {

    private RoleData roleData;
    public RoleData RoleData => roleData;

    public void Init () {
        roleData = new RoleData (RoleType.Enemy);
    }
    public void AddCard (Card card) { }

    public void Damage (int value) { }

    public void DrawStage () {
        Debug.Log ("敌人抽牌");
        App.Make<ITurnManager> ().NextStage ();

    }

    public void MainStage () {
        Debug.Log ("敌人进入了主要阶段");
        App.Make<IPromiseTimer> ().WaitFor (3).Then (() => {
            App.Make<ITurnManager> ().NextStage ();
        });
    }

    public void EndStage () {
        Debug.Log ("敌人进入了结束阶段");
        App.Make<ITurnManager> ().NextStage ();
    }

}