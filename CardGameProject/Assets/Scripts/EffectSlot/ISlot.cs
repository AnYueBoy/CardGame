public interface ISlot {

    void DrawStage (RoleData from, RoleData to = null);

    void ReadyStage (RoleData from, RoleData to = null);

    void EndStage (RoleData from, RoleData to = null);

    void Trigger (RoleData from, RoleData to = null);
}