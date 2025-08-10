using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovementController : MonoBehaviour
{
    public Rigidbody2D _rb { get; private set; }

    public float speed = 8;
    public float speedMultipler = 1;

    public LayerMask ObstacleLayer;

    public Vector2 ilkDirection = Vector2.right;
    public Vector2 mevcutDirection { get; private set; }
    public Vector2 sonrakiDirection { get; private set; }

    public Vector2 mevcutKonum { get; private set; }
    public Vector2 baslangicKonum { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        baslangicKonum = transform.position;
    }

    void Start()
    {
        ResetState();
    }

    void Update()
    {
        if (sonrakiDirection != Vector2.zero)
        {
            SetDirection(sonrakiDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 konum = _rb.position;
        Vector2 hareket = mevcutDirection * speed * speedMultipler * Time.fixedDeltaTime;
        _rb.MovePosition(konum + hareket);
    }

    public void SetDirection(Vector2 yon, bool zorla = false)
    {
        if (!Yuruyebilirmiyim(yon) || zorla)
        {
            mevcutDirection = yon;
            sonrakiDirection = Vector2.zero;
        }
        else
        {
            sonrakiDirection = yon;
        }
    }

    private bool Yuruyebilirmiyim(Vector2 yurunebilirmi)
    {
        //BoxCast: Belirtilen yönde, belirtilen boyutta (0.75x0.75) kutu şeklinde ışın atar
        //1.5 olma sebebi yarım birim de pacmanin kendi genişliği.
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * .75f, 0.0f, yurunebilirmi, 1.5f, ObstacleLayer);
        // Eğer bu yönde herhangi bir engel varsa collider döner → hareket edilemez.
        // Engel yoksa null döner → hareket edilebilir.
        return hit.collider != null;
    }

    public void ResetState()
    {
        speedMultipler = 1;
        mevcutDirection = ilkDirection;
        sonrakiDirection = Vector2.zero;

        transform.position = baslangicKonum;
        _rb.isKinematic = false;
        enabled = true;
    }
}
