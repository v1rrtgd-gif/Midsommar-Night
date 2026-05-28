using UnityEngine;

[CreateAssetMenu(menuName = "Events/Event")]
public class EventData : ScriptableObject
{
    public string eventTitle;

    [TextArea]
    public string description;

    public Sprite image;

    public EventChoice[] choices;
}