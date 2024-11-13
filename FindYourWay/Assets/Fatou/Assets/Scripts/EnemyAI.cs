using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float movementSpeed = 8f;
    public GameObject player;  // Référence au joueur
    private Rigidbody enemyRb;

    public float reactDistance = 50f;   // Distance à laquelle l'ennemi commence à suivre le joueur
    public float destroyDistance = 50f; // Distance à laquelle l'ennemi est détruit (lorsqu'il est trop loin)
    
    private Vector3 startPosition; // Position initiale pour savoir quand l'ennemi doit être réinitialisé

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        startPosition = transform.position;  // Enregistrer la position de départ de l'ennemi
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            // Ne pas faire autre chose si le joueur est détruit
            return;
        }

        // Calculer la distance entre l'ennemi et le joueur
        float distance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // Si l'ennemi est trop loin, on arrête de le faire bouger
        if (distance > destroyDistance)
        {
            // On réinitialise la position de l'ennemi si nécessaire (évite qu'il "vole")
            transform.position = startPosition;
            return;
        }

        // Si l'ennemi est à portée, il suit le joueur
        if (distance <= reactDistance)
        {
            enemyRb.AddForce(lookDirection * movementSpeed, ForceMode.Force);  // Suivre activement le joueur
        }

        // Limite le mouvement sur l'axe Y pour éviter que l'ennemi vole
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z); // Force l'ennemi à rester sur l'axe du sol

        // Si l'ennemi dépasse une certaine distance du joueur sur l'axe Z, il retourne à sa position initiale
        if (transform.position.z - player.transform.position.z < -3f)
        {
            transform.position = startPosition;  // Réinitialisation de la position
        }
    }
}