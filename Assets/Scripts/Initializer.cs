using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Initializer : MonoBehaviour
{

    public int LevelNumber=1;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;
    public GameObject block5;
    public float Scalar = 48;
    public float cellSize = 49;

    // Use this for initialization
    void Start () {

        //level 2 was changed
        //pay attention  solution searching for level 100 takes more than 1 minute
        levels.Add("g221h243g255g346v211v222v232v234v241v244v361");
        levels.Add("g221g222g216g246g234h233v241v323v352v235v364");
        levels.Add("g311h213g214g255g326v215v232v234v351v261v263");
        levels.Add("g321h213g234g256v211v214v324v232v245v251v253v261");
        levels.Add("g211g312h223g244g245g326v213v215v234v351v263v265");
        levels.Add("g321h213g254g225g326v211v332v242v244v255v361");
        levels.Add("g231g251g232h213g214g244g315g316v211v233v245v252v362");
        levels.Add("g331h213g234g255g216v214v221v224v235v352v261");
        levels.Add("g341h223g234g216g246v311v224v231v235v242v362");
        levels.Add("g331g332h213g214g226v215v221v333v344v253v361");
        levels.Add("g252h213g244g325g216g236v214v332v241v255v363");
        levels.Add("g211g251g242h243g224g255g326v314v222v331v244v362");
        levels.Add("g321h243g314g255g346v311v225v232v244v361");
        levels.Add("g211g231g212h213g225g226v214v332v242v244v251v261");
        levels.Add("g221g241h213g214g234g245g216v211v235v352v262");
        levels.Add("g341g212h223g254g215g235g216g236v213v231v243v255");
        levels.Add("g231g251h233g214g255g326v215v321v244v253v362");
        levels.Add("g311g212h213g225g245g216g236v214v232v241v243v261");
        levels.Add("g311h213g254g235g326v215v332v241v243v255v261");
        levels.Add("g221g241g322h213g214g236g256v211v215v225v243v352");
        levels.Add("g311g212h223g234g345g346v213v235v341v351v261v263");
        levels.Add("g211h213g254g235g236g256v314v225v241v243v351v261");
        levels.Add("g321g232h213g254g315g216g236v211v233v243v351v262v265");
        levels.Add("g211h233g314g255g256v222v231v235v344v261v263");
        levels.Add("g231g251g232h213g254g235g316v211v214v221v224v233v243v255v265");
        levels.Add("g341g252h223g235g226g246v212v224v231v242v353v264");
        levels.Add("g321g322h213g224g255g216v211v235v344v361");
        levels.Add("g331h213g324g255g256v214v221v232v235v245v253v261v263");
        levels.Add("g331g212g232h223g224g245g216v213v235v352v261v263");
        levels.Add("g211g212g232h213g254g236g256v314v324v233v243v351v261");
        levels.Add("g251h223g214g336v311v225v231v234v341v253v263v265");
        levels.Add("g221h243g214g255g226v215v222v232v234v241v245v251v361");
        levels.Add("g341g232g252h213g315g316v211v233v244v263v265");
        levels.Add("g311g212h213g224g244g235g255g316v214v232v241v261v263");
        levels.Add("g331h233g214g345g226g246v311v321v234v352v261");
        levels.Add("g221g312h213g244g235g316v214v224v233v351v263v265");
        levels.Add("g331h233g214g345g316v311v321v234v352v261v263");
        levels.Add("g321g322h213g255g256v211v233v243v245v251v361");
        levels.Add("g241g242h223g224g254g315v212v231v243v245v361");
        levels.Add("g321g252h223g244g215g216v311v334v242v263v265");
        levels.Add("g311g232h213g254g235v224v233v243v351v262v265");
        levels.Add("g321g322h213g214g255v211v233v343v251v253v361");
        levels.Add("g311g252h213g244g325g216g236v214v332v241v255v363");
        levels.Add("g311g212h213g224g244g235g255g216v214v232v241v261v263");
        levels.Add("g231g251g332h233g214g255g226g246v211v321v234v244v362");
        levels.Add("g221h243g214g244g255g226g256v222v232v234v245v251v362");
        levels.Add("g211g242h223g214g255g326v212v215v231v343v261v263");
        levels.Add("g211g231h213g254g225g236v215v332v242v244v361");
        levels.Add("g321g222h213g254g335g316v211v214v224v233v351v262v265");
        levels.Add("g211g212h243g254g225g255g326v214v223v331v244v361");
        levels.Add("g252h223g254g245g246v212v214v221v334v241v243v265");
        levels.Add("g231h213g224g256v314v221v232v235v342v251v253v261v263");
        levels.Add("g331g232h213g214g234g255g216v211v221v235v352v261v263");
        levels.Add("g321g222h213g255g326v211v233v242v244v251v261v263");
        levels.Add("g321g322h213g255g216g256v211v233v243v245v361");
        levels.Add("g221g252h243g214g216g236v311v222v232v234v241v263v265");
        levels.Add("g212h243g234g255g226g256v313v223v331v245v261v263");
        levels.Add("g221g251g252h213g224g325g316v211v232v341v353v264");
        levels.Add("g341h213g214g244g236v225v231v233v242v364");
        levels.Add("g221g252h233g234g215g216g246v211v223v235v241v253v263v265");
        levels.Add("g211g231h223g224g346v212v214v225v235v342v251v253v261v263");
        levels.Add("g251h223g314g245g216g246v311v235v241v243v352v263");
        levels.Add("g231g232h213g255g346v314v221v233v235v343v251");
        levels.Add("g331h233g214g345g226g246v212v321v234v352v261");
        levels.Add("g231h213g224g226g246v221v232v242v244v351v261v263");
        levels.Add("g231g251g252h213g324g246v314v221v235v242v353v364");
        levels.Add("g211g241h223g324g255v312v231v245v352v362");
        levels.Add("g231g322h213g255g256v211v233v243v245v251v261v263");
        levels.Add("g311g252h213g324g225g245g226g246v314v232v241v363");
        levels.Add("g231g232h213g254g245g326v215v221v333v243v262v265");
        levels.Add("g311g212h213g224g254g245g216v214v235v241v243v251v261v265");
        levels.Add("g221g241g252h213g224g315g326v211v232v242v244v254v363");
        levels.Add("g341g252h223g235g336v211v213v224v231v242v353v263v265");
        levels.Add("g211g242h233g214g315g316v212v231v344v253v255v261v263");
        levels.Add("g321g322h213g214g255v211v233v243v245v251v253v361");
        levels.Add("g211h223g344g315g216g236v312v231v341v265");
        levels.Add("g231g322h213g214g255v211v233v243v245v251v253");
        levels.Add("g231g251g252h213g324g245v314v221v235v242v253v363");
        levels.Add("g241g242h223g254g215g316v211v213v231v243v245v361");
        levels.Add("g251h213g214g325g226v215v232v241v243v252v262v264");
        levels.Add("g311g222h233g214g245g236g256v212v225v234v241v351v261");
        levels.Add("g311g212h213g224g254g245g216v214v235v241v243v261v265");
        levels.Add("g211g242h233g214g315v212v231v344v253v255v361");
        levels.Add("g221g241g252h213g224g244g315g316v211v232v242v245v263");
        levels.Add("g321g322h213g255g256v211v233v243v245v251v253v261");
        levels.Add("g311g322h243g214g255g316v212v233v344v261v263");
        levels.Add("g311h223g214g245g226g246v212v234v241v243v262v264");
        levels.Add("g211g241h223g324g255g216g256v312v231v245v352v362");
        levels.Add("g321g232h213g214g255g216v211v233v243v245v351v361");
        levels.Add("g241h213g324g225g216v214v331v242v245v361");
        levels.Add("g231g251h233g214g255g226g246v311v321v234v244v253v362");
        levels.Add("g251g252h213g324g216g236v214v221v331v241v364");
        levels.Add("g231g251g342h213g224g315v211v232v244v353v263v265");
        levels.Add("g311g222h223g254g245g316v312v234v241v243v251v265");
        levels.Add("g221g241g242h213g254g315v211v232v243v245v255v261");
        levels.Add("g312h213g244g225v214v233v242v245v351v362");
        levels.Add("g321g322h213g214g255v211v233v243v245v351v261v263");
        levels.Add("g311g312h223g254g245v213v234v241v243v351v265");
        levels.Add("g231g251g332h213g254g235g326v215v221v233v243v255v262v265");
        levels.Add("g231g322h213g255g226g256v211v233v243v245v351v261");

        LoadLevel(LevelNumber);

    }

    void LoadLevel(int number)
    {
        string level = levels[number - 1];
        for (int i = 0; i < level.Length; i+=4)
        {
            int x = int.Parse(level.Substring(i+2, 1));
            int y = int.Parse(level.Substring(i+3, 1));
            SetFigure(level.Substring(i,2), x, y);
        }
    }

    void SetFigure(string figure, int x,int y)
    {
        var size = int.Parse(figure[1].ToString());
        var vertical = (figure[0]=='v');
        float yc = 0.5f;
        float xc = 0.5f;
        if (vertical)
        {
            xc = 0.5f;
            yc = size == 2 ? 1 : 1.5f;
        }
        else
        {
            yc = 0.5f;
            xc = size == 2 ? 1 : 1.5f;
        }
        var Y = 160 - 46*yc- cellSize * (y-1);
        var X = -135 + 46*xc + cellSize*(x - 1);

        GameObject obj=new GameObject();
        if (figure == "v2") obj = block1;
        if (figure == "g3") obj = block2;
        if (figure == "v3") obj = block3;
        if (figure == "g2") obj = block4;
        if (figure == "h2") obj = block5;
        var t = Instantiate(obj);
        t.GetComponent<BlockScript>().codeName = figure.ToString() + x.ToString() + y.ToString();
        t.tag = "Block";
        t.transform.parent = this.transform;
        t.transform.localPosition = new Vector3(X,Y, 0);
        t.transform.localScale = new Vector3(Scalar, Scalar, 1);
    }
    // Update is called once per frame
    void Update () {
	
	}

    public List<string> levels = new List<string>();

}
