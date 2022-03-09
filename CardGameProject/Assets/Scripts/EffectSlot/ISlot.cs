using System.Collections.Generic;

public interface ISlot {

    void DrawStage (IRole from, List<int> effectValue, IRole to = null);

    void MainStage (IRole from, List<int> effectValue, IRole to = null);

    void EndStage (IRole from, List<int> effectValue, IRole to = null);

    void Trigger (IRole from, List<int> effectValue, IRole to = null);

    bool IsAimToRole ();
}