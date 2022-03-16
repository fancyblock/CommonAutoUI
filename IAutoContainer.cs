﻿using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAutoContainer  
{
    void SetEnable(bool enable);
    int LAST_BTN_FRAME { get; set; }

    List<AutoWidget> WIDGETS { get; }

    IAutoWidget GetWidget(string name);
    T GetWidget<T>(string name);

    void SetEventEnable(bool enable);

    bool IsVirtual(string widgetName);
    void SetEnable(string widgetName, bool enable);
    void SetValue(string widgetName, string text);
    void SetValue(string widgetName, bool toggle);
    void SetValue(string widgetName, float progress);
    void SetButtonEvent(string widgetName, Action evt);
    void SetButtonEnable(string widgetName, bool enable);
    void SetImageFillAmount(string widgetName, float value);
    float GetImageFillAmount(string widgetName);
    void PlayAnimation(string widgetName, string aniName);
    void SetToggleValue(string widgetName, bool isOn);
    void SetToggleChangeEvent(string widgetName, Action<bool> onToggleChange);

    void ScrollLoad(string widgetName, int count, Action<int, GameObject> setFunc);
    void ShowTab(string widgetName, int index);
    void ShowTab(string widgetName, string name);


    void Bind<T>(string widgetName, IBindingData data, string fieldName) where T : BaseBindingWidget;

    void SetButtonClkHook(Action<string> hook);

    GameObject GetExObject(int index);
}
