using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class RaySelect : MonoBehaviour
{
    [SerializeField] bool rightHanded;
    [SerializeField] Material targetMat;
    [SerializeField] Material sphereMat;
    [SerializeField] Material correctMat;
    [SerializeField] GameObject targetGroup;
    private MeshRenderer current = null;
    private OVRInput.Button selectBtn;
    private Stopwatch timer = new Stopwatch();
    private List<GameObject> targets = new List<GameObject>();
    private bool btnPressed = true;
    private int prevSelect = -1;
    private TextWriter tw;
    private int samples = -1, maxSamples = 55;

    // Start is called before the first frame update
    void Start()
    {
        tw = new StreamWriter("_data/test.txt");
        tw.WriteLine("time,selection");
        if (rightHanded)
            selectBtn = OVRInput.Button.SecondaryIndexTrigger;
        else
            selectBtn = OVRInput.Button.PrimaryIndexTrigger;
        foreach (var col in targetGroup.GetComponentsInChildren<Collider>())
            targets.Add(col.gameObject);
        timer.Start();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>() != null)
            current = other.gameObject.GetComponent<MeshRenderer>();
    }

    public void OnTriggerExit(Collider other)
    {
        current = null;
    }

    public void OnApplicationQuit()
    {
        tw.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (samples >= maxSamples)
        {
            foreach (var target in targets)
                target.GetComponent<MeshRenderer>().material = targetMat;
            timer.Stop();
            return;
        }
        if (btnPressed)
        {
            if (timer.ElapsedMilliseconds > 3000)
            {
                samples++;
                // Select new target and reset timer
                foreach (var target in targets)
                    target.GetComponent<MeshRenderer>().material = sphereMat;
                int select = Random.Range(0, targets.Count);
                while (select == prevSelect && targets.Count > 1)
                    select = Random.Range(0, targets.Count);
                targets[select].GetComponent<MeshRenderer>().material = targetMat;
                prevSelect = select;
                timer.Restart();
                btnPressed = false;
            }
        }
        else if (OVRInput.Get(selectBtn))
        {
            btnPressed = true;
            // Write timer to file
            timer.Stop();
            tw.Write(timer.ElapsedMilliseconds.ToString() + ",");

            if (current == null)
            {
                // Selected no sphere
                tw.WriteLine(2);
            }
            else if (current.material.color == targetMat.color)
            {
                // Selected correct sphere
                tw.WriteLine(0);
                targets[prevSelect].GetComponent<MeshRenderer>().material = correctMat;
                current.material = correctMat;
            }
            else
            {
                // Selected wrong sphere
                tw.WriteLine(1);
            }
            timer.Restart();
        }
    }
}
