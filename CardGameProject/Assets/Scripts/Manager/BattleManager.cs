using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
public class BattleManager : IBattleManager {
    private List<Role> roleList;
    public void BuildBattleData (List<Role> roleList) {
        this.roleList = roleList;
        // foreach (var role in roleList) {
        //     role.Init ();
        // }

        // GameObject cardPrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Card");
        // GameObject cardNode = App.Make<IObjectPool> ().RequestInstance (cardPrefab);
        // cardNode.transform.SetParent (App.Make<INodeManager> ().CanvasTrans);
        // cardNode.transform.localPosition = Vector3.zero;

        // CardData cardData = App.Make<IConfigManager> ().GetCardDataById (1);
        // Card card = cardNode.GetComponent<Card> ();
        // card.Init (cardData);
        // card.SetRole (player);
    }

    public void InitRoleCard () {
        // 创建玩家手牌
    }

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