using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameMaster gm;
    public GameObject player1Life;
    public GameObject player2Life;
    public Camera cam;
    public float camWidth;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        DrawLives();
        cam = FindObjectOfType<Camera>();
        camWidth = 2f * cam.orthographicSize * cam.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawLives()
    {
        GameObject[] lives = GameObject.FindGameObjectsWithTag("Life");

        for (int i = 0; i < lives.Length; i++)
        {
            Destroy(lives[i]);
        }

        for(int i = 0; i < gm.Player1Lives; i++)
        {
            Instantiate(player1Life, new Vector3(i - 17,6), Quaternion.identity);

        }

        for (int i = 0; i < gm.Player2Lives; i++)
        {
            Instantiate(player2Life, new Vector3(17 - i, 6), Quaternion.identity);

        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
