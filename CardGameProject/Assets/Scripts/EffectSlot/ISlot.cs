public interface ISlot {

    void DrawStage (Role from, Role to = null);

    void ReadyStage (Role from, Role to = null);

    void EndStage (Role from, Role to = null);

    void Trigger (Role from, Role to = null);
}