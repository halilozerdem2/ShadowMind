using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Kapý Ayarlarý")]
    [SerializeField] private float openDistance = 3f;  // Kapý açýlma mesafesi
    [SerializeField] private float openSpeed = 2f;     // Kapý açýlma hýzý
    [SerializeField] private float maxOpenAngle = 90f; // Kapýnýn açýlacaðý maksimum açý

    private bool isPlayerNear = false;   // Oyuncu kapýya yaklaþtýðýnda kontrol
    private bool isDoorOpen = false;     // Kapý açýk mý?

    private Transform player; // Oyuncu referansý
    private float closedRotationZ;  // Kapýnýn kapalý hali Z rotasyonu
    private float openRotationZ;    // Kapýnýn açýk hali Z rotasyonu

    private void Start()
    {
        closedRotationZ = transform.rotation.eulerAngles.z; // Kapý baþlangýçta kapalý
        openRotationZ = closedRotationZ + maxOpenAngle; // Kapý saða açýlacak (z ekseninde)
        player = Camera.main.transform; // Oyuncu kamerayý kullanýyoruz
    }

    private void Update()
    {
        // Eðer oyuncu kapýya yakýnsa, kapýyý açmaya baþla
        if (isPlayerNear && !isDoorOpen)
        {
            OpenDoor();
        }
        else if (!isPlayerNear && isDoorOpen)
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        // Kapýyý z ekseninde yavaþça açýyoruz
        float currentRotationZ = Mathf.LerpAngle(transform.rotation.eulerAngles.z, openRotationZ, Time.deltaTime * openSpeed);
        transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);

        // Kapý tam açýldýðýnda durur
        if (Mathf.Abs(transform.rotation.eulerAngles.z - openRotationZ) < 1f)
        {
            isDoorOpen = true;
        }
    }

    private void CloseDoor()
    {
        // Kapýyý z ekseninde yavaþça kapatýyoruz
        float currentRotationZ = Mathf.LerpAngle(transform.rotation.eulerAngles.z, closedRotationZ, Time.deltaTime * openSpeed);
        transform.rotation = Quaternion.Euler(90,0, currentRotationZ);
        
        // Kapý tam kapandýðýnda durur
        if (Mathf.Abs(transform.rotation.eulerAngles.z - closedRotationZ) < 1f)
        {
            isDoorOpen = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Eðer oyuncu kapýya yaklaþýrsa
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Eðer oyuncu kapýdan uzaklaþýrsa
        {
            isPlayerNear = false;
        }
    }
}
