using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<PlayerViewSO> _playerViewsSO;
    [SerializeField] private Fabrica _fabrica;
    [SerializeField] private ItemView _itemView;

    public event Action<PlayerViewSO> PlayerViewChanged;

    private void Awake()
    {
        InitAllShopItems();
    }

    public void ChangePlayerView(PlayerViewSO playerViewSO)
    {
        PlayerViewChanged?.Invoke(playerViewSO);
    }

    private void InitAllShopItems()
    {
        foreach (var view in _playerViewsSO)
        {
            ItemView newView = _fabrica.CreatePrefab(_itemView, Quaternion.identity, _container);
            newView.Init(view);
            newView.InitShopView(this);
        }
    }
}
