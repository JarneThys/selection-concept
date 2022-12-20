using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    private float scaleFactor = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var scale = gameObject.transform.localScale;
        if (OVRInput.Get(OVRInput.Button.Four))
        {
            scale.x += scaleFactor;
            scale.y += scaleFactor;
        }
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            scale.x -= scaleFactor;
            scale.y -= scaleFactor;
        }
        gameObject.transform.localScale = scale;
    }
}
