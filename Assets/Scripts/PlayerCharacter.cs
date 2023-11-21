using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Rigidbody2D rb;
    public EnemyAttributesManager enemyAtm;
    public ParticleSystem Dust;
    private Animator animator;
    public Vector2 maxPower;
    public Vector2 minPower;
    public int health;
    public int coins;
    public int damage = 10;
    public int maxHealth = 100;
    public float Movepower = 10f;
    
 
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    Camera cam;
    TrajectoryLine tl;

    private void Start()
    {
        cam = Camera.main;
        tl = GetComponent<TrajectoryLine>();
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    void CreateDust()
    {
    
    Dust.Play();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
        }

        if (Input.GetMouseButton(0))
        {
        
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            tl.RenderLine(startPoint, currentPoint);

        }



        if (Input.GetMouseButtonUp(0)) 
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;
            Dust.Play();
            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y -  endPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * Movepower, ForceMode2D.Impulse);
            tl.Endline();
 
        }

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy")) 
        {
            collision.gameObject.GetComponent<EnemyAttributesManager>().TakeDamage(damage);
        
        }
    }









}
