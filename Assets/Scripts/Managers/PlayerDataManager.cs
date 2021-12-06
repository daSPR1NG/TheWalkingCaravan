using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [Header("PLAYER")]
    public Transform playerCharacterTransform;

    [Space]
    [Header("PLAYER INFORMATIONS")]
    public Vector3 playerPosition;

    #region Singleton
    public static PlayerDataManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetPlayerCharacterTransform();
        }
    }
    #endregion

    public void SavePlayerPosition()
    {
        playerPosition = GetCharacterPosition(playerCharacterTransform);
        //Save it with PlayerPrefs
    }

    public Vector3 GetCharacterPosition(Transform observedTransform)
    {
        return observedTransform.position;
    }

    public void SetCharacterPosition(Transform observedTransform, Vector3 newPos)
    {
        SetPlayerCharacterTransform();
        observedTransform.position = newPos;
    }

    private void SetPlayerCharacterTransform()
    {
        playerCharacterTransform = transform;
    }
}