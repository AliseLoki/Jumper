using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private SoundController _soundController;
    [SerializeField] private ShopView _shopView;
   
    [SerializeField] private TMP_Text _diamondsAmountText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _jumpPowerScaleImage;
    [SerializeField] private Button _restartButton;

    private Player _player;
    private PlatformsController _platformsController;

    [Header("JumpPower Values")]
    [SerializeField] float _minValue = 3;
    [SerializeField] float _divider = 10;

    private int _diamonds;
    // ������� ������ ����, ��� ������� ������ �������, �����, �������� ������
    // ��������� �������
    private void OnDisable()
    {
        _player.CollectablesAmountChanged -= OnCollectablesAmountChanged;
        _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
        _platformsController.ScoreController.ScoreChanged -= OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged -= OnPlayerJumpPowerChanged;
    }

    public void Init(Player player, Fabrica fabrica, PlatformsController platformsController)
    {
        _player = player;
        _platformsController = platformsController;
        _shopView.Init(fabrica);
    }

    public void StartGame()
    {
        SetJumpPowerScaleAmount(0);
        _restartButton.onClick.AddListener(OnRestartButtonPressed);
        _player.CollectablesAmountChanged += OnCollectablesAmountChanged;
        _platformsController.ScoreController.ScoreChanged += OnScoreChanged;
        _player.JumpHandler.JumpPowerChanged += OnPlayerJumpPowerChanged;
    }

    private void OnRestartButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    private void OnPlayerJumpPowerChanged(float jumpPower)
    {
        SetJumpPowerScaleAmount((jumpPower - _minValue) / _divider);
    }

    private void OnCollectablesAmountChanged()
    {
        _diamonds++;
        ShowNewValue(_diamondsAmountText, _diamonds);
    }

    private void OnScoreChanged(int score)
    {
        ShowNewValue(_scoreText, score);
    }

    private void SetJumpPowerScaleAmount(float jumpPower)
    {
        _jumpPowerScaleImage.fillAmount = jumpPower;
    }

    private void ShowNewValue(TMP_Text text, int newValue)
    {
        text.text = newValue.ToString();
    }
}