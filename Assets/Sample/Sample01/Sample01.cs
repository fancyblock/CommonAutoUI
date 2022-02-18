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
        public Sprite m_sprValue;
    }


    [SerializeField] AutoContainer m_ui;

    SampleData01 data;

    // Start is called before the first frame update
    void Start()
    {
        data = new SampleData01();

        data.m_strValue = "Init data";
        data.m_intValue = 117;

        m_ui.GetWidget<InputFieldBinding>("input01").Bind(data, "m_intValue");
        m_ui.GetWidget<InputFieldBinding>("input02").Bind(data, "m_strValue");

        m_ui.GetWidget<TextBinding>("txt01").Bind(data, "m_strValue");

        m_ui.GetWidget<ImageBinding>("img01").Bind(data, "m_sprValue");

        m_ui.SetValue("aaa", "asdf");

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            data.SetField("m_sprValue", Resources.Load<Sprite>("korea"));
    }
}
