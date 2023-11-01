using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletprefab;
    [SerializeField]
    private Transform parentTransform;

    [SerializeField]
    private int maxCount = 30;
    private List<GameObject> bulletPool;

    void Start()
    {
        bulletPool = new List<GameObject>();

        for (int i = 0; i < maxCount; i++)
        {
            GameObject bulletObj = Instantiate(bulletprefab, parentTransform);
            bulletObj.SetActive(false);
            bulletPool.Add(bulletObj);
        }
    }

    public GameObject Get()
    {
        foreach (GameObject obj in bulletPool)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }
}