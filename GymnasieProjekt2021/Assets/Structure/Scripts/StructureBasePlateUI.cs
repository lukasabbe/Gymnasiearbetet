using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class StructureBasePlateUI : MonoBehaviour
{
    public bool hasInventory;
    [HideInInspector]
    public int xSizeOfInventory , ySizeOfInventory;

    //private
    private GameObject StructurePannel;
    [HideInInspector]
    public List<slot> Slots = new List<slot>();
    PlayerInputEventManager input;
    private void OnEnable()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
        input.openStructKey += openInventory;
    }
    private void OnDisable()
    {
        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();
        input.openStructKey -= openInventory;
    }
    private void Start()
    {
        StructurePannel = transform.GetChild(0).GetChild(0).gameObject;
        if (hasInventory)
        {
            int i = 0;
            for(int y = 0; y < ySizeOfInventory; y++)
            {
                for(int x = 0; x < xSizeOfInventory; x++)
                {
                    GameObject g = Instantiate(StructurePannel.transform.GetChild(0).gameObject, new Vector3(StructurePannel.transform.GetChild(0).position.x + (x * 70), StructurePannel.transform.GetChild(0).position.y + (y * -65), StructurePannel.transform.GetChild(0).position.z), Quaternion.identity, StructurePannel.transform);
                    slot t = new slot();
                    t.slotGameObject = g;
                    t.slotNum = i;
                    Slots.Add(t);
                    i++;
                }
            }
        }
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(StructurePannel.transform.GetChild(0).gameObject);
    }
    void openInventory()
    {
        Physics.Raycast(GameManager.playerCamera.transform.position, GameManager.playerCamera.transform.forward, out RaycastHit ray, 10f, Layers.structure);
        Debug.Log(ray.transform.gameObject.name);
        ray.transform.GetChild(0).gameObject.SetActive(!ray.transform.GetChild(0).gameObject.activeSelf);
    }
    public int findEmptySlot()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (!Slots[i].isTaken) return i;
        }
        return -1;
    }
}
[CustomEditor(typeof(StructureBasePlateUI))]
public class StructureBasePlateUIInspector : Editor
{
    public override void OnInspectorGUI()
    {
        StructureBasePlateUI st = (StructureBasePlateUI)target;
        base.OnInspectorGUI();
        if (st.hasInventory)
        {
            st.xSizeOfInventory = EditorGUILayout.IntField("X size", st.xSizeOfInventory);
            st.ySizeOfInventory = EditorGUILayout.IntField("Y size", st.ySizeOfInventory);
        }
    }
}
