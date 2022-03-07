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

    private RectTransform rectTransform;
    private RectTransform parentRectTrans;

    public void Init (CardData cardData) {
        this.cardData = cardData;
        this.RefreshCardInfo ();
        this.rectTransform = GetComponent<RectTransform> ();
        this.parentRectTrans = this.rectTransform.parent.GetComponent<RectTransform> ();
    }

    private Role role;
    public void SetRole (Role role) {
        this.role = role;
        _slot = new AttackSlot();
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

    public void DrawStage (Role to = null) {
        _slot.DrawStage (this.role, cardData.effectValue, to);
    }

    public void MainStage (Role to = null) {
        _slot.MainStage (this.role, cardData.effectValue, to);
    }

    public void EndStage (Role to = null) {
        _slot.EndStage (this.role, cardData.effectValue, to);
    }

    private void Trigger (Role to = null) {
        _slot.Trigger (this.role, cardData.effectValue, to);
    }

    public void OnBeginDrag (PointerEventData eventData) {
        Debug.Log ("drag start");
    }

    public void OnDrag (PointerEventData eventData) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle (this.parentRectTrans, eventData.position, eventData.enterEventCamera, out Vector2 localPos);
        this.rectTransform.localPosition = localPos;
    }

    public void OnEndDrag (PointerEventData eventData) {
        Debug.Log ("drag end");
         this.Trigger (this.role);
    }
}