using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Transform target;
    [SerializeField] float moveSpeed;
    [SerializeField] CameraSetting[] cameraSettings;


    Dictionary<string, CameraSetting> settingDic;
    Transform cam;
    bool xAxisFollow;

    private void Awake()
    {
        Instance = this;

        settingDic = new Dictionary<string, CameraSetting>();
        for (int i = 0; i < cameraSettings.Length; i++)
        {
            settingDic.Add(cameraSettings[i].settingName, cameraSettings[i]);
        }
        cam = Camera.main.transform;

        GameManager.onLevelStart += OnLevelStart;
    }

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraHolderControl();
    }
    private void FixedUpdate()
    {
    }
    private void OnDestroy()
    {
        GameManager.onLevelStart -= OnLevelStart;
    }

    void CameraHolderControl()
    {
        if (target != null)
        {
            Vector3 targetMove = target.position;
            if (!xAxisFollow)
                targetMove.x = 0;
            transform.position = Vector3.Lerp(transform.position, targetMove, moveSpeed/* * Time.fixedDeltaTime*/);
        }
    }
    
    public void ChangeCameraPos(string settingName)
    {
        var pos = settingDic[settingName].Pos;
        var rot = settingDic[settingName].Rot;
        xAxisFollow = settingDic[settingName].xAxisFollow;

        cam.DOLocalMove(pos, 0.5f);
        cam.DOLocalRotate(rot, 0.2f);
    }

    private void OnLevelStart() 
    {
        ChangeCameraPos("Normal");
    }
}
[System.Serializable]
public class CameraSetting
{
    public string settingName;
    public bool xAxisFollow;
    public Vector3 Pos;
    public Vector3 Rot;
}
