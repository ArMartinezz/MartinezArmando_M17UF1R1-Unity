using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gamePaused = false;
    public bool gameDebug = false;
    public SoundManager soundManager;
    public static GameManager manager;
    private GameObject player;
    private SceneLoadParams nextScene;

    private void Awake()
    {
        if (manager == null)
        {
            Debug.Log(soundManager.gameObject);
            manager = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(soundManager.gameObject);
            // DontDestroyOnLoad(player);
        } else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManager.PlayMusic("Music");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (nextScene != null)
        {
            var exitObj = GameObject.FindGameObjectWithTag("Exit" + (nextScene.type == SceneLoadParams.Type.A ? "B" : "A"));
            if (exitObj != null)
            {
                Debug.Log(exitObj);
                Vector3 exitPos = exitObj.transform.position;
                player.transform.position = exitPos + new Vector3(
                    nextScene.direction == SceneLoadParams.Direction.RightHorizontal ? 1.5f :
                    nextScene.direction == SceneLoadParams.Direction.LeftHorizontal ? -1.5f : 0,
                    nextScene.direction == SceneLoadParams.Direction.VerticalTop ? 1.5f :
                    nextScene.direction == SceneLoadParams.Direction.VerticalBottom ? -1.5f : 0,
                    0);
                player.GetComponent<PlayerMovement>().ResetSpawnpoint();
            }
        }
    }

    public void ChangeScene(SceneLoadParams scene)
    {
        nextScene = scene;
        if (player == null)
        {  
            player = GameObject.FindGameObjectWithTag("Player");
            DontDestroyOnLoad(player);
        }

        SceneManager.LoadScene(nextScene.sceneName);

        if (scene.sceneName == "Victory") 
        {
            Destroy(player);
            soundManager.PlayMusic("Victory");
            soundManager.PlaySFX("Yipee");
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
