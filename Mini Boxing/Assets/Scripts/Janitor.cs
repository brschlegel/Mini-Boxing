using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor : MonoBehaviour
{
    private GameObject deathPoint;
    public List<GameObject> players;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();
        deathPoint = GameObject.Find("Death Point");
        InvokeRepeating("CleanUp", 0f, 5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CleanUp()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player1"));
        players.AddRange(GameObject.FindGameObjectsWithTag("Player2"));

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].transform.position.y < deathPoint.transform.position.y)
            {
                Destroy(players[i]);
            }
        }

        players.Clear();

    }

}
