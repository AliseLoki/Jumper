using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _diamondsAmountText;
    [SerializeField] private Player _player;

    private int _diamonds;

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
        if (interactable as Diamond)
        {
            ShowCollectablesAmount(_diamondsAmountText, ref _diamonds);
        }
    }

    private void ShowCollectablesAmount(TMP_Text text, ref int amount)
    {
        amount++;
        text.text = amount.ToString();
    }
}
