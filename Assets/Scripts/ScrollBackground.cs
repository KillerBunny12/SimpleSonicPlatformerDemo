using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float backgroundSize;
    public float yy = 152;
    public Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    // Start is called before the first frame update
    void Start()
    {
       // cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
            layers[i] = transform.GetChild(i);
        
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {



         if(cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
         {
             scrollLeft();
         }

         if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
         {
             scrollRight();
        }


        
    }

    private void scrollLeft()
    {
        float y = layers[rightIndex].position.z;
        float z = layers[rightIndex].position.z;
        int lastRight = rightIndex;
        layers[rightIndex].position = new Vector3((layers[leftIndex].position.x - backgroundSize), yy, z);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0)
            rightIndex = layers.Length - 1;
        
    }

    private void scrollRight()
    {
        float y = layers[leftIndex].position.y;
        float z = layers[leftIndex].position.z;
        int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3((layers[rightIndex].position.x + backgroundSize), y, z);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
