using UnityEngine;
using UnityEngine.Scripting;
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrapPointTransform;
    [SerializeField] private LayerMask interactingLayerMask;

    private ObjectGrabbable objectGrabbable;
    float InteractionDistance = 3f;

    private void Start()
    {
        InputManager.Instance.OnEButtonPressed+=HandleEButton;
        InputManager.Instance.OnQButtonPressed+=HandleQButton;
    }

    private void AddObjectToInventory()
    {
        if (objectGrabbable.TryGetComponent(out ItemData itemData))
        {
            Inventory.items.Add(itemData.item);
            Destroy(objectGrabbable.gameObject);
            objectGrabbable = null;
        }
        else
        {
            Debug.LogWarning("Bu obje bir envanter itemi değil!");
        }
    }

    private void HandleEButton()
    {
        if(objectGrabbable==null)
        {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, 
                                    out RaycastHit raycastHit, InteractionDistance, interactingLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrapPointTransform);
                    }
                }
        }
        else
        {
            objectGrabbable.Drop();
            objectGrabbable = null;
        }
    }

    private void HandleQButton()
    {
        if(objectGrabbable!=null)
        {
            AddObjectToInventory();
        }

    }

}
