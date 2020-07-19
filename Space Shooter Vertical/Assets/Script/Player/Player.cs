using System.Collections;
using UnityEngine;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _speedBoost;
    [SerializeField]
    private float _laserOffset = 0.8f;
    [SerializeField]
    private float _fireRate;
    [SerializeField]
    private float _canFire;

    [Space]

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;

    [Space]

    [SerializeField]
    private GameObject _laserPreab;
    [SerializeField]
    private GameObject _trippleLaserPreab;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _explosion;

    [Space]

    [SerializeField]
    private UIManager _manager;
    private SpawnManager _spawnManager;
    [SerializeField]
    private AudioSource _explosionSound;
    [SerializeField]
    private AudioSource _laserSound;

    [Space]

    [SerializeField]
    private bool isTrippleShotActive = false;
    [SerializeField]
    private bool isSpeedBoostActive = false;
    [SerializeField]
    private bool isShieldActive = false;

    private Coroutine trippleShotCorutine;
    private Coroutine speedBoostCorutine;
    private Coroutine shieldCorutine;

    
    void Start()
    {
        transform.position = new Vector3(0, -3.5f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            //Debug.LogError("The spawn manager is null");
        }
    }


    void Update()
    {
        CalculateMovement();

        _fireRate += Time.deltaTime;

#if UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("Fire") && _fireRate >= _canFire)
        {
            ShootLaser();
        }
#else
        if (Input.GetKeyDown(KeyCode.Space) && _fireRate >= _canFire && _fireRate >= _canFire)
        {
            ShootLaser();
        }
#endif


    }


    void CalculateMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxis("H");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * (_speed + _speedBoost) * Time.deltaTime);

        if (transform.position.x < -9 || transform.position.x > 9)
        {
            transform.position = new Vector3(0, transform.position.y, 0);
        }

        if (transform.position.y < -4 || transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
    }


    public void ShootLaser()
    {
        _fireRate = 0;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + _laserOffset, 0);


        if (isTrippleShotActive)
        {
            Instantiate(_trippleLaserPreab, newPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPreab, newPosition, Quaternion.identity);
        }

        _laserSound.Play();
    }


    public void Damage()
    {
        if (isShieldActive)
        {
            _shield.SetActive(false);
            isShieldActive = false;
            StopCoroutine(shieldCorutine);
        }
        else
        {
            _lives--;
            _manager.UpdateLives(_lives);
        }

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _explosionSound.Play();
            Destroy(this.gameObject, 0.25f);
        }

        switch (_lives) 
        {
            case 2 : _rightEngine.SetActive(true);
                break;
            case 1 : _leftEngine.SetActive(true);
                break;
            default : break;
        }
    }


#region Tripple shot coroutine
    public void TrippleShotActivate()
    {
        if (trippleShotCorutine == null)
        {
            trippleShotCorutine = StartCoroutine(TrippleShotPowerDownRoutine());
        }
        else 
        {
            StopCoroutine(trippleShotCorutine);
            trippleShotCorutine = StartCoroutine(TrippleShotPowerDownRoutine());
        }

        PowerUpAnimations.timePassTrippleShot = 5;
    }

    IEnumerator TrippleShotPowerDownRoutine()
    {
        isTrippleShotActive = true;
        yield return new WaitForSeconds(5);
        isTrippleShotActive = false;
    }
#endregion


#region Speed boost coroutine
    public void SpeedBoostActve()
    {
        if (speedBoostCorutine == null)
        {
            speedBoostCorutine = StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        else
        {
            StopCoroutine(speedBoostCorutine);
            speedBoostCorutine = StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        PowerUpAnimations.timePassSpeedBoost = 5;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        isSpeedBoostActive = true;
        _speedBoost = 3;
        yield return new WaitForSeconds(5);
        isSpeedBoostActive = false;
        _speedBoost = 0;
    }
#endregion


#region Shield coroutine
    public void ShieldActve()
    {
        if (shieldCorutine == null)
        {
            shieldCorutine = StartCoroutine(ShieldPowerDownRoutine());
        }
        else
        {
            StopCoroutine(shieldCorutine);
            shieldCorutine = StartCoroutine(ShieldPowerDownRoutine());
        }
        PowerUpAnimations.timePassShield = 5;
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        _shield.SetActive(true);
        isShieldActive = true;

        yield return new WaitForSeconds(5);

        isShieldActive = false;
        _shield.SetActive(false);
    }
#endregion


#region Get booleans 
    public bool GetTrippleShotBoolean()
    {
        return isTrippleShotActive;
    }

    public bool GetSpeedBoostAcive()
    {
        return isSpeedBoostActive;
    }

    public bool GetShiedActives()
    {
        return isShieldActive;
    }
#endregion

    public void AddScore(int points) 
    {
        _score += points;
        _manager.UpdateScore(_score);
    }
}
