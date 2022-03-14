using System.Collections.Generic;
using UFramework.Core;
public class DefenceAttackSlot : ISlot {
    public void DrawStage (IRole from, List<int> effectValue, IRole to = null) { }

    public void EndStage (IRole from, List<int> effectValue, IRole to = null) { }

    public void MainStage (IRole from, List<int> effectValue, IRole to = null) { }

    public void Trigger (IRole from, List<int> effectValue, IRole to = null) {
        to.Damage (from.RoleData.armor);
    }

    public bool IsAimToRole () {
        return true;
    }

}