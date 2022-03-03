using System.Collections.Generic;
public class CardConfig : IConfig {

    public List<CardData> cards = new List<CardData> ();

    private Dictionary<int, CardData> cardDic = new Dictionary<int, CardData> ();

    private Dictionary<RoleType, List<CardData>> cardRoleDic = new Dictionary<RoleType, List<CardData>> ();

    public CardData GetCardDataById (int id) {
        return this.cards[id];
    }

    public List<CardData> GetCardDataByRoleType (RoleType roleType) {
        return this.cardRoleDic[roleType];
    }

    public void ConvertData () {
        foreach (CardData cardData in cards) {
            cardDic.Add (cardData.id, cardData);

            if (!cardRoleDic.TryGetValue ((RoleType) cardData.belongRole, out List<CardData> cardRoleList)) {
                cardRoleList = new List<CardData> ();
                cardRoleDic.Add ((RoleType) cardData.belongRole, cardRoleList);
            }

            cardRoleList.Add (cardData);
        }

    }
}