using UnityEngine;
using UnityEngine.Events;

public class Usable : MonoBehaviour
{
    public virtual void Use(UserController user) 
    {
        OnUse?.Invoke(user);
    }
    public virtual void Select(UserController user)
    {
        OnSelect?.Invoke(user);
    }
    public virtual void Deselect(UserController user)
    {
        OnDeselect?.Invoke(user);
    }
    public UnityEvent<UserController> OnUse;
    public UnityEvent<UserController> OnSelect;
    public UnityEvent<UserController> OnDeselect;
}


