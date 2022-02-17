using TMPro;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPBinding : BaseBindingWidget
{
    private TextMeshProUGUI m_text;


    // Start is called before the first frame update
    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }

    protected override void onDataChange(object val)
    {
        m_text.text = val.ToString();
    }
}
