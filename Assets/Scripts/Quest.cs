using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public Text coinQuest;

    Player player;

    private bool triggerEnter = false;
    private bool questAngenommen = false;

    public GameObject coins;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!questAngenommen)
        {
            coinQuest.text = "";
        }
        if (Input.GetKeyDown(KeyCode.F) && triggerEnter == true)
        {
            questAngenommen = true;
            coins.SetActive(true);
        }
        if (questAngenommen)
        {
            if (player.muenze >= 15)
            {
                coinQuest.text = "Abgeschlossen!";
                coinQuest.color = Color.green;
                if (Input.GetKeyDown(KeyCode.F) && triggerEnter == true)
                {
                    SceneManager.LoadScene(2);
                }
            }
            else
            {
                coinQuest.text = "Finde (" + player.muenze.ToString() + "/15) Coins";
            }
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        switch (target.tag)
        {
            case "vendor":
                triggerEnter = true;
                break;
            default:
                break;
        }
    }
}
