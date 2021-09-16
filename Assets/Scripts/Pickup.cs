using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public int respawnTime;
    public float rotationsSpeed;
    public AudioSource collectSound;
    private int maxRespawnTime;

    private float scale;
    private float growthRate;
    private bool isGrown;

    private float myY;
    private float moveRate;
    private bool movingUp;
    private float spawnHeight;

    void Start()
    {
        spawnHeight = transform.position.y;
        growthRate = 0.005f;
        moveRate = 0.0005f;
        isGrown = true;
        movingUp = false;
        respawnTime = respawnTime * 600;
        maxRespawnTime = respawnTime;
    }

    // Update is called once per frame
    void Update()
    {   
        respawnTime--;
        transform.position = new Vector3(transform.position.x, myY, transform.position.z);
        transform.Rotate(0, rotationsSpeed * Time.deltaTime, 0);

        //growth / shrinking
        transform.localScale = new Vector3(scale, scale, scale);
        if (isGrown == true){ scale = scale + growthRate;}
        if (isGrown == false){ scale = scale - growthRate;}
        //caps
        if (scale >= 0.5f){ scale = 0.5f;}
        if (scale <= 0f){ scale = 0f;}

        //moving up and down
        if (transform.position.y <= spawnHeight - 0.5) { movingUp = true; }
        if (transform.position.y >= spawnHeight + 0.5) { movingUp = false; }
        if (movingUp == true) { myY = myY + moveRate; }
        if (movingUp == false) { myY = myY - moveRate; }

        if (respawnTime <= 0f)
        {
            isGrown = true;
            GetComponent<Collider>().enabled = true;
        }
      
    }

    public void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        respawnTime = maxRespawnTime;
        isGrown = false;
        //collider is turned off until resawned to prevent collecting between respawns
        GetComponent<Collider>().enabled = false;
    }

    //Slight bug, cube always at Y=0, but then floats to disired spot and doesn't come back...... 
}
