using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterFrameHandler : MonoBehaviour
{
    //Debug set to private
    public TeamManager.Unit unitRef;
    public Image iconImage;

    public void SetUnitRef(TeamManager.Unit newUnit)
    {
        unitRef = newUnit;
        SetCharacterFrameIcon(newUnit.icon);
    }

    public void SetCharacterFrameIcon(Sprite newIconSprite)
    {
        iconImage.sprite = newIconSprite;
    }

}