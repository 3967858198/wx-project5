using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂在开火按钮上, 运行时绑定 Weapon 单例的开火方法,
/// 避免场景中持久化绑定 Weapon 组件导致的跨场景引用断裂。
/// </summary>
public class FireButton : MonoBehaviour
{
    private void Start()
    {
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnFire);
        }
    }

    private void OnFire()
    {
        if (Weapon.Instance != null)
        {
            Weapon.Instance.OnFireButtonDown();
        }
    }
}
