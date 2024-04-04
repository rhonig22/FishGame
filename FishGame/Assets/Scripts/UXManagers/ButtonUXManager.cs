using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUXManager : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonClick;

    public void ButtonClick()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
    }
}
