using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class HeroParameters // таже запись HeroParameters hero = new HeroParameters();
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private int experience;
    [SerializeField] private int nextExperienceLevel = 100;
    [SerializeField] private int previousExperienceLevel = 0;
    [SerializeField] private int Level = 1;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Experience
    {
        get => experience;
        set
        {
            experience = value;
            CheckExperienceLevel();
        }
           
    }


    private void CheckExperienceLevel()
    {
        if(experience >= nextExperienceLevel)
        {
            Level++;

            int addition = previousExperienceLevel;
            previousExperienceLevel = nextExperienceLevel;
            nextExperienceLevel += addition;
         

            switch (Random.Range(0,3))
            {
                case 0:
                    maxHealth += 10f;
                    break;
                case 1:
                    damage++;
                    break;
                case 2:
                    speed += 0.1f;
                    break;  
            }

            GameController.Instance.LevelUp();
        }
    }

}
