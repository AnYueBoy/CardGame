using UnityEngine;
public class AdaptUI : MonoBehaviour {

    [SerializeField] private RectTransform leftRectTrans;
    [SerializeField] private float leftOffset = 0;
    [SerializeField] private RectTransform rightRectTrans;
    [SerializeField] private float rightOffset = 0;
    [SerializeField] private RectTransform topRectTrans;
    [SerializeField] private float topOffset = 0;
    [SerializeField] private RectTransform bottomRectTrans;
    [SerializeField] private float bottomOffset = 0;
    private void Awake () {
        Rect safeArea = Screen.safeArea;

        if (leftRectTrans != null) {
            Vector3 leftVec = this.leftRectTrans.localPosition.Clone ();
            leftVec.x = safeArea.xMin + leftOffset;
            this.leftRectTrans.localPosition = leftVec;
        }

        if (rightRectTrans != null) {
            Vector3 rightVec = this.rightRectTrans.localPosition.Clone ();
            rightVec.x = safeArea.xMax + rightOffset;
            this.rightRectTrans.localPosition = rightVec;
        }

        if (topRectTrans != null) {
            Vector3 topVec = this.topRectTrans.localPosition.Clone ();
            topVec.y = safeArea.yMax + topOffset;
            this.topRectTrans.localPosition = topVec;
        }

        if (bottomRectTrans != null) {
            Vector3 bottomVec = this.bottomRectTrans.localPosition.Clone ();
            bottomVec.y = safeArea.yMin + bottomOffset;
            this.bottomRectTrans.localPosition = bottomVec;
        }
    }
}