//������ �������������� ��� ������ �������
//������ ������ ���� ������� � ����� � ������ "Editor", �����, ��� ������ �����, ��������� ������

using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class OnLoad
    {
        //�������� ������ ����� �� ������ ���� ��� ������� ����� ����� � ���������
        [InitializeOnLoadMethod]
        static void MainSceneAutoLoader()
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }
    }
}
