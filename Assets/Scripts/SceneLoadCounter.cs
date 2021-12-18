using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadCounter : MonoBehaviour
{
    public static int SceneLoadCount { get; private set; }
    private static SceneLoadCounter instance;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
        SceneLoadCount = 0;
        SceneManager.sceneLoaded += IncrementSceneLoad;
    }

    public void IncrementSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // If there is some scene you want to skip, just return here
        SceneLoadCount++;
    }

    public int getCount()
    {
        return SceneLoadCount;
    }
}
