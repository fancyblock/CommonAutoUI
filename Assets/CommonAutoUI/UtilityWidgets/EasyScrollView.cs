using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class EasyScrollView : MonoBehaviour
{
    public enum Dir
    {
        vertical,
        horizontal,
    }

    public event Action<int, GameObject> ON_ITEM_FILL;

    [SerializeField] private GameObject m_itemPrefab;
    [SerializeField] private Vector2 m_itemSize;
    [SerializeField] private Vector2 m_itemInterval;
    [SerializeField] private int m_perItemCount = 1;                // 同行/列Item个数

    [SerializeField]private ScrollRect m_scrollRect;
    [SerializeField]private RectTransform m_itemContainer;
    [SerializeField] private Dir m_direction;

    private int m_itemCount;
    private int m_maxVisualCount;

    private List<GameObject> m_pendingItemList = new List<GameObject>();
    private float m_currentScrollPosotion;
    private Dictionary<int, GameObject> m_itemDic = new Dictionary<int, GameObject>();
    private List<int> m_currentPageIndeies = new List<int>();


    private void Awake()
    {
        m_scrollRect.onValueChanged.AddListener(onScrollChange);
    }

    // Update is called once per frame
    private void Update() 
    {
        refreshView();
    }

    private void onScrollChange(Vector2 arg0)
    {
        refreshView();
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
            m_itemContainer.sizeDelta = new Vector2((m_itemSize.x + m_itemInterval.x) * realCount , m_itemContainer.sizeDelta.y);
        }

        // precreate the items
        if (m_pendingItemList.Count == 0 && m_itemDic.Count == 0)
        {
            m_maxVisualCount = 0;
            
            if (m_direction == Dir.vertical)
            {
                float viewHeight = GetComponent<RectTransform>().rect.height;
                float viewCount = viewHeight / (m_itemSize.y + m_itemInterval.y);

                m_maxVisualCount = Mathf.CeilToInt(viewCount) + 1;
                m_maxVisualCount *= m_perItemCount;

                m_maxVisualCount = Math.Min(m_maxVisualCount, count);
            }
            else if(m_direction == Dir.horizontal)
            {
                float viewWidth = GetComponent<RectTransform>().rect.width;
                float viewCount = viewWidth / (m_itemSize.x + m_itemInterval.x);

                m_maxVisualCount = Mathf.CeilToInt(viewCount) + 1;
                m_maxVisualCount *= m_perItemCount;

                m_maxVisualCount = Math.Min(m_maxVisualCount, count);
            }

            for (int i = 0; i < m_maxVisualCount; i++)
            {
                GameObject go = Instantiate(m_itemPrefab, m_itemContainer);
                go.transform.localScale = Vector3.one;

                recycleItem(go);
            }
        }
        else
        {
            // 回收之前的Item 
            foreach (GameObject go in m_itemDic.Values)
                recycleItem(go);

            m_itemDic.Clear();
        }

        if (m_direction == Dir.vertical)
            initVerticalItems();
        else if (m_direction == Dir.horizontal)
            initHorizontalItems();
    }

    private void initVerticalItems()
    {
        m_currentScrollPosotion = m_itemContainer.localPosition.y;

        int startIndex = Mathf.FloorToInt(m_currentScrollPosotion / (m_itemSize.y + m_itemInterval.y) ) * m_perItemCount;
        startIndex = Mathf.Clamp(startIndex, 0, int.MaxValue);

        m_currentPageIndeies.Clear();

        for(int i = 0; i < m_maxVisualCount; i++)
        {
            int index = startIndex + i;

            if (index >= m_itemCount)
                break;

            GameObject itemGo = allocItem();
            itemGo.transform.localPosition = getItemPosition(index);
            m_itemDic.Add(index, itemGo);
            ON_ITEM_FILL(index, itemGo);

            m_currentPageIndeies.Add(index);
        }
    }

    private void initHorizontalItems()
    {
        m_currentScrollPosotion = m_itemContainer.localPosition.x;

        int startIndex = Mathf.FloorToInt(m_currentScrollPosotion / (m_itemSize.x + m_itemInterval.x)) * m_perItemCount;
        startIndex = Mathf.Clamp(startIndex, 0, int.MaxValue);

        m_currentPageIndeies.Clear();

        for (int i = 0; i < m_maxVisualCount; i++)
        {
            int index = startIndex + i;

            if (index >= m_itemCount)
                break;

            GameObject itemGo = allocItem();
            itemGo.transform.localPosition = getItemPosition(index);
            m_itemDic.Add(index, itemGo);
            ON_ITEM_FILL(index, itemGo);

            m_currentPageIndeies.Add(index);
        }
    }

    private Vector2 getItemPosition(int index)
    {
        Vector2 position = Vector2.zero;

        if(m_direction == Dir.vertical)
        {
            int row = index / m_perItemCount;
            int col = index % m_perItemCount;

            float rowOffset = (m_itemContainer.rect.width - (m_itemSize.x * m_perItemCount + m_itemInterval.x * (m_perItemCount - 1))) / 2.0f + m_itemSize.x * 0.5f;

            position.x = rowOffset + col * (m_itemSize.x + m_itemInterval.x);
            position.y = -(m_itemSize.y * 0.5f + m_itemInterval.y + row * (m_itemSize.y + m_itemInterval.y));
        }
        else if(m_direction == Dir.horizontal)
        {
            int row = index % m_perItemCount;
            int col = index / m_perItemCount;

            float colOffset = (m_itemContainer.rect.height - (m_itemSize.y * m_perItemCount + m_itemInterval.y * (m_perItemCount - 1))) / 2.0f + m_itemSize.y * 0.5f;

            position.x = m_itemSize.x * 0.5f + m_itemInterval.x + col * (m_itemSize.x + m_itemInterval.x);
            position.y = -(colOffset + row * (m_itemSize.y + m_itemInterval.y));
        }

        return position;
    }

    private void refreshView()
    {
        float scrollPosition = 0;

        if (m_direction == Dir.vertical)
            scrollPosition = m_itemContainer.localPosition.y;
        else if (m_direction == Dir.horizontal)
            scrollPosition = m_itemContainer.localPosition.x;

        if (Mathf.Abs(m_currentScrollPosotion - scrollPosition) < float.Epsilon)
            return;

        if (m_direction == Dir.vertical)
            refreshVerticalView(scrollPosition);
        else if (m_direction == Dir.horizontal)
            refreshHorizontalView(scrollPosition);

        m_currentScrollPosotion = scrollPosition;
    }

    private void refreshVerticalView(float scrollPosition)
    {
        int startIndex = Mathf.FloorToInt(scrollPosition / (m_itemSize.y + m_itemInterval.y)) * m_perItemCount;
        startIndex = Mathf.Clamp(startIndex, 0, int.MaxValue);

        refreshViewItems(startIndex);
    }

    private void refreshHorizontalView(float scrollPosition)
    {
        int startIndex = Mathf.FloorToInt(-scrollPosition / (m_itemSize.x + m_itemInterval.x)) * m_perItemCount;
        startIndex = Mathf.Clamp(startIndex, 0, int.MaxValue);

        refreshViewItems(startIndex);
    }

    private void refreshViewItems(int startIndex)
    {
        List<int> pageIndeies = new List<int>();

        for (int i = 0; i < m_maxVisualCount; i++)
        {
            int index = startIndex + i;

            if (index >= m_itemCount)
                break;

            pageIndeies.Add(index);
        }

        foreach (int oldIndex in m_currentPageIndeies.Except<int>(pageIndeies))
        {
            recycleItem(m_itemDic[oldIndex]);
            m_itemDic.Remove(oldIndex);
        }

        foreach (int newIndex in pageIndeies.Except<int>(m_currentPageIndeies))
        {
            GameObject itemGo = allocItem();
            itemGo.transform.localPosition = getItemPosition(newIndex);
            m_itemDic.Add(newIndex, itemGo);
            ON_ITEM_FILL(newIndex, itemGo);
        }

        m_currentPageIndeies = pageIndeies;
    }

    private GameObject allocItem()
    {
        GameObject go = m_pendingItemList.First<GameObject>();
        m_pendingItemList.Remove(go);
        go.SetActive(true);

        return go;
    }

    private void recycleItem(GameObject item)
    {
        m_pendingItemList.Add(item);
        item.SetActive(false);
    }
}
