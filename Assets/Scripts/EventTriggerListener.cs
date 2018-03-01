using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 事件触发器类
/// </summary>
public class EventTriggerListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public delegate void VoidDelegate(GameObject go);

    public VoidDelegate onClick;    //点击
    public VoidDelegate onDown;     //按下
    public VoidDelegate onEnter;    //按下抬起
    public VoidDelegate onExit;     //退出
    public VoidDelegate onUp;       //抬起

    static public EventTriggerListener Get(GameObject go)
    {
        //获取事件触发对象
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        //如果为空，添加一个事件触发对象
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }
    /// <summary>
    /// 按下事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }
    /// <summary>
    /// 触发函数
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    /// <summary>
    /// 退出事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    /// <summary>
    /// 释放事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }
    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }

}
