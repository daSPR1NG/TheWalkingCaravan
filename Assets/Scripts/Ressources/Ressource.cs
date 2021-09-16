using UnityEngine;
using UnityEditor;

public enum RessourceType
{
    Unassigned,
    Wood,
    Minerals,
    Food,
    Herbs,
    Leather,
}

[System.Serializable]
public class Ressource
{
    [Header("GENERAL SETTINGS")]
    public string ressourceName;
    public RessourceType ressourceType;

    [Space]

    [Header("VALUES")]
    public float startingValue = 0f;
    public float currentValue;
    public float maxValue;

    [Space]

    [Header("UI SETTINGS")]
    public Sprite ressourceIcon;

    public delegate void RessourceValueHandler(RessourceType ressourceType);
    public event RessourceValueHandler OnRessourceValueChanged;

    public void InitValue(float startingValue)
    {
        this.startingValue = startingValue;

        OnRessourceValueChanged?.Invoke(ressourceType);
    }

    public void AddToCurrentValue(float valueToAdd)
    {
        currentValue += valueToAdd;

        if (maxValue != 0 && currentValue >= maxValue)
        {
            currentValue = maxValue;
        }

        OnRessourceValueChanged?.Invoke(ressourceType);

    }

    public void RemoveToCurrentValue(float valueToRemove)
    {
        currentValue -= valueToRemove;

        if (currentValue <= 0)
        {
            currentValue = 0;
        }

        OnRessourceValueChanged?.Invoke(ressourceType);
    }
}