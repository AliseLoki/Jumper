using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlatformsController _platformsController;
    [SerializeField] private Fabrica _fabrica;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private SoundController _soundController;

    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private JumpHandler _jumpHandler;
    [SerializeField] private ViewHandler _viewHandler;

    private bool _isJumpingOnAxisX = true;
   
    public event Action<Interactable> CollectablesAmountChanged;

    public bool IsJumpingOnAxisX => _isJumpingOnAxisX;   

    public Fabrica Fabrica => _fabrica;

    public CollisionHandler CollisionHandler => _collisionHandler;
    public JumpHandler JumpHandler => _jumpHandler;

    public SoundController SoundController => _soundController;

    private void OnEnable()
    {
        _platformsController.PlatformHasSpawnedOnAxisX += OnPlatformSpawned;
        _shopView.PlayerViewChanged += OnPlayerViewChanged;
    }

    private void Start()
    {
        //_viewHandler.InitView();
    }

    private void OnDisable()
    {
        _platformsController.PlatformHasSpawnedOnAxisX -= OnPlatformSpawned;
    }

    public void ChangeCollectablesAmount(Interactable interactable)
    {
        CollectablesAmountChanged?.Invoke(interactable);
    }

    private void OnPlayerViewChanged(PlayerViewSO playerViewSO)
    {
        _viewHandler.InitNewView(playerViewSO);
    }

    private void OnPlatformSpawned(bool isTrue)
    {
        _isJumpingOnAxisX = isTrue;
    }
}
