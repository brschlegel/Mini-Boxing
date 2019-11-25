using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    public float forceMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(0, forceMultiplier / Mathf.Log(GetDistanceFromOrigin(gameObject.transform) + 6, 6 )));
    }

    float GetDistanceFromOrigin(Transform sender)
    {
        return Vector2.Distance(transform.position, sender.position);
    }

    
}
