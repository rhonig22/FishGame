using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RingController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private AudioClip _coinSound;
    public void Collect()
    {
        _collider.enabled = false;
        SoundManager.Instance.PlaySound(_coinSound, transform.position);
        _animator.SetTrigger("Pickup");
        _particles.Play();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
