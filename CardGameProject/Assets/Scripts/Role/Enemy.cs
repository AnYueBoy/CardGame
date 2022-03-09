using UnityEngine;
public class Enemy : MonoBehaviour, IRole {
    public void Init () { }
    public void AddCard (Card card) { }

    public void Damage (int value) { }

    public void DrawStage () { }

    public void EndStage () { }

    public void MainStage () { }
}