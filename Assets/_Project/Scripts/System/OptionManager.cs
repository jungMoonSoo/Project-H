using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : Singleton<OptionManager>
{
    [SerializeField] private GameObject optionUiObject;
    
    [SerializeField] private Slider sldMasterVolume;
    [SerializeField] private Slider sldMusicVolume;
    [SerializeField] private Slider sldEffectVolume;
    [SerializeField] private Slider sldVoiceVolume;


    [NonSerialized] public float MasterVolume;
    [NonSerialized] public float MusicVolume;
    [NonSerialized] public float EffectVolume;
    [NonSerialized] public float VoiceVolume;


    void Start()
    {
        Init();
    }
    

    public void Init()
    {
        sldMasterVolume.onValueChanged.AddListener((value) => MasterVolume = value);
        sldMusicVolume.onValueChanged.AddListener((value) => MusicVolume = value);
        sldEffectVolume.onValueChanged.AddListener((value) => EffectVolume = value);
        sldVoiceVolume.onValueChanged.AddListener((value) => VoiceVolume = value);
    }

    private void FirstSetting()
    {
        
    }
    
    public void OpenUi() => optionUiObject.SetActive(true);
    public void CloseUi() => optionUiObject.SetActive(false);
}

public enum VolumeType
{
    Master,
    Music,
    Effect,
    Voice
}