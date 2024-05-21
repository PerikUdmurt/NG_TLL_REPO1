using UnityEngine;
using UnityEngine.UI;
public abstract class Effect : ScriptableObject
{
    [HideInInspector]public EffectReceiver effectReceiver;
    public Image icon;
    public string effectName;
    public string description;
    public float Duration;
    public abstract void Init();

    public virtual void Run() { }

    public virtual void EverySecondRun() { }
    public virtual void End() { }
}
