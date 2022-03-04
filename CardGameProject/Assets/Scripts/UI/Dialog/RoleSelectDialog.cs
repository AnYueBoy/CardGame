using System.Collections;
using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;

public class RoleSelectDialog : BaseUI {

    [SerializeField] private GameObject roleInfoNode;

    [SerializeField] private GameObject highLightNode;

    [SerializeField] private GameObject startBtnNode;

    public override void OnShow (params object[] args) {
        roleInfoNode.SetActive (false);
        this.highLightNode.SetActive (false);
        this.startBtnNode.SetActive (false);
    }

    public void SelectRoleClick () {
        this.roleInfoNode.SetActive (true);
        this.highLightNode.SetActive (true);
        this.startBtnNode.SetActive (true);
    }

    public void ConfirmClick () {
        // App.Make<IUIManager> ().CloseDialog<RoleSelectDialog> ();
    }

    public void BackClick () {
        App.Make<IUIManager> ().CloseDialog<RoleSelectDialog> ();
        App.Make<IUIManager> ().ShowDialog<ModeSelectDialog> ();
    }

}