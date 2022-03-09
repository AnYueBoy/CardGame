using System;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] private Image cardBg;

    [SerializeField] private Image cardIcon;

    [SerializeField] private Image cardFrame;

    [SerializeField] private Image cardBanner;

    [SerializeField] private Text cardName;

    [SerializeField] private Image cardConsumeIcon;

    [SerializeField] private Text cardConsume;

    [SerializeField] private Text cardDescribe;

    [SerializeField] private Text cardType;

    private CardData cardData;

    private ISlot _slot;

    public RectTransform rectTransform;
    private RectTransform parentRectTrans;

    public void Init (CardData cardData) {
        this.cardData = cardData;
        this.RefreshCardInfo ();
        this.rectTransform = GetComponent<RectTransform> ();

        _slot = new AttackSlot ();
    }

    private IRole role;
    public void SetRole (IRole role) {
        this.role = role;
    }

    private void RefreshCardInfo () {
        var cardType = this.cardData.cardType;
        var belongRole = this.cardData.belongRole;
        var cardRarity = this.cardData.rarity;

        string cardBgPath = GameUtil.GetCardBgPath (cardType, belongRole);
        this.cardBg.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (cardBgPath);

        this.cardIcon.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (this.cardData.cardIcon);

        string cardFramePath = GameUtil.GetCardFramePath (cardType);
        this.cardFrame.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (cardFramePath);

        string cardBannerPath = GameUtil.GetCardBannerPath (cardRarity);
        this.cardBanner.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (cardBannerPath);

        this.cardName.text = this.cardData.cardName;

        string cardConsumePath = GameUtil.GetCardConsumePath (belongRole);
        this.cardConsumeIcon.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (cardConsumePath);

        this.cardConsume.text = this.cardData.consume.ToString ();
        this.cardDescribe.text = this.cardData.cardDescribe;
        this.cardType.text = GameUtil.GetCardTypeValue (cardType);
    }

    public void DrawStage (IRole to = null) {
        _slot.DrawStage (this.role, cardData.effectValue, to);
    }

    public void MainStage (IRole to = null) {
        _slot.MainStage (this.role, cardData.effectValue, to);
    }

    public void EndStage (IRole to = null) {
        _slot.EndStage (this.role, cardData.effectValue, to);
    }

    private void Trigger (IRole to = null) {
        if (cardData.consume > role.RoleData.energy) {
            return;
        }
        _slot.Trigger (this.role, cardData.effectValue, to);
        // TODO: 回收卡牌
        App.Make<IObjectPool> ().ReturnInstance (gameObject);
    }

    private readonly float triggerInterval = 40f;

    #region   触摸事件
    public void OnBeginDrag (PointerEventData eventData) { }

    public void OnDrag (PointerEventData eventData) {
        this.parentRectTrans??= this.rectTransform.parent.GetComponent<RectTransform> ();
        RectTransformUtility.ScreenPointToLocalPointInRectangle (this.parentRectTrans, eventData.position, eventData.enterEventCamera, out Vector2 localPos);
        this.rectTransform.localPosition = localPos;
    }

    public void OnEndDrag (PointerEventData eventData) {
        this.Trigger (this.role);
    }

    #endregion
}