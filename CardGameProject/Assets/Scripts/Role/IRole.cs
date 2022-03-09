public interface IRole {

    void Init ();

    RoleData RoleData { get; }

    void DrawStage ();

    void MainStage ();

    void EndStage ();

    void Damage (int value);

    void AddCard (Card card);
}