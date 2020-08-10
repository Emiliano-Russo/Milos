using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Milk : MonoBehaviour
{
    public float speed;
    private Vector3 shootDir;


    public void SetUp_ShootDir(Vector3 dir)
    {        
        shootDir = dir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    void Update()
    {
        transform.position += shootDir * speed * Time.deltaTime;
        Destroy(this.gameObject, 2);
    }
}
