using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    #region SETTER
    public static void SetLevelIndex(int value) => PlayerPrefs.SetInt("LevelIndex", value);
    public static void SetLevelNumber(int value) => PlayerPrefs.SetInt("LevelNumber", value);
    public static void SetCoin(int value) => PlayerPrefs.SetInt("Coin", value);
    #endregion

    #region GETTER
    public static int GetLevelIndex() => PlayerPrefs.GetInt("LevelIndex");
    public static int GetLevelNumber() => PlayerPrefs.GetInt("LevelNumber", 1);
    public static int GetCoin(int defaultValue) => PlayerPrefs.GetInt("Coin", defaultValue);
    #endregion
}
