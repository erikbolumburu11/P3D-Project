using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] AudioClip soundEffect;

    public void PlaySound(){
        GetComponent<AudioSource>().PlayOneShot(soundEffect);
    }
}
