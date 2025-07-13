using System.Collections.Generic;
using UnityEngine;

public class ViewHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _viewContainer;
    [SerializeField] private PlayerView _currentPlayerView;

    [SerializeField] private PlayerViewSO _currentPlayerViewSO;
    [SerializeField] private List<PlayerViewSO> _allAccessablePlayerViewsSO;

    public void InitNewView(PlayerViewSO playerViewSO)
    {
        if (_currentPlayerViewSO != playerViewSO)
        {
            if (!CheckIfPlayerViewSOExist(playerViewSO))
            {
                _allAccessablePlayerViewsSO.Add(playerViewSO);
                AddNewViewPrefab(playerViewSO);
            }

            _currentPlayerViewSO = playerViewSO;
            SetExistingPrefabActive(playerViewSO);
        }
    }

    private bool CheckIfPlayerViewSOExist(PlayerViewSO playerViewSO)
    {
        foreach (var item in _allAccessablePlayerViewsSO)
        {
            if (item == playerViewSO)
            {
                return true;
            }
        }

        return false;
    }

    private void AddNewViewPrefab(PlayerViewSO playerViewSO)
    {
        _currentPlayerView.gameObject.SetActive(false);
        _currentPlayerView = _player.Fabrica.CreatePrefab(playerViewSO.Prefab, Quaternion.identity, _viewContainer);
        _currentPlayerView.gameObject.SetActive(true);
    }

    private void SetExistingPrefabActive(PlayerViewSO playerViewSO)
    {
        foreach (Transform child in _viewContainer)
        {
          PlayerView existingView = child.GetComponent<PlayerView>();

            if (existingView.PlayerViewSO == playerViewSO)
            {
                existingView.gameObject.SetActive(true);
            }
            else
            {
                existingView.gameObject.SetActive(false);
            }
        }
    }
    // инициализация дефолтного со
    // проверка нового со на идентичность старому
}
