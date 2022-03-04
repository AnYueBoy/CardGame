public interface ITurnManager {

    TurnStage GetCurStage ();

    void NextStage ();

    void SetActiveRole (Role role);
}