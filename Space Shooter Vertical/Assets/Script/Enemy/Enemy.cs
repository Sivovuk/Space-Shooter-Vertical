using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [Space]

    private Player player;

    private Animator animator;
    [SerializeField]
    private AudioSource explosion;
	
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        if (player == null) 
        {
            Debug.LogError("Player script empty");
        }

        animator = GetComponent<Animator>();

        if (animator == null) 
        {
            Debug.LogError("Enemy Animator empty");
        }
    }

   
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -8) 
        {
            float radnom = Random.Range(-9,9);
            transform.position = new Vector3(radnom, 8, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (player != null)
            {
                player.Damage();
            }

            DestroyEnemy();
        }

        if (other.CompareTag("Laser")) 
        {
            if (player != null) 
            {
                player.AddScore(10);
            }

            DestroyEnemy();
            Destroy(other.gameObject);
        }
    }

    public void DestroyEnemy()
    {
        explosion.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("OnEnemyDeath");
        _speed = 0;
        Destroy(this.gameObject, 2f);
    }

}
