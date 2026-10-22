using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float xmin, ymin, xmax, ymax;
    private float px, py;

    private void Start()
    {
        FindPlayer();
    }

    private void LateUpdate()
    {
        // Si la cámara sigue sin referencia al jugador, la intenta buscar dinámicamente
        if (player == null)
        {
            FindPlayer();
            if (player == null) return;
        }

        // Delimitar coordenadas X e Y
        px = Mathf.Clamp(player.position.x, xmin, xmax);
        py = Mathf.Clamp(player.position.y, ymin, ymax);

        transform.position = new Vector3(px, py, -10f);
    }

    // Permite que el PlayerSpawner le asigne el jugador directamente
    public void SetTarget(Transform newTarget)
    {
        player = newTarget;
        if (player != null)
        {
            px = Mathf.Clamp(player.position.x, xmin, xmax);
            py = Mathf.Clamp(player.position.y, ymin, ymax);
            transform.position = new Vector3(px, py, -10f);
        }
    }

    private void FindPlayer()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            SetTarget(p.transform);
        }
    }
}