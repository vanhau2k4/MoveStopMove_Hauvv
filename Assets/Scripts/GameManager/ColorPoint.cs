using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class ColorPoint : MonoBehaviour
{
/*    public ColorType color;
    public Image image;
    public List<Material> listMau = new List<Material>();*/
    private void Start()
    {
/*        if (!TryGetComponent<CharactedBase>(out _))
        {
            gameObject.AddComponent<CharactedBase>();
        }
        ChangePointColor(color);*/
    }
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
/*    public void ChangePointColor(ColorType newColor)
    {
        if (TryGetComponent(out CharactedBase characted))
        {
            Debug.Log(characted.color);
            color = characted.color;
            Material mats = image.material;
            mats = listMau[(int)newColor];
            image.material = mats;
        }
    }*/
}
