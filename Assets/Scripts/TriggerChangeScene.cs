using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadParams
{
    public string sceneName;
    public enum Direction { VerticalTop, VerticalBottom, LeftHorizontal, RightHorizontal, };
    public Direction direction;
    public enum Type { A, B, };
    public Type type;

    public SceneLoadParams(string name, Direction direction, Type type) 
    {
        this.sceneName = name;
        this.direction = direction;
        this.type = type;
    }
}

public class TriggerChangeScene : MonoBehaviour
{
    public string nextScene;
    public SceneLoadParams.Direction direction;
    private GameManager gameManager;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            SceneLoadParams newScene = new SceneLoadParams(nextScene, direction, parent.tag == "ExitA" ? SceneLoadParams.Type.A : SceneLoadParams.Type.B );
            gameManager.ChangeScene(newScene);
        }

    }
}
