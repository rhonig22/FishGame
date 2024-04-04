using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    [SerializeField] private Animator _animator;
    private UnityEvent _transitionFinishedEvent = new UnityEvent();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void FadeOut(UnityAction action)
    {
        _transitionFinishedEvent.RemoveAllListeners();
        _transitionFinishedEvent.AddListener(action);
        _animator.SetTrigger("FadeOut");
    }

    public void FadeIn(UnityAction action)
    {
        _transitionFinishedEvent.RemoveAllListeners();
        _transitionFinishedEvent.AddListener(action);
        _animator.SetTrigger("FadeIn");
    }

    public void TransitionFinished()
    {
        _transitionFinishedEvent.Invoke();
    }
}