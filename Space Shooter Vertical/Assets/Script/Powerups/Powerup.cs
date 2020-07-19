using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int powerupID;      //  0 = tripple shot, 1 = speed, 2 = shield
    [SerializeField]
    private AudioClip _pickupSound;

    void Start()
    {

    }


    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -8)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_pickupSound, transform.position);

                switch (powerupID)
                {
                    case 0:
                        player.TrippleShotActivate();
                        break;

                    case 1:
                        player.SpeedBoostActve();
                        break;

                    case 2:
                        player.ShieldActve();
                        break;

                    default:
                        break;
                }
                //Instantiate(_pickupSound, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

}