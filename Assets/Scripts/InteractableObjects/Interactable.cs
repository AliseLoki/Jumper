using UnityEngine;

public abstract class Interactable : MonoBehaviour 
{
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
