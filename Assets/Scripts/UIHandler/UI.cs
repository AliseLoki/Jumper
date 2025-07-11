using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _crystallsAmountText;
    [SerializeField] private TMP_Text _coinsAmountText;
    [SerializeField] private Player _player;

    private int _crystalls;
    private int _coins;

    private void OnEnable()
    {
        _player.CollectablesAmountChanged += OnCollectablesAmountChanged;
    }

    private void OnDisable()
    {
        _player.CollectablesAmountChanged -= OnCollectablesAmountChanged;
    }

    public void OnRestartButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollectablesAmountChanged(Interactable interactable)
    {
        if (interactable as Coin)
        {
            ShowCollectablesAmount(_coinsAmountText, ref _coins);
        }
        else
        {
            ShowCollectablesAmount(_crystallsAmountText, ref _crystalls);
        }
    }

    private void ShowCollectablesAmount(TMP_Text text, ref int amount)
    {
        amount++;
        text.text = amount.ToString();
    }
}
