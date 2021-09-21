using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Buildings", fileName = "Building_SO_", order = 0)]
public class BuildingSO : ScriptableObject
{
    public string buildingName;
    public Sprite buildingIcon;
    public GameObject buildingPrefab;
    public List<Ressource> neededRessourcesToBuild;

    #region Editor
    private void OnValidate()
    {
        Editor_SetRessourcesDatas();
    }

    private void Editor_SetRessourcesDatas()
    {
        for (int i = 0; i < neededRessourcesToBuild.Count; i++)
        {
            neededRessourcesToBuild [ i ].ressourceName = neededRessourcesToBuild [ i ].ressourceType.ToString();
        }
    }
    #endregion
}