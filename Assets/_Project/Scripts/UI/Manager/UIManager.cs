using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private void Start()
    {
        LobbyManager lm = LobbyManager.Instance;
        lm.SettingButton();
    }
}
