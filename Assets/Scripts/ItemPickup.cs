using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType { Consumable, Equipment }
    [Header("Configuración del Ítem")]
    public ItemType itemType;
    public string itemName; // Ej: "LlaveSalida", "Botiquin", "Linterna"
    public int quantity = 1;

    private bool isPlayerInRange = false;
    private GameObject playerRef;

    private void Update()
    {
        if (isPlayerInRange && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerRef = other.gameObject;
            Debug.Log("Presiona 'E' para recoger: " + itemName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerRef = null;
        }
    }

    private void PickUp()
    {
        if (GameManager.Instance != null)
        {
            if (itemType == ItemType.Consumable)
            {
                GameManager.Instance.AddConsumable(itemName, quantity);
            }
            else if (itemType == ItemType.Equipment)
            {
                GameManager.Instance.SetEquipment(itemName, true);
            }

            Destroy(gameObject); // Destruye el objeto del mundo
        }
    }
}