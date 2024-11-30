using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrapPointTransform;
    [SerializeField] private LayerMask interactingLayerMask;

    private ObjectGrabbable objectGrabbable;
    float InteractionDistance = 3f;

    private void Update()
    {
        if (objectGrabbable == null)
        {
            if (InputManager.Instance.returnEButtonValue())
            {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, InteractionDistance, interactingLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrapPointTransform);
                    }
                }
            }
        }
        else
        {
            if (InputManager.Instance.returnQButtonValue())
            {
                AddObjectToInventory();
            }

            if (InputManager.Instance.returnEButtonValue())
            {
                objectGrabbable.Drop();
                objectGrabbable = null;

            }
        }
    }

    private void AddObjectToInventory()
    {

        if (objectGrabbable.TryGetComponent(out ItemData itemData))
        {
            // Envantere ekle
            Inventory.items.Add(itemData.item);

            // Objeyi yok et
            Destroy(objectGrabbable.gameObject);

            // Referansý sýfýrla
            objectGrabbable = null;
        }
        else
        {
            Debug.LogWarning("Bu obje bir envanter itemi deðil!");
        }
    }
}
