using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVManager : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();
    private void Start()
    {
        string[] rowDataTemp = new string[26];
        rowDataTemp[0] = "collectorsCount";
        rowDataTemp[1] = "collectorsDeathByMonster";
        rowDataTemp[2] = "collectorsDeathByHunger";
        rowDataTemp[3] = "collectorsDeathByThirst";
        rowDataTemp[4] = "lumberjacksCount";
        rowDataTemp[5] = "lumberjacksDeathByMonster";
        rowDataTemp[6] = "lumberjacksDeathByHunger"; ;
        rowDataTemp[7] = "lumberjacksDeathByThirst";
        rowDataTemp[8] = "artisansCount";
        rowDataTemp[9] = "artisansDeathByMonster";
        rowDataTemp[10] = "artisansDeathByHunger";
        rowDataTemp[11] = "artisansDeathByThirst";
        rowDataTemp[12] = "babysCount";
        rowDataTemp[13] = "babysDeathByMonster";
        rowDataTemp[14] = "babysDeathByHunger";
        rowDataTemp[15] = "babysDeathByThirst";
        rowDataTemp[16] = "mayorDeathReson";
        rowDataTemp[17] = "warehouseFoodCountAverage";
        rowDataTemp[18] = "warehouseFoodCountMax";
        rowDataTemp[19] = "warehouseFoodCountMin";
        rowDataTemp[20] = "sceneFoodCountAverage ";
        rowDataTemp[21] = "warehouseWoodCountAverage";
        rowDataTemp[22] = "warehouseWoodCountMax";
        rowDataTemp[23] = "warehouseWoodCountMin";
        rowDataTemp[24] = "sceneTreeCountAverage";
        rowDataTemp[25] = "sceneWoodCountAverage";
        rowData.Add(rowDataTemp);
    }

    public void AddData(ResearchData researchData)
    {
        string[] rowDataTemp = new string[27];
        rowDataTemp[0] = researchData.collectorsCount.ToString();
        rowDataTemp[1] = researchData.collectorsDeathByMonster.ToString();
        rowDataTemp[2] = researchData.collectorsDeathByHunger.ToString();
        rowDataTemp[3] = researchData.collectorsDeathByThirst.ToString();
        rowDataTemp[4] = researchData.lumberjacksCount.ToString();
        rowDataTemp[5] = researchData.lumberjacksDeathByMonster.ToString();
        rowDataTemp[6] = researchData.lumberjacksDeathByHunger.ToString();
        rowDataTemp[7] = researchData.lumberjacksDeathByThirst.ToString();
        rowDataTemp[8] = researchData.artisansCount.ToString();
        rowDataTemp[9] = researchData.artisansDeathByMonster.ToString();
        rowDataTemp[10] = researchData.artisansDeathByHunger.ToString();
        rowDataTemp[11] = researchData.artisansDeathByThirst.ToString();
        rowDataTemp[12] = researchData.babysCount.ToString();
        rowDataTemp[13] = researchData.babysDeathByMonster.ToString();
        rowDataTemp[14] = researchData.babysDeathByHunger.ToString();
        rowDataTemp[15] = researchData.babysDeathByThirst.ToString();
        rowDataTemp[16] = researchData.mayorDeathReson.ToString();
        rowDataTemp[17] = researchData.warehouseFoodCountAverage.ToString();
        rowDataTemp[18] = researchData.warehouseFoodCountMax.ToString();
        rowDataTemp[19] = researchData.warehouseFoodCountMin.ToString();
        rowDataTemp[20] = researchData.sceneFoodCountAverage.ToString();
        rowDataTemp[21] = researchData.warehouseWoodCountAverage.ToString();
        rowDataTemp[22] = researchData.warehouseWoodCountMax.ToString();
        rowDataTemp[23] = researchData.warehouseWoodCountMin.ToString();
        rowDataTemp[24] = researchData.sceneTreeCountAverage.ToString();
        rowDataTemp[25] = researchData.sceneWoodCountAverage.ToString();
        rowData.Add(rowDataTemp);
    }

    public void SaveData()
    {
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        StreamWriter outStream = System.IO.File.CreateText(GetPath("csv"));
        outStream.WriteLine(sb);
        outStream.Close();

        ParametersGiver parametersGiver = GetComponent<ParametersGiver>();
        string paramGiver = JsonUtility.ToJson(parametersGiver);

        outStream = System.IO.File.CreateText(GetPath("json"));
        outStream.WriteLine(paramGiver);
        outStream.Close();
    }

    private string GetPath(string type)
    {

        return Application.dataPath + "/Data/" + type + "/" + DateTime.Now.ToString("MM'-'dd'-'yyyy'_'hh'-'mm'-'ss") + "." + type;

    }

}
