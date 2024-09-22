using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedFactor = 1f;
    public float maxLifeTime = 3f; //measured in seconds
    public Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speedFactor * targetVector * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); //Destruyo el meteorito
            Destroy(gameObject); //Destruyo la bala
        }
    }
}
