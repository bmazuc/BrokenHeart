using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private BoolValue boolValue;
    [SerializeField] private GameObject isOn;
    [SerializeField] private GameObject isOff;

    private void Start()
    {
        isOn.SetActive(!boolValue.value);
        isOff.SetActive(boolValue.value);
    }

    public void SetIsMute(bool value)
    {
        boolValue.value = value;
    }

    public void CallPopup()
    {
        popup.gameObject.SetActive(true);
    }
}
