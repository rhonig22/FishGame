using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FinishLineController : MonoBehaviour
{
    [SerializeField] private AudioClip _finishSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(_finishSound, transform.position);
            var player = collision.GetComponent<PlayerController>();
            player.FinishLevel();
        }
    }
}
