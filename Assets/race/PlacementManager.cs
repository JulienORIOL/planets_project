using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacementManager : MonoBehaviour
{

    private List<Vector3> startingPositions = new List<Vector3>();
    private double left = 0;
    private double right = 5.4;
    private double front = 0;
    private double lng = -5;

    void Awake()
    {
        GetStartingPositions();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetStartingPositions()
    {
        // get all the starting positions by gridX position
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            int gridX = (int) child.position.x;
            int gridZ = (int) child.position.z;

            Vector3 gridPosition = new Vector3(gridX, 20, gridZ);

            if (startingPositions.Count <= transform.childCount)
            {
                startingPositions.Add(gridPosition);
            }
            else
            {
                startingPositions[i] = gridPosition;
            }
        }    
    }

    public Vector3 GetStartingPosition(int position)
    {
        if (startingPositions.Count > position)
        {
            return startingPositions[position];
        }
        return Vector3.zero;
    }

    public Vector3 GetInvisibleCarPosition()
    {
        // retrieve last checkpoint position
        LapManager arrival = GameObject.Find("Arrival").GetComponent<LapManager>();
        Checkpoint checkpoint = arrival.getLastCheckpoint();
        return new Vector3(checkpoint.transform.position.x, 20, checkpoint.transform.position.z);
    }
}