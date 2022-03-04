using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;

public class ProviderBattleManager : IServiceProvider {
    public void Init () {
        GameObject rolePrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Role");
        GameObject roleNode = App.Make<IObjectPool> ().RequestInstance (rolePrefab);
        Role enemy = roleNode.GetComponent<Role> ();
        enemy.Init();
        List<Role> enemyList = new List<Role> ();
        enemyList.Add (enemy);
        App.Make<IBattleManager> ().BuildBattleData (enemyList);
    }

    public void Register () {
        App.Singleton<IBattleManager, BattleManager> ();
    }
}