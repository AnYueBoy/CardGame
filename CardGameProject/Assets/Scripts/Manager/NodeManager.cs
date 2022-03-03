using UnityEngine;

public class NodeManager : MonoBehaviour, INodeManager {
    [SerializeField] private Transform canvasTrans;
    public Transform CanvasTrans => canvasTrans;

}