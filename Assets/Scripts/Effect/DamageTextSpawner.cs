using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class DamageTextSpawner : MonoBehaviour
{
    public static DamageTextSpawner Instance;
    public GameObject damageTextPrefab; //伤害文字预制体
    public float floatUpSeepd = 0.5f; //文字上升速度
    public float lifeTime = 0.5f; //文字生存时间
    public float radius = 0.5f; //圆心范围

    private void Awake()
    {
        Instance = this;
        damageTextPrefab = Resources.Load<GameObject>("Prefabs/DamageText");
    }

    public void SpawnDamageText(float damage, Vector2 hitPoint, bool isCritical)
    {
        //1.在hit point 附近生成随机偏移
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        Vector2 spawnPos = hitPoint + randomOffset; //最终生成点

        //2.生成对象
        GameObject textObj = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);

        //3.设置数据
        textObj.GetComponent<DamageText>().SetText(damage.ToString(),
            floatUpSeepd,
            lifeTime,
            isCritical);
    }
}