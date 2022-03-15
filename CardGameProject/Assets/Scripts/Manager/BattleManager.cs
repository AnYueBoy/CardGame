using System.Collections.Generic;
using UFramework.Core;
using UFramework.FrameUtil;
using UFramework.GameCommon;
using UFramework.Promise;
using UnityEngine;
public class BattleManager : IBattleManager {
    private List<IRole> roleList;
    public void BuildBattleData (List<IRole> roleList) {
        this.roleList = roleList;
        this.InitRoleCard ();
        App.Make<ITurnManager> ().SetActiveRole (this.roleList[0]);
    }

    public void InitRoleCard () {
        // 创建玩家手牌
        for (var i = 0; i < 5; i++) {
            GameObject cardPrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Card");
            GameObject cardNode = App.Make<IObjectPool> ().RequestInstance (cardPrefab);
            int cardId = CommonUtil.getRandomValue (1, 2);
            CardData cardData = App.Make<IConfigManager> ().GetCardDataById (cardId);
            Card card = cardNode.GetComponent<Card> ();
            card.Init (cardData);
            card.SetRole (roleList[0]);
            roleList[0].AddCard (card);
        }
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

    public RoleData GetPlayerRoleData () {
        return this.roleList[0].RoleData;
    }

}