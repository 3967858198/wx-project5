using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMesh text;

    public float floatUpSpeed; //上升的速度
    public float lifeTime; //生命
    private float timer; //定时器
    private Color redColor = new Color(1, 0, 0);


    private void Awake()
    {
        if (text == null)
        {
            text = GetComponent<TextMesh>();
        }
      
    }
    
    //设置文字
    public void SetText(string value, float floatUpSpeed, float lifeTime, bool isCritical)
    {
        this.floatUpSpeed = floatUpSpeed;
        this.lifeTime = lifeTime;
    
        //防止为空
        if (text == null)
        {
            text.GetComponent<TextMesh>();
        }
        text.text = value; //设置文件

        //如果暴击
        if (isCritical)
        {
            Vector3 ls = transform.localScale; //获取原始缩放
            transform.localScale = ls * 1.3f; //扩大文字
            text.color = redColor; //更改为红色


        }
        
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //向上浮动
        transform.position += Vector3.up * floatUpSpeed * Time.deltaTime; 
        
        //生命存活
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }

    }
}
