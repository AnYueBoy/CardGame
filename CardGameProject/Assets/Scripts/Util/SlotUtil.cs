using UnityEngine;
public class SlotUtil {

    public static ISlot GetSlot (string slotType) {
        switch (slotType) {
            case SlotNameEnum.Attack:
                return new AttackSlot ();

            case SlotNameEnum.DefenceAttack:
                return new DefenceAttackSlot ();

            default:
                Debug.LogError ($"except slot type: {slotType}");
                return null;
        }
    }
}