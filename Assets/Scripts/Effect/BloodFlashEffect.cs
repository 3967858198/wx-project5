using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodFlashEffect : MonoBehaviour
{
    public float fadeSpeed = 2f; // 颜色渐变的速度

    public bool isFlashing = false; //是否开启特效
    public Image flashImage; //血图片
    public Color originColor = new Color(1, 0, 0, 0.6f);

    private void Awake()
    {
        flashImage = GetComponent<Image>();
        flashImage.color = Color.clear;  //隐藏掉
    }

    // Start is called before the first frame update
    void Start()
    {
        // Flash();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            //颜色慢慢变淡
            flashImage.color = Color.Lerp(flashImage.color, Color.clear, fadeSpeed * Time.deltaTime);
            
            //受伤完全结束
            if (flashImage.color.a <= 0.01f)
            {
                isFlashing = false; 
                flashImage.color = Color.clear;
            }
        }
    }

    //启动特效
    public void Flash()
    {
        isFlashing = true;
        flashImage.color = originColor;

    }
    
    
}
