using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalAnimation : MonoBehaviour
{
    public float floatStrength = 0.1f;  // Amplitude du flottement (oscillation)
    public float floatSpeed = 2f;       // Vitesse du flottement (comment il monte et descend)
    public float startHeight = 0.5f;    // Hauteur initiale du cristal avant de commencer l'oscillation
    public float rotationSpeed = 50f;   // Vitesse de rotation autour de l'axe Y

    private Vector3 startPos;  // Position initiale du cristal (avant flottement)

    // Start is called before the first frame update
    void Start()
    {
        // Initialiser la position de départ avec la hauteur définie
        startPos = transform.position;
        startPos.y = startHeight;  // Placer le cristal à une hauteur de départ
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        // Appliquer le flottement : Ajout d'une oscillation sinusoïdale sur l'axe Y
        float newY = startPos.y; //+ Mathf.Sin(Time.time * floatSpeed) * floatStrength;

        // Mettre à jour la position du cristal tout en maintenant la hauteur de départ
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotation : Faire tourner l'objet autour de l'axe Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}