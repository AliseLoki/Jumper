using UnityEngine;
using UnityEngine.UI;

public class JumpPowerView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _player.JumpPowerChanged += OnPlayerJumpPowerIncreased;
    }

    private void OnDisable()
    {
        _player.JumpPowerChanged -= OnPlayerJumpPowerIncreased;
    }

    private void OnPlayerJumpPowerIncreased(float jumpPower)
    {
        _slider.value = jumpPower;
    }
}
