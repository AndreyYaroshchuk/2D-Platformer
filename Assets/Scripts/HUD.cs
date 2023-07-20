using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection.Emit;

public class HUD : MonoBehaviour 
{

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private GameObject inventoriWindow;
    [SerializeField] private GameObject levelWonWindow;
    [SerializeField] private GameObject levelLoseWindow;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Creature owner;
    [SerializeField] private InventoryUIButton inventoryItemPrefab;
    [SerializeField] private Transform inventoryConteiner;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text speedValue;
    [SerializeField] private Text healthValue_;
    [SerializeField] private GameObject damageLabel;
    [SerializeField] private GameObject speedLabel;
    [SerializeField] private GameObject healthLabel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject AudioMenu;
    static private HUD instance;
    
    
    public static HUD Instance { get => instance;}
    public Slider HealthBar { get => healthBar; set => healthBar = value; }
    public Text SpeedValue { get => speedValue; set => speedValue = value; }

    public Text DamageValue { get => damageValue; set => damageValue = value; }
    public Text HealthValue_ { get => healthValue_; set => healthValue_ = value; }
    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LoadInventory();
        
        GameController.Instance.OnUpdateHeroParameters += GameController_OnUpdateHeroParameters;
        GameController.Instance.StartNewLevel();
    }

    private void GameController_OnUpdateHeroParameters(HeroParameters parameters)
    {
        HealthBar.maxValue = parameters.MaxHealth;
        HealthBar.value = parameters.MaxHealth;
        UpddateCharacterValues(parameters.MaxHealth, parameters.Speed, parameters.Damage);


    }

    private void Update()
    {
        HealthBar.value = owner.Health;
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    //ShowWindow(pauseWindow);
            
        //    //pauseWindow.SetActive(true);
        //    GameController.Instance.State = GameState.Play;
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            ButtonOpenMainMenu();
            //GameController.Instance.State = GameState.Pause;
            //pauseWindow.SetActive(false);
        }
        
    }
    
    public void SetScore(string scoreValue)
    {
        scoreLabel.text = scoreValue;
    }
    public void ShowWindow(GameObject window)
    {
        if (GameController.Instance.State != GameState.Pause)
        {
            window.GetComponent<Animator>().SetBool("Open", true);
            GameController.Instance.State = GameState.Pause;

            damageLabel.SetActive(true);
            speedLabel.SetActive(true);
            healthLabel.SetActive(true);
        }

    }
    public void HideWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("Open", false);
        GameController.Instance.State = GameState.Play;

        damageLabel.SetActive(false);
        speedLabel.SetActive(false);
        healthLabel.SetActive(false);
    }
    public InventoryUIButton AddNewInventoryItem(InventoriItem itemDate) 
    {
        InventoryUIButton newItem = Instantiate(inventoryItemPrefab) as InventoryUIButton;
        newItem.transform.SetParent(inventoryConteiner);
        newItem.ItemDate = itemDate;
        return newItem;
    }
    public void UpddateCharacterValues( float newHealth, float newSpeed, float newDamage)
    {
        
        healthValue_.text = newHealth.ToString();
        speedValue.text = newSpeed.ToString();
        damageValue.text = newDamage.ToString();
    }
    public void ButtonOpenMainMenu()
    {
        if (GameController.Instance.State != GameState.Pause)
        {
            pauseWindow.GetComponent<Animator>().SetBool("ActivOpen", true);
            GameController.Instance.State = GameState.Pause;
        }
    }
    public void ButtonNext()
    {
        GameController.Instance.loadNextLevel();
    }
    public void ButtonMenu()
    {
        GameController.Instance.loadMainMenu();
    }
    public void ButtonRestart() 
    {
        GameController.Instance.State = GameState.Play;
        GameController.Instance.loadRestartLevel();
    }

    public void ButtonCloseAudioMenu()
    {
        AudioMenu.SetActive(false);
    }

    public void ButtonCloseMainMenu()
    {
        pauseWindow.GetComponent<Animator>().SetBool("ActivOpen", false);
        GameController.Instance.State = GameState.Play;
    }
    public void ShowLevelWonWindow()
    {
        GameController.Instance.State = GameState.Pause;
        // ShowWindow(levelWonWindow); для показа окна через анимацию сундука 
        levelWonWindow.SetActive(true);
    }
    
    public void ShowLevelLoseWindow()
    {
        GameController.Instance.State = GameState.Pause;
        levelLoseWindow.SetActive(true);
    }

    public void ButtonOption()
    {
        AudioMenu.SetActive(true);
    }

    public void LoadInventory()
    {
        InventoruUSedCallback callback = new InventoruUSedCallback(GameController.Instance.InventoryItemUsed);

        for (int i = 0; i < GameController.Instance.Inventory.Count; i++)
        {
            InventoryUIButton newItem = AddNewInventoryItem(GameController.Instance.Inventory[i]);

            newItem.Callback = callback;
        }
    }
    private void OnDestroy()
    {
        GameController.Instance.OnUpdateHeroParameters -= GameController_OnUpdateHeroParameters;
    }

    public void SliderAudio()
    {
        audioSource.volume = slider.value;
    }
    public void MuteAudio()
    {

        audioSource.mute = !audioSource.mute;
    }
}
