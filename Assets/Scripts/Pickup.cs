using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public int respawnTime;
    public float rotationsSpeed;
    private int maxRespawnTime;

    private Color myColor;
    private Renderer myRenderer;
    private float transparency;
    private bool isSolid;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        respawnTime = respawnTime * 600;
        maxRespawnTime = respawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isSolid);
        Debug.Log(transparency);
        respawnTime--;
        transform.Rotate(0, rotationsSpeed * Time.deltaTime, 0);
        
       
        this.transform.localScale = new Vector3(transparency, transparency, transparency);
        if (isSolid == true)
        {
            transparency = transparency - 0.5f;
        }
        if (isSolid == false)
        {
            transparency = transparency + 0.5f;
        }

        if (transparency >= 1f)
        {
            transparency = 1f;
        }
        if (transparency <= 0f)
        {
            transparency = 0f;
        }

        if (respawnTime <= 0f)
        {
            isSolid = true;
            GetComponent<Collider>().enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        respawnTime = maxRespawnTime;
        isSolid = false;
        GetComponent<Collider>().enabled = false;
    }
}
