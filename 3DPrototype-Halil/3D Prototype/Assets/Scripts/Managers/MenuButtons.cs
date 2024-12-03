using UnityEngine;

public class MenuButtons : MonoBehaviour
{
  public void play()
    {
        GameManager.Instance.LoadScene("Morning");
    }
}
