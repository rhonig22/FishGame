using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineController : MonoBehaviour
{
    [SerializeField] private AudioClip _finishSound;
    private bool _finished = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_finished && collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(_finishSound, transform.position);
            var player = collision.GetComponent<PlayerController>();
            player.FinishLevel();
            _finished = true;
        }
    }
}
