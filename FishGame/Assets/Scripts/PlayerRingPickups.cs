using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRingPickups : MonoBehaviour
{
    private PlayerController _player;

    private void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ring"))
        {
            _player.CollectRing();
            collision.gameObject.GetComponent<RingController>().Collect();
        }
    }
}
