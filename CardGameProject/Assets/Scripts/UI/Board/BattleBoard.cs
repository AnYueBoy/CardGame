using System;
using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.UI;
public class BattleBoard : BaseUI {
    [SerializeField] private RectTransform rolesTrans;
    [SerializeField] private RectTransform cardParentTrans;

    public override void OnShow (params object[] args) {
        SpawnRoles ();
    }

    private void SpawnRoles () {
        // 生成玩家
        List<IRole> roleList = new List<IRole> ();
        GameObject playerPrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Role/Player");
        GameObject playerNode = App.Make<IObjectPool> ().RequestInstance (playerPrefab);
        playerNode.transform.SetParent (rolesTrans);
        Player player = playerNode.GetComponent<Player> ();
        // 设置玩家卡牌父节点
        player.SetCardParent (cardParentTrans);
        player.Init ();
        roleList.Add (player);

        GameObject enemyPrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Role/Enemy");
        GameObject enemyNode = App.Make<IObjectPool> ().RequestInstance (enemyPrefab);
        enemyNode.transform.SetParent (rolesTrans);
        Enemy enemy = enemyNode.GetComponent<Enemy> ();
        roleList.Add (enemy);
        enemy.Init ();

        App.Make<IBattleManager> ().BuildBattleData (roleList);

    }

    public void EndStage () {
        App.Make<ITurnManager> ().NextStage ();
    }
}