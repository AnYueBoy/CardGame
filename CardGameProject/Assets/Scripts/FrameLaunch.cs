using UnityEngine;
using UApplication = UFramework.Core.Application;
using UFramework.Bootstarp;

public class FrameLaunch : MonoBehaviour {
    private UApplication _application;
    private void Awake () {
        this._application = UApplication.New ();
        this._application.Bootstrap (new SystemProviderBootstrap (this));
    }

    void Start () {
        this._application.Init ();
    }

    void Update () {

    }
}