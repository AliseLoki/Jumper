using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Vector3 _offset;

    private void Start()
    {
        _offset = CalculateOffset();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = _player.transform.position + _offset;
    }

    private Vector3 CalculateOffset()
    {
        return transform.position - _player.transform.position;
    }
}
