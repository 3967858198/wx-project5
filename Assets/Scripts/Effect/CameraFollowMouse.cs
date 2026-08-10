using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraFollowMouse : MonoBehaviour
{
    //主要功能****
    //镜头跟随玩家移动, 玩家在镜头中心
    //鼠标向四周偏移, 镜头也相应偏移
    //震动
    //**********


    //玩家对象
    public Transform target; //目标

    public Vector3 cameraOffset; //镜头z轴偏移-10

    //平滑移动速度
    public float smoothSpeed = 5f;

    //镜头偏移的系数
    public float maxMouseOffset = 0.3f;

    //最终的镜头偏移量
    public Vector3 mouseOffsetFinal = Vector2.zero;


    //震动
    public float shakeDuration = 0f; //震动时间
    public float shakeMagnitude = 0.2f; //震动参数
    public Vector3 shakeOffset; //震动偏移


    private void Awake()
    {
        target = GameObject.Find("Player").transform;

        cameraOffset = new Vector3(0, 0, -10);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        //求出鼠标偏移量
        FollowMouseOffset();
        //求出震动
        CameraShake();

        //镜头平滑移动
        transform.position =
            Vector3.Lerp(transform.position,
                //目标位置 + 镜头z轴偏移 + 追随鼠标偏移 + 震动偏移
                target.position + cameraOffset + mouseOffsetFinal + shakeOffset,
                Time.deltaTime * smoothSpeed);
    }

    //镜头震动
    private void CameraShake()
    {
        if (shakeDuration > 0)
        {
            shakeOffset = Random.insideUnitCircle * shakeMagnitude;

            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
        }
    }

    public void Shake(float duration = 0.2f, float magnitude = 0.2f)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }


    private void FollowMouseOffset()
    {
        //拿到鼠标相对中心的偏移

        //屏幕中心
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f);
        //拿到鼠标位置
        Vector3 mousePos = Input.mousePosition;
        //鼠标相对屏幕中心偏移量, 归一化
        Vector2 offsetMouse = new Vector2(
            (mousePos.x - screenCenter.x) / (Screen.width / 2f),
            (mousePos.y - screenCenter.y) / (Screen.height / 2f)
        );
        //限制模长
        Vector2 offsetMouseNormal = Vector2.ClampMagnitude(offsetMouse, 1f);

        //最终镜头的偏移量
        mouseOffsetFinal = offsetMouseNormal * maxMouseOffset;
    }
}