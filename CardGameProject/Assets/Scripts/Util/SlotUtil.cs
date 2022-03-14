using UnityEngine;
public class SlotUtil {

    public static ISlot GetSlot (string slotType) {
        switch (slotType) {
            case SlotNameEnum.Attack:
                return new AttackSlot ();
            default:
                Debug.LogError ($"except slot type: {slotType}");
                return null;
        }
    }
}