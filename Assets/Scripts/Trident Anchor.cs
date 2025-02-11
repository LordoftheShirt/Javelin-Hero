using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentAnchor : MonoBehaviour
{
    [SerializeField] TridentBehaviour trident = null;
    [SerializeField, Range(0f, 5f)] private float anchorHeightValue = 0.9f;
    [SerializeField, Range(0f, 5f)] private float anchorVerticalValue = 0.5f;
    private Vector2 anchorPoint;

    private void FixedUpdate()
    {
        // moving right
        if (trident.GetTridentVectorDirection().x > 0)
        {
            anchorPoint = new Vector2(-anchorVerticalValue, anchorHeightValue);
            transform.localPosition = anchorPoint;
        }
        // moving left
        if (trident.GetTridentVectorDirection().x < 0)
        {
            anchorPoint = new Vector2(anchorVerticalValue, anchorHeightValue);
            transform.localPosition = anchorPoint;
        }
    }
}
