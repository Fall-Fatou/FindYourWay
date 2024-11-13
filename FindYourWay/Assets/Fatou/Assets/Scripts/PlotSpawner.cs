using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    private int initAmount = 100;
    //private initStartZ = 15f;
    private float plotSize = 20f;
    private float xPosLeft = -39f;
    private float xPosRight = 39f;
    private float lastZPos = 15f;

    public List<GameObject> plots;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnPlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlot()
    {
        int rand = Random.Range(0, plots.Count);
        GameObject plotLeft = plots[rand];
        GameObject plotRight = plots[rand];

        float zPos = lastZPos + plotSize;

        Instantiate(plotLeft, new Vector3(xPosLeft, 0.3f, zPos), plotLeft.transform.rotation);
        Instantiate(plotRight, new Vector3(xPosRight, 0.3f, zPos), new Quaternion(0, 180, 0, 0));

        lastZPos += plotSize;
    }
}
