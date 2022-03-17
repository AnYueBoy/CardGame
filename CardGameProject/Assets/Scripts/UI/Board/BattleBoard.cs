using System;
using System.Collections.Generic;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.UI;

public class BattleBoard : BaseUI
{
    [SerializeField] private RectTransform rolesTrans;
    [SerializeField] private RectTransform cardParentTrans;
    [SerializeField] private Text energyText;
    [SerializeField] private RectTransform pointerParent;

    private RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform ??= GetComponent<RectTransform>();
    }

    public override void OnShow(params object[] args)
    {
        SpawnRoles();
        SpawnPointer();
        RefreshEnergy();
    }

    private void SpawnRoles()
    {
        // 生成玩家
        List<IRole> roleList = new List<IRole>();
        GameObject playerPrefab = App.Make<IAssetsManager>().GetAssetByUrlSync<GameObject>("Role/Player");
        GameObject playerNode = App.Make<IObjectPool>().RequestInstance(playerPrefab);
        playerNode.transform.SetParent(rolesTrans);
        Player player = playerNode.GetComponent<Player>();
        // 设置玩家卡牌父节点
        player.SetCardParent(cardParentTrans);
        player.Init();
        roleList.Add(player);

        GameObject enemyPrefab = App.Make<IAssetsManager>().GetAssetByUrlSync<GameObject>("Role/Enemy");
        GameObject enemyNode = App.Make<IObjectPool>().RequestInstance(enemyPrefab);
        enemyNode.transform.SetParent(rolesTrans);
        Enemy enemy = enemyNode.GetComponent<Enemy>();
        roleList.Add(enemy);
        enemy.Init();

        App.Make<IBattleManager>().BuildBattleData(roleList);
    }

    private void RefreshEnergy()
    {
        RoleData roleData = App.Make<IBattleManager>().GetPlayerRoleData();
        this.energyText.text = roleData.energy + "/" + roleData.maxEnergy;
    }

    public void EndStage()
    {
        App.Make<ITurnManager>().NextStage();
    }

    private Pointer _pointer;

    private void SpawnPointer()
    {
        // 生成指示标
        GameObject pointerPrefab = App.Make<IAssetsManager>().GetAssetByUrlSync<GameObject>(ItemUrl.PointerUrl);
        GameObject pointerNode = App.Make<IObjectPool>().RequestInstance(pointerPrefab);
        RectTransform pointerRectTransform = pointerNode.GetComponent<RectTransform>();
        pointerRectTransform.SetParent(pointerParent, false);
        pointerRectTransform.localPosition = Vector3.down * 150;
        pointerNode.SetActive(false);

        _pointer = pointerNode.GetComponent<Pointer>();
    }

    public Pointer Pointer => _pointer;
}