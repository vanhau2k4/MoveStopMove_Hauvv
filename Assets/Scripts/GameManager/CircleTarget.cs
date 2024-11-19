using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTarget : MonoBehaviour
{
    private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(90,0,speed);
        speed += 100 * Time.deltaTime;
    }
}
