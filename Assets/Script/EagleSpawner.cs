using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    [SerializeField] Eagle eagle;
    [SerializeField] Frogy frogy;
    [SerializeField] float initialTimer;

    float timer;

    void Start()
    {
        timer = initialTimer; 
        eagle.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timer <= 0 && eagle.isActiveAndEnabled == false)
        {
            eagle.gameObject.SetActive(true);
            eagle.transform.position = frogy.transform.position + new Vector3(0,0,13);
            frogy.SetMoveable(false);
        }
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = initialTimer;
    }
}
