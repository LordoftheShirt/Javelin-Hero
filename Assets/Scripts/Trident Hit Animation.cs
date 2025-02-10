using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TridentHitAnimation : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float animationTime = 0.5f;
    [SerializeField, Range(0, 90)] private float sineWaveAmplitude = 1.0f;
    [SerializeField, Range(0, 100)] private float sineWaveFrequency = 1.0f;
    [SerializeField] private GameObject trident;
    private TridentBehaviour tridentBehaviour;
    private float animationCounter;
    private float sinusResult, sineWaveX = 0;
    private float rememberRotationZ;
    private bool mustRealign;



    // Start is called before the first frame update
    void Start()
    {
        tridentBehaviour = trident.GetComponent<TridentBehaviour>();
    }

    void Update()
    {
        if (tridentBehaviour.GetImpactCheck())
        {
            animationCounter -= Time.deltaTime;
        }
        else 
        {
            animationCounter = animationTime;
        }

        if (animationCounter > 0 && tridentBehaviour.GetImpactCheck())
        {
            transform.SetParent(null);
            sineWaveX += Time.deltaTime;
            sinusResult = Mathf.Sin(sineWaveFrequency * sineWaveX) * sineWaveAmplitude * animationCounter * animationCounter * animationCounter;
            mustRealign = true;
        }
        else 
        {
            if (mustRealign && !tridentBehaviour.GetImpactCheck())
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                transform.position = tridentBehaviour.TridentStandCoordinates() + new Vector2(0, 2.5f);
                transform.SetParent(trident.transform);
                mustRealign = false;
            }

            rememberRotationZ = transform.eulerAngles.z;
            sineWaveX = 0;
        }
    }

    private void FixedUpdate()
    {
        if (animationCounter > 0 && tridentBehaviour.GetImpactCheck())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rememberRotationZ + sinusResult);
        }
        else
        {
            //transform.up = tridentBehaviour.GetTridentVectorDirection();
        }
    }
}
