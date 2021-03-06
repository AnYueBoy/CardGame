public class TurnManager : ITurnManager {
    private IRole curRole;

    private TurnStage curTurnStage = TurnStage.DrawStage;

    private void TurnToDrawStage () {
        this.curTurnStage = TurnStage.DrawStage;
        curRole.DrawStage ();
    }

    private void TurnToMainStage () {
        this.curTurnStage = TurnStage.MainStage;
        curRole.MainStage ();
    }

    private void TurnToEndStage () {
        this.curTurnStage = TurnStage.EndStage;
        curRole.EndStage ();
    }

    public void NextStage () {
        switch (curTurnStage) {
            case TurnStage.DrawStage:
                TurnToMainStage ();
                break;

            case TurnStage.MainStage:
                TurnToEndStage ();
                break;

            case TurnStage.EndStage:
                curTurnStage = TurnStage.None;
                break;
            default:
                throw new System.Exception ($"except value {curTurnStage}");
        }

    }

    public TurnStage GetCurStage () {
        return curTurnStage;
    }

    public void SetActiveRole (IRole role) {
        this.curRole = role;
        this.TurnToDrawStage ();
    }
}