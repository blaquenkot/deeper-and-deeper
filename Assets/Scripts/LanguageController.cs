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

    public string getIntroText() {
        switch (CurrentLanguage) 
        {
            case Language.EN: return 
@"INTRO";
            case Language.ES: return 
@"INTRO";
        }

        return "";
    }

    public string getStartButtonText() {
        switch (CurrentLanguage) 
        {
            case Language.EN: return "Start";
            case Language.ES: return "Comenzar";
        }

        return "";
    }

    public string getHelpText() {
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
}