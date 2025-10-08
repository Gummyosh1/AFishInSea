using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingSprites : MonoBehaviour
{
    public Sprite[] I1H1;
    public Sprite[] I1H2;

    public Sprite[] I2H1;
    public Sprite[] I2H2;

    public Sprite[] I3H1;
    public Sprite[] I3H2;

    public Sprite[] I4H1;
    public Sprite[] I4H2;

    public Sprite[] I5H1;
    public Sprite[] I5H2;

    public Sprite[] I6H1;
    public Sprite[] I6H2;

    public Sprite[] I7H1;
    public Sprite[] I7H2;

    public Sprite[] I8H1;
    public Sprite[] I8H2;

    public Sprite[] I9H1;
    public Sprite[] I9H2;

    public Sprite[] I10H1;
    public Sprite[] I10H2;

    public Sprite[] I11H1;
    public Sprite[] I11H2;
    public Sprite[] I11H3;

    public Sprite[] I12H1;
    public Sprite[] I12H2;

    public Sprite[] I13H1;
    public Sprite[] I13H2;
    public Sprite[] I13H3;

    public Sprite[] I14H1;
    public Sprite[] I14H2;
    public Sprite[] I14H3;

    public Sprite[] I15H1;

    public Sprite[] I16H1;
    public Sprite[] I16H2;


    [NonSerialized] public string[] I1N1 = {"Hop","",""};
    [NonSerialized] public string[] I1N2 = {"Bonney","",""};

    [NonSerialized] public string[] I2N1 = {"Garth","",""};
    [NonSerialized] public string[] I2N2 = {"Petunia","Emily",""};

    [NonSerialized] public string[] I3N1 = {"Peach","Jack 'Superboy'",""};
    [NonSerialized] public string[] I3N2 = {"Josh Fire","",""};

    [NonSerialized] public string[] I4N1 = {"Captain Dash","",""};
    [NonSerialized] public string[] I4N2 = {"Moss Head","",""};

    [NonSerialized] public string[] I5N1 = {"Laila","Grem",""};
    [NonSerialized] public string[] I5N2 = {"Guardian","",""};

    [NonSerialized] public string[] I6N1 = {"Blight","",""};
    [NonSerialized] public string[] I6N2 = {"Nami","Fireboy",""};

    [NonSerialized] public string[] I7N1 = {"Robin","",""};
    [NonSerialized] public string[] I7N2 = {"Pumpkin","",""};

    [NonSerialized] public string[] I8N1 = {"Pinky","Lightmare","Grenad"};
    [NonSerialized] public string[] I8N2 = {"Skipper","",""};

    [NonSerialized] public string[] I9N1 = {"Jane","Marlia",""};
    [NonSerialized] public string[] I9N2 = {"Amelia","",""};

    [NonSerialized] public string[] I10N1 = {"Rockworth","Brightful",""};
    [NonSerialized] public string[] I10N2 = {"Brown Eyes","",""};

    [NonSerialized] public string[] I11N1 = {"Sunny","",""};
    [NonSerialized] public string[] I11N2 = {"Mr. Claus","Mrs. Claus",""};
    [NonSerialized] public string[] I11N3 = {"Skelly","",""};

    [NonSerialized] public string[] I12N1 = {"Lift Knight","",""};
    [NonSerialized] public string[] I12N2 = {"Poppy","Capri",""};

    [NonSerialized] public string[] I13N1 = {"Bluen","Gretta","Damascus"};
    [NonSerialized] public string[] I13N2 = {"Ironheart","",""};
    [NonSerialized] public string[] I13N3 = {"Zyra","Vlert",""};

    [NonSerialized] public string[] I14N1 = {"Highlorn","Vivi",""};
    [NonSerialized] public string[] I14N2 = {"Keppler","",""};
    [NonSerialized] public string[] I14N3 = {"Cobalt Warrior","",""};

    [NonSerialized] public string[] I15N1 = {"Adamantite","",""};

    [NonSerialized] public string[] I16N1 = {"Glorious","",""};
    [NonSerialized] public string[] I16N2 = {"Fallen One","",""};

    public Sprite[][][] Islands = new Sprite[16][][];

    public string[][][] Names = new string[16][][];

    public void loadSpriteSpreadsheet(){
        for (int i = 0; i < 16; i++){
            Islands[i] = new Sprite[3][];
        }

        for (int i = 0; i < 16; i++){
            Names[i] = new string[3][];
        }

        Islands[0][0] = I1H1;
        Islands[0][1] = I1H2;
        
        Islands[1][0] = I2H1;
        Islands[1][1] = I2H2;

        Islands[2][0] = I3H1;
        Islands[2][1] = I3H2;

        Islands[3][0] = I4H1;
        Islands[3][1] = I4H2;

        Islands[4][0] = I5H1;
        Islands[4][1] = I5H2;

        Islands[5][0] = I6H1;
        Islands[5][1] = I6H2;

        Islands[6][0] = I7H1;
        Islands[6][1] = I7H2;

        Islands[7][0] = I8H1;
        Islands[7][1] = I8H2;

        Islands[8][0] = I9H1;
        Islands[8][1] = I9H2;

        Islands[9][0] = I10H1;
        Islands[9][1] = I10H2;

        Islands[10][0] = I11H1;
        Islands[10][1] = I11H2;
        Islands[10][2] = I11H3;

        Islands[11][0] = I12H1;
        Islands[11][1] = I12H2;

        Islands[12][0] = I13H1;
        Islands[12][1] = I13H2;
        Islands[12][2] = I13H3;

        Islands[13][0] = I14H1;
        Islands[13][1] = I14H2;
        Islands[13][2] = I14H3;

        Islands[14][0] = I15H1;

        Islands[15][0] = I16H1;
        Islands[15][1] = I16H2;

        //NAME INIT

        Names[0][0] = I1N1;
        Names[0][1] = I1N2;
        
        Names[1][0] = I2N1;
        Names[1][1] = I2N2;

        Names[2][0] = I3N1;
        Names[2][1] = I3N2;

        Names[3][0] = I4N1;
        Names[3][1] = I4N2;

        Names[4][0] = I5N1;
        Names[4][1] = I5N2;

        Names[5][0] = I6N1;
        Names[5][1] = I6N2;

        Names[6][0] = I7N1;
        Names[6][1] = I7N2;

        Names[7][0] = I8N1;
        Names[7][1] = I8N2;

        Names[8][0] = I9N1;
        Names[8][1] = I9N2;

        Names[9][0] = I10N1;
        Names[9][1] = I10N2;

        Names[10][0] = I11N1;
        Names[10][1] = I11N2;
        Names[10][2] = I11N3;

        Names[11][0] = I12N1;
        Names[11][1] = I12N2;

        Names[12][0] = I13N1;
        Names[12][1] = I13N2;
        Names[12][2] = I13N3;

        Names[13][0] = I14N1;
        Names[13][1] = I14N2;
        Names[13][2] = I14N3;

        Names[14][0] = I15N1;

        Names[15][0] = I16N1;
        Names[15][1] = I16N2;
    }

    public void Start(){
        loadSpriteSpreadsheet();
    }
}
