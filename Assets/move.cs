using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Vector3 maxBounds = new Vector3(5, 3, 10),
                    minBounds = new Vector3(-5, 1.5f, 5);
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(new Vector3(Random.Range(0f, 180f), Random.Range(0f, 180f), Random.Range(0f, 180f)));
        speed = Random.Range(0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition += gameObject.transform.forward * speed;
        var pos = gameObject.transform.localPosition;
        if (pos.x < minBounds.x || pos.x > maxBounds.x ||
            pos.y < minBounds.y || pos.y > maxBounds.y ||
            pos.z < minBounds.z || pos.z > maxBounds.z)
        {
            speed = Random.Range(0f, 0.1f) * -1 * (this) => {speed > 0};
        }
    }
}
