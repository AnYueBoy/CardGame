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

        App.DebugLevel = DebugLevel.Development;
    }

    void Start () {
        this._application.Init ();
        GameObject cardPrefab = App.Make<IAssetsManager> ().GetAssetByUrlSync<GameObject> ("Card");
        GameObject cardNode = App.Make<IObjectPool> ().RequestInstance (cardPrefab);
        cardNode.transform.SetParent(App.Make<INodeManager>().CanvasTrans);
        cardNode.transform.localPosition = Vector3.zero;

        CardData cardData = App.Make<IConfigManager> ().GetCardDataById (1);
        cardNode.GetComponent<Card> ().Init (cardData);
    }

    void Update () {
        float deltaTime = Time.deltaTime;
        App.Make<IPromiseTimer> ().LocalUpdate (deltaTime);
        App.Make<ITweenManager> ().LocalUpdate (deltaTime);
    }
}