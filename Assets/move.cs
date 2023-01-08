using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Vector3 maxBounds = new Vector3(8, 8, 30),
                    minBounds = new Vector3(-8, 1.2f, 5);
    private float speed, maxSpeed = 0.03f, minScale = 0.2f, maxScale = 0.8f;
    private int offset = 5;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.position.z < 5)
        {
            speed = 0;
        }
        else
        {
            setRandom();
        }
    }

    private void setRandom()
    {
        speed = Random.Range(0.005f, maxSpeed);
        float scale = Random.Range(minScale, maxScale);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        gameObject.transform.position = new Vector3(Random.Range(minBounds.x + offset, maxBounds.x - offset),
                                                    Random.Range(minBounds.y + offset, maxBounds.y - offset),
                                                    Random.Range(minBounds.z + offset, maxBounds.z - offset));
        gameObject.transform.Rotate(new Vector3(Random.Range(0f, 360), Random.Range(0f, 360), Random.Range(0f, 360)));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.z < 5)
            return;

        gameObject.transform.localPosition += gameObject.transform.forward * speed;
        var pos = gameObject.transform.localPosition;
        if (pos.x < minBounds.x || pos.x > maxBounds.x || pos.y < minBounds.y || pos.y > maxBounds.y || pos.z < minBounds.z || pos.z > maxBounds.z)
            setRandom();
    }
}
