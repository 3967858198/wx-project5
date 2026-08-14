using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waittow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waittow()
    {
        yield return new WaitForSeconds(2f);

        //2秒后切换到游戏主场景
        SceneManager.LoadScene(2);

        GameManager.Get().SetShootCursor();
    }

}
