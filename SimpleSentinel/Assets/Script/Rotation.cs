using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    //Sentinel value
    public float rotationSpeed = 20f;
    public Transform patronalObject;
    public Color originalColor;
    public Color findColor;
    public Light findYouLight;
    
    //Camera setup
    public Camera sentinelCamera;
    //Player setup
    public GameObject player;
    public bool findPlayer;
    bool gibsec = true;
    
    //current adding
    
    
    // Start is called before the first frame update
    void Start()
    {
     
        originalColor = findYouLight.color;
        
        findPlayer = false;
    }

    bool isVisible(Camera c, GameObject target){
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;
        foreach(var plane in planes){
            if(plane.GetDistanceToPoint(point) < 0){
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isVisible(sentinelCamera, player)){
            findPlayer = true;
            FindPlayer();
        }
        transform.RotateAround(patronalObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }


    
    void FindPlayer(){
        if(findPlayer && gibsec){
            gibsec = false;
            findYouLight.color = findColor;
            StartCoroutine(iFoundThePlayer());
        }
    }
    IEnumerator iFoundThePlayer(){
        yield return new WaitForSeconds(10);
        findYouLight.color = originalColor;
        findPlayer = false;
        gibsec = true;
    }
}
