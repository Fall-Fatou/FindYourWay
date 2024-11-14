using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner;
    PlotSpawner plotSpawner;

    // Variables pour le délai de spawn
    public float maxRoadSpawnDelay = 12f; // Délai max pour une vitesse lente
    public float minRoadSpawnDelay = 0.2f; // Délai min pour une vitesse rapide

    private bool canSpawnRoad = true;   // Contrôle si une route peut être spawnée
    private GameObject player;         // Référence au joueur
    private PlayerController playerController; // Référence au script du joueur

    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
        plotSpawner = GetComponent<PlotSpawner>();

        // Récupérer le joueur par tag et son script associé
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    public void SpawnTriggerEntered()
    {
        // Appeler le spawn des plots immédiatement
        plotSpawner.SpawnPlot();

        // Lancer la coroutine pour gérer le spawn des routes
        if (canSpawnRoad)
        {
            StartCoroutine(SpawnRoadWithDelay());
        }
    }

    // Coroutine pour gérer le délai entre les spawns de route
    IEnumerator SpawnRoadWithDelay()
    {
        canSpawnRoad = false; // Empêcher d'autres spawns avant la fin du délai

        // Déplacer la route
        roadSpawner.MoveRoad();

        // Calculer le délai en fonction de la vitesse du joueur
        float playerSpeed = playerController != null ? playerController.currentSpeedPlayer : 1.0f;
        float roadSpawnDelay = Mathf.Lerp(maxRoadSpawnDelay, minRoadSpawnDelay, playerSpeed / playerController.maxSpeed);

        // Attendre avant de permettre le prochain spawn
        yield return new WaitForSeconds(roadSpawnDelay);

        canSpawnRoad = true; // Autoriser un nouveau spawn de route
    }
}
