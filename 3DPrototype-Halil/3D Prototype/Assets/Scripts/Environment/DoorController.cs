using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Kapı Ayarları")]
    [SerializeField] private float openSpeed = 2f;     // Kapının açılma hızı
    [SerializeField] private float openAngle = 90f;   // Kapının açılma açısı

    private Vector3 closedRotation;  // Kapının kapalı durumdaki rotasyonu
    private Vector3 openRotation;    // Kapının açık durumdaki rotasyonu
    private bool isPlayerNear = false; 
    private bool isDoorOpen = false; 
    private Coroutine doorCoroutine;

    private void Start()
    {
        closedRotation = transform.rotation.eulerAngles;
        openRotation = new Vector3(closedRotation.x, closedRotation.y, closedRotation.z - openAngle);
    }

    private void Update()
    {
        // Oyuncu yakındayken kapıyı aç, uzaklaştığında kapıyı kapat
        if (isPlayerNear && !isDoorOpen)
        {
            OpenDoor();
        }
        else if (!isPlayerNear && isDoorOpen)
        {
            CloseDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true; // Oyuncu yakında
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false; // Oyuncu uzaklaştı
        }
    }

    private void OpenDoor()
    {
        if (doorCoroutine != null) StopCoroutine(doorCoroutine);
        doorCoroutine = StartCoroutine(SmoothRotate(openRotation));
    }

    private void CloseDoor()
    {
        if (doorCoroutine != null) StopCoroutine(doorCoroutine);
        doorCoroutine = StartCoroutine(SmoothRotate(closedRotation));
    }

    private IEnumerator SmoothRotate(Vector3 targetRotation)
    {
        isDoorOpen = targetRotation == openRotation;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
