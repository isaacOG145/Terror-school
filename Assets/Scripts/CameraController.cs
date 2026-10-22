using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    public float xmin, ymin, xmax, ymax;
    float px, py;

    void Update()
    {
        
        transform.position = new Vector3(px, py, -10);

        if(player.position.x >= xmin && player.position.x <= xmax)
        {
            px = player.position.x;
        }

        if (player.position.y >= ymin && player.position.y <= ymax)
        {
            py = player.position.y;
        }

    }
}
