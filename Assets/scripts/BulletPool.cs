using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> bulletList;

    //Singleton Pattern for the pool
    private static BulletPool instance;
    public static BulletPool Instance { get {return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AddBulletsToPool(poolSize);
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            
            //todas las instancias de balas queda como hijos del pool
            bullet.transform.parent = transform;
        }
    }

    public GameObject RequestBullet()
    {
        GameObject res = null;
        bool flag = false;

        for (int i = 0; !flag && i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                res = bulletList[i];
                flag = true;
            }
        }
        if (!flag) //el jugador quiere más balas de las creadas originalmente
        {
            AddBulletsToPool(1);
            bulletList[bulletList.Count - 1].SetActive(true);
            res = bulletList[bulletList.Count - 1];
        }
        return res;
    }
}
