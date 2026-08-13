using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance; 

    public bool currentFaceRight = true; //当前武器的朝向
    public PlayerMovement _pm; //玩家的移动脚本
    
    
     
    public float fireRate = 0.2f; //开枪间隔
    public float lastFireTime; //最后一次开火的时间

    public bool isReloading = false; //是否正在换弹
    public float reloadTime = 1f; //装弹时间 1秒

    public int currentAmmo; //当前子弹数量
    public int clipSize = 10; //弹夹容量

    
    
    public Transform firePoint; //开枪位置
    public GameObject bulletPrefab; //子弹预制体

    public float recoilDistance = 0.05f; //后坐力距离    
    public float recoildRecoverTime = 0.1f ; //后坐力恢复的时间
    
    
    public float minDamage = 20f; //枪的最小伤害
    public float maxDamage = 30f; //枪的最大伤害
    public float critChance = 0.3f; //暴击率
    public float critMultiplier = 2f; //暴击倍率
    
    

    public GameObject _fireEffect; //开火特效
    public GameObject _reloadBullet; //换弹特效
    
    
    private void Awake()
    {
        Instance = this;
        
        _pm = GameObject.Find("Player").GetComponent<PlayerMovement>();

        firePoint = GameObject.Find("FirePoint").transform;
        bulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        
        _fireEffect = GameObject.Find("FireEffect");
        _fireEffect.SetActive(false);
        
        _reloadBullet = GameObject.Find("ReloadBullet");
        _reloadBullet.SetActive(false);
        
        
        currentAmmo = clipSize; //初始化子弹
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //鼠标的世界坐标
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //求出瞄准方向
        //Vector2 aimDirection = mouseWorldPos - transform.position;
        Vector2 aimDirection = _pm.lastMoveDir;

        //通过三角函数, 把矢量转为角度
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //自身旋转
        transform.rotation = Quaternion.Euler(0, 0 , angle);
        
        
        //修正武器的左右挂载
        if (_pm.currentFaceRight ^ currentFaceRight)
        {
            //水平翻转武器的挂载点
            Vector3 parentPos = transform.parent.localPosition;
            parentPos.x = -parentPos.x;
            transform.parent.localPosition = parentPos; 
            
            //同步武器的朝向变量
            currentFaceRight = _pm.currentFaceRight;
            
            //修正武器在y轴上的翻转
            var scale = transform.localScale;
            scale.y = Mathf.Abs(scale.y) *  (currentFaceRight ? 1 : -1);
            transform.localScale = scale;
        }
        
        //按R手动换弹
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryReload();
        }
        
        //PC键盘/鼠标开火(不依赖UI按钮, 移动端仍用Fire按钮)
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            OnFireButtonDown();
        }
    }

    //开火按钮按下
    public void OnFireButtonDown()
    {
        TryFire();
    }
    
    //尝试开火
    private void TryFire()
    {
        //正在装弹
        if (isReloading)
        {
            return;
        }
        
        //超出射速
        if ((Time.time - lastFireTime) < fireRate )
        {
            return;
        }
        
        //没有子弹
        if (currentAmmo <= 0)
        {
            // 自动装弹
            StartCoroutine(ReloadBullet());
            
            return;
        }
        
        //以上3个条件都不满足, 继续执行
        
        //真正的开火
        RealFire();

    }
    
    //手动换弹(按R触发), 弹夹不满时才执行
    public void TryReload()
    {
        //正在换弹或子弹已满, 不重复换弹
        if (isReloading || currentAmmo >= clipSize)
        {
            return;
        }
        
        StartCoroutine(ReloadBullet());
    }

  
    //实际开火
    private void RealFire()
    {
        //记录最后一次开火时间
        lastFireTime = Time.time; 
        
        //计算伤害
        float baseDamage = Random.Range(minDamage, maxDamage);
        //判断是否暴击
        bool isCritical = false;
        if (Random.value < critChance)
        {
            isCritical = true;
            baseDamage = Mathf.RoundToInt(baseDamage * critMultiplier);
        }
        else
        {
            isCritical = false;
            baseDamage = Mathf.RoundToInt(baseDamage);
        }
        
        //创建子弹
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().Init(firePoint.transform.right, baseDamage , isCritical);

        //枪口火焰
        StartCoroutine(FireEffect());        

        //窗口抖动
        Camera.main.gameObject.GetComponent<CameraFollowMouse>().Shake();
        
        //子弹减少
        currentAmmo--;
        //更新子弹UI
        HudPanel.Instance.UpdateBulletUI(currentAmmo, clipSize, true);

        Vector2 dir = firePoint.transform.right;
        //枪的后坐力
        StartCoroutine(ApplyRecoil(dir));

        //射击音效
        //播放音效
        MusicManager.Instance.CreateMusic("playerAttack");


    }
    
    //应用后坐力
    IEnumerator ApplyRecoil(Vector2 dir)
    {
       //保存原始位置
       var originalLocalPos = transform.localPosition; 
       //加上后坐力
       transform.localPosition = originalLocalPos + 
                                 (Vector3)(-dir * recoilDistance );
       float t = 0f; //计时器
       while ( t < recoildRecoverTime )
       {
           t += Time.deltaTime;
           transform.localPosition = Vector3.Lerp(transform.localPosition, 
               originalLocalPos,
               t/recoildRecoverTime
               );
           yield return null;
       }

       transform.localPosition = originalLocalPos;



    }

    //开火特效
    IEnumerator FireEffect()
    {
        //展示火焰
        _fireEffect.SetActive(true);
        //等待0.1秒
        yield return new WaitForSeconds(0.1f); 
        //关闭火焰
        _fireEffect.SetActive(false);
    }

    //装弹
    IEnumerator ReloadBullet()
    {
        //标记开始装弹
        isReloading = true;

        //激活对象
        _reloadBullet.SetActive(true);
        
        //播放音效
        MusicManager.Instance.CreateMusic("playerReload");

        //等待换弹时间
        yield return new WaitForSeconds(reloadTime);
        
        //增加子弹数量
        currentAmmo = clipSize; 
        
        //更新子弹UI
        HudPanel.Instance.UpdateBulletUI(currentAmmo, clipSize);
        
        
        
        
        //标记 不是在换弹中
        isReloading = false;
        
        //关闭换弹特效
        _reloadBullet.SetActive(false);


    }
    
    
}