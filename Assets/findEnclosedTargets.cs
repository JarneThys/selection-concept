using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findEnclosedTargets : MonoBehaviour
{
    [SerializeField] GameObject targetGroup, selectPlane;
    [SerializeField] bool RightHanded;
    private OVRInput.Button selectBtn;
    private List<Collider> targetColliders = new List<Collider>();
    private List<GameObject> inBox = new List<GameObject>();
    private bool btnReleased = true;
    // Start is called before the first frame update
    void Start()
    {
        if (RightHanded)
            selectBtn = OVRInput.Button.PrimaryIndexTrigger;
        else
            selectBtn = OVRInput.Button.SecondaryIndexTrigger;
        selectPlane.SetActive(false);
        foreach (var collider in targetGroup.GetComponentsInChildren<Collider>())
        {
            targetColliders.Add(collider);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (targetColliders.Contains(other))
        {
            inBox.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (targetColliders.Contains(other))
        {
            inBox.Remove(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(selectBtn))
        {
            if (btnReleased)
            {
                btnReleased = false;
                selectPlane.SetActive(true);
                float offX = -0.35f, offY = 0.4f;
                for (int i = 0; i < inBox.Count; i++)
                {
                    var copy = Instantiate(inBox[i]);
                    copy.tag = inBox[i].tag;
                    copy.transform.parent = selectPlane.transform;
                    copy.transform.position = selectPlane.transform.position + selectPlane.transform.up * 0.2f + selectPlane.transform.right * offX + selectPlane.transform.forward * offY;
                    copy.transform.localScale = new Vector3(1f, 1f, 1f);
                    offX += 0.15f;
                    if (offX > 0.35f)
                    {
                        // row finished
                        offX = -0.35f;
                        offY -= 15f;
                    }
                }
            }
        }
        else
        {
            btnReleased = true;
            selectPlane.SetActive(false);
            for (int i = 0; i < selectPlane.transform.childCount; i++)
            {
                Destroy(selectPlane.transform.GetChild(i).gameObject);
            }
        }
    }
}
