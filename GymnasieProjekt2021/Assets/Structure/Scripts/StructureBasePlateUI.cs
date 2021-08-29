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
    [HideInInspector]
    public InventoryManager PlayerInventory;
    private GameObject StructurePannel;
    [HideInInspector]
    public List<slot> Slots = new List<slot>();
    PlayerInputEventManager input;
    private void Start()
    {
        PlayerInventory = FindObjectOfType<InventoryManager>();
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
    public void openInventory(RaycastHit ray)
    {
        if(ray.transform.GetChild(0).gameObject.activeSelf == false)
        {
            ray.transform.GetChild(0).gameObject.SetActive(true);
            MovmentStates.States = MovementState.off;
            Cursor.lockState = CursorLockMode.None;
            if (hasInventory) PlayerInventory.LatestOpenInventoryStructure = Slots;
        }
        else
        {
            ray.transform.GetChild(0).gameObject.SetActive(false);
            MovmentStates.States = MovementState.walking;
            Cursor.lockState = CursorLockMode.Locked;
            if (hasInventory) PlayerInventory.LatestOpenInventoryStructure = null;
        }
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
