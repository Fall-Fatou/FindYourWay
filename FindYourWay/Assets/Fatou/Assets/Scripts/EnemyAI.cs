using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float movementSpeed = 25f;
    private Rigidbody enemyRb;
    private Transform player;
    //private bool isChasing = false;
    private Vector3 moveDirection;

    void Start()
    {
        moveDirection = -transform.forward;
        // Récupérer les composants nécessaires
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;

        // Désactiver la gravité si nécessaire
        enemyRb.useGravity = false;

        // Geler les positions sur les axes X et Y (si tes véhicules sont supposés se déplacer seulement en Z)
        enemyRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Limiter la détection des collisions à "Continuous" si nécessaire pour éviter de traverser les objets
        enemyRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        // Déplacer le véhicule en arrière (inverse de sa direction actuelle)
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime);

    }

    // Gestion des collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Arrêter l'ennemi en cas de collision avec le joueur
            enemyRb.velocity = Vector3.zero;
            enemyRb.isKinematic = true; // Désactiver la physique pour l'ennemi pendant la collision

            // Eventuellement désactiver la physique du joueur aussi
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero; // Arrêter le joueur
                playerRb.isKinematic = true;
                //StartCoroutine(ResumePhysicsAfterDelay(0.1f));  // Réactiver la physique après un court délai
            }

            Debug.Log("Collision avec le joueur !");
        }
    }

    private IEnumerator ResumePhysicsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyRb.isKinematic = false;  // Réactiver la physique pour l'ennemi
    }
}