using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

/// <summary>
/// Тестовый метод печати на клавиатуре. Нажатие производится «тычком» контроллера вперед,
/// в сторону желаемой клавиши. Нужно протестировать, насколько данный способ ввода удобен.
/// </summary>
public class KbKeyPointerMovement : KbKeyPointer
{
    private float touchProgres = 0;

    private void Awake()
    {
        laserPointer.PointerOut += ResetTouch;
    }

    private void Update()
    {
        TrackTouch();
    }

    /// <summary>
    /// Сброс прогресса нажатия при остановке наведения на клавишу.
    /// </summary>
    private void ResetTouch(object sender, PointerEventArgs e)
    {
        touchProgres = 0;
    }

    private void TrackTouch()
    {
        // Здесь будет происходить считывание прогресса нажатия на клавишу.

        throw new NotImplementedException();
    }
}
