//Скрипт отрабатывающий при старте проекта
//Скрипт должен быть помещен в папку с именем "Editor", иначе, при сборке билда, возникнет ошибка

using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class OnLoad
    {
        //Загрузка первой сцены из списка сцен при запуске любой сцены в редакторе
        [InitializeOnLoadMethod]
        static void MainSceneAutoLoader()
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }
    }
}
