using UnityEngine;

public class PowerPellet : Pellet
{
    public override int Points => 50;
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
        //gameObject.SetActive(false);

    }
}