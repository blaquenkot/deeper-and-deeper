using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public int MAX_OXYGEN_CAPACITY = 200;
    public int INITIAL_OXYGEN_CAPACITY = 100;
    public int OXYGEN_CAPACITY_INCREMENTS = 40;
    public int MAX_FLASHLIGHTS = 3;

    public PlayerController player;
    public GameUIController gameUIController;
    public BoardController boardController;

    public Light gameLight;

    private int oxygenCapacity = 0;
    private int oxygen = 100;
    private int deepness = 0;

    public int coins { get; set; } = 0;
    public bool flashlightActivated { get; private set; } = false;
    public int flashlights { get; private set; } = 0;
    public bool hasHarpoon { get; private set; } = false;
    public bool hasMermaidTail { get; private set; } = false;

    void Awake()
    {
        Object.FindObjectOfType<SceneManagerController>().currentSceneIndex = 2;
        this.gameLight.intensity = 0.8f;
    }

    void Start()
    {
        this.oxygenCapacity = INITIAL_OXYGEN_CAPACITY;
        this.oxygen = this.oxygenCapacity;
        this.gameUIController.updateOxygen(this.getOxygenPercentage(), false);
        this.gameUIController.updateDeepness(this.deepness, false);
        this.gameUIController.updateCoins(this.coins, false);
        this.gameUIController.updateFlashlight(this.flashlights, false);
    }

    public void updateMaxOxygen(int dMaxOxygen)
    {
        this.oxygenCapacity += dMaxOxygen;
        this.oxygen = this.oxygenCapacity;
        this.gameUIController.updateMaxOxygen(this.oxygenCapacity);
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

    public Sequence updateDeepness(int dDeepness)
    {
        this.deepness += dDeepness;
        this.gameLight.intensity *= 0.99f;
        return this.gameUIController.updateDeepnessAnimated(this.deepness);
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

    public bool isAlive()
    {
        return this.oxygen >= 0;
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

    public void rechargeFlashlight()
    {
        this.flashlights = MAX_FLASHLIGHTS;
        this.gameUIController.updateFlashlight(this.flashlights);
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
        this.flashlights = Mathf.Clamp(this.flashlights - 1, 0, MAX_FLASHLIGHTS);
        this.gameUIController.updateFlashlight(this.flashlights);
        this.gameUIController.deactivateFlashlight();
    }

    public bool hasFlashlights()
    {
        return this.flashlights > 0;
    }

    public void enteredAtlantis()
    {
        this.boardController.gameFinished();
        this.gameUIController.gameFinished(true);
    }

    private void playerDied()
    {
        this.boardController.gameFinished();
        this.gameUIController.gameFinished(false);
    }

    private float getOxygenPercentage()
    {
        return ((float)this.oxygen / (float)this.oxygenCapacity);
    }
}
