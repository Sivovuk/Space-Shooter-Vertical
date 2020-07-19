using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Asteroid : MonoBehaviour
{
    //[SerializeField]
    //private float _speed = 0;
    [SerializeField]
    private float _speedRotation = 0;

    [Space]

    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager spawnManager;
	
    void Start()
    {
        spawnManager = GameObject.FindObjectOfType<SpawnManager>();

        if (spawnManager == null) 
        {
            Debug.LogError("Spawn manager is empty");
        }
    }

   
    void Update()
    {
        //transform.Translate(Vector2.down * Time.deltaTime * _speed);

        transform.Rotate(Vector3.forward * Time.deltaTime * _speedRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser") 
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }

}
