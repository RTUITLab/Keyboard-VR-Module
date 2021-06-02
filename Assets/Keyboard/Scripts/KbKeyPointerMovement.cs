using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

/// <summary>
/// Тестовый метод печати на клавиатуре. Нажатие производится «тычком» контроллера вперед,
/// в сторону желаемой клавиши. Нужно протестировать, насколько данный способ ввода удобен.
/// </summary>
public class KbKeyPointerMovement : KbKeyPointer
{
    [SerializeField] private Gradient colorPrograss;
    [SerializeField] private float touchAccuracy; // При меньшем значении меньше усилий для нажатия.
    private float touchProgress = 0;

    private void Awake()
    {
        laserPointer.PointerOut += ResetTouch;
        laserPointer.PointerClick -= PointerClick; // У нас собственный метод считывания нажатия.
    }

    private void Update()
    {
        TrackTouch();
        SetLaserColor();
    }

    /// <summary>
    /// Сброс прогресса нажатия при остановке наведения на клавишу.
    /// </summary>
    private void ResetTouch(object sender, PointerEventArgs e)
    {
        touchProgress = 0;
    }

    private void TrackTouch()
    {
        // Здесь будет происходить считывание прогресса нажатия на клавишу.

        // В случае успешного нажатия вызываем PointerClick() и сбрасываем прогресс.

        throw new NotImplementedException();
    }

    private void SetLaserColor()
    {
        laserPointer.color = colorPrograss.Evaluate(touchProgress);
    }
}
