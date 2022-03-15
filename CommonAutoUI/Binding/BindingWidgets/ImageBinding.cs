using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class ImageBinding : BaseBindingWidget
{
    private Image m_image;


    // Start is called before the first frame update
    void Awake()
    {
        m_image = GetComponent<Image>();
    }

    protected override void onDataChange(object val)
    {
        m_image.sprite = val as Sprite;
    }
}
