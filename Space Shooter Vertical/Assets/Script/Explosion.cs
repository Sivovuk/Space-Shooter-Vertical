using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float _delay = 2;
    void Start()
    {
        Destroy(this.gameObject, _delay);
    }

}
