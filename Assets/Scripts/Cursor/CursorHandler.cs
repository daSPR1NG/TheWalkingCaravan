using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorType
{
    Default,
    Enemy,
    Ressource,
}

[System.Serializable]
public class CursorAppearance
{
    [Header("GENERAL SETTINGS")]
    public string appearanceName;
    public CursorType cursorType = CursorType.Default;
    public bool isSelected = false;

    [Space] [Header("VISUAL SETTINGS")]
    public List<Texture2D> cursorTextures;

    [Space] [Header("POSITION SETTINGS")]
    public Vector2 offsetPosition;
    public float updateDelayValue = 0.15f;
    
    public int frameCount => cursorTextures.Count;
}

public class CursorHandler : MonoBehaviour
{
    [Header("APPEARANCE SETTINGS")]
    public List<CursorAppearance> cursorAppearances;

    [SerializeField] private float frameRate = 0.1f;
    private int currentFrame = 0;
    private float frameTimer = 0f;
    public bool cursorIsConfined = false;
    public bool cursorIsLocked= false;

    public static CursorHandler Instance;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetCursorAppearance(CursorType.Default);
    }

    private void LateUpdate()
    {
        UpdateCursorAppearance();
    }

    public void SetCursorAppearance(CursorType cursorType)
    {
        CursorAppearance currentCursorAppearance = null;

        for (int i = 0; i < cursorAppearances.Count; i++)
        {
            cursorAppearances [ i ].isSelected = false;

            if (cursorAppearances [ i ].cursorType == cursorType)
            {
                currentCursorAppearance = cursorAppearances [ i ];
            }
        }

        currentCursorAppearance.isSelected = true;

        Cursor.SetCursor(currentCursorAppearance.cursorTextures[0], currentCursorAppearance.offsetPosition, CursorMode.Auto);
        currentFrame = 0;
    }

    private void UpdateCursorAppearance()
    {
        if (GetCurrentCursorAppearance().cursorTextures.Count <= 1) return;

        frameTimer -= Time.deltaTime;

        if (frameTimer <= 0f)
        {
            frameTimer += frameRate;
            currentFrame = (currentFrame + 1) % GetCurrentCursorAppearance().frameCount;
            Cursor.SetCursor(GetCurrentCursorAppearance().cursorTextures[currentFrame], GetCurrentCursorAppearance().offsetPosition, CursorMode.Auto);
        }
    }

    private CursorAppearance GetCurrentCursorAppearance()
    {
        foreach (CursorAppearance thisAppearance in cursorAppearances)
        {
            if (thisAppearance.isSelected)
            {
                return thisAppearance;
            }
        }

        return null;
    }

    #region Editor
    private void OnValidate()
    {
        foreach (CursorAppearance thisCursorAppearance in cursorAppearances)
        {
            thisCursorAppearance.appearanceName = thisCursorAppearance.cursorType.ToString();
        }
    }
    #endregion
}
