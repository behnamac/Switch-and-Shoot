using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    [SerializeField] private PanelController[] panels;
    private Dictionary<string, PanelController> panelDic;

    [Header("Buttons")]
    public Button SwichButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI currencyText;

    [Header("About Player")]
    public Joystick Joystick;
    public FixedTouchField TouchField;
    public Transform shooterAim;
    [SerializeField] private int firstCurrency;
    [SerializeField] private Transform screenWidthPoint;
    [SerializeField] private Transform screenHeightPoint;
    [SerializeField] private Image enemyImagePrefab;
    [SerializeField] private Transform enemyImageParent;
    [HideInInspector] public int Currency;
    private List<Image> _enemyImages;
    #region Unity Function
    private void Awake()
    {
        Instance = this;

        GameManager.onLevelStart += onLevelStart;
        GameManager.onLevelCompelet += onLevelCompelet;
        GameManager.onLevelFail += onLevelFail;
        _enemyImages = new List<Image>();

        panelDic = new Dictionary<string, PanelController>();
        for (int i = 0; i < panels.Length; i++)
        {
            panelDic.Add(panels[i].panelName, panels[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (levelNumberText != null)
            levelNumberText.text = "Level " + PlayerPrefsManager.GetLevelNumber();
        else
            Debug.LogWarning("Set Level_text");

        ActivePanel("Start");
        Currency = PlayerPrefsManager.GetCoin(firstCurrency);
        currencyText.text = Currency.ToString();
    }
    private void OnDestroy()
    {
        GameManager.onLevelStart -= onLevelStart;
        GameManager.onLevelCompelet -= onLevelCompelet;
        GameManager.onLevelFail -= onLevelFail;
    }
    #endregion

    #region Events
    void onLevelStart()
    {
        ActivePanel("GamePlay");
    }
    void onLevelCompelet()
    {
        ActivePanel("Compelet");
    }
    void onLevelFail()
    {
        ActivePanel("Fail");
    }
    #endregion

    #region Public Functions
    public void ActivePanel(string panelname)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].panel.SetActive(false);
        }

        panelDic[panelname].panel.SetActive(true);
    }
    public void UpdateShooterAim(Vector2 axis) 
    {
        Vector2 pos = shooterAim.localPosition;
        pos += axis;

        float width = screenWidthPoint.localPosition.x;
        float height = screenHeightPoint.localPosition.y;
        pos = new Vector2(Mathf.Clamp(pos.x, -width, width), Mathf.Clamp(pos.y, -height, height));

        shooterAim.localPosition = pos;
    }
    public void AddCurrency(int value) 
    {
        Currency += value;
        PlayerPrefsManager.SetCoin(Currency);
        currencyText.text = Currency.ToString();
    }
    public void SpawnEnemyImage(int numberSpawn) 
    {
        for (int i = 0; i < numberSpawn; i++)
        {
            var image = Instantiate(enemyImagePrefab, enemyImageParent);
            _enemyImages.Add(image);
        }
    }
    public void UpdateEnemyImage(int targetIndex) 
    {
        for (int i = 0; i < _enemyImages.Count; i++)
        {
            if (targetIndex > i) 
            {
                _enemyImages[i].color = Color.red;
            }
        }
    }
    //Buttons
    public void StartButton()
    {
        GameManager.Instance.LevelStart();
    }
    public void ResetLevelButton()
    {
        GameManager.Instance.ResetLevel();
    }
    #endregion
}
[System.Serializable]
public class PanelController
{
    public string panelName;
    public GameObject panel;
}
