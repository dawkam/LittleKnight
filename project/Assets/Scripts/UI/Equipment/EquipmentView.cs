using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentView : MonoBehaviour
{
    public Text defStats;
    public Text defValue;
    public Text dmgStats;
    public Text dmgValue;


    public void SetDescription(string[] stats)
    {
        defStats.text = stats[0];
        defValue.text = stats[1];
        dmgStats.text = stats[2];
        dmgValue.text = stats[3];
    }

}
