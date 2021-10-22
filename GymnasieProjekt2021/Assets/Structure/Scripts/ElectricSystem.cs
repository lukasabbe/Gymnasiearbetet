using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSystem : MonoBehaviour
{
    public bool isGenerator;
    public int electricPerMin;
    [HideInInspector]
    public int electricGroupIndex;

    public bool StructerHasElectry;
    private void Start()
    {
        ElectricStructre electric = new ElectricStructre(isGenerator, electricPerMin, this);
        staticElectricSystem.addNewStructure(electric);
        electricGroupIndex = staticElectricSystem.electricGroups.Count - 1;
        staticElectricSystem.electricGroups[electricGroupIndex].hasEnergy();
    }

    public void connectWire(int groupindexToConectTo)
    {
        staticElectricSystem.changeGruop(electricGroupIndex, groupindexToConectTo);
        electricGroupIndex = groupindexToConectTo;
        staticElectricSystem.electricGroups[electricGroupIndex].hasEnergy();
    }
}

public static class staticElectricSystem
{
    public static List<ElectricGroup> electricGroups = new List<ElectricGroup>();

    public static void addNewStructure(ElectricStructre electricStructre)
    {
        ElectricGroup group =  new ElectricGroup();
        group.electricStructres.Add(electricStructre);
        electricGroups.Add(group);
    }

    public static void changeGruop(int oldGruop, int newGruop)
    {
        for(int i = 0; i < electricGroups[oldGruop].electricStructres.Count; i++)
        {
            electricGroups[newGruop].electricStructres.Add(electricGroups[oldGruop].electricStructres[i]);
        }
        electricGroups[oldGruop].electricStructres.Clear();
    }

    public static void reloadElectricSystem()
    {
        
    }
}

public class ElectricGroup
{
    public List<ElectricStructre> electricStructres = new List<ElectricStructre>();
    public int amountOfEnergy;
    public void hasEnergy()
    {
        int amount = 0;
        for(int i = 0; i< electricStructres.Count; i++)
        {
            amount += electricStructres[i].electricPerMin;
        }
        amountOfEnergy = amount;
        if(amount >= 0)
        {
            for (int i = 0; i < electricStructres.Count; i++) electricStructres[i].structureSystem.StructerHasElectry = true;
        }
        else
        {
            for (int i = 0; i < electricStructres.Count; i++) electricStructres[i].structureSystem.StructerHasElectry = false;
        }
    }
}

public class ElectricStructre
{
    public bool isGenerator;
    //public List<ElectricStructre> ConnectedStructers = new List<ElectricStructre>();
    //Generating Stats
    public int electricPerMin;

    public bool hasCheked;

    public int groupindex;

    public ElectricSystem structureSystem;

    public ElectricStructre(bool isGenerator, int electricPerMin , ElectricSystem structureSystem )
    {
        this.isGenerator = isGenerator;
        this.electricPerMin = electricPerMin;
        this.structureSystem = structureSystem;
    }
}
