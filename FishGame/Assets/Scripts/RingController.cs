using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particles;
    public void Collect()
    {
        _animator.SetTrigger("Pickup");
        _particles.Play();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
