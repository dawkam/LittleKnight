using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements
{
    public List<float> elements;

    public Elements(float physical, float air, float water, float fire, float earth)
    {
        elements = new List<float>
        {
            physical,
            air,
            water,
            fire,
            earth
        };
    }

    public Elements(Elements other)
    {
        elements = new List<float>(other.elements); 
    }

    public void SetArmor(Elements other)
    {
        for (int i = 0; i < this.elements.Count; i++)
            if (other.elements[i] > 1)
                elements[i] = 1;
            else if (other.elements[i] < 0)
                elements[i] = 0;
            else
                elements[i] = other.elements[i];
    }

    public float GetTrueDamage(Elements other)
    {
        float result = 0;
        for (int i = 0; i < elements.Count; i++)
        {
            result += (other.elements[i] - other.elements[i] * elements[i]/100);
        }
        return result;
    }

    public void Add(Elements other)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i] += other.elements[i];

        }
    }

    public void Sub(Elements other)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i] -= other.elements[i];
        }
    }

    public void Mul(Elements other)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i] *= other.elements[i];
        }
    }

}
