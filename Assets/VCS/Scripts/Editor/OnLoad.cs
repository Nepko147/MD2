//������ �������������� ��� ������ �������
//������ ������ ���� ������� � ����� � ������ "Editor", �����, ��� ������ �����, ��������� ������

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    public class OnLoad : MonoBehaviour
    {
        //�������� ������ ����� �� ������ ���� ��� ������� ����� ����� � ���������
        [InitializeOnLoadMethod]
        static void MainSceneAutoLoader()
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }
    }
}
