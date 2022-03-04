using System.Collections.Generic;
using UFramework.Core;
public class BattleManager : IBattleManager {
    private List<Role> roleList;
    public void BuildBattleData () {

    }

    public void InitRoleCard () {
        throw new System.NotImplementedException ();
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