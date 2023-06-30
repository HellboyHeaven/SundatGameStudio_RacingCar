using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarAudioHandler : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float lowSpeed, mediumSpeed;
    [SerializeField] private AudioSource idleSpeedAudio, lowSpeedAudio, mediumSpeedAudio, highSpeedAudio;
    
    private SpeedEnum _speedEnum;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float currentSpeed = _rigidbody.velocity.magnitude;
        if (currentSpeed <= 2 )
        {
            if (_speedEnum == SpeedEnum.Idle) return;
            _speedEnum = SpeedEnum.Idle;
            lowSpeedAudio.Stop();
            mediumSpeedAudio.Stop();
            highSpeedAudio.Stop();
            if (!highSpeedAudio.isPlaying)  idleSpeedAudio.Play();;
        }
        else if (currentSpeed <= lowSpeed)
        {
            if (_speedEnum == SpeedEnum.Low) return;
            _speedEnum = SpeedEnum.Low;
            idleSpeedAudio.Stop();
            mediumSpeedAudio.Stop();
            highSpeedAudio.Stop();
            if (!highSpeedAudio.isPlaying) lowSpeedAudio.Play();
        }
        else if (currentSpeed <= mediumSpeed)
        {
            if (_speedEnum == SpeedEnum.Medium) return;
            _speedEnum = SpeedEnum.Medium;
            idleSpeedAudio.Stop();
            lowSpeedAudio.Stop();
            highSpeedAudio.Stop();
            if (!highSpeedAudio.isPlaying) mediumSpeedAudio.Play();
        }
        else
        {
            if (_speedEnum == SpeedEnum.High) return;
            _speedEnum = SpeedEnum.High;
            
            idleSpeedAudio.Stop();
            lowSpeedAudio.Stop();
            mediumSpeedAudio.Stop();
            if (!highSpeedAudio.isPlaying) highSpeedAudio.Play();
        }


       
    }
}

public enum SpeedEnum
{
    Idle,
    Low,
    Medium,
    High
}
