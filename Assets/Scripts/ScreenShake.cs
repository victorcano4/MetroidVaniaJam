using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }
    public float ShakeIntensity;
    public float ShakeTime;

    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin m_ChannelPerlin;
    private float timer;


    private void Start()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        StopShake();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void TriggerShake()
    {
        m_ChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        m_ChannelPerlin.m_AmplitudeGain = ShakeIntensity;

        timer = ShakeTime;
    }

    private void StopShake()
    {
        m_ChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        m_ChannelPerlin.m_AmplitudeGain = 0f;

        timer = 0;
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0 )
            {
                StopShake();
            }
        }
    }
}
