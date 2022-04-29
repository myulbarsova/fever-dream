using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    int timeCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyObject(gameObject, 5);
        //Object.DestroyObject(gameObject, 5);
    }

}
