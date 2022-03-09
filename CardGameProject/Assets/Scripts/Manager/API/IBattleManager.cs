using System.Collections.Generic;
using UnityEngine;

public interface IBattleManager {

    void BuildBattleData (List<IRole> enemyList);

    void InitRoleCard ();

    void LocalUpdate (float dt);
}