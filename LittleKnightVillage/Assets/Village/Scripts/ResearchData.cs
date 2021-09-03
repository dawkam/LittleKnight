using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchData
{

    public List<int> collectorsCount;
    public List<int> collectorsDeathByMonster;
    public List<int> collectorsDeathByHunger;
    public List<int> collectorsDeathByThirst;
    public List<int> lumberjacksCount;
    public List<int> lumberjacksDeathByMonster;
    public List<int> lumberjacksDeathByHunger;
    public List<int> lumberjacksDeathByThirst;
    public List<int> artisansCount;
    public List<int> artisansDeathByMonster;
    public List<int> artisansDeathByHunger;
    public List<int> artisansDeathByThirst;
    public List<int> babysCount;
    public List<int> babysDeathByMonster;
    public List<int> babysDeathByHunger;
    public List<int> babysDeathByThirst;
    public List<int> warehouseFoodCount;
    public List<int> sceneFoodCount; 
    public List<int> warehouseWoodCount; 
    public List<int> sceneTreeCount; 
    public List<int> sceneWoodCount;
    
    public DeathReson mayorDeathReson;
    public float simulationTime;
    public float comfortMax;


    public ResearchData()
    {
        simulationTime = Time.realtimeSinceStartup;
    }
}

public enum DeathReson
{
    None,
    Monster,
    Hunger,
    Thirst
}