using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findEnclosedTargets : MonoBehaviour
{
    [SerializeField] GameObject targetGroup;
    List<Collider> targetColliders = new List<Collider>();
    List<Material> inBox = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var collider in targetGroup.GetComponentsInChildren<Collider>())
        {
            targetColliders.Add(collider);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (targetColliders.Contains(other))
        {
            inBox.Add(other.GetComponent<MeshRenderer>().material);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (targetColliders.Contains(other))
        {
            inBox.Remove(other.GetComponent<MeshRenderer>().material);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            foreach (var mat in inBox)
                Debug.Log(mat);
        }
    }
}
