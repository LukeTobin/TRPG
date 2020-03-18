using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsLoader : MonoBehaviour
{
    [SerializeField] GameObject UI_Element;
    GameObject[] unitArray;
    Sprite[] teamSprites;
    GameObject[] playerUnitsUI;

    // Start is called before the first frame update
    void Start()
    {
        //This code creates UI elements at the left side of the screen for player stats
        unitArray = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < unitArray.Length; i++)
        {
            if (unitArray[i].transform.parent.name == "Team")
            {
                //For all the teammates, but since I'm going to be refactoring it, and because it looks fuller, I'll leave it down here for all units to access
            }

            var instantiatedUI = Instantiate(UI_Element, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            instantiatedUI.transform.parent = gameObject.transform;
            instantiatedUI.transform.position = new Vector2(transform.position.x + 120, transform.position.y - 80 * i);
            instantiatedUI.transform.Find("Character").GetComponent<Image>().sprite =
                unitArray[i].GetComponent<SpriteRenderer>().sprite;
            instantiatedUI.transform.Find("HealthText").GetComponent<Text>().text =
                unitArray[i].GetComponent<Unit>().health.ToString() + "/" +
                unitArray[i].GetComponent<Unit>().maxHealth.ToString();
        }     
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //playerUnitsUI = GameObject.FindGameObjectsWithTag("HealthText");
            //playerUnitsUI[0].GetComponent<Text>().text ;
        //}
    }

    
}
