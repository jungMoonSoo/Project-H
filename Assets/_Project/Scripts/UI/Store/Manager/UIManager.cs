using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private void Start()
    {
        StoreLobbyManager lm = StoreLobbyManager.Instance;
        lm.SettingButton();
    }
}
