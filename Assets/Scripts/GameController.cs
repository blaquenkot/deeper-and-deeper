using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameUIController gameUIController;

    public Light gameLight;

    private int oxygen = 100;
    private int deepness = 0;

    private int coins = 0;

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
        this.gameLight.intensity *= 0.9f;
    }

    public void updateCoins(int dCoins)
    {
        this.coins += dCoins;
        this.gameUIController.updateCoins(this.coins);
    }

    private void playerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
