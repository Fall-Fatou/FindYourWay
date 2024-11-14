using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;       // Liste des routes
    public GameObject roadPrefab;        // Prefab de la route
    private float offset = 40f;          // Décalage pour positionner les nouvelles routes
    public int initialRoadCount = 50;    // Nombre initial de routes à spawn au début
    public float distanceThreshold = 50f; // Distance à laquelle la route derrière est déplacée

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Trouve le joueur

        // Spawn initial de routes
        SpawnInitialRoads();

        // Trier les routes par leur position Z
        SortRoadsByPosition();
    }

    // Fonction pour spawn plusieurs routes au début
    void SpawnInitialRoads()
    {
        float startPosZ = 0f;

        for (int i = 0; i < initialRoadCount; i++)
        {
            GameObject road = Instantiate(roadPrefab, new Vector3(0, 0, startPosZ), Quaternion.identity);
            roads.Add(road);
            startPosZ += offset;
        }
    }

    // Fonction pour trier les routes par leur position Z
    void SortRoadsByPosition()
    {
        roads.Sort((a, b) => a.transform.position.z.CompareTo(b.transform.position.z));
    }

    // Cette fonction va déplacer les routes derrière le joueur si elles sont trop loin
    public void MoveRoad()
    {
        // Vérifie la première route dans la liste
        GameObject firstRoad = roads[0];

        // Si la première route est trop loin du joueur (par exemple, au-delà de distanceThreshold), on la déplace
        if (firstRoad.transform.position.z < player.transform.position.z - distanceThreshold)
        {
            // Déplacer la route derrière la dernière
            float newZ = roads[roads.Count - 1].transform.position.z + offset;
            firstRoad.transform.position = new Vector3(0, 0, newZ);

            // Réorganiser la liste des routes
            roads.RemoveAt(0);
            roads.Add(firstRoad);
        }
    }

    // Cette fonction est appelée pour s'assurer qu'il y a toujours assez de routes devant le joueur
    public void EnsureSufficientRoads()
    {
        // Si le joueur est trop proche de la dernière route (moins de 2 routes devant lui)
        if (roads.Count < 3 || roads[roads.Count - 1].transform.position.z < player.transform.position.z + 100f)
        {
            // Ajouter une nouvelle route devant
            SpawnNewRoad();
        }
    }

    // Fonction pour spawn une nouvelle route devant le joueur
    void SpawnNewRoad()
    {
        float lastRoadZ = roads[roads.Count - 1].transform.position.z;
        GameObject newRoad = Instantiate(roadPrefab, new Vector3(0, 0, lastRoadZ + offset), Quaternion.identity);
        roads.Add(newRoad);
    }
}