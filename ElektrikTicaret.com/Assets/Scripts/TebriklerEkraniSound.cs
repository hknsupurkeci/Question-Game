using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TebriklerEkraniSound : MonoBehaviour
{
    AudioSource tebriklerSound;
    void Start()
    {
        tebriklerSound = GetComponent<AudioSource>();
        tebriklerSound.Play();
    }
}
