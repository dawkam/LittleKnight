using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchData
{

    public int collectorsCount;
    public int collectorsDeathByMonster;
    public int collectorsDeathByHunger;
    public int collectorsDeathByThirst;
    public int lumberjacksCount;
    public int lumberjacksDeathByMonster;
    public int lumberjacksDeathByHunger;
    public int lumberjacksDeathByThirst;
    public int artisansCount;
    public int artisansDeathByMonster;
    public int artisansDeathByHunger;
    public int artisansDeathByThirst;
    public int babysCount;
    public int babysDeathByMonster;
    public int babysDeathByHunger;
    public int babysDeathByThirst;
    public DeathReson mayorDeathReson;
    public int warehouseFoodCountAverage;
    public int warehouseFoodCountMax;
    public int warehouseFoodCountMin;
    public int sceneFoodCountAverage; 
    public int warehouseWoodCountAverage; 
    public int warehouseWoodCountMax;
    public int warehouseWoodCountMin;
    public int sceneTreeCountAverage; 
    public int sceneWoodCountAverage;
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