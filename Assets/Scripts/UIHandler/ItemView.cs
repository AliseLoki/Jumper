using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private SoundController _soundController;
    private PlayerViewSO _playerViewSO;
    private ShopView _shopView;

    private void OnEnable()
    {
        _button.onClick?.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick?.RemoveListener(OnButtonClick);
    }

    public void InitShopView(ShopView shopView, SoundController soundController)
    {
        _shopView = shopView;
        _soundController = soundController;
    }

    public void Init(PlayerViewSO playerViewSO)
    {
        _image.sprite = playerViewSO.Sprite;
        _text.text = playerViewSO.Price.ToString();
        _playerViewSO = playerViewSO;
    }

    private void OnButtonClick()
    {
        _soundController.PlaySound(SoundName.ButtonPressed.ToString());
        _shopView.ChangePlayerView(_playerViewSO);
    }
}
