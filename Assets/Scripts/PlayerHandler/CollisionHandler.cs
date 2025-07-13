using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    private bool _isGrounded = true;

    public bool IsGrounded => _isGrounded;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground)) _isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground)) _isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Crystall crystall))
        {
            PickUpCollectable(crystall);
        }

        if (other.TryGetComponent(out Coin coin))
        {
            PickUpCollectable(coin);
        }
    }

    private void PickUpCollectable(Interactable interactable)
    {
        interactable.gameObject.SetActive(false);
        _player.ChangeCollectablesAmount(interactable);
    }
}
