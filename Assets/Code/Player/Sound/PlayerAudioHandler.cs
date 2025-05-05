using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource engineAudioSource;
    public AudioSource elevatorAudioSource;
    public AudioSource rudderAudioSource;
    public AudioSource aelironAudioSource;
    public AudioSource flapAudioSource;
    public AudioSource landingGearAudioSource;

    [Header("Audio Clips")]
    public AudioClip engineAudioClip;
    public AudioClip elevatorAudioClip;
    public AudioClip rudderAudioClip;
    public AudioClip aelironAudioClip;
    public AudioClip flapAudioClip;
    public AudioClip landingGearAudioClip;


    public void PlayEngineSound()
    {
        engineAudioSource.clip = engineAudioClip;
        
        if(!engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
    }

    public void EngineSoundPitchModifier(float input , float maxPitch)
    {
        engineAudioSource.pitch = Mathf.Lerp(1f, 1f + (input * maxPitch), 1f);
    }

    public void SetEnginePitch(float pitch)
    {
        engineAudioSource.pitch = pitch;
    }

    public void PlayElevatorSound(float input)
    {
        elevatorAudioSource.clip = elevatorAudioClip;
        elevatorAudioSource.volume = Mathf.Lerp(0f, 1f, Mathf.Abs(input));

        if(!elevatorAudioSource.isPlaying)
        {
            elevatorAudioSource.Play();
        }
    }

    public void PlayRudderSound(float input)
    {
        rudderAudioSource.clip = rudderAudioClip;
        rudderAudioSource.volume = Mathf.Lerp(0f, 1f, Mathf.Abs(input));
        
        if(!rudderAudioSource.isPlaying)
        {
            rudderAudioSource.Play();
        }
    }

    public void PlayAelironSound(float input)
    {
        aelironAudioSource.clip = aelironAudioClip;
        aelironAudioSource.volume = Mathf.Lerp(0f, 1f, Mathf.Abs(input));
        
        if(!aelironAudioSource.isPlaying)
        {
            aelironAudioSource.Play();
        }
    }

    public void PlayFlapSound()
    {
        flapAudioSource.clip = flapAudioClip;
        
        if(!flapAudioSource.isPlaying)
        {
            flapAudioSource.Play();
        }
    }

    public void PlayLandingGearSound()
    {
        landingGearAudioSource.clip = landingGearAudioClip;
        
        if(!landingGearAudioSource.isPlaying)
        {
            landingGearAudioSource.Play();
        }
    }
}
