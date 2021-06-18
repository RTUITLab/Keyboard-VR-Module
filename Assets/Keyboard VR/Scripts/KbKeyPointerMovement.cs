using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.Extras;

/// <summary>
/// Тестовый метод печати на клавиатуре. Нажатие производится «тычком» контроллера вперед,
/// в сторону желаемой клавиши. Нужно протестировать, насколько данный способ ввода удобен.
/// </summary>
public class KbKeyPointerMovement : KbKeyPointer
{
    [SerializeField] private Gradient colorProgress;
    [SerializeField] private float distanceToActivation = 0.4f;
    private float distanceTravelled;
    private float touchProgress = 0f;

    private Vector3 keyPosition;
    private float startDistance;
    private KbKey targetKey;
    
    private new void Awake()
    {
        laserPointer.PointerClick -= PointerClick;

        laserPointer.PointerIn += StartTracking;
        laserPointer.PointerOut += EndTracking;
    }

    private void Update()
    {
        TrackClick();
        SetLaserColor();
    }

    private void TrackClick()
    {
        if (keyPosition == Vector3.zero) {
            touchProgress = 0f;
            return;
        }

        // Считывание прогресса нажатия на клавишу.
        distanceTravelled = startDistance - Vector3.Distance(transform.position, keyPosition);

        touchProgress = distanceTravelled / distanceToActivation;

        // При оттягивании руки далеко назад происходит сброс расстояния.
        if (distanceTravelled < 0)
        {
            startDistance = Vector3.Distance(transform.position, keyPosition);
        }

        if (distanceTravelled > distanceToActivation)
        {
            PointerClick();
        }
    }


    private void SetLaserColor()
    {
        laserPointer.color = colorProgress.Evaluate(touchProgress);
    }

    private void StartTracking(object sender, PointerEventArgs e)
    {
        KbKey button = e.target.GetComponent<KbKey>();

        keyPosition = button.transform.position;
        startDistance = Vector3.Distance(transform.position, keyPosition);
    }

    private void EndTracking(object sender, PointerEventArgs e)
    {
        keyPosition = Vector3.zero;
        startDistance = 0;
    }

    public void PointerClick()
    {
        KbKey button = targetKey.GetComponent<KbKey>();

        if (button != null)
        {
            if (button.EventClick != null)
            {
                button.EventClick.Invoke();
            }
        }
    }
}
