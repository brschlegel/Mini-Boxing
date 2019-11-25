using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlX : PowerUps
{


 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

      
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {

            Player p = collision.gameObject.GetComponent<Player>();
            p.PowerUpState = PowerUp.CtrlX;

        }

        Destroy(gameObject);

      
    }
}
