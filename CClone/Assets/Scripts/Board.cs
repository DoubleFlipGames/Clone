using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]public int width; //grid alaný
    [SerializeField]public int height;

    public GameObject[] dots;//olusturalacak assetler 

    public GameObject tilePrefab;

    private BackgroundTile[,] allTiles; //gridler

    public GameObject[,] allDots;


    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[width, height];  //grid oluþturma
        allDots = new GameObject[width, height];
        SetUp(); //instantiate fonksiyon
    }

    private void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "(" + i + "," + j + ")";

                //kullanýlacak assetler,þekerler

                int dotToUse = Random.Range(0, dots.Length);

                int maxIterations = 0; 
                while (MachesAt(i, j, dots[dotToUse]) && maxIterations < 100){
                    dotToUse = Random.Range(0, dots.Length);
                    maxIterations++;
                }
                maxIterations = 0;

                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = "(" + i + "," + j + ")";
                allDots[i, j] = dot;
            }
        }

    }

    private bool MachesAt(int column,int row, GameObject piece)
    {
        if (column > 1 && row >1)
        {
            if (allDots[column-1,row].tag == piece.tag && allDots[column-2,row].tag == piece.tag)
            {
                return true;
            }
            if (allDots[column, row-1].tag == piece.tag && allDots[column, row-2].tag == piece.tag)
            {
                return true;
            }

        }else if(column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if(allDots[column,row-1].tag == piece.tag && allDots[column,row-2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allDots[column-1, row].tag == piece.tag && allDots[column-2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }

        return false;
    }






    // Update is called once per frame
    void Update()
    {

    }
}
