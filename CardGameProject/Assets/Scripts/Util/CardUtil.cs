using System;

public class CardUtil {

    public static string GetCardBgPath (CardType cardType, RoleType roleType) {
        return "Textures/Card/" + Enum.GetName (typeof (CardType), cardType) + "/" + Enum.GetName (typeof (RoleType), roleType);
    }

    public static string GetCardFramePath (CardType cardType) {
        return "Textures/Card/" + Enum.GetName (typeof (CardType), cardType) + "/Frame";
    }

    public static string GetCardBannerPath (CardRarity cardRarity) {
        return "Textures/Card/Banner/" + Enum.GetName (typeof (CardRarity), cardRarity);
    }

    public static string GetCardConsumePath (RoleType roleType) {
        return "Textures/Card/Consume/" + Enum.GetName (typeof (RoleType), roleType);
    }

    public static string GetCardTypeValue (CardType cardType) {
        switch (cardType) {
            case CardType.Attack:
                return "攻击";

            case CardType.Skill:
                return "技能";

            case CardType.Ability:
                return "能力";

            default:
                return "错误值";
        }
    }
}