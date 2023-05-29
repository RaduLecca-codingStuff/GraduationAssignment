using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    Slider _sfx;
    Slider _bkg;
    // Start is called before the first frame update
    void Start()
    {
        _sfx = transform.Find("Volume").Find("SFXVolumeSlider").GetComponent<Slider>();
        _bkg = transform.Find("Volume").Find("BKGVolumeSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.sfxVolume = _sfx.value;
        GameManager.bkgVolume = _bkg.value;
    }
}
