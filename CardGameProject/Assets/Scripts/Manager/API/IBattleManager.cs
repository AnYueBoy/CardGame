using System.Collections.Generic;
using UnityEngine;

public interface IBattleManager {

    void InitRectTrans (RectTransform cardParentTrans);
    void BuildBattleData (List<Role> enemyList);

    void InitRoleCard ();

    void LocalUpdate (float dt);
}