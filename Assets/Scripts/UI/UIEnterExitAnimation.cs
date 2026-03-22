using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;

public class UIEnterExitAnimation : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [Header("Background")]
    [SerializeField] private Gradient _backgroundGradient;
    [SerializeField] private UIAnimation _uiAnimation;
    
    private Vector2 _target;
    
    public void AnimatedEnter()
    {
        if (_uiAnimation != null) _uiAnimation.backgroundGradient = _backgroundGradient;
        StartCoroutine(SmoothMove(Vector2.zero, new Vector2((float)Screen.height * direction.x * 2, (float)Screen.width * direction.y * 2), speed));
    }
    IEnumerator SmoothMove(Vector2 target , Vector2 startingPosition, float duration, bool disable = false)
    {
        
        float time = 0;
        if (disable)
        {
            while (time < duration)
            {
                transform.localPosition = Vector2.Lerp(startingPosition, target, time / duration);
                time += Time.deltaTime;
                yield return new WaitForNextFrameUnit();
            }

            this.GameObject().SetActive(false);
        }
        else
        {
            while (time < duration)
            {
                transform.localPosition = Vector2.Lerp(startingPosition, target, 1 - (Mathf.Pow(time / duration -1,5) *-1)  ); 
                time += Time.deltaTime;
                yield return new WaitForNextFrameUnit();
            }
        }
    }
    // Update is called once per frame
    public void AnimatedDisable()
    {
        StartCoroutine(SmoothMove(new Vector2((float)Screen.height * direction.x * 2, (float)Screen.width * direction.y * 2),Vector2.zero, speed, true));
    }
}
