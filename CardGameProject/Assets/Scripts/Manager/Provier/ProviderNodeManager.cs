using UFramework.Core;
using UnityEngine;
public class ProviderNodeManager : MonoBehaviour, IServiceProvider {
    public void Init () { }

    public void Register () {
        App.Instance<INodeManager> (this.GetComponent<NodeManager> ());
    }
}