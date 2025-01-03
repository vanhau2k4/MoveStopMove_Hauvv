using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PantType
{
    Chambi = 0, Comy = 1, Dabao = 2, Onion = 3, Rainbow = 4, Vantim = 5, None ,Random
}
public class ChangePant : MonoBehaviour
{
    public PantType PantType;
    public List<Material> ListPant = new List<Material>();
    public SkinnedMeshRenderer SkinnedMeshRenderer;

    void Start()
    {
        ApplyPantChange();
    }

    public void ApplyPantChange()
    {
        if (ListPant.Count == 0 || SkinnedMeshRenderer == null)
        {
            Debug.LogWarning("Danh sách quần trống hoặc chưa có SkinnedMeshRenderer.");
            return;
        }
        if(PantType == PantType.None)
        {
            return;
        }
        // Nếu PantType là None hoặc chỉ số không hợp lệ, chọn ngẫu nhiên một quần
        int pantIndex;
        if (PantType == PantType.Random || (int)PantType >= ListPant.Count)
        {
            pantIndex = Random.Range(0, ListPant.Count);
        }
        else
        {
            pantIndex = (int)PantType;
        }

        Material[] mat = SkinnedMeshRenderer.materials;
        mat[0] = ListPant[pantIndex];
        SkinnedMeshRenderer.materials = mat;
    }
}
