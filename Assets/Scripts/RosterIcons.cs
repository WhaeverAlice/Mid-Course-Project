using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RosterIcons : MonoBehaviour
{
    [SerializeField] private CharSwitcher CharSwitcher;
    [SerializeField] private Image ActiveIcon;
    [SerializeField] private Image InactiveLeft;
    [SerializeField] private Image InactiveRight;
    [SerializeField] private Sprite WaterIcon; 
    [SerializeField] private Sprite TeleportIcon; 
    [SerializeField] private Sprite BashIcon; 

    // Start is called before the first frame update
    void Start()
    {
        GetCharIcon();
    }

    // Update is called once per frame
    void Update()
    {
        GetCharIcon();
        //if (PlayableCharacter.dead) this.gameObject.SetActive(false);
    }

    public void GetCharIcon()
    {
        string name = CharSwitcher.activeChar.name;
        switch (name) 
        {
            case "WaterPlayer":
                ActiveIcon.sprite = WaterIcon;
                InactiveLeft.sprite = BashIcon;
                InactiveRight.sprite = TeleportIcon;
                break;
            case "TeleportPlayer":
                ActiveIcon.sprite = TeleportIcon;
                InactiveLeft.sprite = WaterIcon;
                InactiveRight.sprite = BashIcon;
                break;
            case "BashPlayer":
                ActiveIcon.sprite = BashIcon;
                InactiveLeft.sprite = TeleportIcon;
                InactiveRight.sprite = WaterIcon;
                break;
        }
    }
}
