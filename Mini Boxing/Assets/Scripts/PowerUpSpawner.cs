using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public List<float> distribution = new List<float>();
    public List<GameObject> powerUps = new List<GameObject>();
    public GameObject ctrlX;
    public GameObject trampoline;
   
    public GameMaster gm;

    public float timeBetweenSpawns;
    
    // Start is called before the first frame update
    void Start()
    {

        

        powerUps.Add(ctrlX);
        powerUps.Add(trampoline);
        distribution.Add(.5f);
        distribution.Add(.5f);
        InvokeRepeating("SpawnPowerUp", 2f, timeBetweenSpawns);
        
    }

   

    // Update is called once per frame
    void Update()
    {
        

    }

    void SpawnPowerUp()
    {
        GameObject powerUpGO = GetRandomPU();
        PowerUps toBeSpawned = powerUpGO.GetComponent<PowerUps>();
        toBeSpawned.Spawn(gm.RandomPointInZones(), toBeSpawned.gameObject);
    }

    GameObject GetRandomPU()
    {
      
        float rand = Random.value;
        float cumulative = 0;

        for (int i = 0; i < distribution.Count; i++)
        {
            cumulative += distribution[i];
            if (rand <= cumulative)
            {
                return powerUps[i];
            }

        }
        return powerUps[0];
    }
        
}
