using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectable
{
    public abstract void OnMouseEnter();
    public abstract void OnMouseExit();

    public static class IDetectableExtension
    {
        public static void SetCursorAppearanceOnDetection(CursorType cursorType, OutlineModule outlineComponent, bool isOutlineEnabled, string debugMessage)
        {
            CursorController.Instance.SetCursorAppearance(cursorType);

            outlineComponent.enabled = isOutlineEnabled;

            UtilityClass.DebugMessage(debugMessage);
        }
    }
}