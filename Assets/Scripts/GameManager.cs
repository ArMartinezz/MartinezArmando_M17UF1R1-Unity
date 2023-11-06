using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gamePaused = false;
    public bool gameDebug = false;
    private int sceneIndex = 1;
    public static GameManager manager;
    private GameObject player;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
            // DontDestroyOnLoad(player);
        } else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ChangeScene(int scene, string axis, float position, bool gravityUp)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            DontDestroyOnLoad(player);
        }

        SceneManager.LoadScene(scene);
        player.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform;
    }
}
