using System.Collections.Generic;

public interface ISlot {

    void DrawStage (Role from, List<int> effectValue, Role to = null);

    void MainStage (Role from, List<int> effectValue, Role to = null);

    void EndStage (Role from, List<int> effectValue, Role to = null);

    void Trigger (Role from, List<int> effectValue, Role to = null);
}