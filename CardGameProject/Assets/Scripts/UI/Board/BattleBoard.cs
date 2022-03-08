using System;
using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.UI;
public class BattleBoard : BaseUI {
    [SerializeField] private RectTransform rolesTrans;
    [SerializeField] private RectTransform cardParentTrans;
    [SerializeField] private InputField radiusInput;
    [SerializeField] private InputField angleIntervalInput;
    public override void OnShow (params object[] args) {
        SpawnRoles ();
    }

    private Role _player;
    private void SpawnRoles () {
        // 生成玩家与敌人
        List<Role> roleList = new List<Role> ();
        GameObject rolePrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Role/Role");
        GameObject playerNode = App.Make<IObjectPool> ().RequestInstance (rolePrefab);
        playerNode.transform.SetParent (rolesTrans);
        Role player = playerNode.GetComponent<Role> ();
        _player = player;
        // 设置玩家卡牌父节点
        player.SetCardParent (cardParentTrans);
        player.Init (RoleType.Warrior);
        roleList.Add (player);

        GameObject enemyNode = App.Make<IObjectPool> ().RequestInstance (rolePrefab);
        enemyNode.transform.SetParent (rolesTrans);
        Role enemy = enemyNode.GetComponent<Role> ();
        roleList.Add (enemy);
        enemy.Init (RoleType.Enemy);

        App.Make<IBattleManager> ().BuildBattleData (roleList);
    }

    public void ReOrderCard () {
        float radiusValue = Convert.ToSingle (this.radiusInput.text);
        float angleInterval = Convert.ToSingle (this.angleIntervalInput.text);
        _player.OrderCards (radiusValue, angleInterval);
    }
}