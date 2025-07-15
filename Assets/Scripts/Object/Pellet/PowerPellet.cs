using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

    protected override void Eat()
    {
        GameManager.Instance.PowerPelletEaten(this);
    }
}