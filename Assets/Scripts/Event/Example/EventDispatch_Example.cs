using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatch_Example : MonoBehaviour
{    
    void Start()
    {
        StartCoroutine(IEA_SendAll());
    }
    
    
    //================================== 调用 ==================================
    IEnumerator IEA_SendAll()
    {
        yield return new WaitForSeconds(1);
        //
        EventDispatch.GetInstance().Publish(DefineProject_Editor.Event_ID.EDT_System_TestA, null);
        //
        EventExample_TestB SendB = new EventExample_TestB(10,1.1f, "this is begin",new Vector3(1,2,3));
        EventDispatch.GetInstance().Publish(DefineProject_Editor.Event_ID.EDT_System_TestB, SendB);

    }


}
