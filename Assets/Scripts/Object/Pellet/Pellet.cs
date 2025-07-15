using UnityEngine;

public class Pellet : MonoBehaviour
{
    public virtual int Points => 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
    
    protected virtual void Eat()
    {
        GameManager.Instance.PelletEaten(this);
    }

}