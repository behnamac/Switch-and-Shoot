using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Events
    public static UnityAction onLoadLevel;
    public static UnityAction onLevelStart;
    public static UnityAction onLevelCompelet;
    public static UnityAction onLevelFail;
    #endregion

    #region Private Field
    [Header("Level Controller")]
    [SerializeField] LevelSorce[] levelSorce;
    [SerializeField] bool getLevelRandom;
    [SerializeField] int returnLevelIndex;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < levelSorce.Length; i++)
        {
            levelSorce[i].level.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    private void OnDrawGizmos()
    {
        returnLevelIndex = Mathf.Clamp(returnLevelIndex, 0, levelSorce.Length - 1);
    }
    #endregion

    #region Invok Events
    void LoadLevel()
    {
        GetLevel().SetActive(true);
        onLoadLevel?.Invoke();

        int index = PlayerPrefsManager.GetLevelIndex();
        LevelSorce level = levelSorce[index];
        if (level.changeSkybox)
        {
            RenderSettings.skybox = level.skyBox;
        }
        RenderSettings.fog = level.fog;
        RenderSettings.fogColor = level.colorFog;
    }
    public void LevelStart()
    {
        onLevelStart?.Invoke();
    }
    public void LevelCompelet()
    {
        onLevelCompelet?.Invoke();
        PlayerPrefsManager.SetLevelIndex(PlayerPrefsManager.GetLevelIndex() + 1);
        PlayerPrefsManager.SetLevelNumber(PlayerPrefsManager.GetLevelNumber() + 1);
    }
    public void LevelFail()
    {
        onLevelFail?.Invoke();
    }
    #endregion

    #region Public Function
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region Return Functions
    GameObject GetLevel()
    {
        int level = PlayerPrefsManager.GetLevelIndex();

        if (level >= levelSorce.Length)
        {
            if (getLevelRandom)
            {
                int r = Random.Range(returnLevelIndex, levelSorce.Length);
                PlayerPrefsManager.SetLevelIndex(r);
                return levelSorce[r].level;
            }
            else
            {
                PlayerPrefsManager.SetLevelIndex(returnLevelIndex);
                return levelSorce[returnLevelIndex].level;
            }
        }

        return levelSorce[level].level;
    }
    #endregion
}

#region Class's
[System.Serializable]
public class LevelSorce
{
    public GameObject level;
    public bool changeSkybox;
    [ConditionalHide("changeSkybox", true)] public Material skyBox;
    public bool fog;
    [ConditionalHide("fog", true)] public Color colorFog;
}
#endregion
