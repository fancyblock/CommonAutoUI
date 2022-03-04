using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class AutoContainer : MonoBehaviour, IAutoContainer
{
    [SerializeField] private string m_id;
    [SerializeField] private List<string> m_widgetNames = new List<string>();
    [SerializeField] private List<AutoWidget> m_widgets = new List<AutoWidget>();
    [SerializeField] private List<GameObject> exObjects;

    [SerializeField] private bool m_allowVirtualWidget = true;

    private bool m_init = false;
    private Dictionary<string, AutoWidget> m_widgetDic = new Dictionary<string, AutoWidget>();

    private Action<string> m_buttonClkHook;


#if UNITY_EDITOR

    public void ClearWidgets()
    {
        m_widgetNames.Clear();
        m_widgets.Clear();
    }

    public void AddWidget(string name, AutoWidget widget)
    {
        m_widgetNames.Add(name);
        m_widgets.Add(widget);
    }

#endif

    /// <summary>
    /// init 
    /// </summary>
    void Awake()
    {
        init();
    }

    public GameObject GetExObject(int index)
    {
        return exObjects[index];
    }

    /// <summary>
    /// 启用/关闭 
    /// </summary>
    /// <param name="enable"></param>
    public void SetEnable( bool enable )
    {
        gameObject.SetActive(enable);
    }

    /// <summary>
    /// 获取控件
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IAutoWidget GetWidget(string name)
    {
        if (m_widgetDic.ContainsKey(name))
            return m_widgetDic[name];

        int widgetIndex = m_widgetNames.IndexOf(name);
        if (widgetIndex >= 0)
            return m_widgets[widgetIndex];

        if(m_allowVirtualWidget)
            return VirtualWidget.Instance;

        return null;
    }

    /// <summary>
    /// 获取控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetWidget<T>(string name)
    {
        if (m_widgetDic.ContainsKey(name))
            return m_widgetDic[name].GetComponent<T>();

        int widgetIndex = m_widgetNames.IndexOf(name);
        if (widgetIndex >= 0)
            return m_widgets[widgetIndex].GetComponent<T>();

        return default(T);
    }

    public int LAST_BTN_FRAME { get; set; }

    /// <summary>
    /// 所有控件Enable/Disable
    /// </summary>
    /// <param name="enable"></param>
    public void SetEventEnable( bool enable )
    {
        foreach (AutoWidget widget in m_widgets)
        {
            widget.SetEventEnable(enable);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected void init()
    {
        if (m_init)
            return;

        for (int i = 0; i < m_widgetNames.Count; i++)
        {
            Assert.IsFalse(m_widgetDic.ContainsKey(m_widgetNames[i]), m_widgetNames[i] + " is already exist in " + gameObject.name);

            AutoWidget widget = m_widgets[i];
            widget.SetButtonClkHook(buttonClkHook);

            m_widgetDic.Add(m_widgetNames[i], widget);
        }

        m_init = true;
    }

    public bool IsVirtual(string widgetName)
    {
        return GetWidget(widgetName).IsVirtual();
    }

    public void SetEnable(string widgetName, bool enable)
    {
        GetWidget(widgetName).SetEnable(enable);
    }

    public void SetValue(string widgetName, string text)
    {
        GetWidget(widgetName).SetValue(text);
    }

    public void SetValue(string widgetName, bool toggle)
    {
        GetWidget(widgetName).SetValue(toggle);
    }

    public void SetValue(string widgetName, float progress)
    {
        GetWidget(widgetName).SetValue(progress);
    }

    public void SetButtonEvent(string widgetName, Action evt)
    {
        GetWidget(widgetName).SetButtonEvent(evt);
    }

    public void SetButtonEnable(string widgetName, bool enable)
    {
        GetWidget(widgetName).SetButtonEnable(enable);
    }

    public void SetImageFillAmount(string widgetName, float value)
    {
        GetWidget(widgetName).SetImageFillAmount(value);
    }

    public float GetImageFillAmount( string widgetName )
    {
        return GetWidget(widgetName).GetImageFillAmount();
    }

    public void PlayAnimation( string widgetName, string aniName )
    {
        GetWidget(widgetName).PlayAnimation(aniName);
    }

    public void ScrollLoad(string widgetName, int count, Action<int, GameObject> setFunc)
    {
        GetWidget(widgetName).ScrollLoad(count, setFunc);
    }


    public void SetButtonClkHook(Action<string> hook)
    {
        m_buttonClkHook = hook;
    }

    public void Bind<T>(string widgetName, IBindingData data, string fieldName) where T : BaseBindingWidget
    {
        T bindingWidget = GetWidget<T>(widgetName);

        if (bindingWidget == null)
        {
            bindingWidget = GetWidget(widgetName).GetGameObj().AddComponent<T>();
        }

        bindingWidget.Bind(data, fieldName);
    }


    private void buttonClkHook(string widgetName)
    {
        if(m_buttonClkHook != null)
            m_buttonClkHook(widgetName);
    }
}
