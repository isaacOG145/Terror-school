using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    
    [Header("Identificación de Puerta y Spawn")]
    public string doorID; // ID único de esta puerta (Ej: "Puerta_Habitacion1")
    public Transform spawnPoint; // Posición donde aparecerá el jugador si entra por esta puerta

    public GameObject advert;
    public string playerTag = "Player";

    [Header("Condición de Llave (Opcional)")]
    public bool requiresKey = false;
    public string requiredKeyName = "LlaveSalida";

    private bool isPlayerInTrigger = false;

    private void Update()
    {
        if (isPlayerInTrigger && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AttemptChangeScene();
        }
    }

    private void AttemptChangeScene()
    {
        if (requiresKey)
        {
            if (GameManager.Instance != null && GameManager.Instance.GetConsumableCount(requiredKeyName) > 0)
            {
                GameManager.Instance.UseConsumable(requiredKeyName, 1);
                Debug.Log("¡Puerta abierta con éxito usando " + requiredKeyName + "!");
                ExecuteSceneChange();
            }
            else
            {
                Debug.Log("La puerta está cerrada con seguro. Necesitas: " + requiredKeyName);
            }
        }
        else
        {
            ExecuteSceneChange();
        }
    }

    private void ExecuteSceneChange()
    {
        // Guardamos el ID de esta puerta en el GameManager antes de cambiar de escena
        if (GameManager.Instance != null)
        {
            GameManager.Instance.nextSpawnDoorID = doorID;
        }

        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = true;
            if (advert != null) advert.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = false;
            if (advert != null) advert.SetActive(false);
        }
    }
}