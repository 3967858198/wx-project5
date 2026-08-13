#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// 监听 .codely-cli/scene_reload_trigger 文件内容变化，
/// 变化时刷新资源并重新加载当前场景，方便外部修改后立即在编辑器中预览。
/// 场景有未保存修改时跳过，避免覆盖编辑器中的改动。
/// </summary>
[InitializeOnLoad]
public static class SceneAutoReload
{
    private const string TriggerRelativePath = ".codely-cli/scene_reload_trigger";
    private static readonly string ProjectRoot =
        Directory.GetParent(Application.dataPath).FullName;
    private static readonly string TriggerPath =
        Path.Combine(ProjectRoot, TriggerRelativePath);
    private static readonly string DiagPath =
        Path.Combine(ProjectRoot, ".codely-cli/scene_reload_diag.log");
    private static string _lastContent;

    static SceneAutoReload()
    {
        File.AppendAllText(DiagPath, $"[{System.DateTime.Now:HH:mm:ss}] script loaded, trigger={TriggerPath}\n");
        if (File.Exists(TriggerPath))
        {
            _lastContent = File.ReadAllText(TriggerPath);
        }
        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
            return;

        if (!File.Exists(TriggerPath))
            return;

        string content = File.ReadAllText(TriggerPath);
        if (content == _lastContent)
            return;

        _lastContent = content;
        EditorApplication.delayCall += Reload;
    }

    private static void Reload()
    {
        // 刷新资源，让新导入/修改的图片、场景等生效
        AssetDatabase.Refresh();

        // 特殊命令: CMD:xxx 触发指定编辑器操作
        if (_lastContent.StartsWith("CMD:"))
        {
            ExecuteCommand(_lastContent.Substring(4).Trim());
            return;
        }

        var scene = EditorSceneManager.GetActiveScene();
        if (scene.isDirty)
        {
            File.AppendAllText(DiagPath, $"[{System.DateTime.Now:HH:mm:ss}] skipped (scene dirty)\n");
            return;
        }

        if (scene.IsValid() && scene.isLoaded && !string.IsNullOrEmpty(scene.path))
        {
            EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Single);
            File.AppendAllText(DiagPath, $"[{System.DateTime.Now:HH:mm:ss}] reloaded {scene.path}\n");
        }
    }

    private static void ExecuteCommand(string cmd)
    {
        switch (cmd)
        {
            case "COPY_FIRE_BUTTON":
                FireButtonCopier.CopyToAllScenes();
                break;
            case "LIST_CANVAS_CHILDREN":
                ListCanvasChildren();
                break;
            case "LIST_IMAGES":
                ListImages();
                break;
            case "HIDE_JOYSTICK":
                HideJoystick();
                break;
            default:
                File.AppendAllText(DiagPath, $"[{System.DateTime.Now:HH:mm:ss}] unknown cmd: {cmd}\n");
                break;
        }
    }

    private static void HideJoystick()
    {
        foreach (var scenePath in new[] { "Assets/Scenes/Game.unity", "Assets/Scenes/Game_1.unity", "Assets/Scenes/Game_2.unity" })
        {
            var cur = EditorSceneManager.GetActiveScene();
            if (cur.isDirty && !string.IsNullOrEmpty(cur.path))
                EditorSceneManager.SaveScene(cur);

            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            var joystick = GameObject.Find("JoystickRoot");
            if (joystick == null)
            {
                File.AppendAllText(DiagPath, $"[{scenePath}] 无 JoystickRoot\n");
                continue;
            }

            var cg = joystick.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = joystick.AddComponent<CanvasGroup>();
            }
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;

            bool saved = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            File.AppendAllText(DiagPath, $"[{scenePath}] JoystickRoot 已隐藏 (saved={saved})\n");
        }

        var active = EditorSceneManager.GetActiveScene();
        if (active.isDirty && !string.IsNullOrEmpty(active.path))
            EditorSceneManager.SaveScene(active);
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity", OpenSceneMode.Single);
    }

    private static void ListImages()
    {
        foreach (var scenePath in new[] { "Assets/Scenes/Game.unity", "Assets/Scenes/Game_1.unity", "Assets/Scenes/Game_2.unity" })
        {
            var cur = EditorSceneManager.GetActiveScene();
            if (cur.isDirty && !string.IsNullOrEmpty(cur.path))
                EditorSceneManager.SaveScene(cur);

            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            var canvas = GameObject.Find("Canvas");
            if (canvas == null)
            {
                File.AppendAllText(DiagPath, $"[{scenePath}] 无 Canvas\n");
                continue;
            }
            foreach (var img in canvas.GetComponentsInChildren<UnityEngine.UI.Image>(true))
            {
                string spriteName = img.sprite != null ? img.sprite.name : "NONE";
                string parent = img.transform.parent != null ? img.transform.parent.name : "root";
                File.AppendAllText(DiagPath,
                    $"[{scenePath}] {img.transform.name} (parent={parent}) alpha={img.color.a} sprite={spriteName} size={img.rectTransform.sizeDelta}\n");
            }
        }

        var active = EditorSceneManager.GetActiveScene();
        if (active.isDirty && !string.IsNullOrEmpty(active.path))
            EditorSceneManager.SaveScene(active);
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity", OpenSceneMode.Single);
    }

    private static void ListCanvasChildren()
    {
        foreach (var scenePath in new[] { "Assets/Scenes/Game_1.unity", "Assets/Scenes/Game_2.unity" })
        {
            var cur = EditorSceneManager.GetActiveScene();
            if (cur.isDirty && !string.IsNullOrEmpty(cur.path))
                EditorSceneManager.SaveScene(cur);

            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            var canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                var names = string.Join(",", System.Linq.Enumerable.Select(canvas.transform.Cast<Transform>(), t => t.name));
                File.AppendAllText(DiagPath, $"[{scenePath}] {names}\n");
            }
            else
            {
                File.AppendAllText(DiagPath, $"[{scenePath}] 无 Canvas\n");
            }
        }

        var active = EditorSceneManager.GetActiveScene();
        if (active.isDirty && !string.IsNullOrEmpty(active.path))
            EditorSceneManager.SaveScene(active);
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity", OpenSceneMode.Single);
    }
}
#endif
