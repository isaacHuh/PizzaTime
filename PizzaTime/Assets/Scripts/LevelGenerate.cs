using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    public static int level = 0;
    public int arraySize = 10;
    public int[,] levelArray;

    public List<GameObject> blocks;
    public float blockSize = 5;

    public List<Vector2> openSpots = new List<Vector2>();

    private void Awake()
    {
        LevelGenerate.level++;
        levelArray = new int[arraySize, arraySize];

        while (openSpots.Count < (arraySize * arraySize)/2)
        {
            CreateLevel();
        }

        CreateMap();
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        LevelGenerate.level++;
        levelArray = new int[arraySize, arraySize];

        while (openSpots.Count < 10)
        {
            createLevel();
        }

        createMap();
        */

        //createEnemies(LevelGeneration.level);

        //createEnd();
    }


    private void CreateLevel()
    {
        openSpots.Clear();
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                levelArray[i, j] = -1;
            }
        }

        int x = arraySize / 2;
        int y = arraySize / 2;

        levelArray[x, y] = 1;

        //player.transform.position = new Vector3(x * blockSize, 0, y * blockSize);

        ChooseType(x - 1, y);
        ChooseType(x + 1, y);
        ChooseType(x, y - 1);
        ChooseType(x, y + 1);

    }

    public int[,] GetLevelArray() {
        return levelArray;
    }

    private void ChooseType(int x, int y)
    {
        int randomType = Random.Range(0, 2);

        if (x < 0 || x >= arraySize)
        {
            return;
        }

        if (y < 0 || y >= arraySize)
        {
            return;
        }


        if (levelArray[x, y] != -1)
        {
            return;
        }

        levelArray[x, y] = randomType;

        if (randomType == 0)
        {
            return;
        }
        else
        {
            openSpots.Add(new Vector2(x, y));
        }

        ChooseType(x - 1, y);
        ChooseType(x + 1, y);
        ChooseType(x, y - 1);
        ChooseType(x, y + 1);
    }

    private void CreateMap()
    {
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                if (levelArray[i, j] != 1)
                {
                    if (levelArray[i, Mathf.Clamp(j + 1, 0, arraySize - 1)] == 1 || 
                        levelArray[i, Mathf.Clamp(j - 1, 0, arraySize - 1)] == 1 ||
                        levelArray[Mathf.Clamp(i + 1, 0, arraySize - 1), j] == 1 ||
                        levelArray[Mathf.Clamp(i - 1, 0, arraySize - 1), j] == 1) {

                        Quaternion angle = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);
                        Instantiate(blocks[Random.Range(0, blocks.Count)], transform.position + new Vector3(i * blockSize, -1.1f, j * blockSize), angle, transform);
                    }
                }
            }
        }

        //create outer layer
        /*
        for (int i = 0; i < arraySize + 1; i++)
        {
            Instantiate(block, new Vector3(i * blockSize, 0, -1 * blockSize), Quaternion.identity, transform);
            Instantiate(block, new Vector3(-1 * blockSize, 0, i * blockSize), Quaternion.identity, transform);

            Instantiate(block, new Vector3(i * blockSize, 0, (arraySize + 1) * blockSize), Quaternion.identity, transform);
            Instantiate(block, new Vector3((arraySize + 1) * blockSize, 0, i * blockSize), Quaternion.identity, transform);
        }
        */
    }
}