using UnityEngine;

public class MenuButtons : MonoBehaviour
{
  public void play()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }
}
