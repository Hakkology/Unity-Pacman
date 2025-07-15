using UnityEngine;

public class MapPassageController : MonoBehaviour 
{
    [SerializeField] Transform mapPassageDestination;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = collision.transform.position;
        position.x = mapPassageDestination.position.x;
        position.y = mapPassageDestination.position.y;
        collision.transform.position = position;
    }
}