using System.Collections.Generic;
using UnityEngine;

public interface IBattleManager {

    void BuildBattleData (List<Role> enemyList);

    void InitRoleCard ();

    void LocalUpdate (float dt);
}