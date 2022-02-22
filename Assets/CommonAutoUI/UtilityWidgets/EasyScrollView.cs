using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(ScrollRect))]
public class EasyScrollView : MonoBehaviour
{
    private enum Dir
    {
        vertical,
        horizontal,
    }

    public event Action<int, GameObject> ON_ITEM_FILL;

    [SerializeField] private GameObject m_itemPrefab;
    [SerializeField] private Vector2 m_itemSize;
    [SerializeField] private Vector2 m_itemInterval;
    [SerializeField] private int m_perItemCount = 1;                // 同行/列Item个数

    private ScrollRect m_scrollRect;
    private RectTransform m_itemContainer;

    private Dir m_direction;

    private int m_itemCount;
    private List<GameObject> m_itemGoList = new List<GameObject>();


    private void Awake()
    {
        m_scrollRect = GetComponent<ScrollRect>();
        m_itemContainer = m_scrollRect.viewport.transform.GetChild(0).GetComponent<RectTransform>();

        m_scrollRect.onValueChanged.AddListener(onScrollChange);

        // 根据ScrollRect属性判定是横向还是纵向的
        m_direction = Dir.vertical; //TODO
    }


    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO
    }

    private void onScrollChange(Vector2 arg0)
    {
        //TODO 
    }


    public void Load(int count)
    {
        m_itemCount = count;

        int realCount = count / m_perItemCount + ( count % m_perItemCount > 0 ? 1 : 0);

        // size the content
        if(m_direction == Dir.vertical)
        {
            m_itemContainer.sizeDelta = new Vector2(m_itemContainer.sizeDelta.x, (m_itemSize.y + m_itemInterval.y) * realCount);
        }
        else if(m_direction == Dir.horizontal)
        {
            //TODO 
        }

        // precreate the items
        if (m_itemGoList.Count == 0)
        {
            int maxVisualCount = 0;
            
            if (m_direction == Dir.vertical)
            {
                float viewHeight = GetComponent<RectTransform>().rect.height;

                float viewCount = viewHeight / (m_itemSize.y + m_itemInterval.y);

                maxVisualCount = Mathf.FloorToInt(viewCount);

                if ((viewCount - maxVisualCount) > float.Epsilon) // 多余
                    maxVisualCount++;

                maxVisualCount *= 2;

                maxVisualCount = Math.Min(maxVisualCount, count);
            }
            else
            {
                float viewWidth = GetComponent<RectTransform>().rect.width;

                //TODO 
            }

            for (int i = 0; i < maxVisualCount; i++)
            {
                GameObject go = Instantiate(m_itemPrefab, m_itemContainer);
                go.transform.localScale = Vector3.one;

                m_itemGoList.Add(go);
            }
        }

        if (m_direction == Dir.vertical)
            initVerticalItems();
        else if (m_direction == Dir.horizontal)
            initHorizontalItems();
    }

    private void initVerticalItems()
    {
        float currentPosition = m_itemContainer.localPosition.y;

        int startIndex = Mathf.FloorToInt( currentPosition / (m_itemSize.y + m_itemInterval.y) ) * m_perItemCount;

        for(int i = 0; i < m_itemGoList.Count; i++)
        {
            int index = startIndex + i;
            m_itemGoList[i].transform.localPosition = getItemPosition(index);

            ON_ITEM_FILL(index, m_itemGoList[i]);
        }
    }

    private void initHorizontalItems()
    {
        //TODO 
    }

    private Vector2 getItemPosition(int index)
    {
        Vector2 position = Vector2.zero;

        if(m_direction == Dir.vertical)
        {
            //TODO 
        }
        else if(m_direction == Dir.horizontal)
        {
            //TODO 
        }

        return position;
    }

}
