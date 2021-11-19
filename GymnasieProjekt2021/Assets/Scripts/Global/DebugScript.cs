using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    public GameObject Pannel;
    public GameObject DebugText;
    public ElectricSystem script;
    public ElectricGroup DebugElectricGruop = new ElectricGroup();
    private List<GameObject> text = new List<GameObject>();
    public float timer = 1;

    private void Update()
    {
        timer -= Time.deltaTime;
        DebugElectricGruop = staticElectricSystem.electricGroups[script.electricGroupIndex];
        if(DebugElectricGruop.electricStructres.Count > text.Count || timer <= 0)
        {
            timer = 1;
            for(int i = 0; i < text.Count; i++)
            {
                Destroy(text[i]);
            }
            text.Clear();
            for (int i = 0; i < DebugElectricGruop.electricStructres.Count; i++)
            {
                text.Add(Instantiate(DebugText, DebugText.transform.position + new Vector3(0, -25f * (i +1), 0), Quaternion.identity, Pannel.transform));
                text[i].GetComponent<Text>().text = DebugElectricGruop.electricStructres[i].structureSystem.gameObject.name + "-" + DebugElectricGruop.electricStructres[i].structureSystem.electricPerMin + "-" + DebugElectricGruop.electricStructres[i].structureSystem.StructerHasElectry;
            }
        }
    }
}