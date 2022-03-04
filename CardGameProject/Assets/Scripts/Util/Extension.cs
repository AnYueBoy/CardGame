using UnityEngine;
public static class Extension {

    public static Vector3 Clone (this Vector3 target) {
        return new Vector3 (target.x, target.y, target.z);
    }
}