using UnityEngine;

[CreateAssetMenu(fileName = "PlayerViewSO", menuName = "Scriptable Objects/PlayerViewSO")]
public class PlayerViewSO : ScriptableObject
{
    public int Price;
    public string Name;
    public PlayerView Prefab;
    public Sprite Sprite;
}
