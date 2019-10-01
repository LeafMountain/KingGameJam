using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevelButton : MonoBehaviour
{
    [ExecuteInEditMode]
   public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
