using System.Collections.Generic;
using UnityEngine;

public class RessourcesHandler : MonoBehaviour
{
    [Header("OVERTIME INCOME SETTINGS")]
    public bool usesOvertimeIncome = false;
    public float overtimeIncome = 5f;
    public float overtimeDelay = 0.15f;

    [Header("RESSOURCES")]
    public List<Ressource> characterRessources;
    
    [Header("UI COMPONENTS")]
    public Transform ressourcesParentLayout;
    [SerializeField] private int instanceValue = 0;
    [SerializeField] private GameObject ressourceUICompartment;
    private List<RessourceUI> ressourceUIs = new List<RessourceUI>();

    public static RessourcesHandler Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        SubscribeRessourceEvent();
    }

    private void OnDisable()
    {
        UnsubscribeRessourceEvent();
    }

    void Start()
    {
        InstanciateRessourcesUICompartment();

        if (usesOvertimeIncome) InvokeRepeating(nameof(IncreaseRessourcesOvertime), overtimeDelay, 1);
    }

    private void Update()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.M))
        {
            GetThisRessource(RessourceType.Minerals).AddToCurrentValue(600);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetThisRessource(RessourceType.Minerals).RemoveToCurrentValue(600);
        }
    }

    public Ressource GetThisRessource(RessourceType wantedRessourceType)
    {
        Ressource ressource;

        foreach (Ressource thisRessource in characterRessources)
        {
            if (thisRessource.ressourceType == wantedRessourceType)
            {
                ressource = thisRessource;
                return ressource;
            }
        }

        return null;
    }

    private void SetRessourceDatas(RessourceType ressourceType)
    {
        foreach (RessourceUI thisRessourceUI in ressourceUIs)
        {
            if (thisRessourceUI.ressourceType == ressourceType)
            {
                thisRessourceUI.UpdateRessourceValue(GetThisRessource(ressourceType).currentValue);
            }
        }
    }

    private void IncreaseRessourcesOvertime()
    {
        foreach (Ressource thisRessource in characterRessources)
        {
            thisRessource.AddToCurrentValue(overtimeIncome);
        }
    }

    #region UI
    private void InstanciateRessourcesUICompartment()
    {
        for (int i = 0; i < instanceValue; i++)
        {
            GameObject prefab = Instantiate(ressourceUICompartment, ressourcesParentLayout);

            RessourceUI ressourceUI = prefab.GetComponent<RessourceUI>();
            ressourceUIs.Add(ressourceUI);
            ressourceUI.SetThisUI(characterRessources[ i ].ressourceType, characterRessources [ i ].currentValue, characterRessources [ i ].ressourceIcon);
        }
    }
    #endregion

    #region Event
    private void SubscribeRessourceEvent()
    {
        foreach (Ressource thisRessource in characterRessources)
        {
            thisRessource.OnRessourceValueChanged += SetRessourceDatas;
        }
    }

    private void UnsubscribeRessourceEvent()
    {
        foreach (Ressource thisRessource in characterRessources)
        {
            thisRessource.OnRessourceValueChanged -= SetRessourceDatas;
        }
    }
    #endregion

    #region Editor
    private void OnValidate()
    {
        Editor_SetRessourcesDatas();
    }

    private void Editor_SetRessourcesDatas()
    {
        for (int i = 0; i < characterRessources.Count; i++)
        {
            characterRessources [ i ].ressourceName = characterRessources [ i ].ressourceType.ToString();
            characterRessources [ i ].currentValue = characterRessources [ i ].startingValue;
        }

        instanceValue = System.Enum.GetValues(typeof(RessourceType)).Length - 1;
    }
    #endregion
}
