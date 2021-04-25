using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int MAX_OXYGEN_CAPACITY = 200;
    public int INITIAL_OXYGEN_CAPACITY = 100;
    public int OXYGEN_CAPACITY_INCREMENTS = 20;
    public int FLASHLIGHTS_MAX = 3;

    public PlayerController player;
    public GameUIController gameUIController;

    public Light gameLight;

    private int oxygenCapacity = 0;

    private int oxygen = 100;
    private int deepness = 100;

    public int coins { get; set; } = 100;

    public bool flashlightActivated { get; private set; } = false;
    private int flashlights = 3;
    public bool hasHarpoon { get; private set; } = false;
    public bool hasMermaidTail { get; private set; } = false;

    void Start()
    {
        this.oxygenCapacity = INITIAL_OXYGEN_CAPACITY;
        this.oxygen = this.oxygenCapacity;
        this.gameUIController.updateOxygen(this.getOxygenPercentage(), false);
        this.gameUIController.updateDeepness(this.deepness);
        this.gameUIController.updateCoins(this.coins, false);
    }

    public void updateMaxOxygen(int dMaxOxygen)
    {
        this.oxygenCapacity += dMaxOxygen;
    }

    public void updateOxygen(int dOxygen)
    {
        this.oxygen = Mathf.Clamp(this.oxygen + dOxygen, 0, this.oxygenCapacity);
        this.gameUIController.updateOxygen(this.getOxygenPercentage());

        if (dOxygen < 0)
        {
            this.player.lostOxygen();
        }
        else 
        {
            this.player.wonOxygen();
        }

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

        if (dCoins > 0)
        {
            this.player.wonCoins();
        }
    }

    public void spend(int coins)
    {
        this.updateCoins(-coins);
    }

    public void giveHarpoon()
    {
        this.hasHarpoon = true;
        this.player.updateSprite(this.hasHarpoon, this.hasMermaidTail);
    }

    public void giveMermaidTail()
    {
        this.hasMermaidTail = true;
        this.player.updateSprite(this.hasHarpoon, this.hasMermaidTail);
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

    public void activateFlashlight()
    {
        if (this.flashlights > 0)
        {
            this.flashlightActivated = true;
        }
    }

    public void deactivateFlashlight()
    {
        this.flashlightActivated = false;
    }

    public void flashlightUsed()
    {
        this.flashlights = Mathf.Clamp(this.flashlights - 1, 0, FLASHLIGHTS_MAX);

        if (this.flashlights == 0)
        {
            this.gameUIController.deactivateFlashlight();
        }
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
