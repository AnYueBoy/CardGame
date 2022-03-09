using System.Collections.Generic;
public class AttackSlot : ISlot {
    public void DrawStage (IRole from, List<int> effectValue, IRole to = null) { }

    public void MainStage (IRole from, List<int> effectValue, IRole to = null) { }

    public void EndStage (IRole from, List<int> effectValue, IRole to = null) { }

    public void Trigger (IRole from, List<int> effectValue, IRole to = null) {
        to.Damage (effectValue[0]);
    }
}