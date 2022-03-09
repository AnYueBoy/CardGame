public interface ITurnManager {

    TurnStage GetCurStage ();

    void NextStage ();

    void SetActiveRole (IRole role);
}