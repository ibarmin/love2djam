using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public enum GameState
{
    Playing,
    Won
}

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.Playing;
    ItemArray matrix;
    public GameObject GO2, GO4, GO8, GO16, GO32, GO64, GO128, GO256, GO512, GO1024, blankGO;
    public Text ScoreText, DebugText;
    private readonly float distance = 0.109f;

    public IInputDetector inputDetector;

    private int ZIndex = 0, score = 0;

    //will read a file from Resources folder
    //and create the matrix with the preloaded data
    void InitArrayWithPremadeData()
    {
        string[,] sampleArray = Utilities.GetMatrixFromResourcesData();
        for (int row = 0; row < Globals.Rows; row++) {
            for (int column = 0; column < Globals.Columns; column++) {
                if (int.TryParse(sampleArray[Globals.Rows - 1 - row, column], out int value)) {
                    CreateNewItem(value, row, column);
                }
            }
        }
    }

    // Use this for initialization
    public void Start()
    {
        InitialPositionBackgroundSprites();

        Initialize();

        inputDetector = GetComponent<IInputDetector>();


        string x = Utilities.ShowMatrixOnConsole(matrix);
        DebugDisplay(x);
    }

    public void Initialize()
    {
        if (matrix != null) {
            for (int row = 0; row < Globals.Rows; row++) {
                for (int column = 0; column < Globals.Columns; column++) {
                    if (matrix[row, column] != null && matrix[row, column].GO != null)
                        Destroy(matrix[row, column].GO);
                }
            }
        }

        matrix = new ItemArray();



        //InitArrayWithPremadeData();
        CreateNewItem();
        CreateNewItem();

        score = 0;
        UpdateScore(0);

        gameState = GameState.Playing;
    }

    public void Complete()
    {
        gameState = GameState.Won;
    }

    void DebugDisplay(string content)
    {
        DebugText.text = content;
    }

    private void CreateNewItem(int value = 2, int? row = null, int? column = null)
    {
        int randomRow, randomColumn;

        if (row == null && column == null) {
            matrix.GetRandomRowColumn(out randomRow, out randomColumn);
        } else {
            randomRow = row.Value;
            randomColumn = column.Value;
        }

        var newItem = new Item
        {
            Row = randomRow,
            Column = randomColumn,
            Value = value
        };

        GameObject newGo = GetGOBasedOnValue(value);
        newGo.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        newItem.GO = Instantiate(newGo, this.transform.position +
        new Vector3(randomColumn + randomColumn * distance, randomRow + randomRow * distance, ZIndex),
        Quaternion.identity) as GameObject;

        newItem.GO.transform.localScale =  new Vector3(1.0f, 1.0f, 1.0f);

        matrix[randomRow, randomColumn] = newItem;
    }



    private void InitialPositionBackgroundSprites()
    {
        for (int row = 0; row < Globals.Rows; row++)
        {
            for (int column = 0; column < Globals.Columns; column++)
            {
                Instantiate(blankGO, transform.position +
                new Vector3(column + column * distance, row + row * distance, ZIndex), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Playing)
        {
            InputDirection? value = inputDetector.DetectInputDirection();


            if (value.HasValue)
            {
                List<ItemMovementDetails> movementDetails = new();
                //Debug.Log(value);
                if (value == InputDirection.Left)
                    movementDetails = matrix.MoveHorizontal(HorizontalMovement.Left);
                else if (value == InputDirection.Right)
                    movementDetails = matrix.MoveHorizontal(HorizontalMovement.Right);
                else if (value == InputDirection.Top)
                    movementDetails = matrix.MoveVertical(VerticalMovement.Top);
                else if (value == InputDirection.Bottom)
                    movementDetails = matrix.MoveVertical(VerticalMovement.Bottom);


                if (movementDetails.Count > 0)
                {
                    StartCoroutine(AnimateItems(movementDetails));
                }
                string x = Utilities.ShowMatrixOnConsole(matrix);
                DebugDisplay(x);
            }
        }
    }

    IEnumerator AnimateItems(IEnumerable<ItemMovementDetails> movementDetails)
    {
        List<GameObject> objectsToDestroy = new();
        foreach (var item in movementDetails) {
            //calculate the new position in the world space
            var newGoPosition = new Vector3(item.NewColumn + item.NewColumn * distance,
                item.NewRow + item.NewRow * distance, ZIndex);

            //move it there
            item.GOToAnimatePosition.transform.position = transform.position + newGoPosition;

            //the scale is != null => this means that this item will also move and duplicate
            if (item.WillMoveAndDuplicate) {
                var duplicatedItem = matrix[item.NewRow, item.NewColumn];

                UpdateScore(duplicatedItem.Value);

                //check if the item is 2048 => game has ended
                if (duplicatedItem.Value == 2048)
                {
                    Complete();
                    yield return new WaitForEndOfFrame();
                }

                //create the duplicated item
                var newGO = Instantiate(GetGOBasedOnValue(duplicatedItem.Value), transform.position + newGoPosition, Quaternion.identity);

                //assign it to the proper position in the array
                matrix[item.NewRow, item.NewColumn].GO = newGO;
                objectsToDestroy.Add(item.GOToAnimatePosition);
                objectsToDestroy.Add(item.ToRemove);

                ///TODO:
                //we need two animations to happen in chain
                //first, the movement animation
                //then, the scale one
            }
        }

        CreateNewItem();
        //hold on till the animations finish
        yield return new WaitForSeconds(Globals.AnimationDuration * movementDetails.Count() * 3);
        foreach (var go in objectsToDestroy) {
            Destroy(go);
        }
    }

    private void UpdateScore(int toAdd)
    {
        score += toAdd;
        ScoreText.text = "Score: " + score;
    }

    private GameObject GetGOBasedOnValue(int value)
    {
        GameObject newGo = value switch
        {
            2 => GO2,
            4 => GO4,
            8 => GO8,
            16 => GO16,
            32 => GO32,
            64 => GO64,
            128 => GO128,
            256 => GO256,
            512 => GO512,
            1024 => GO1024,
            _ => throw new System.Exception("Uknown value:" + value),
        };
        return newGo;
    }
}
