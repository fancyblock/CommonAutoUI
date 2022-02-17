using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sample01 : MonoBehaviour
{
    
    class SampleData01 : BaseBindingData
    {
        public string m_strValue;
        public int m_intValue;
    }


    [SerializeField] AutoContainer m_ui;


    // Start is called before the first frame update
    void Start()
    {
        SampleData01 data = new SampleData01();

        data.m_strValue = "Init data";
        data.m_intValue = 117;

        m_ui.GetWidget<InputFieldBinding>("input01").Bind(data, "m_intValue");
        m_ui.GetWidget<InputFieldBinding>("input02").Bind(data, "m_strValue");

        m_ui.GetWidget<TextBinding>("txt01").Bind(data, "m_strValue");

    }
}
