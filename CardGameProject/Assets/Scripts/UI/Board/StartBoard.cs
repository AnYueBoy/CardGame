using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
public class StartBoard : BaseUI {
    public override void OnShow (params object[] args) {
    }

    public void StartGameClick () {
        App.Make<IUIManager> ().ShowDialog<ModeSelectDialog> ();
    }
}