using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour  // to jest kolejny widok dla playera
{

    public Transform helthBar;
    public Text level;
    new public Text name;

   // private RectTransform _dynamicHealthBar;

    public void SetSize(float normalizedSize)
    {
        helthBar.localScale = new Vector3(normalizedSize, 1f);
    }
    public void SetName(string name)
    {
        if (name != null)
            this.name.text = name;
    }

    public void SetLvl(string lvl)
    {
        level.text = lvl;
    }
}
