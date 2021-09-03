using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVManager : MonoBehaviour
{
    public string fileName;
    private List<string[]> rowSingleData = new List<string[]>();
    private List<string[]> rowMultipleData = new List<string[]>();
    private void Start()
    {
        string[] rowMultipleDataTemp = new string[3];
        string[] rowSingleDataTemp = new string[22];

        rowSingleDataTemp[0] = "mayorDeathReson";
        rowSingleDataTemp[1] = "simulationTime";
        rowSingleDataTemp[2] = "comfortMax";

        rowMultipleDataTemp[0] = "collectorsCount";
        rowMultipleDataTemp[1] = "lumberjacksCount";
        rowMultipleDataTemp[2] = "artisansCount";
        rowMultipleDataTemp[3] = "babysCount";
        rowMultipleDataTemp[4] = "warehouseFoodCount";
        rowMultipleDataTemp[5] = "sceneFoodCount";
        rowMultipleDataTemp[6] = "warehouseWoodCount";
        rowMultipleDataTemp[7] = "sceneTreeCount";
        rowMultipleDataTemp[8] = "sceneWoodCount";
        rowMultipleDataTemp[9] = "collectorsDeathByMonster";
        rowMultipleDataTemp[10] = "collectorsDeathByHunger";
        rowMultipleDataTemp[12] = "collectorsDeathByThirst";
        rowMultipleDataTemp[13] = "lumberjacksDeathByMonster";
        rowMultipleDataTemp[14] = "lumberjacksDeathByHunger";
        rowMultipleDataTemp[15] = "lumberjacksDeathByThirst";
        rowMultipleDataTemp[16] = "artisansDeathByMonster";
        rowMultipleDataTemp[17] = "artisansDeathByHunger";
        rowMultipleDataTemp[18] = "artisansDeathByThirst";
        rowMultipleDataTemp[19] = "babysDeathByMonster";
        rowMultipleDataTemp[20] = "babysDeathByHunger";
        rowMultipleDataTemp[21] = "babysDeathByThirst";


        rowSingleData.Add(rowSingleDataTemp);
        rowMultipleData.Add(rowMultipleDataTemp);
    }

    public void AddData(ResearchData researchData) 
    {
        AddSingleData(researchData);
        AddMultipleData(researchData);
    }

    private void AddSingleData(ResearchData researchData)
    {
        string[] rowSingleDataTemp = new string[3];

        rowSingleDataTemp[0] = researchData.mayorDeathReson.ToString();
        rowSingleDataTemp[1] = researchData.simulationTime.ToString();
        rowSingleDataTemp[2] = researchData.comfortMax.ToString();

        rowSingleData.Add(rowSingleDataTemp);
    }

    private void AddMultipleData(ResearchData researchData)
    {
        List<string[]> rows = new List<string[]>();
        for (int i = 0; i < researchData.collectorsCount.Count; i++)
        {
            string[] rowMultipleDataTemp = new string[13];
            rowMultipleDataTemp[0] = (researchData.collectorsCount.ToString());
            rowMultipleDataTemp[1] = (researchData.lumberjacksCount.ToString());
            rowMultipleDataTemp[2] = (researchData.artisansCount.ToString());
            rowMultipleDataTemp[3] = (researchData.babysCount.ToString());
            rowMultipleDataTemp[4] = (researchData.warehouseFoodCount.ToString());
            rowMultipleDataTemp[5] = (researchData.sceneFoodCount.ToString());
            rowMultipleDataTemp[6] = (researchData.warehouseWoodCount.ToString());
            rowMultipleDataTemp[7] = (researchData.sceneTreeCount.ToString());
            rowMultipleDataTemp[8] = (researchData.sceneWoodCount.ToString());
            rowMultipleDataTemp[9] = researchData.collectorsDeathByMonster.ToString();
            rowMultipleDataTemp[10] = researchData.collectorsDeathByHunger.ToString();
            rowMultipleDataTemp[12] = researchData.collectorsDeathByThirst.ToString();
            rowMultipleDataTemp[13] = researchData.lumberjacksDeathByMonster.ToString();
            rowMultipleDataTemp[14] = researchData.lumberjacksDeathByHunger.ToString();
            rowMultipleDataTemp[15] = researchData.lumberjacksDeathByThirst.ToString();
            rowMultipleDataTemp[16] = researchData.artisansDeathByMonster.ToString();
            rowMultipleDataTemp[17] = researchData.artisansDeathByHunger.ToString();
            rowMultipleDataTemp[18] = researchData.artisansDeathByThirst.ToString();
            rowMultipleDataTemp[19] = researchData.babysDeathByMonster.ToString();
            rowMultipleDataTemp[20] = researchData.babysDeathByHunger.ToString();
            rowMultipleDataTemp[21] = researchData.babysDeathByThirst.ToString();
            rows.Add(rowMultipleDataTemp);
        }

        rowMultipleData.AddRange(rows);
    }


    public void SaveData()
    {
        string[][] output = new string[rowSingleData.Count + rowMultipleData.Count][];

        for (int i = 0; i < rowSingleData.Count; i++)
        {
            output[i] = rowSingleData[i];
        }

        for (int i = rowSingleData.Count - 1; i < rowMultipleData.Count; i++)
        {
            output[i] = rowMultipleData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        StreamWriter outStream = System.IO.File.AppendText(GetPath("csv"));
        outStream.WriteLine(sb);
        outStream.Close();

        ParametersGiver parametersGiver = GetComponent<ParametersGiver>();
        string paramGiver = JsonUtility.ToJson(parametersGiver);

        outStream = System.IO.File.AppendText(GetPath("json"));
        outStream.WriteLine(paramGiver);
        outStream.Close();

    }

    private string GetPath(string type)
    {
        return Application.dataPath + "/Data/" + fileName + "." + type;
    }

}
