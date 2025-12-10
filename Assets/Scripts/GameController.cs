using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Player player;
    public ChristmasDudeAI christmasdude;
    public int Presents = 40;
    public int maxPresents = 0;
    public int items;
    public int maxItems;
    public int currentItem;
    public int exitReached = 0;
    public TMP_Text presentsCounter;
    public TMP_Text itemCountText;
    public TMP_Text currentItemText;
    public GameObject ChristmasDude;
    public GameObject ChristmasDudeMad;
    public Entrance entrance;
    public AudioSource gameMusic;
    public bool activateSpoopMode;
    public bool activateFinaleMode;
    public Slider stamina;
    public Image[] itemBG;
    public Image[] itemImages;
    public string[] itemNames;
    public string itemName;
    public Material daySky;
    public Material nightSky;
    public Color itemSelectColor;
    private Sprite emptyItem;
    private int randomSky;

    public void Start()
    {
        activateSpoopMode = false;
        randomSky = Random.Range(0, 2);

        if (randomSky == 0)
        {
            RenderSettings.skybox = daySky;
            RenderSettings.ambientLight = Color.white;
        }
        if (randomSky == 1)
        {
            RenderSettings.skybox = nightSky;
            RenderSettings.ambientLight = Color.grey;
        }
    }

    public void Update()
    {
        UpdatePresentCount();

        if (Input.GetMouseButtonDown(1))
        {
            UseItem();
            SetItemName(currentItem);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ItemColorClear();
            itemBG[0].color = itemSelectColor;
            currentItem = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            ItemColorClear();
            itemBG[1].color = itemSelectColor;
            currentItem = 1;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            ItemColorClear();
            itemBG[2].color = itemSelectColor;
            currentItem = 2;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            ItemColorClear();
            itemBG[3].color = itemSelectColor;
            currentItem = 3;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentItem == 3)
            {
                currentItem = 0;
            }
            else
            {
                currentItem++;
            }
            ItemColorClear();
            itemBG[currentItem].color = itemSelectColor;
            SetItemName(currentItem);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentItem == 0)
            {
                currentItem = 3;
            }
            else
            {
                currentItem--;
            }
            ItemColorClear();
            itemBG[currentItem].color = itemSelectColor;
            SetItemName(currentItem);
        }

        if(activateSpoopMode == false)
        {
            ChristmasDude.SetActive(true);
            ChristmasDudeMad.SetActive(false);
            entrance.ExitTop();
        }
    }

    public void UpdatePresentCount()
    {
        presentsCounter.text = maxPresents + "/" + Presents + " Presents";

        if (maxPresents == 2)
        {
            SpoopMode();
            activateSpoopMode = true;
        }

        if(maxPresents == Presents)
        {
            FinaleMode();
            activateFinaleMode = true;
        }
    }

    public void CollectPresent()
    {
        maxPresents++;
        player.stamina = 100;
        christmasdude.madvalue -= 0.1f;
    }

    public void SpoopMode()
    {
        ChristmasDude.SetActive(false);
        ChristmasDudeMad.SetActive(true);
        entrance.ExitDown();
        gameMusic.Stop();
        activateSpoopMode = true;
    }

    public void FinaleMode()
    {
        entrance.ExitTop();
        activateFinaleMode = true;
    }
    private void ItemColorClear()
    {
        itemBG[0].color = Color.white;
        itemBG[1].color = Color.white;
        itemBG[2].color = Color.white;
        itemBG[3].color = Color.white;
        SetItemName(currentItem);
    }

    private void SetItemName(int current)
    {
        if (itemNames[current] == null || itemNames[current] == "")
        {
            currentItemText.text = "Nothing";
        }
        else
        {
            currentItemText.text = itemNames[current];
        }
    }

    public void RemoveItem(int item)
    {
        itemImages[item].sprite = emptyItem;
        itemNames[item] = "";
        SetItemName(currentItem);
    }
    public void UseItem()
    {
        if (itemNames[currentItem] == "name of item")
        {
            
        }
    }
    }
