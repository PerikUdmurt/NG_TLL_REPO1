using System.Collections.Generic;
using UnityEngine;
using MyPetProject;
using System.Linq;

public class UserController : MonoBehaviour
{
    public FrameInput frameInput {  get; set; }

    private IInput _InputHandler;

    private Usable currentUsableItem = null;
    private HashSet<Usable> UsableItemsInArea = new HashSet<Usable>();  //Все Deselect объекты, которые находятся в пределах области использования, попадают в эттот лист

    private void Construct(IInput inputHandler)
    {
        _InputHandler = inputHandler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Usable item))
        {
            SetCurrentSelectedItem(item);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Usable item))
        {
            if (UsableItemsInArea.Contains(item))
            {
                UsableItemsInArea.Remove(item);
            }
            
            if (currentUsableItem == item) 
            { 
                currentUsableItem.Deselect(this); 
                currentUsableItem = null; 
            }
        }
    }

    private void Update()
    {
        CheckAvailableUsableItem();

        if (frameInput.UseButtonPressed)
        {
            Use(currentUsableItem);
        }
    }
    private void Use(Usable usable)
    {
        if (currentUsableItem != null)
        {
            currentUsableItem.Use(this);
            currentUsableItem = null;
        }
    }

    private void SetCurrentSelectedItem(Usable usable)
    {
        if (currentUsableItem != null)
        {
            UsableItemsInArea.Add(currentUsableItem);
            currentUsableItem.Deselect(this);
            currentUsableItem=null;
        }
        currentUsableItem = usable;
        currentUsableItem.Select(this);
    }

    private void CheckAvailableUsableItem()
    {
        if (currentUsableItem == null && UsableItemsInArea.Count > 0)
        {
            SetCurrentSelectedItem(UsableItemsInArea.ElementAt(0));
        }
    }
}
