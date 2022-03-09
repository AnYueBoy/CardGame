public interface IRole {

    void Init ();

    void DrawStage ();

    void MainStage ();

    void EndStage ();

    void Damage (int value);

    void AddCard (Card card);
}