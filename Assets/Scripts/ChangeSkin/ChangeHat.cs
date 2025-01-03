using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HatType
{
    Arrow = 0, Ear = 1, Hair = 2, Officer = 3, None ,random
}

public class ChangeHat : MonoBehaviour
{
    public HatType hatType;
    public List<GameObject> Hats = new List<GameObject>();

    private GameObject currentHat; 
    public Transform parentObject; 

    void Start()
    {
        CreateSpecificHat(); 
    }
    public void CreateSpecificHat()
    {
        if (Hats.Count == 0 || parentObject == null)
        {
            Debug.LogWarning("Danh sách mũ trống hoặc chưa có đối tượng cha để gắn mũ.");
            return;
        }

        // Nếu chọn None hoặc chỉ số mũ không hợp lệ, chọn ngẫu nhiên một mũ
        int hatIndex;
        if(hatType == HatType.None)
        {
            return;
        }
        if (hatType == HatType.random)
        {
            hatIndex = Random.Range(0, Hats.Count);
        }
        else
        {
            hatIndex = (int)hatType;
        }

        if (currentHat != null)
        {
            Destroy(currentHat);
        }

        currentHat = Instantiate(Hats[hatIndex], parentObject.position, Quaternion.identity);
        currentHat.transform.SetParent(parentObject);
        currentHat.transform.localPosition = Vector3.zero;
    }
}
