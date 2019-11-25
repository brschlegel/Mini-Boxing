using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //zone
    private List<GameObject> platforms;
    private float xZone;
    private float yZone;
    private List<GameObject> zones;
    private List<float> zoneAreaPercentage;
    private GameObject chosenZone;
    public GameObject floorPrefab;


    //players
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Player player1;
    public Player player2;
    public GameObject deathPoint;
    public int player1Lives;
    public int player2Lives;

    //powerups
    private float ctrlXRadius;

    //UI
    private GameLogic gl;



    //Properties
    public int Player1Lives
    {
        get { return player1Lives; }
        set { player1Lives = value; }
    }

    public int Player2Lives
    {
        get { return player2Lives; }
        set { player2Lives = value; }
    }

    void Start()
    {
        xZone = 1f;
        yZone = 3f;

        platforms = new List<GameObject>();
        zones = new List<GameObject>();
        zoneAreaPercentage = new List<float>();

        CreateZones();
        deathPoint = GameObject.Find("Death Point");
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        InvokeRepeating("CheckForDeaths", 0f, 0.5f);

        ctrlXRadius = 1f;

        gl = GetComponent<GameLogic>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckForDeaths()
    {


        if (player1.transform.position.y < deathPoint.transform.position.y)
        {
            player1 = null;
            GameObject go = (GameObject)Instantiate(player1Prefab, RandomPointInZones(), Quaternion.identity);
            player1 = go.GetComponent<Player>();
            player1Lives--;
            gl.DrawLives();


        }

        if (player2.transform.position.y < deathPoint.transform.position.y)
        {
            player2 = null;
            GameObject go = (GameObject)Instantiate(player2Prefab, RandomPointInZones(), Quaternion.identity);
            player2 = go.GetComponent<Player>();
            player2Lives--;
            gl.DrawLives();
            
        }


    }

    //Zones

    void CreateZones()
    {
        ClearZones();

        platforms.AddRange(GameObject.FindGameObjectsWithTag("Platform"));
        foreach (GameObject p in platforms)
        {
            GameObject zone = new GameObject("Zone");
            //setting the parent as the platform allows for local coordinates to be used
            zone.transform.SetParent(p.transform);
            zone.transform.localPosition = new Vector2(0, yZone);
            zone.transform.localScale = new Vector2(xZone, yZone);
            //used for debugging purposes
            zone.AddComponent<SpriteRenderer>();
            zones.Add(zone);
            zoneAreaPercentage.Add(FindArea(zone));


        }
        GetNormalizedList(zoneAreaPercentage);


    }

    void ClearZones()
    {
        for (int i = 0; i < zones.Count; i++)
        {
            Destroy(zones[i]);
        }

        zones.Clear();
        platforms.Clear();
        zoneAreaPercentage.Clear();

    }

    public Vector2 RandomPointInZones()
    {

        chosenZone = GetRandomZone();

        Vector2 topLeft = new Vector2(chosenZone.transform.position.x - chosenZone.transform.lossyScale.x / 2,
            chosenZone.transform.position.y + chosenZone.transform.lossyScale.y / 2);

        Vector2 bottomRight = new Vector2(chosenZone.transform.position.x + chosenZone.transform.lossyScale.x / 2,
            chosenZone.transform.position.y - chosenZone.transform.lossyScale.y / 2);

        Vector2 finalVect2 = new Vector2(Random.Range(topLeft.x, bottomRight.x), Random.Range(bottomRight.y, topLeft.y));

        return finalVect2;
    }

    //This method chooses a zone based on the relative area of that zone, using the normalized areas as a probability spread
    GameObject GetRandomZone()
    {
        float rand = Random.value;
        float cumulative = 0;

        for (int i = 0; i < zoneAreaPercentage.Count; i++)
        {
            cumulative += zoneAreaPercentage[i];
            if (rand <= cumulative)
            {
                return zones[i];
            }

        }
        return zones[0];
    }

    float FindArea(GameObject g)
    {
        Transform platformScale = g.GetComponentInParent<Transform>();

        return Mathf.Abs(platformScale.position.x * platformScale.position.y);
    }

    void GetNormalizedList(List<float> wacky)
    {
        float max = FindSum(wacky);
        for (int i = 0; i < wacky.Count; i++)
        {
            zoneAreaPercentage[i] = wacky[i] * 1 / max;
        }

    }

    float FindSum(List<float> numbers)
    {
        float total = 0;
        foreach (float n in numbers)
        {
            total += n;
        }

        return total;
    }

    //power up methods

    //sender is the transform of the player who used the powerupweqw
    public void SplitPlatform(GameObject platform, Transform sender)
    {
        StartCoroutine(SplitPlat(platform, sender));


    }

    IEnumerator SplitPlat(GameObject platform, Transform sender)
    {

        float leftSide = platform.transform.position.x - platform.transform.localScale.x / 2;
        float rightSide = platform.transform.position.x + platform.transform.localScale.x / 2;


        //making sure the player is actually on the platform
        if (sender.position.x > leftSide && sender.position.x < rightSide)
        {
            GameObject leftPlatform = Instantiate(floorPrefab, new Vector3((sender.position.x - ctrlXRadius + leftSide) / 2, platform.transform.position.y), Quaternion.identity);
            leftPlatform.transform.localScale = new Vector3(leftPlatform.transform.localScale.x * sender.position.x - ctrlXRadius - leftSide, platform.transform.localScale.y, 0);



            GameObject rightPlatform = Instantiate(floorPrefab, new Vector3((sender.position.x + ctrlXRadius + rightSide) / 2, platform.transform.position.y), Quaternion.identity);
            rightPlatform.transform.localScale = new Vector3(rightPlatform.transform.localScale.x * sender.position.x + ctrlXRadius - rightSide, platform.transform.localScale.y, 0);



            yield return new WaitForSeconds(1);
            DestroyImmediate(platform);

            CreateZones();
        }



    }

    public void AddPlatform(Transform sender)
    {
        Debug.Log("Platform added!");
        GameObject newPlat = Instantiate(floorPrefab, new Vector3(sender.position.x, sender.position.y - 1), Quaternion.identity);
        newPlat.transform.localScale = new Vector3(newPlat.transform.localScale.x * 2 * ctrlXRadius, newPlat.transform.localScale.y, 0f);
    }

}

    
