using UFramework.Core;
using UFramework.GameCommon;
public class ModeSelectDialog : BaseUI {

    public override void OnShow (params object[] args) { }

    public void SelectModeClick () {
        this.BackClick ();
        App.Make<IUIManager> ().ShowDialog<RoleSelectDialog> ();
    }

    public void BackClick () {
        App.Make<IUIManager> ().CloseDialog<ModeSelectDialog> ();
    }

}