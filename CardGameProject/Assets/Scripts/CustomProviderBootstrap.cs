using UFramework.Core;
public class CustomProviderBootstrap : IBootstrap {
    public void Bootstrap () {
        IServiceProvider[] providerArray = new IServiceProvider[] {
            new ProviderConfigManager (),
            new ProviderTurnManager (),
            new ProviderBattleManager ()
        };

        foreach (IServiceProvider provider in providerArray) {
            if (provider == null) {
                continue;
            }

            if (App.IsRegistered (provider)) {
                continue;
            }

            App.Register (provider);
        }
    }
}