using UFramework.Core;
public class ProviderBattleManager : IServiceProvider {
    public void Init () { }

    public void Register () {
        App.Singleton<IBattleManager, BattleManager> ();
    }
}