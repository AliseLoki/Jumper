using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    public void OnRestartButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
