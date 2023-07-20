using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnRocket : MonoBehaviour
{
    [SerializeField] private List<GameObject> Rocket;
    [SerializeField] private int TimeSpawnRocket;
    
    private void Start()
    {
        InvokeRepeating("SpawnRockets", TimeSpawnRocket, 1f);
    }
    private void Update()
    {
        ShowMainMenu();
    }
    private void SpawnRockets()
    {
        int i = Random.Range(0, Rocket.Count);
        Vector3 newVector3 = new Vector3(Random.Range(200, 500), 0 , Random.Range(200, 500));
        Instantiate(Rocket[i],  gameObject.transform.position + newVector3, Quaternion.identity); // Quaternion.identity поворт такой который находится в прифабе 
    }

    private void ShowMainMenu()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        
    }
}
