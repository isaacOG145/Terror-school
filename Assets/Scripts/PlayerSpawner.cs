using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Prefab del Jugador")]
    [Tooltip("Arrastra aquí el Prefab del personaje desde la carpeta Project")]
    public GameObject playerPrefab; 

    [Header("Configuración por Defecto")]
    public string defaultDoorID = "";

    // Se usa Start() para asegurar que todas las puertas y el GameManager ya estén cargados
    private void Start()
    {
        SpawnOrPositionPlayer();
    }

    private void SpawnOrPositionPlayer()
    {
        // 1. Buscar si el jugador ya existe en la escena
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        string targetDoorID = "";
        if (GameManager.Instance != null)
        {
            targetDoorID = GameManager.Instance.nextSpawnDoorID;
        }

        if (string.IsNullOrEmpty(targetDoorID))
        {
            targetDoorID = defaultDoorID;
        }

        // 2. Buscar las puertas de la escena
        ChangeScene[] doors = FindObjectsOfType<ChangeScene>();
        Vector3 spawnPosition = transform.position;
        bool doorFound = false;

        foreach (ChangeScene door in doors)
        {
            if (door.doorID == targetDoorID)
            {
                spawnPosition = (door.spawnPoint != null) ? door.spawnPoint.position : door.transform.position;
                doorFound = true;
                break;
            }
        }

        // 3. Instanciar o reubicar al jugador
        if (doorFound || player == null)
        {
            if (player == null && playerPrefab != null)
            {
                player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            }
            else if (player != null)
            {
                player.transform.position = spawnPosition;
                
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.nextSpawnDoorID = "";
            }
        }

        // 4. Asignar de inmediato el Transform del jugador a la cámara
        if (player != null)
        {
            CameraController cameraController = FindObjectOfType<CameraController>();
            if (cameraController != null)
            {
                cameraController.SetTarget(player.transform);
            }
        }
    }
}