using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributesManager : MonoBehaviour
{
    public int health;
    public int attack;
    public PlayerCharacter playerCharacter;
    public GameObject Blood;
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {

            GetComponent<Lootbag>().InstantiateLoot(transform.position);
            Destroy(gameObject);
            Instantiate(Blood, transform.position, Quaternion.identity);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            playerCharacter.TakeDamage(attack); 
        }
    }

}
