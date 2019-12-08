using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour  // to jest kolejny widok dla playera
{

    public Transform helthBar;
    public Text level;

   // private RectTransform _dynamicHealthBar;


    private void Start()
    {
        //_dynamicHealthBar = GetComponent<RectTransform>();
    }

    public void SetSize(float normalizedSize)
    {
        helthBar.localScale = new Vector3(normalizedSize, 1f);
    }
}
