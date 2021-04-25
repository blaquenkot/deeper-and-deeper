using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int MAX_OXYGEN_CAPACITY = 200;
    public int INITIAL_OXYGEN_CAPACITY = 100;
    public int OXYGEN_CAPACITY_INCREMENTS = 20;

    public GameUIController gameUIController;

    public Light gameLight;

    private int oxygenCapacity = 0;

    private int oxygen = 100;
    private int deepness = 100;

    public int coins { get; set; } = 100;

    public bool hasHarpoon { get; private set; } = false;
    public bool hasMermaidTail { get; private set; } = false;

    void Start()
    {
        this.oxygenCapacity = INITIAL_OXYGEN_CAPACITY;
        this.oxygen = this.oxygenCapacity;
        this.gameUIController.updateOxygen(this.getOxygenPercentage());
        this.gameUIController.updateDeepness(this.deepness);
        this.gameUIController.updateCoins(this.coins);
    }

    public void updateMaxOxygen(int dMaxOxygen)
    {
        this.oxygenCapacity += dMaxOxygen;
    }

    public void updateOxygen(int dOxygen)
    {
        this.oxygen = Mathf.Clamp(this.oxygen + dOxygen, 0, this.oxygenCapacity);
        this.gameUIController.updateOxygen(this.getOxygenPercentage());

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

    public void spend(int coins)
    {
        this.updateCoins(-coins);
    }

    public void giveHarpoon()
    {
        this.hasHarpoon = true;
    }

    public void giveMermaidTail()
    {
        this.hasMermaidTail = true;
    }

    public bool hasMaxOxygen()
    {
        return this.oxygen >= this.oxygenCapacity;
    }

    public void rechargeOxygen()
    {
        this.oxygen = this.oxygenCapacity;
        this.gameUIController.updateOxygen(this.oxygen);
    }

    public bool hasMaxOxygenCapacity()
    {
        return this.oxygenCapacity >= MAX_OXYGEN_CAPACITY;
    }

    public void increaseOxygenCapacity()
    {
        this.updateMaxOxygen(OXYGEN_CAPACITY_INCREMENTS);
    }

    private void playerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private float getOxygenPercentage()
    {
        return ((float)this.oxygen / (float)this.oxygenCapacity);
    }
}
