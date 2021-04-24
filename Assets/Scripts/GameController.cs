using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameUIController gameUIController;
    private int oxygen = 100;
    private int deepness = 0;

    void Start()
    {
        this.gameUIController.updateOxygen(this.oxygen);
        this.gameUIController.updateDeepness(this.deepness);
    }

    public void updateOxygen(int dOxygen)
    {
        this.oxygen += dOxygen;
        this.gameUIController.updateOxygen(this.oxygen);

        if (this.oxygen <= 0)
        {
            this.playerDied();
        }
    }

    public void updateDeepness(int dDeepness)
    {
        this.deepness += dDeepness;
        this.gameUIController.updateDeepness(this.deepness);
    }

    private void playerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
