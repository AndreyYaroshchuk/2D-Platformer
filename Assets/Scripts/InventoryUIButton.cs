using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIButton : MonoBehaviour
{
    private InventoriItem itemDate;
    private InventoruUSedCallback callback;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textCount;
    [SerializeField] private TextMeshProUGUI textLabel;
    [SerializeField] private List<Sprite> sprites;

   
    public InventoruUSedCallback Callback { get => callback; set => callback = value; }
    public InventoriItem ItemDate { get => itemDate; set => itemDate = value; }

    private void Start()
    {
        string spriteNameToSearch = itemDate.CrystallType.ToString().ToLower();
        image.sprite = sprites.Find(x => x.name.Contains(spriteNameToSearch));


        textCount.text = itemDate.Count.ToString();
        textLabel.text = itemDate.CrystallType.ToString();

        //public void ButtnClick()                                первый вариант 
        //{
        //    GameController.Instance.InventoryItemUsed(this);
        //} 
        gameObject.GetComponent<Button>().onClick.AddListener(() => callback(this)); // второй вариант 

    }
}
