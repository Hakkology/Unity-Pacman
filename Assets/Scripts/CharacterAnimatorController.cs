using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//RequireComponent[nameof(SpriteRenderer)];
public class CharacterAnimatorController : MonoBehaviour
{
    SpriteRenderer charSpriteRenderer;
    CharacterMovementController charMovementController;

    private int animationFrame;
    public Sprite[] animationSprites;

    [SerializeField] private float animationTime;
    private bool animationLoop = true;

    private void Awake()
    {
        charSpriteRenderer = GetComponent<SpriteRenderer>();
        charMovementController = GetComponent<CharacterMovementController>();
    }
    void Start()
    {
        InvokeRepeating(nameof(PlayAnimation), animationTime, animationTime);
    }

    // Update is called once per frame
    void Update()
    {
        CharUpdateDirection();
    }

    private void PlayAnimation()
    {
        animationFrame++;

        if (animationFrame >= animationSprites.Length 
            && animationLoop) 
        {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            charSpriteRenderer.sprite = animationSprites[animationFrame];
        }
    }

    private void RestartAnimation()
    {
        animationFrame = -1;
        PlayAnimation();
    }

    private void CharUpdateDirection()
    {
        Vector2 dir = charMovementController.mevcutDirection;

        if (dir.x > 0) // sað
        {
            charSpriteRenderer.flipX = false;
            charSpriteRenderer.transform.rotation = Quaternion.identity;
        }
        else if (dir.x < 0) // sol
        {
            charSpriteRenderer.flipX = true;
            charSpriteRenderer.transform.rotation = Quaternion.identity;
        }
        else if (dir.y > 0) // yukarý
        {
            charSpriteRenderer.flipX = false;
            charSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir.y < 0) // aþaðý
        {
            charSpriteRenderer.flipX = false;
            charSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
}
