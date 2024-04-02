using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterCollider : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            _playerController.EnterWater();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            _playerController.LeaveWater();
        }
    }
}
