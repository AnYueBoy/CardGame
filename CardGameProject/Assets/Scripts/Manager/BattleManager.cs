using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
public class BattleManager : IBattleManager {
    private List<Role> roleList;
    public void BuildBattleData (List<Role> enemyList) {
        roleList = new List<Role> ();
        GameObject rolePrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Role");
        GameObject roleNode = App.Make<IObjectPool> ().RequestInstance (rolePrefab);
        Role player = roleNode.GetComponent<Role> ();
        player.Init ();
        roleList.Add (player);
        roleList.AddRange (enemyList);
    }

    public void InitRoleCard () { }

    private int curRoleIndex = 0;

    public void LocalUpdate (float dt) {
        TurnStage curStage = App.Make<ITurnManager> ().GetCurStage ();
        if (curStage == TurnStage.None) {
            curRoleIndex++;
            curRoleIndex %= roleList.Count;
            App.Make<ITurnManager> ().SetActiveRole (roleList[curRoleIndex]);
        }
    }
}