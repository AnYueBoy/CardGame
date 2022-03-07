using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
public class BattleBoard : BaseUI {
    [SerializeField] private RectTransform rolesTrans;
    public override void OnShow (params object[] args) { }

    private void SpawnRoles () {
        // 生成玩家与敌人
        List<Role> roleList = new List<Role> ();
        GameObject rolePrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Role/Role");
        GameObject playerNode = App.Make<IObjectPool> ().RequestInstance (rolePrefab);
        playerNode.transform.SetParent (rolesTrans);
        Role player = playerNode.GetComponent<Role> ();
        roleList.Add (player);

        GameObject enemyNode = App.Make<IObjectPool> ().RequestInstance (rolePrefab);
        enemyNode.transform.SetParent (rolesTrans);
        Role enemy = enemyNode.GetComponent<Role> ();
        roleList.Add (enemy);

        App.Make<IBattleManager> ().BuildBattleData (roleList);
    }
}