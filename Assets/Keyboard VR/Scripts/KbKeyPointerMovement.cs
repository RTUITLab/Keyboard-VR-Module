﻿using System;
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
    [SerializeField] private float touchAccuracy = 1f; // При меньшем значении меньше усилий для нажатия.
    private float touchProgress = 0;
    private PointerEventHandler click;

    private new void Awake()
    {
        base.Awake();

        laserPointer.PointerOut += ResetClick;

        // Переподписываеся, у нас собственный метод считывания нажатия.
        laserPointer.PointerClick -= PointerClick; 
        click += PointerClick;
    }

    private void Update()
    {
        TrackClick();
        SetLaserColor();
    }

    /// <summary>
    /// Сброс прогресса нажатия при остановке наведения на клавишу.
    /// </summary>
    private void ResetClick(object sender, PointerEventArgs e)
    {
        touchProgress = 0;
    }

    private void TrackClick()
    {
        // Здесь будет происходить считывание прогресса нажатия на клавишу.


        if (true)
        {
            // В случае успешного нажатия вызываем click и сбрасываем прогресс.

            click.Invoke(default, default);
        }

        throw new NotImplementedException();
    }

    private void SetLaserColor()
    {
        laserPointer.color = colorProgress.Evaluate(touchProgress);
    }
}