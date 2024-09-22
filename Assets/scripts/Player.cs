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


    public static float xBorderLimit, yBorderLimit;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        yBorderLimit = Camera.main.orthographicSize + 1;
        xBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
    }

    //usado para updates relacionados con físicas y rigidbodies
    //asegura que los calculos físicos son correctos para cada intervalo
    void FixedUpdate()
    {
        //detectamos el input del usuario desde los controles
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right; //la cabeza inicialmente está orientada a la derecha
        _rigid.AddForce(thrustDirection * thrust * _thrustForce);

        transform.Rotate(Vector3.forward, -rotation * _rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = BulletPool.Instance.RequestBullet();
            bullet.transform.position = gun.transform.position;
            Bullet bulletScript = bullet.GetComponent<Bullet>(); //componente bala script para modificar su dirección al disparar
            bulletScript.targetVector = transform.right;
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
