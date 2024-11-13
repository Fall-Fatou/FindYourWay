using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float mouvementSpeed = 30f;
    public float deceleration = 5f;

    private float vitesseActuelle;

    // Start is called before the first frame update
    void Start()
    {
        vitesseActuelle = mouvementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, 0, 1);

        transform.Translate(direction * vitesseActuelle * Time.deltaTime, Space.World);

        //vitesseActuelle -= deceleration * Time.deltaTime;

        if (vitesseActuelle <= 10)
        {
            vitesseActuelle = 5;
        }
    }
}
