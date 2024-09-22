using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speedFactor = 1f;
    public float maxLifeTime = 3f; //measured in seconds
    public Vector3 targetVector;

    public static float xBorderLimit, yBorderLimit;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);

        yBorderLimit = Camera.main.orthographicSize + 1;
        xBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x > xBorderLimit || pos.x < -xBorderLimit ||
            pos.y > yBorderLimit || pos.y < -yBorderLimit)
            Destroy(gameObject);
        else
            transform.Translate(speedFactor * targetVector * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            Destroy(collision.gameObject); //Destruyo el meteorito
            Destroy(gameObject); //Destruyo la bala
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI"); //cualquier objeto de la escena que tenga dicho tag
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }
}
