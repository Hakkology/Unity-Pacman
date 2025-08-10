using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    CircleCollider2D cc;
    Rigidbody2D rb;
    CharacterMovementController pmc;
    CharacterAnimatorController pac;

    void Awake()
    {
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        pmc = GetComponent<CharacterMovementController>();
        pac = GetComponent<CharacterAnimatorController>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        pmc.ResetState();
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pmc.SetDirection(Vector2.up);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pmc.SetDirection(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pmc.SetDirection(Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pmc.SetDirection(Vector2.right);
        }

        //y ile x deðeriyle verilen vektörün baktýðý açýyý alýr.
        //float angle = Mathf.Atan2(pmc.pacmanCurrentDirection.y, pmc.pacmanCurrentDirection.x);
        //istenilen aksta dönme yapar.
        //transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }


}
