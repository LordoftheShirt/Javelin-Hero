using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBob : MonoBehaviour
{
    [Header("Bob Values")]
    [SerializeField] private float bobFrequency = 1f;
    [SerializeField] private float bobAmplitude = 1f;

    [Header("Rotation Values")]
    [SerializeField] private float rotateFrequency = 1f;
    [SerializeField] private float rotateAmplitude = 1f;

    private float sineWaveTime = 0;
    private float sineWaveResult;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        sineWaveTime += Time.deltaTime;
        sineWaveResult = Mathf.Sin(bobFrequency * sineWaveTime) * bobAmplitude;

        transform.position = transform.position + new Vector3(0, sineWaveResult);
        transform.rotation = Quaternion.Euler(0, Mathf.Sin(rotateFrequency * sineWaveTime) * rotateAmplitude, Mathf.Sin(rotateFrequency * sineWaveTime) * rotateAmplitude);
    }
}
