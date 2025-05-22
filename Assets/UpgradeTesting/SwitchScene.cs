using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void ChangeScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
