#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 把 Game.unity 中的开火按钮(名为 Button 的黄色 Fire 按钮)复制到 Game_1/Game_2 场景的 Canvas 下。
/// 通过菜单 Tools/Copy Fire Button To All Scenes 或 SceneAutoReload 的 CMD:COPY_FIRE_BUTTON 命令触发。
/// </summary>
public static class FireButtonCopier
{
    private const string SourceScene = "Assets/Scenes/Game.unity";
    private static readonly string[] TargetScenes =
    {
        "Assets/Scenes/Game_1.unity",
        "Assets/Scenes/Game_2.unity"
    };

    [MenuItem("Tools/Copy Fire Button To All Scenes")]
    public static void CopyToAllScenes()
    {
        // 0. 先保存当前场景, 避免切换场景时弹出保存对话框
        SaveActiveSceneIfDirty();

        // 1. 打开源场景, 找到开火按钮模板
        EditorSceneManager.OpenScene(SourceScene, OpenSceneMode.Single);
        GameObject srcBtn = FindFireButton();
        if (srcBtn == null)
        {
            Debug.LogError("[FireButtonCopier] 在 Game.unity 中找不到开火按钮(Canvas/Button)");
            return;
        }

        // 2. 复制模板并移除持久化 onClick(它引用源场景的 Weapon 组件)
        GameObject copy = Object.Instantiate(srcBtn);
        copy.name = "FireButton";
        var buttonComp = copy.GetComponent<Button>();
        if (buttonComp != null)
        {
            var so = new SerializedObject(buttonComp);
            var onClickProp = so.FindProperty("m_OnClick");
            onClickProp.ClearArray();
            so.ApplyModifiedPropertiesWithoutUndo();
        }
        // 替换运行时绑定脚本(绑定 Weapon.Instance)
        if (copy.GetComponent<FireButton>() == null)
        {
            copy.AddComponent<FireButton>();
        }

        string prefabPath = "Assets/Resources/Prefabs/FireButton.prefab";
        System.IO.Directory.CreateDirectory("Assets/Resources/Prefabs");
        PrefabUtility.SaveAsPrefabAsset(copy, prefabPath);
        Object.DestroyImmediate(copy);
        AssetDatabase.SaveAssets();

        // 3. 对每个目标场景实例化到 Canvas 下
        var prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (prefabAsset == null)
        {
            Debug.LogError("[FireButtonCopier] 加载 FireButton.prefab 失败");
            return;
        }

        foreach (var scenePath in TargetScenes)
        {
            SaveActiveSceneIfDirty();
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            var canvas = GameObject.Find("Canvas");
            if (canvas == null)
            {
                Debug.LogError($"[FireButtonCopier] {scenePath} 中找不到 Canvas");
                continue;
            }
            if (canvas.transform.Find("Button") != null || canvas.transform.Find("FireButton") != null)
            {
                Debug.Log($"[FireButtonCopier] {scenePath} 已有开火按钮, 跳过");
                continue;
            }

            var inst = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
            if (inst == null)
            {
                Debug.LogError($"[FireButtonCopier] {scenePath} 实例化 FireButton 失败");
                continue;
            }
            inst.transform.SetParent(canvas.transform, false);
            var rt = inst.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = new Vector2(818, -274);
                rt.sizeDelta = new Vector2(160, 160);
            }

            bool saved = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            Debug.Log($"[FireButtonCopier] 已把开火按钮添加到 {scenePath} (saved={saved}, child={inst.name})");
        }

        // 4. 回到用户当前场景
        SaveActiveSceneIfDirty();
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity", OpenSceneMode.Single);
        AssetDatabase.SaveAssets();
        Debug.Log("[FireButtonCopier] 完成");
    }

    private static void SaveActiveSceneIfDirty()
    {
        var scene = EditorSceneManager.GetActiveScene();
        if (scene.isDirty && !string.IsNullOrEmpty(scene.path))
        {
            EditorSceneManager.SaveScene(scene);
        }
    }

    private static GameObject FindFireButton()
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas == null) return null;
        var btn = canvas.transform.Find("Button");
        return btn != null ? btn.gameObject : null;
    }
}
#endif
