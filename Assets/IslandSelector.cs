using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class IslandSelector : MonoBehaviour
{
    public IslandScript islandScript;
    public GameProgress gameProgress;
    public Sprite unselectedCircle;
    public Sprite selectedCircle;
    public Sprite completedCircle;
    public GameObject[] islands;
    public TMP_Text[] textInfoHolders;

    //ISLAND DATA
    public IslandData islandOne = new IslandData{
        name = "Mainyard",
        villageType = "Tourist Island",
        population = 2,
    };
    public IslandData islandTwo = new IslandData{
        name = "Southland",
        villageType = "Farming Island",
        population = 2,
    };
    public IslandData islandThree = new IslandData{
        name = "Multan",
        villageType = "Tourist Island",
        population = 4,
    };
    public IslandData islandFour = new IslandData{
        name = "Grackheart",
        villageType = "Hunting Island",
        population = 2,
    };
    public IslandData islandFive = new IslandData{
        name = "Dillmans Paradise",
        villageType = "Fishing Island",
        population = 2,
    };
    public IslandData islandSix = new IslandData{
        name = "Huntsward",
        villageType = "Hunting Island",
        population = 3,
    };
    public IslandData islandSeven = new IslandData{
        name = "Tylenmar",
        villageType = "Farming Island",
        population = 3,
    };
    public IslandData islandEight = new IslandData{
        name = "Fedolfaim",
        villageType = "Fishing Island",
        population = 2,
    };
    public IslandData islandNine = new IslandData{
        name = "Parlontamite",
        villageType = "Tourist Island",
        population = 1,
    };
    public IslandData islandTen = new IslandData{
        name = "Parralaxia",
        villageType = "Hunting Island",
        population = 2,
    };
    public IslandData islandEleven = new IslandData{
        name = "Southern Luith",
        villageType = "Fishing Island",
        population = 2,
    };
    public IslandData islandTwelve = new IslandData{
        name = "Northern Luith",
        villageType = "Fishing Island",
        population = 1,
    };
    public IslandData islandThirteen = new IslandData{
        name = "The Dip",
        villageType = "Hunting Island",
        population = 3,
    };
    public IslandData islandFourteen = new IslandData{
        name = "Gate's Gail",
        villageType = "Farming Island",
        population = 3,
    };
    public IslandData islandFifteen = new IslandData{
        name = "The Groom",
        villageType = "City Town",
        population = 53,
    };
    public IslandData islandSixteen = new IslandData{
        name = "Deaths Bride",
        villageType = "City Town",
        population = 71,
    };
    public IslandData islandSeventeen = new IslandData{
        name = "Isle of The 50 Wandering Souls",
        villageType = "City Town",
        population = 89,
    };
    public IslandData islandEighteen = new IslandData{
        name = "?????", // Extrana, name reveal
        villageType = "?????",
        population = 100,
    };
    // ISLAND DATA END
    


    public void islandTouch(GameObject tappedIsland){
        if (tappedIsland.GetComponent<Image>().sprite != selectedCircle){
            foreach (GameObject i in islands){
                i.GetComponent<Image>().sprite = unselectedCircle;
            }
            for (int i = gameProgress.currentIslandIndex - 1; i >= 0; i--) {
                islands[i].GetComponent<Image>().sprite = completedCircle;
            }
            tappedIsland.GetComponent<Image>().sprite = selectedCircle;
        }

        switch(tappedIsland.name){
            case "Island1":
                islandOne.fishGiven = islandScript.fishGivenList[0];
                infoSetter(islandOne);
                break;
            case "Island2":
                islandTwo.fishGiven = islandScript.fishGivenList[1];
                infoSetter(islandTwo);
                break;
            case "Island3":
                islandThree.fishGiven = islandScript.fishGivenList[2];
                infoSetter(islandThree);
                break;
            case "Island4":
                islandFour.fishGiven = islandScript.fishGivenList[3];
                infoSetter(islandFour);
                break;
            case "Island5":
                islandFive.fishGiven = islandScript.fishGivenList[4];
                infoSetter(islandFive);
                break;
            case "Island6":
                islandSix.fishGiven = islandScript.fishGivenList[5];
                infoSetter(islandSix);
                break;
            case "Island7":
                islandSeven.fishGiven = islandScript.fishGivenList[6];
                infoSetter(islandSeven);
                break;
            case "Island8":
                islandEight.fishGiven = islandScript.fishGivenList[7];
                infoSetter(islandEight);
                break;
            case "Island9":
                islandNine.fishGiven = islandScript.fishGivenList[8];
                infoSetter(islandNine);
                break;
            case "Island10":
                islandTen.fishGiven = islandScript.fishGivenList[9];
                infoSetter(islandTen);
                break;
            case "Island11":
                islandEleven.fishGiven = islandScript.fishGivenList[10];
                infoSetter(islandEleven);
                break;
            case "Island12":
                islandTwelve.fishGiven = islandScript.fishGivenList[11];
                infoSetter(islandTwelve);
                break;
            case "Island13":
                islandThirteen.fishGiven = islandScript.fishGivenList[12];
                infoSetter(islandThirteen);
                break;
            case "Island14":
                islandFourteen.fishGiven = islandScript.fishGivenList[13];
                infoSetter(islandFourteen);
                break;
            case "Island15":
                islandFifteen.fishGiven = islandScript.fishGivenList[14];
                infoSetter(islandFifteen);
                break;
            case "Island16":
                islandSixteen.fishGiven = islandScript.fishGivenList[15];
                infoSetter(islandSixteen);
                break;
            case "Island17":
                islandSeventeen.fishGiven = islandScript.fishGivenList[16];
                infoSetter(islandSeventeen);
                break;
            case "Island18":
                islandEighteen.fishGiven = islandScript.fishGivenList[17];
                infoSetter(islandEighteen);
                break;
        }
    }

    public void infoSetter(IslandData island){
        textInfoHolders[0].text = island.name;
        textInfoHolders[1].text = island.villageType;
        textInfoHolders[2].text = "Population: " + island.population;
        textInfoHolders[3].text = "Fish Given: " + island.fishGiven;
        textInfoHolders[4].text = "Distance: \n" + island.timeBetweenIslands + " hours";
    }

    public void mapButtonInit(){
        for (int i = gameProgress.currentIslandIndex - 1; i >= 0; i--){
            islands[i].GetComponent<Image>().sprite = completedCircle;
        }
        switch(gameProgress.currentIslandIndex){
            case 0:
                islandOne.fishGiven = islandScript.fishGivenList[0];
                infoSetter(islandOne);
                break;
            case 1:
                islandTwo.fishGiven = islandScript.fishGivenList[1];
                infoSetter(islandTwo);
                break;
            case 2:
                islandThree.fishGiven = islandScript.fishGivenList[2];
                infoSetter(islandThree);
                break;
            case 3:
                islandFour.fishGiven = islandScript.fishGivenList[3];
                infoSetter(islandFour);
                break;
            case 4:
                islandFive.fishGiven = islandScript.fishGivenList[4];
                infoSetter(islandFive);
                break;
            case 5:
                islandSix.fishGiven = islandScript.fishGivenList[5];
                infoSetter(islandSix);
                break;
            case 6:
                islandSeven.fishGiven = islandScript.fishGivenList[6];
                infoSetter(islandSeven);
                break;
            case 7:
                islandEight.fishGiven = islandScript.fishGivenList[7];
                infoSetter(islandEight);
                break;
            case 8:
                islandNine.fishGiven = islandScript.fishGivenList[8];
                infoSetter(islandNine);
                break;
            case 9:
                islandTen.fishGiven = islandScript.fishGivenList[9];
                infoSetter(islandTen);
                break;
            case 10:
                islandEleven.fishGiven = islandScript.fishGivenList[10];
                infoSetter(islandEleven);
                break;
            case 11:
                islandTwelve.fishGiven = islandScript.fishGivenList[11];
                infoSetter(islandTwelve);
                break;
            case 12:
                islandThirteen.fishGiven = islandScript.fishGivenList[12];
                infoSetter(islandThirteen);
                break;
            case 13:
                islandFourteen.fishGiven = islandScript.fishGivenList[13];
                infoSetter(islandFourteen);
                break;
            case 14:
                islandFifteen.fishGiven = islandScript.fishGivenList[14];
                infoSetter(islandFifteen);
                break;
            case 15:
                islandSixteen.fishGiven = islandScript.fishGivenList[15];
                infoSetter(islandSixteen);
                break;
            case 16:
                islandSeventeen.fishGiven = islandScript.fishGivenList[16];
                infoSetter(islandSeventeen);
                break;
            case 17:
                islandEighteen.fishGiven = islandScript.fishGivenList[17];
                infoSetter(islandEighteen);
                break;
        }
        islands[gameProgress.currentIslandIndex].GetComponent<Image>().sprite = selectedCircle;
    }

    public void backArrow(){
        foreach (GameObject i in islands){
            i.GetComponent<Image>().sprite = unselectedCircle;
        }
    }
    


}

public class IslandData
{
    [NonSerialized]
    public string name;
    [NonSerialized]
    public string villageType;
    [NonSerialized]
    public int population;
    [NonSerialized]
    public double timeBetweenIslands = 0;
    [NonSerialized]
    public int fishGiven = 0;
}
