using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class KbKeyPointer : MonoBehaviour
{
    [SerializeField] protected SteamVR_LaserPointer laserPointer;

    private void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        KbKey button = e.target.GetComponent<KbKey>();

        if (button != null)
        {
            if (button.EventClick != null)
            {
                button.EventClick.Invoke();
            }
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        KbKey button = e.target.GetComponent<KbKey>();

        if (button != null)
        {
            if (button.EventInside != null)
            {
                button.EventInside.Invoke();
            }
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        KbKey button = e.target.GetComponent<KbKey>();

        if (button != null)
        {
            if (button.EventOutside != null)
            {
                button.EventOutside.Invoke();
            }
        }
    }
}
