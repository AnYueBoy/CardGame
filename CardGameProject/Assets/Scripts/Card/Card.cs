using System;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour {

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

    public void Init (CardData cardData) {
        this.cardData = cardData;
        this.RefreshCardInfo ();
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

    public void DrawStage (Role from, Role to = null) {
        _slot.DrawStage (from, cardData.effectValue, to);
    }

    public void ReadyStage (Role from, Role to = null) {
        _slot.ReadyStage (from, cardData.effectValue, to);
    }

    public void EndStage (Role from, Role to = null) {
        _slot.EndStage (from, cardData.effectValue, to);
    }

    public void Trigger (Role from, Role to = null) {
        _slot.Trigger (from, cardData.effectValue, to);
    }
}