using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PacmanMovementController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    public float pacmanSpeed = 8;
    public float pacmanSpeedMultiplier = 1;
    public LayerMask pacmanObstacleLayer;


    private Vector2 pacmanInitialDirection = Vector2.right;         // Oyunun başında başlayacağı yön
    public Vector2 pacmanCurrentDirection { get; private set; }     // Şu an ilerlediği yön
    public Vector2 pacmanNextDirection { get; private set; }        // Oyuncunun girmek istediği bir sonraki yön

    public Vector2 pacmanCurrentPosition { get; private set; }      // Rigidbody üzerinden alınan mevcut pozisyon
    public Vector3 pacmanStartingPosition { get; private set; }     // Oyuna başlarken konumlanacağı nokta


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pacmanStartingPosition = transform.position;
    }

    void Start()
    {
        ResetState();
    }

    void Update()
    {
        // Oyuncunun yön değiştirme girişini kontrol etmek için.
        if (pacmanNextDirection != Vector2.zero)
        {
            SetDirection(pacmanNextDirection);
        }
    }

    void FixedUpdate()
    {
        // Pacmani sabit hareket ettirmek için.
        Vector2 position = rb.position;
        // Hareket yönünü, hızı ve deltaTime'ı kullanarak hareket miktarını hesapla
        Vector2 translation = pacmanCurrentDirection * pacmanSpeed * pacmanSpeedMultiplier * Time.fixedDeltaTime;
        rb.MovePosition(position + translation);
    }

    public void ResetState()
    {
        pacmanSpeedMultiplier = 1;
        pacmanCurrentDirection = pacmanInitialDirection;
        pacmanNextDirection = Vector2.zero;

        transform.position = pacmanStartingPosition;
        rb.isKinematic = false;
        enabled = true;
    }

    public void SetDirection(Vector2 _direction, bool _forced = false)
    {
        if (!CanMove(_direction) || _forced)
        {
            pacmanCurrentDirection = _direction;
            pacmanNextDirection = Vector2.zero;
        }
        else
        {
            pacmanNextDirection = _direction;
        }
    }

    public bool CanMove(Vector2 _canSetDirection)
    {
        //BoxCast: Belirtilen yönde, belirtilen boyutta (0.75x0.75) kutu şeklinde ışın atar
        //1.5 olma sebebi yarım birim de pacmanin kendi genişliği.
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, _canSetDirection, 1.5f, pacmanObstacleLayer);
        // Eğer bu yönde herhangi bir engel varsa collider döner → hareket edilemez.
        // Engel yoksa null döner → hareket edilebilir.
        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)pacmanCurrentDirection * 1.5f);
        }
    }


}