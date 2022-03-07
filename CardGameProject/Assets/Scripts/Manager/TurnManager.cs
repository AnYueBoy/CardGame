public class TurnManager : ITurnManager {
    private Role curRole;

    private TurnStage curTurnStage = TurnStage.DrawStage;

    private void TurnToDrawStage () {
        this.curTurnStage = TurnStage.DrawStage;
        curRole.DrawStage ();
    }

    private void TurnToMainStage () {
        this.curTurnStage = TurnStage.MainStage;
        curRole.ReadyStage ();
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

    public void SetActiveRole (Role role) {
        this.curRole = role;
        this.TurnToDrawStage ();
    }
}