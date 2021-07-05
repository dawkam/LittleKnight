using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatVisualer : MonoBehaviour
{
    VillagerAgent villagerAgent;
    ParametersGiver parametersGiver;

    [SerializeField] GameObject thirstBar;
    [SerializeField] GameObject hungerBar;
    [SerializeField] GameObject staminaBar;

    private void Start()
    {
        parametersGiver = GameObject.FindObjectOfType<ParametersGiver>();
        villagerAgent = GetComponentInParent<VillagerAgent>();
    }

    private void Update()
    {
        SetSize(thirstBar.transform, villagerAgent.ThirstCurrent / parametersGiver.ThirstMax);
        SetSize(hungerBar.transform, villagerAgent.HungerCurrent / parametersGiver.HungerMax);
        SetSize(staminaBar.transform, villagerAgent.StaminaCurrent / parametersGiver.ComfortMin);
    }

    public void SetSize(Transform bar, float normalizedSize)
    {
        bar.localScale = new Vector3(normalizedSize, 1f);
    }
}
