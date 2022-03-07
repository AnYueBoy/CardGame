using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;

public class ProviderBattleManager : IServiceProvider {
    public void Init () {

    }

    public void Register () {
        App.Singleton<IBattleManager, BattleManager> ();
    }
}