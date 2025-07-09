using Unity.VisualScripting;
using UnityEngine;

public class Platform : Interactable
{
    [SerializeField] private Collider _collider;

    private float _sector1 = 1.5f;
    private float _sector2 = 1f;
    private float _sector3 = 0.5f;

    private float _score;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Player player))
        {
            // считать по модулю
            float differenceX = player.transform.position.x - transform.position.x;
            float differenceZ = player.transform.position.z - transform.position.z;

            int diffX = Mathf.RoundToInt(differenceX);

            //int positiveX = (x < 0) ? -x : x;
            //if (player.transform.position.x -  transform.position.x < _sector3 &&
            //    player.transform.position.z - transform.position.z < _sector3)
            //{
            //    Debug.Log("В самый центр");
            //    Debug.Log(player.transform.position -  transform.position);
            //}
            //else if(player.transform.position.x - transform.position.x < _sector2 &&
            //    player.transform.position.z - transform.position.z < _sector2)
            //{
            //    Debug.Log("Средненько");
            //    Debug.Log(player.transform.position - transform.position);
            //}
            //else if(player.transform.position.x - transform.position.x < _sector1 &&
            //    player.transform.position.z - transform.position.z < _sector1)
            //{
            //    Debug.Log("Почти сдох");
            //    Debug.Log(player.transform.position - transform.position);
            //}
        }
    }
}
