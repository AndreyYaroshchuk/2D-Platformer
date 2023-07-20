using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { Play, Pause }
public delegate void InventoruUSedCallback(InventoryUIButton item);
public delegate void UpdateHeroParametersHandler(HeroParameters parameters);
public class GameController : MonoBehaviour
{
    public event UpdateHeroParametersHandler OnUpdateHeroParameters;
    [SerializeField] private int dragonKillExpirions = 101;
    [SerializeField] private int dragonHitScore;
    [SerializeField] private int dragonKillScore;
  //  [SerializeField] private GameObject newKnight;
    private GameState state;
    private int score;
    static private GameController instance;
    [SerializeField] private List<InventoriItem> inventory;
    [SerializeField] private HeroParameters hero;
    public static GameController Instance

    {
        get
        {
            if(instance == null)
            {
                //Instantiate(Resources.Load("Название объекта"))// загрузщик ресурсов в Unity 
                GameObject gameControler = Instantiate(Resources.Load("GemeController")) as GameObject; // Спавн скрипта из папки 
                instance = gameControler.GetComponent<GameController>();
            }

            return instance ;
        }

    } // инициализация класса 

//    public GameObject NewKnight { get => newKnight; set => newKnight = value; }
    public int Score
    {
        get => score;
        set
        {
            if (value != score)
            {
                score = value;
                HUD.Instance.SetScore(score.ToString());

            }
        }
    }

    public GameState State
    {
        get => state;
        set
        {
            if (value == GameState.Play)
            {
                Time.timeScale = 1.0f; // Остановка игрового времени (Пауза)
            }
            else
            {
                Time.timeScale = 0.0f;
            }
            state = value;
        }
    }

    public List<InventoriItem> Inventory { get => inventory; set => inventory = value; }
    public HeroParameters Hero { get => hero; set => hero = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject); // Текущий объект нельзя удалить 
        
        State = GameState.Play;
        Inventory = new List<InventoriItem>();
       
    }

    public void StartNewLevel()
    {
        HUD.Instance.SetScore(Score.ToString());
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(hero);
        }
        State = GameState.Play;
    }
    private void Update()
    {
        HUD.Instance.UpddateCharacterValues(hero.MaxHealth,hero.Speed,hero.Damage);
    }

    public void Hit(Idestractebl victim)
    {

        if (victim.GetType() == typeof(Dragon))
        {
                Score += dragonHitScore;
                Debug.Log("Dragon hid - add 10 score points. Curren score" + Score);
        }

        if (victim.GetType() == typeof(knight))
        {
            HUD.Instance.HealthBar.value = victim.Health;
        }
    }
    public void Killed(Idestractebl victim)
    {
        if(victim.GetType() == typeof(Dragon))
        {
            Score += dragonKillScore;
            hero.Experience += dragonKillExpirions;
            Destroy((victim as MonoBehaviour).gameObject);
        }
        if(victim.GetType() == typeof(knight))
        {
            GameOwer();
        }
    }
    public void LevelUp()
    {
        if(OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(hero);
        }
    }
    public void AddNewInventoryItem(InventoriItem itemData)
    {
        InventoryUIButton newUIButton = HUD.Instance.AddNewInventoryItem(itemData);
        InventoruUSedCallback newCallback = new InventoruUSedCallback(InventoryItemUsed);
        newUIButton.Callback = newCallback;
        Inventory.Add(itemData); 

    }

    public void InventoryItemUsed(InventoryUIButton item)
    {
        switch (item.ItemDate.CrystallType) // что мы проверяем
        {
            case CrystallType.Blue://             
                hero.Speed += item.ItemDate.Count / 10f;
            break;
            case CrystallType.Green:
                hero.MaxHealth += item.ItemDate.Count;
            break;

            case CrystallType.Red:
                hero.Damage += item.ItemDate.Count;
            break;

            default:
               Debug.LogError("Wrong crystall type!");
                break;

        }

        Inventory.Remove(item.ItemDate); // очистка листа 
        Destroy(item.gameObject);
        if(OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(hero);
        }
      
    }
    public void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void loadRestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void PrincesFound()
    {
        HUD.Instance.ShowLevelWonWindow();
    }
    public void GameOwer()
    {
        HUD.Instance.ShowLevelLoseWindow();
    }

}
