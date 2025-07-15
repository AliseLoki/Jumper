using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private bool _isGrounded = true;

    public bool IsGrounded => _isGrounded;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground)) _isGrounded = true;
        _player.SoundController.PlaySound(SoundName.Landing.ToString());

        if(collision.collider.TryGetComponent(out Floor floor))
            _player.SoundController.PlaySound(SoundName.FallingOnTheFloor.ToString());
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground)) _isGrounded = false;
        _player.SoundController.PlaySound(SoundName.Jump.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            PickUpCollectable(coin);
            _player.SoundController.PlaySound(SoundName.Coin.ToString());
        }
    }

    private void PickUpCollectable(Interactable interactable)
    {
        interactable.gameObject.SetActive(false);
        _player.ChangeCollectablesAmount(interactable);
    }
}