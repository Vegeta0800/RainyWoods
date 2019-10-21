using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RainyWoods;

// TODO: Rewrite
public class MenuController : MonoBehaviour
{
    public Player player;

    public float imageScale = 1.0f;
    public Sprite downImage;
    public Sprite otherImage;

    public Sprite[] abilities;

    public Sprite markedDownImage;
    public Sprite markedOtherImage;

    public GameObject[] images;
    public GameObject closedMenu;
    public GameObject openedMenu;
    public GameObject currentAbility;
    public GameObject currentCount;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("L1"))
        {
            if (player.mechanicIndex == 1)
            {
                images[0].GetComponent<Image>().sprite = otherImage;
                images[1].GetComponent<Image>().sprite = markedDownImage;
                images[2].GetComponent<Image>().sprite = otherImage;
                images[3].GetComponent<Image>().sprite = otherImage;
            }
            else
            {
                images[0].GetComponent<Image>().sprite = otherImage;
                images[1].GetComponent<Image>().sprite = downImage;
                images[2].GetComponent<Image>().sprite = otherImage;
                images[3].GetComponent<Image>().sprite = otherImage;
                images[player.mechanicIndex].GetComponent<Image>().sprite = markedOtherImage;
            }

            closedMenu.SetActive(false);
            openedMenu.SetActive(true);
        }
        else
        {
            closedMenu.SetActive(true);
            openedMenu.SetActive(false);

            currentAbility.GetComponent<Image>().sprite = abilities[player.mechanicIndex];
            currentAbility.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(abilities[player.mechanicIndex].texture.width * imageScale, abilities[player.mechanicIndex].texture.height * imageScale);
            currentCount.GetComponent<Text>().text = player.mechanics[player.mechanicIndex].amount.ToString();
        }
    }
}
