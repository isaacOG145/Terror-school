using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Inventario de Consumibles (Ej: LlaveSotano, Botiquin, Pilas)")]
    private Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    [Header("Inventario de Equipamiento Permanente (Ej: Linterna)")]
    private Dictionary<string, bool> equipmentItems = new Dictionary<string, bool>();

    [Header("Configuración de Victoria")]
    public string winningSceneName = "PantallaVictoria"; // Cambia por el nombre de tu escena final si aplica

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Método para añadir o sumar consumibles
    public void AddConsumable(string itemName, int amount = 1)
    {
        if (inventoryItems.ContainsKey(itemName))
        {
            inventoryItems[itemName] += amount;
        }
        else
        {
            inventoryItems[itemName] = amount;
        }
        Debug.Log("Consumible añadido: " + itemName + " | Cantidad actual: " + inventoryItems[itemName]);
    }

    // Método para consumir/gastar un objeto (ej. usar llave)
    public bool UseConsumable(string itemName, int amount = 1)
    {
        if (inventoryItems.ContainsKey(itemName) && inventoryItems[itemName] >= amount)
        {
            inventoryItems[itemName] -= amount;
            Debug.Log("Usado: " + itemName + " | Restantes: " + inventoryItems[itemName]);
            return true;
        }
        Debug.Log("No tienes suficientes unidades de: " + itemName);
        return false;
    }

    public int GetConsumableCount(string itemName)
    {
        if (inventoryItems.ContainsKey(itemName))
            return inventoryItems[itemName];
        return 0;
    }

    // Equipamiento permanente
    public void SetEquipment(string equipmentName, bool state)
    {
        equipmentItems[equipmentName] = state;
        Debug.Log("Equipamiento actualizado: " + equipmentName + " = " + state);
    }

    public bool HasEquipment(string equipmentName)
    {
        if (equipmentItems.ContainsKey(equipmentName))
            return equipmentItems[equipmentName];
        return false;
    }
}