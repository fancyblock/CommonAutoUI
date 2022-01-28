using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample01 : MonoBehaviour
{
    class SampleData01 : BaseBindingData
    {
        public string m_strValue;
    }


    [SerializeField] AutoContainer m_ui;


    // Start is called before the first frame update
    void Start()
    {
        SampleData01 data = new SampleData01();
        data.m_strValue = "Init data";

        // m_ui.GetWidget<TextBinding>("text01");
        m_ui.GetWidget<InputFieldBinding>("input01").Bind(data, "m_strValue");
    }
}
