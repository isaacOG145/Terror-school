using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
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
            // Verificamos si el GameManager tiene al menos 1 unidad de la llave requerida
            if (GameManager.Instance != null && GameManager.Instance.GetConsumableCount(requiredKeyName) > 0)
            {
                // Opcional: Consumir la llave al abrir la puerta definitiva
                GameManager.Instance.UseConsumable(requiredKeyName, 1);
                Debug.Log("¡Puerta abierta con éxito usando " + requiredKeyName + "!");
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("La puerta está cerrada con seguro. Necesitas: " + requiredKeyName);
            }
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
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