using System.Collections.Generic;
public class AttackSlot : ISlot {
    public void DrawStage (Role from, List<int> effectValue, Role to = null) { }

    public void ReadyStage (Role from, List<int> effectValue, Role to = null) { }

    public void EndStage (Role from, List<int> effectValue, Role to = null) { }

    public void Trigger (Role from, List<int> effectValue, Role to = null) {
        to.Damage (effectValue[0]);
    }
}