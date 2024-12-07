using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;
    public Transform spawnPosition;

    private void Awake()
    {
        if(Instance!=null && Instance !=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance=this;
        DontDestroyOnLoad(gameObject);
    }


    private void OnDisable()
    {
        ResetPlayer();
    }

    public void SpawnCharacter()
{
    Transform spawnPoint = GameObject.FindWithTag("PlayerSpawnPoint")?.transform;

    if (spawnPoint != null)
    {
        spawnPosition = spawnPoint;
        gameObject.transform.position = spawnPosition.position;
        gameObject.transform.rotation = spawnPosition.rotation;

        // Obje aktif hale getiriliyor.
        gameObject.SetActive(true);
    }
    else
    {
        Debug.Log("Karakter Spawnlanamadı: PlayerSpawnPoint tag'li obje bulunamadı.");
    }
}

    public void ResetPlayer()
    {
        spawnPosition=null;
        gameObject.SetActive(false);
    }
 
}
