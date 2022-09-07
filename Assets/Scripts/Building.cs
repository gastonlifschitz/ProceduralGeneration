using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    List<GameObject> building = new List<GameObject>();
    int interval = 2; 
    float nextTime = 0;
    private int numberCube = 0;
    private Queue<Vector3> yPositions = new Queue<Vector3>();

    private float currentX = -1.0f;
    private float currentY = 0.75f;
    private float currentZ = -1.0f;
    private int delta = 1;
    private int cubesPerFloor = 0;
    private int currentHeight = 0;

    private Vector3 DEFAULT_SCALE = new Vector3(1, 1, 1);
    private int MAX_CUBES_PER_FLOOR = 9;
    private int MAX_HEIGHT = 10;
    private int BUILDING_WIDTH = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        generatePositions();
        GameObject firstBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
        firstBlock.transform.parent = transform;
        firstBlock.transform.localPosition = yPositions.Dequeue();
        firstBlock.transform.localScale = DEFAULT_SCALE;
        building.Add(firstBlock);
        cubesPerFloor++;
    }

    void generatePositions() {
        yPositions = new Queue<Vector3>();

        for(int i = 0; i < BUILDING_WIDTH; i++){
            for(int j = 0; j < BUILDING_WIDTH; j++){
                yPositions.Enqueue(new Vector3(currentX + (i * delta),currentY ,currentZ + (j * delta)));
            }
        }
    }
     
     // Update is called once per frame
     void Update () {
        if (Time.time >= nextTime) {
            generateCube();
            numberCube++;
 
            nextTime += interval; 
        }
    }

    void resetCoordinates() {
        currentX = -1.0f;
        currentZ = -1.0f;
        currentY += 1.0f; 
        cubesPerFloor = 0;
        currentHeight++;
    }


    void generateCube()
    {
        if(cubesPerFloor == MAX_CUBES_PER_FLOOR) { 
            resetCoordinates();
            generatePositions();
        }

        if(currentHeight >= MAX_HEIGHT) return;

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = yPositions.Dequeue();
        cube.transform.localScale = DEFAULT_SCALE;
        cube.transform.parent = transform;

        building.Add(cube);
        cubesPerFloor++;
  
        Debug.Log("Cube generated");
    }
}
