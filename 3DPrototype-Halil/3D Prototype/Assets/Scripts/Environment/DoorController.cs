using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Kapý Ayarlarý")]
    [SerializeField] private float openSpeed = 2f;       // Kapý açýlma hýzý
    [SerializeField] private float openAngle = 90f;     // Kapýnýn açýlacaðý z eksenindeki açý

    private Vector3 closedRotation;  // Kapýnýn kapalý pozisyondaki rotasyonu
    private Vector3 targetRotation;  // Kapýnýn hedef rotasyonu
    private bool isPlayerNear = false; // Oyuncu kapýya yaklaþtýðýnda kontrol
    private bool isDoorOpen = false;  // Kapý açýk mý?

    private void Start()
    {
        // Kapýnýn baþlangýçtaki rotasyonunu kaydet
        closedRotation = transform.rotation.eulerAngles;

        // Baþlangýçta kapýyý kapalý pozisyona ayarla
        targetRotation = closedRotation;
        transform.rotation = Quaternion.Euler(closedRotation);
    }

    private void Update()
    {
        // Eðer oyuncu kapýya yaklaþtýysa ve kapý kapalýysa aç
        if (isPlayerNear && !isDoorOpen)
        {
            OpenDoor();
        }
        // Eðer oyuncu uzaklaþtýysa ve kapý açýksa kapat
        else if (!isPlayerNear && isDoorOpen)
        {
            CloseDoor();
        }

        // Kapýyý hedef rotasyona doðru hareket ettir
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(targetRotation),
            openSpeed * Time.deltaTime * 100f
        );
    }

    private void OpenDoor()
    {
        // Kapýyý açýk hale getir, sadece z ekseninde deðiþiklik yap
        targetRotation = new Vector3(closedRotation.x, closedRotation.y, closedRotation.z - openAngle);
        isDoorOpen = true;
    }

    private void CloseDoor()
    {
        // Kapýyý kapalý hale getir, ilk rotasyona geri dön
        targetRotation = closedRotation;
        isDoorOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu kapýya yaklaþýrsa
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu kapýdan uzaklaþýrsa
        {
            isPlayerNear = false;
        }
    }
}
