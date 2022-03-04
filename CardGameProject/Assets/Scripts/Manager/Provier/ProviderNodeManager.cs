using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
public class ProviderNodeManager : MonoBehaviour, IServiceProvider {
    public void Init () {
        App.Make<IUIManager> ().Init (App.Make<INodeManager> ().CanvasTrans);
    }

    public void Register () {
        App.Instance<INodeManager> (this.GetComponent<NodeManager> ());
    }
}