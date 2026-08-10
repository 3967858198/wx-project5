using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.gameObject.GetComponent<CameraFollowMouse>().Shake(1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
