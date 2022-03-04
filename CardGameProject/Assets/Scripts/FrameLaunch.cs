using UnityEngine;
using UApplication = UFramework.Core.Application;
using UFramework.Bootstarp;
using UFramework.Core;
using UFramework.GameCommon;
using UFramework.Promise;
using UFramework.Tween;

public class FrameLaunch : MonoBehaviour {
    private UApplication _application;
    private void Awake () {
        this._application = UApplication.New ();
        this._application.Bootstrap (
            new SystemProviderBootstrap (this),
            new CustomProviderBootstrap ());

        App.DebugLevel = DebugLevel.Production;
    }

    void Start () {
        this._application.Init ();
      
    }

    void Update () {
        float deltaTime = Time.deltaTime;
        App.Make<IPromiseTimer> ().LocalUpdate (deltaTime);
        App.Make<ITweenManager> ().LocalUpdate (deltaTime);
        App.Make<IBattleManager> ().LocalUpdate (deltaTime);
    }
}