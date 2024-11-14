using UnityEngine;

public enum SIDE { Left, Mid, Right }

public class PlayerController : MonoBehaviour
{
    public SIDE m_side = SIDE.Mid;

    private float NewXPos = 0f;
    public bool SwipeLeft;
    public bool SwipeRight;
    public float XValue = 5.0f;

    private CharacterController m_char;
    private Animator m_animator;
    public SpawnManager spawnManager;

    private float x;
    private float smoothXVelocity = 0.0f; // Variable pour SmoothDamp
    public float SpeedDodge = 5.0f;
    public float currentSpeedPlayer;
    public float maxSpeed; // Multiplicateur de vitesse pour la course rapide
    private Vector3 direction;

    public float DodgeForwardSpeed = 44.0f; // Vitesse d'avance pendant l'esquive
    public float DodgeDuration = 0.2f;      // Durée de l'esquive

    private float dodgeTime = 0.0f; 

    void Start()
    {
        m_char = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Détection des swipes pour les mouvements latéraux
        SwipeLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.RightArrow);

        // Gestion des swipes gauche et droite
        if (SwipeLeft)
        {
            if (m_side == SIDE.Mid)
            {
                NewXPos = -XValue;
                m_side = SIDE.Left;
                m_animator.SetTrigger("DodgeLeft"); // Animation d'esquive
                dodgeTime = DodgeDuration; // Initialiser la durée de l'esquive
            }
            else if (m_side == SIDE.Right)
            {
                NewXPos = 0;
                m_side = SIDE.Mid;
                m_animator.SetTrigger("DodgeLeft"); // Animation d'esquive
                dodgeTime = DodgeDuration; // Initialiser la durée de l'esquive
            }
        }
        else if (SwipeRight)
        {
            if (m_side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_side = SIDE.Right;
                m_animator.SetTrigger("DodgeRight"); // Animation d'esquive
                dodgeTime = DodgeDuration; // Initialiser la durée de l'esquive
            }
            else if (m_side == SIDE.Left)
            {
                NewXPos = 0;
                m_side = SIDE.Mid;
                m_animator.SetTrigger("DodgeRight"); // Animation d'esquive
                dodgeTime = DodgeDuration; // Initialiser la durée de l'esquive
            }
        }

        // Smooth transition sur l'axe X pour un mouvement progressif
        // Utilisation de SmoothDamp pour un mouvement plus fluide
        x = Mathf.SmoothDamp(x, NewXPos, ref smoothXVelocity, 0.2f); // Le dernier paramètre est la durée du lissage

        // Mouvement en avant pendant l'esquive (axe Z)
        float zSpeed = 0;
        if (dodgeTime > 0)
        {
            zSpeed = DodgeForwardSpeed;
            dodgeTime -= Time.deltaTime; // Réduire le temps restant pour l'esquive
        }

        // Appliquer le mouvement sur X et Z
        Vector3 moveVector = new Vector3(x - transform.position.x, 0, currentSpeedPlayer * Time.deltaTime);
        m_char.Move(moveVector);
    }

    private void Die()
    {
        m_animator.SetBool("isDead", true); 
    }

    private void QuitGame()
    {
        // Si le jeu est en cours d'exécution dans l'éditeur Unity
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Si le jeu est en cours d'exécution en tant qu'application
            Application.Quit();
        #endif
    }
  private void ShowMessage(string message)
{
    // Trouver le Canvas
    GameObject canvasObject = GameObject.Find("Canvas");

    // Vérifier si le Canvas existe dans la scène
    if (canvasObject == null)
    {
        Debug.LogError("Canvas not found. Make sure a Canvas object exists in the scene.");
        return;  // Sortir de la méthode si le Canvas n'est pas trouvé
    }

    // Créer un objet Text pour afficher le message
    GameObject textObject = new GameObject(message);
    textObject.transform.SetParent(canvasObject.transform);

    // Ajouter un composant TextMeshProUGUI à l'objet
    TMPro.TextMeshProUGUI textComponent = textObject.AddComponent<TMPro.TextMeshProUGUI>();

    // Définir le texte et ses propriétés
    textComponent.text = message;
    textComponent.fontSize = 50;
    textComponent.color = Color.white; // Couleur noire
textComponent.alignment = TMPro.TextAlignmentOptions.Center;

    // Assurer que le texte est visible dans le Canvas
    RectTransform rectTransform = textObject.GetComponent<RectTransform>();
    rectTransform.localPosition = Vector3.zero;  // Positionner au centre
    rectTransform.sizeDelta = new Vector2(600, 100);  // Taille du texte
}

    // Détection des collisions avec les obstacles
    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
        if (other.CompareTag("Ennemy"))
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();

            // Trouver le joueur (assuré qu'il a bien le tag "Player")
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Boucler sur chaque objet trouvé
            foreach (GameObject obj in allObjects)
            {
                // Ne pas désactiver les objets suivants :
                // 1. L'objet avec le tag "Player"
                // 2. La caméra avec le tag "MainCamera"
                // 3. Les lumières (objets avec un composant Light)
                // 4. Tous les enfants de "Player"

                if (obj == player || obj.CompareTag("MainCamera") || obj.GetComponent<Light>() != null)
                {
                    continue; // Ne pas désactiver ces objets
                }

                // Vérifier si l'objet est un enfant du "Player"
                if (obj.transform.IsChildOf(player.transform))
                {
                    continue; // Ne pas désactiver les sous-objets du joueur
                }
                if (obj.CompareTag("Canvas") || obj.CompareTag("EventSystem"))
                {
                     continue; // Ne pas désactiver le Canvas ou EventSystem
                }

                // Désactiver l'objet
                obj.SetActive(false);
            }
            Die();
            ShowMessage("TU ES MORT!");
        }

        if (other.CompareTag("Cristal"))
        {
            //m_animator.SetBool("isFastRunning",true);
            if(currentSpeedPlayer < maxSpeed)
            {
                currentSpeedPlayer += 2.0f;
                m_animator.speed += 0.01f;
            }
            Debug.Log("Cristal collision");
            Destroy(other.gameObject);
        }

        if(other.CompareTag("BigDoor"))
        {
            Transform doorChild = other.transform.Find("Door");  // Assurez-vous que le nom du sous-objet correspond à "Door"
            if (doorChild != null)
            {
                // Supprimer le sous-objet
                Destroy(doorChild.gameObject);
                Debug.Log("Door deleted!");
            }
            Debug.Log("GOOD MORNING");
            //SceneManager.LoadScene("");
            QuitGame();
        }
    }
}
