using System;
using DG.Tweening;
using UFramework.Core;
using UFramework.EventDispatcher;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler {

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

        string cardBgPath = CardUtil.GetCardBgPath (cardType, belongRole);
        this.cardBg.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (cardBgPath);

        this.cardIcon.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (this.cardData.cardIcon);

        string cardFramePath = CardUtil.GetCardFramePath (cardType);
        this.cardFrame.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (cardFramePath);

        string cardBannerPath = CardUtil.GetCardBannerPath (cardRarity);
        this.cardBanner.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (cardBannerPath);

        this.cardName.text = this.cardData.cardName;

        string cardConsumePath = CardUtil.GetCardConsumePath (belongRole);
        this.cardConsumeIcon.sprite = App.Make<IAssetsManager> ().GetAssetByUrlSync<Sprite> (cardConsumePath);

        this.cardConsume.text = this.cardData.consume.ToString ();
        this.cardDescribe.text = this.cardData.cardDescribe;
        this.cardType.text = CardUtil.GetCardTypeValue (cardType);
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

    private readonly float triggerInterval = 300f;
    private void Trigger (IRole to = null) {
        if (cardData.consume > role.RoleData.energy) {
            return;
        }
        _slot.Trigger (this.role, cardData.effectValue, to);
        // TODO: 回收卡牌
        App.Make<IObjectPool> ().ReturnInstance (gameObject);
        App.Make<IEventDispatcher> ().Raise (EventTypeEnum.RecycleCard, this, new EventParam (this));
    }

    #region   触摸事件
    public void OnBeginDrag (PointerEventData eventData) {
        this.rectTransform.DOKill ();
        this.rectTransform.localEulerAngles = Vector3.zero;
        this.rectTransform.localScale = Vector3.one * 0.7f;
        if (this.parentRectTrans == null) {
            this.parentRectTrans = this.rectTransform.parent.GetComponent<RectTransform> ();
        }
        this.rectTransform.SetAsLastSibling ();
        this.isEnergyLack = false;
    }

    private bool isEnergyLack = false;
    public void OnDrag (PointerEventData eventData) {
        float verticalDis = (rectTransform.localPosition.y - this.originPos.y);
        if (verticalDis >= triggerInterval) {
            // 到达触发距离
            float curPlayerEnergy = App.Make<IBattleManager> ().GetPlayerRoleData ().energy;
            if (curPlayerEnergy < cardData.consume) {
                // 能量不足
                this.isEnergyLack = true;
                this.OnPointerUp (null);
                return;
            }
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle (this.parentRectTrans, eventData.position, eventData.enterEventCamera, out Vector2 localPos);
        this.rectTransform.localPosition = localPos;
    }

    private Vector3 originPos;
    private Vector3 originAngle;
    private int renderIndex;
    private bool isSaveData = false;
    private void SaveCardState () {
        if (isSaveData) {
            return;
        }
        isSaveData = true;
        this.originPos = rectTransform.localPosition;
        this.originAngle = rectTransform.localEulerAngles;
        this.renderIndex = this.rectTransform.GetSiblingIndex ();
    }

    private readonly float animationTime = 0.25f;
    public void OnPointerDown (PointerEventData eventData) {
        SaveCardState ();

        this.rectTransform.SetSiblingIndex (this.renderIndex + 1);
        this.rectTransform.DOLocalRotate (Vector3.zero, animationTime);
        this.rectTransform.DOLocalMoveY (100, animationTime);
        this.rectTransform.DOScale (Vector3.one, animationTime);
    }

    public void OnPointerUp (PointerEventData eventData) {
        if (this.isEnergyLack) {
            // TODO: 显示能量不足提示
            this.recoveryCard ();
            return;
        }
        float verticalDis = (rectTransform.localPosition.y - this.originPos.y);
        Debug.Log ($"distance: {verticalDis}");
        if (verticalDis < triggerInterval) {
            this.recoveryCard ();
            return;
        }

        // 触发效果
        Trigger (this.role);
    }

    private void recoveryCard () {
        this.rectTransform.SetSiblingIndex (this.renderIndex);
        this.rectTransform.DOLocalRotate (originAngle, animationTime);
        this.rectTransform.DOLocalMove (originPos, animationTime);
        this.rectTransform.DOScale (Vector3.one * 0.7f, animationTime);
    }
    #endregion
}