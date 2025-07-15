using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovementController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    public float charSpeed = 8;
    public float charSpeedMultiplier = 1;
    public LayerMask ObstacleLayer;


    public Vector2 charInitialDirection = Vector2.right;         // Oyunun başında başlayacağı yön
    public Vector2 charCurrentDirection { get; private set; }     // Şu an ilerlediği yön
    public Vector2 charNextDirection { get; private set; }        // Oyuncunun girmek istediği bir sonraki yön

    public Vector2 charCurrentPosition { get; private set; }      // Rigidbody üzerinden alınan mevcut pozisyon
    public Vector3 charStartingPosition { get; private set; }     // Oyuna başlarken konumlanacağı nokta


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        charStartingPosition = transform.position;
    }

    void Start()
    {
        ResetState();
    }

    void Update()
    {
        // Oyuncunun yön değiştirme girişini kontrol etmek için.
        if (charNextDirection != Vector2.zero)
        {
            SetDirection(charNextDirection);
        }
    }

    void FixedUpdate()
    {
        // Pacmani sabit hareket ettirmek için.
        Vector2 position = rb.position;
        // Hareket yönünü, hızı ve deltaTime'ı kullanarak hareket miktarını hesapla
        Vector2 translation = charCurrentDirection * charSpeed * charSpeedMultiplier * Time.fixedDeltaTime;
        rb.MovePosition(position + translation);
    }

    public void ResetState()
    {
        charSpeedMultiplier = 1;
        charCurrentDirection = charInitialDirection;
        charNextDirection = Vector2.zero;

        transform.position = charStartingPosition;
        rb.isKinematic = false;
        enabled = true;
    }

    public void SetDirection(Vector2 _direction, bool _forced = false)
    {
        if (!CanMove(_direction) || _forced)
        {
            charCurrentDirection = _direction;
            charNextDirection = Vector2.zero;
        }
        else
        {
            charNextDirection = _direction;
        }
    }

    public bool CanMove(Vector2 _canSetDirection)
    {
        //BoxCast: Belirtilen yönde, belirtilen boyutta (0.75x0.75) kutu şeklinde ışın atar
        //1.5 olma sebebi yarım birim de pacmanin kendi genişliği.
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, _canSetDirection, 1.5f, ObstacleLayer);
        // Eğer bu yönde herhangi bir engel varsa collider döner → hareket edilemez.
        // Engel yoksa null döner → hareket edilebilir.
        return hit.collider != null;
    }
}