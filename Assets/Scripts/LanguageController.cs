using UnityEngine;

class LanguageController : MonoBehaviour {
    public enum Language
    {
        EN = 0,
        ES
    }

    private static LanguageController _instance;
    public static LanguageController Shared {
        get {
            if (_instance != null) {
                return _instance;
            } else {
                _instance = Object.FindObjectOfType<LanguageController>();
                if (_instance != null) {
                    return _instance;
                } else {
                    _instance = (new GameObject()).AddComponent<LanguageController>();
                    return _instance;
                }
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public Language CurrentLanguage = Language.EN;

    public string getIntroText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return 
@"INTRO";
            case Language.ES: return 
@"INTRO";
        }

        return "";
    }

    public string getStartButtonText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Start";
            case Language.ES: return "Comenzar";
        }

        return "";
    }

    public string getHelpText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return 
@"If you run out of <b>oxygen</b>, the game is over.
You can <b>find oxygen tanks</b> along the way, or <b>buy</b> a refill in a <b>store</b>.
Exploring <b>caves</b> costs oxygen, but you can find <b>treasures</b> there too.
<b>Enemies</b> can be attacked, if you win you obtain gold. Increase your chances by buying a <b>weapon</b>.
The <b>flashlight</b> allows you to reveal the cards before picking.
See how deep you can dive, and <b>enter Atlantis successfully to win the game!</b>";
            case Language.ES: return 
@"Si te quedás sin <b>oxígeno</b>, el juego termina.
Podés <b>encontrar tanques de oxígeno</b> en el camino, o <b>comprarlos</b> en una <b>tienda</b>.
Explorar <b>cuevas</b> demanda oxígeno, pero podés encontrar <b>tesoros</b> si lo hacés.
Los <b>enemigos</b> pueden ser atacados, si ganás obtendrás oro. Incrementá tus chances comprando un <b>arma</b>.
La <b>linterna</b> te permite revelar cartas antes de elegir.
Sumergite a las profundidades del océano, y <b>lográ entrar a Atlantis para ganar el juego!</b>";
        }

        return "";
    }

    public string getGoAwayText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Go away";
            case Language.ES: return "Irse";
        }

        return "";
    }

    public string getAtlantisFoundText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "You found a mysterious city!";
            case Language.ES: return "Encontraste una ciudad misteriosa!";
        }

        return "";
    }

    public string getAtlantisTryToEnterText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Try to enter";
            case Language.ES: return "Tratar de entrar";
        }

        return "";
    }

    public string getAtlantisEnteredText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "You entered!";
            case Language.ES: return "Entraste!";
        }

        return "";
    }

    public string getAtlantisNotEnteredText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "They attacked you!";
            case Language.ES: return "Ellos te atacaron!";
        }

        return "";
    }

    public string getCaveFoundText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "You found a cave!";
            case Language.ES: return "Encontraste una cueva!";
        }

        return "";
    }

    public string getEnterText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Enter";
            case Language.ES: return "Entrar";
        }

        return "";
    }

    public string getChestFoundText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "You found a chest!";
            case Language.ES: return "Encontraste un tesoro!";
        }

        return "";
    }

    public string getNothingFoundText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "You found nothing!";
            case Language.ES: return "No encontraste nada!";
        }

        return "";
    }

    public string getYouDiedText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "You died!";
            case Language.ES: return "Moriste!";
        }

        return "";
    }

    public string getEnemyLostText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "It attacked you and run away!";
            case Language.ES: return "Te atacó y escapó!";
        }

        return "";
    }

    public string getEnemyWonText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "You won and found a chest!";
            case Language.ES: return "Ganaste y encontraste un tesoro!";
        }

        return "";
    }

    public string getEnemyEscapeText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "You ran away!";
            case Language.ES: return "Escapaste!";
        }

        return "";
    }

    public string getFightButtonText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Fight";
            case Language.ES: return "Pelear";
        }

        return "";
    }

    public string getStoreCoinsText(int coins) 
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return $"({coins} coins)";
            case Language.ES: return $"({coins} monedas)";
        }

        return "";
    }

    public string getStoreOxygenTankRechargeText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Tank recharge";
            case Language.ES: return "Cargar tanque";
        }

        return "";
    }

    public string getStoreOxygenTankCapacityText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Tank capacity";
            case Language.ES: return "Sumar capacidad";
        }

        return "";
    }

    public string getStoreHarpoonText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Harpoon";
            case Language.ES: return "Arpón";
        }

        return "";
    }

    public string getStoreFlashlightText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Flashlight";
            case Language.ES: return "Linterna";
        }

        return "";
    }

    public string getStoreMermaidTaleText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Mermaid tail";
            case Language.ES: return "Cola de sirena";
        }

        return "";
    }

    public string getStoreBuyButtonText()
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Buy";
            case Language.ES: return "Comprar";
        }

        return "";
    }

    public string getStoreExitButtonText() 
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Exit";
            case Language.ES: return "Salir";
        }

        return "";
    }

    public string getDeepnessText(int deepness) 
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return deepness + " METERS";
            case Language.ES: return deepness + " METROS";
        }

        return "";
    }

    public string getEnemyName(string enemy) 
    {
        switch (CurrentLanguage) 
        {
            case Language.EN: return enemy;
            case Language.ES: 
                if (enemy == "Piranha") 
                {
                    return "Piraña";
                }
                else if (enemy == "Shark")
                {
                    return "Tiburón";
                }
                else
                {
                    return enemy;
                }
        }

        return "";
    }
}