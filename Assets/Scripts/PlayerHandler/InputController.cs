using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnMouseDown()
    {
        _player.JumpHandler.IncreaseJumpPower();
    }

    private void OnMouseUp()
    {
        _player.SoundController.StopSound();
        _player.JumpHandler.Jump();
    }
}
