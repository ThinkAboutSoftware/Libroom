using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteBtn : MonoBehaviour
{
    private bool isMuted;

    void Start()
    {
        isMuted = false;
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}
