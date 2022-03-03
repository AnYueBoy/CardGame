using UFramework.Core;
public class ProviderTurnManager : IServiceProvider {
    public void Init () { }

    public void Register () {
        App.Singleton<ITurnManager, TurnManager> ();
    }
}