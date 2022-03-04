using System.Collections.Generic;

public interface IBattleManager {
    void BuildBattleData (List<Role> enemyList);

    void InitRoleCard ();

    void LocalUpdate (float dt);
}