using System;
using System.Collections;
using UnityEngine;

public class FlickFX : MonoBehaviour
{
    private float flashDuration = 1f; // 闪烁总时长
    private int flashCount = 2; //闪烁2次
    private float minAlpha = 0; //最小透明度
    private bool affectChildren = true; //是否会影响子物体

    private SpriteRenderer[] _spriteRenderers; //存放图片的数组
    public Coroutine flashRoutine;
    public bool isFlashing = false; //当前是否正在闪烁

    private void Awake()
    {
        _spriteRenderers = affectChildren ? 
            GetComponentsInChildren<SpriteRenderer>()
            : new SpriteRenderer[] { GetComponent<SpriteRenderer>() };
    }

    //触发闪烁
    public void TriggerFlash()
    {
        if (isFlashing)
        {
            return;
            
        }

        flashRoutine = StartCoroutine(FlashCoroutine());

    }
    
    
    
    IEnumerator  FlashCoroutine()
    {
        isFlashing = true; //标记特效开始

        float halfFlashTime = flashDuration / (flashCount * 2);
        
        // 不透明-> 透明, 透明->不透明
        // 不透明-> 透明, 透明->不透明

        for (int i = 0; i < flashCount; i++)
        {
            yield return StartCoroutine(FadeAlpha(minAlpha, halfFlashTime));
            yield return StartCoroutine(FadeAlpha(1f, halfFlashTime));
        }


        isFlashing = false;
    }

    IEnumerator FadeAlpha(float targetAlpha, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / duration);
            
            //遍历数组
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                //拿到结构体颜色
                Color newColor =  _spriteRenderers[i].color;
                //修改a
                newColor.a = 
                    Mathf.Lerp(_spriteRenderers[i].color.a, targetAlpha, t);
                //赋值回去
                _spriteRenderers[i].color = newColor;
                

            }
            
            
            yield return null;
        }
        
    }
}