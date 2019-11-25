using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameMaster gm;
 
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void Spawn(Vector2 spawnPoint, GameObject prefab)
    {
       Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}
