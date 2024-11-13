using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalSpawner : MonoBehaviour
{
    private float spawnInterval = 5f;  // Intervalle entre chaque apparition
    private float lastSpawnZ = 20f;    // Dernière position Z à laquelle un ennemi a spawné

    public List<GameObject> cristals;  // Liste d'ennemis (les véhicules)
    public Transform player;          // Référence au joueur (pour déterminer la position du spawn)

    // Start is called before the first frame update
    private void Start()
    {
        // Lancement immédiat du spawn
        StartCoroutine(SpawnCristals());
    }

    private void Update()
    {
        // Optionnel : Mettre à jour si tu veux des ennemis qui spawnent à chaque frame ou à un intervalle plus court.
    }

    private IEnumerator SpawnCristals()
    {
        while (true)
        {
            // Attente avant de spawn un nouvel ennemi
            yield return new WaitForSeconds(spawnInterval);

            // Calcule la position devant le joueur pour instancier les ennemis
            lastSpawnZ = player.position.z + 80f;  // Position Z devant le joueur, à 50 unités devant lui

            // Crée un seul ennemi aléatoire à une position devant le joueur
            GameObject cristal = cristals[Random.Range(0, cristals.Count)];
            float randomX = Random.Range(-4f, 4f);  // Position aléatoire sur l'axe X

            // Instancie un seul ennemi avec des positions calculées devant le joueur
            Instantiate(cristal, new Vector3(randomX, 0.25f, lastSpawnZ + Random.Range(5f, 10f)), cristal.transform.rotation);

            // Affiche un log pour vérifier la position du spawn
            Debug.Log("Spawn à la position Z : " + (lastSpawnZ + Random.Range(5f, 10f)));

            // Réajuste la position du spawn pour éviter les multiples spawns en un seul cycle
            lastSpawnZ += spawnInterval;
        }
    }
}
