using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roughselect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform handTransform;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
        }
        else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        {
        }
        gameObject.transform.position = handTransform.position;
    }
}
