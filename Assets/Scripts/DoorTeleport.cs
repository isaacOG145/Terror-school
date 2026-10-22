using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Asegura la compatibilidad con el nuevo Input System

public class DoorTeleport : MonoBehaviour
{
    [Header("Configuración de Destino")]
    [Tooltip("Nombre exacto de la escena a cargar")]
    public string targetSceneName = "Level1_RA";

    [Tooltip("Tag asignado al personaje principal")]
    public string playerTag = "Player";

    private bool isPlayerInTrigger = false;

    private void Update()
    {
        // Detecta la tecla E al presiónala estando sobre la puerta
        if (isPlayerInTrigger && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            LoadTargetScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = false;
        }
    }

    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("No se definió el nombre de la escena de destino en el Inspector.");
        }
    }
}