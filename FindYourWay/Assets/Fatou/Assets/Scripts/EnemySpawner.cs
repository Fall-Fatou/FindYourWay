using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;    // Référence au prefab de l'ennemi (véhicule)
    public Transform Player;          // Référence au joueur
    public float spawnInterval = 2f;  // Intervalle entre chaque apparition d'ennemi
    public float spawnRange = 50f;    // Plage de spawn sur l'axe Z
    public float minHeight = -5f;     // Plage de spawn sur l'axe Y (pour éviter qu'ils apparaissent sous terre)
    public float maxHeight = 5f;      // Plage de spawn sur l'axe Y

    private void Start()
    {
        // Assure-toi que le prefab est assigné dans l'inspecteur avant de commencer à spawn
        if (Enemy == null)
        {
            Debug.LogError("Le prefab Enemy n'est pas assigné dans l'inspecteur !");
            return;
        }

        // Commence à spawn des ennemis à intervalles réguliers
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Position aléatoire sur l'axe Z et Y
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),  // Position X aléatoire
                Random.Range(minHeight, maxHeight),     // Position Y aléatoire
                Player.position.z + Random.Range(20f, 50f)  // Position Z devant le joueur
            );

            // Instanciation de l'ennemi à la position calculée
            GameObject newEnemy = Instantiate(Enemy, spawnPosition, Quaternion.identity);

            // Associer la référence du joueur à l'ennemi
            if (newEnemy != null)
            {
                newEnemy.GetComponent<EnemyAI>().player = Player.gameObject;
            }
            else
            {
                Debug.LogError("Erreur d'instanciation de l'ennemi.");
            }
        }
    }
}