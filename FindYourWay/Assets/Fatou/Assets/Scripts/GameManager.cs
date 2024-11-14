using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private GameObject player;
    public Text uiDistance;
    public Text uiCrystals;
    private int playerCrystals = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.RoundToInt(player.transform.position.z);
        uiDistance.text = distance.ToString() + "m";
        uiCrystals.text = playerCrystals.ToString();
    }
    public void CrystalsCollected()
    {
        playerCrystals ++; 
    }
}
