using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigid;

    //definimos factores por defecto de empuje y rotación
    public float _thrustForce = 100f;
    public float _rotationSpeed = 100f;

    public static uint SCORE = 0;

    public GameObject gun; //spawner
    public GameObject bulletPrefab; //bullet itself

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //detectamos el input del usuario desde los controles
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right; //la cabeza inicialmente está orientada a la derecha
        _rigid.AddForce(thrustDirection * thrust * _thrustForce);

        transform.Rotate(Vector3.forward, -rotation * _rotationSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet balaScript = bullet.GetComponent<Bullet>(); //componente bala script para modificar su dirección al disparar
            balaScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reiniciamos el nivel porque hemos perdido
        }
        else
            Debug.Log("nave ha colisionado con " + collision.gameObject.tag);
    }
}
