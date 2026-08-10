using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectManager : MonoBehaviour
{
    public static BulletEffectManager Instance;

    public GameObject smokePrefab; //烟雾特效 子弹撞墙
    public GameObject boomPrefab; //子弹打到敌人

    private void Awake()
    {
        Instance = this;

        boomPrefab = Resources.Load<GameObject>("Prefabs/Effect/Boom");
        smokePrefab = Resources.Load<GameObject>("Prefabs/Effect/Smoke");
        
    }

    //子弹撞墙
    public void Wall_Smoke(Vector3 t)
    {
        var go = Instantiate(smokePrefab, t, Quaternion.identity);
        Destroy(go, 0.5f);
        
    }
    
    //子弹撞墙
    public void Hit_Enemy(Vector3 t)
    {
        var go = Instantiate(boomPrefab, t, Quaternion.identity);
        Destroy(go, 0.5f);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
