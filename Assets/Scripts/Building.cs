using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Buildings", fileName = "Building_", order = 0)]
public class Building : ScriptableObject
{
    public string buildingName;
    public Sprite buildingIcon;
    public GameObject ObjectToBuild;
    public List<Ressource> needRessourcesToBuild;

    private void OnValidate()
    {
        Editor_SetRessourcesDatas();
    }

    private void Editor_SetRessourcesDatas()
    {
        for (int i = 0; i < needRessourcesToBuild.Count; i++)
        {
            needRessourcesToBuild [ i ].ressourceName = needRessourcesToBuild [ i ].ressourceType.ToString();
        }

        //for (int i = 0; i < RessourcesHandler.Instance.characterRessources.Count; i++)
        //{
        //    if (RessourcesHandler.Instance.characterRessources [ i ].ressourceType == needRessourcesToBuild [ i ].ressourceType)
        //    {
        //        needRessourcesToBuild [ i ].ressourceIcon = RessourcesHandler.Instance.characterRessources [ i ].ressourceIcon;
        //    }
        //}
    }
}