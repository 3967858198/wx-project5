using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salesman : MonoBehaviour
{

    public bool isNearPlayer = false;  //玩家是否靠近 
    public GameObject Tip;


    private void Awake()
    {
        Tip = transform.Find("Tip").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            // ShopPanel
            ShopPanel.Instance.OpenShop();
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isNearPlayer = true;
            Tip.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            Tip.SetActive(false);
        }
    }
    
}
