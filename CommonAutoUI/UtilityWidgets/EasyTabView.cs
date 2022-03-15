using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EasyTabView : MonoBehaviour
{
    [SerializeField] List<Button> m_tabButtons;
    [SerializeField] List<GameObject> m_tabList;


    void Awake()
    {
        if(m_tabButtons != null)
        {
            for(int i = 0; i < m_tabButtons.Count; i++)
            {
                int idx = i;
                m_tabButtons[i].onClick.AddListener(() => 
                {
                    for(int j = 0; j < m_tabList.Count; j++)
                    {
                        m_tabList[j].SetActive(idx == j);
                    }
                });
            }
        }
    }

    void Start()
    {
        ShowTab(0);
    }

    
    public void ShowTab(int index)
    {
        for (int i = 0; i < m_tabList.Count; i++)
            m_tabList[i].SetActive(index == i);
    }

    public void ShowTab(string name)
    {
        for (int i = 0; i < m_tabList.Count; i++)
        {
            m_tabList[i].SetActive(m_tabList[i].gameObject.name == name);
        }
    }

}
