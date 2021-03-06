using System;
using System.Collections.Generic;
using DG.Tweening;
using UFramework.Core;
using UFramework.EventDispatcher;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image cardBg;

    [SerializeField] private Image cardIcon;

    [SerializeField] private Image cardFrame;

    [SerializeField] private Image cardBanner;

    [SerializeField] private Text cardName;

    [SerializeField] private Image cardConsumeIcon;

    [SerializeField] private Text cardConsume;

    [SerializeField] private Text cardDescribe;

    [SerializeField] private Text cardTypeText;

    private CardData cardData;

    private List<ISlot> _slots;

    public RectTransform rectTransform;
    private RectTransform parentRectTrans;

    public void Init(CardData cardData)
    {
        this.cardData = cardData;
        RefreshCardInfo();
        rectTransform = GetComponent<RectTransform>();
        BuildSlot();
    }

    private void BuildSlot()
    {
        _slots = new List<ISlot>();
        foreach (var slotName in cardData.slots)
        {
            ISlot targetSlot = SlotUtil.GetSlot(slotName);
            _slots.Add(targetSlot);
        }
    }

    private IRole role;

    public void SetRole(IRole role)
    {
        this.role = role;
    }

    private void RefreshCardInfo()
    {
        var cardType = cardData.cardType;
        var belongRole = cardData.belongRole;
        var cardRarity = cardData.rarity;

        string cardBgPath = CardUtil.GetCardBgPath(cardType, belongRole);
        cardBg.sprite = App.Make<IAssetsManager>().GetAssetByUrlSync<Sprite>(cardBgPath);

        cardIcon.sprite = App.Make<IAssetsManager>().GetAssetByUrlSync<Sprite>(cardData.cardIcon);

        string cardFramePath = CardUtil.GetCardFramePath(cardType);
        cardFrame.sprite = App.Make<IAssetsManager>().GetAssetByUrlSync<Sprite>(cardFramePath);

        string cardBannerPath = CardUtil.GetCardBannerPath(cardRarity);
        cardBanner.sprite = App.Make<IAssetsManager>().GetAssetByUrlSync<Sprite>(cardBannerPath);

        cardName.text = cardData.cardName;

        string cardConsumePath = CardUtil.GetCardConsumePath(belongRole);
        cardConsumeIcon.sprite = App.Make<IAssetsManager>().GetAssetByUrlSync<Sprite>(cardConsumePath);

        cardConsume.text = cardData.consume.ToString();
        cardDescribe.text = cardData.cardDescribe;
        cardTypeText.text = CardUtil.GetCardTypeValue(cardType);
    }

    private bool IsAimRole()
    {
        foreach (ISlot slot in _slots)
        {
            if (slot.IsAimToRole())
            {
                return true;
            }
        }

        return false;
    }

    #region ??????????????????

    public void DrawStage(IRole to = null)
    {
        foreach (var slot in _slots)
        {
            slot.DrawStage(role, cardData.effectValue, to);
        }
    }

    public void MainStage(IRole to = null)
    {
        foreach (var slot in _slots)
        {
            slot.MainStage(role, cardData.effectValue, to);
        }
    }

    public void EndStage(IRole to = null)
    {
        foreach (var slot in _slots)
        {
            slot.EndStage(role, cardData.effectValue, to);
        }
    }

    private readonly float triggerInterval = 300f;

    private void Trigger(IRole to = null)
    {
        if (cardData.consume > role.RoleData.energy)
        {
            return;
        }

        foreach (var slot in _slots)
        {
            slot.Trigger(role, cardData.effectValue, to);
        }

        // TODO: ????????????
        App.Make<IObjectPool>().ReturnInstance(gameObject);
        App.Make<IEventDispatcher>().Raise(EventTypeEnum.RecycleCard, this, new EventParam(this));
    }

    #endregion

    #region ????????????

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.DOKill();
        rectTransform.localEulerAngles = Vector3.zero;
        rectTransform.localScale = Vector3.one * 0.7f;
        if (parentRectTrans == null)
        {
            parentRectTrans = rectTransform.parent.GetComponent<RectTransform>();
        }

        rectTransform.SetAsLastSibling();
        isEnergyLack = false;
    }

    private bool isEnergyLack = false;
    private bool isReadyAim = false;

    public void OnDrag(PointerEventData eventData)
    {
        if (isReadyAim)
        {
            Pointer pointer = App.Make<IUIManager>().GetCurBoard<BattleBoard>().Pointer;
            Vector2 convertPos = ConvertPos(eventData);
            pointer.MovePointer(convertPos);
            return;
        }

        float verticalDis = (rectTransform.localPosition.y - originPos.y);
        // ??????????????????
        if (verticalDis >= triggerInterval)
        {
            float curPlayerEnergy = App.Make<IBattleManager>().GetPlayerRoleData().energy;
            // ????????????
            if (curPlayerEnergy < cardData.consume)
            {
                isEnergyLack = true;
                OnPointerUp(null);
                // ??????????????????????????????
                eventData.pointerDrag = null;
                return;
            }

            // ??????????????????
            if (IsAimRole())
            {
                rectTransform.DOLocalMove(Vector3.zero, animationTime);
                Pointer pointer = App.Make<IUIManager>().GetCurBoard<BattleBoard>().Pointer;
                Vector2 convertPos = ConvertPos(eventData);
                pointer.MovePointer(convertPos);
                isReadyAim = true;
                return;
            }
        }

        Vector2 localPos = ConvertPos(eventData);
        rectTransform.localPosition = localPos;
    }

    private Vector2 ConvertPos(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRectTrans,
            eventData.position,
            eventData.enterEventCamera,
            out Vector2 localPos);

        return localPos;
    }

    private Vector3 originPos;
    private Vector3 originAngle;
    private int renderIndex;
    private bool isSaveData = false;

    private void SaveCardState()
    {
        if (isSaveData)
        {
            return;
        }

        isSaveData = true;
        originPos = rectTransform.localPosition;
        originAngle = rectTransform.localEulerAngles;
        renderIndex = rectTransform.GetSiblingIndex();
    }

    private readonly float animationTime = 0.25f;

    public void OnPointerDown(PointerEventData eventData)
    {
        SaveCardState();

        rectTransform.SetSiblingIndex(renderIndex + 1);
        rectTransform.DOLocalRotate(Vector3.zero, animationTime);
        rectTransform.DOLocalMoveY(100, animationTime);
        rectTransform.DOScale(Vector3.one, animationTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isReadyAim)
        {
            Trigger(role);
            return;
        }

        if (isEnergyLack && eventData != null)
        {
            return;
        }

        if (isEnergyLack)
        {
            // TODO: ????????????????????????
            recoveryCard();
            return;
        }

        float verticalDis = (rectTransform.localPosition.y - originPos.y);
        if (verticalDis < triggerInterval)
        {
            recoveryCard();
            return;
        }

        // ????????????
        Trigger(role);
    }

    private void recoveryCard()
    {
        rectTransform.SetSiblingIndex(renderIndex);
        rectTransform.DOLocalRotate(originAngle, animationTime);
        rectTransform.DOLocalMove(originPos, animationTime);
        rectTransform.DOScale(Vector3.one * 0.7f, animationTime);
    }

    #endregion
}