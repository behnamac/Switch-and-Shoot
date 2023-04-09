using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Button button;
    public void Active(UpgradeController.UpgradeHolder holder) 
    {
        button.onClick.AddListener(() =>
        {
            UpgradeController.Instance.Upgrade(holder);
        });
    }
}
