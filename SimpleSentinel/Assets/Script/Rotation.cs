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
    public float minimumDistance = 20f;
    
    //Camera setup
    public Camera sentinelCamera;
    //Player setup
    public GameObject player;
    public bool findPlayer;
    bool gibsec = true;
    public float radius;
    [Range (0, 360)]
    public float angle;
    public LayerMask obstacles;
    public LayerMask playerMask;
    
    //current adding
    Vector3 playerPos;
    
    // Start is called before the first frame update
    void Start()
    {
     
        originalColor = findYouLight.color;
        
        findPlayer = false;
        StartCoroutine(alternativeUpdate());
    }

    bool isVisible(Camera c, GameObject target){
        if(Vector3.Distance(target.transform.position, this.transform.position) < minimumDistance){
            return true;
        }
        Collider[] collidersRange = Physics.OverlapSphere(transform.position, radius, playerMask);
        if(collidersRange.Length != 0){
            Transform targett = collidersRange[0].transform;
            Vector3 directionTarget = (targett.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionTarget) < angle/2){
                float distance = Vector3.Distance(transform.position, targett.position);
                if(!Physics.Raycast(transform.position, directionTarget, distance, obstacles)){
                    return true;
                }else return false;
            }else return false;
        
        }else return false;
        /*var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;
        foreach(var plane in planes){
            
            if(plane.GetDistanceToPoint(point) < 0){
                return false;
            }
        }
        return true;*/
    }

    IEnumerator alternativeUpdate(){
       
        while(true){
            yield return new WaitForSeconds(1f);
            bool check = isVisible(sentinelCamera, player);
            if(check){
                
                playerPos = player.transform.position;
                transform.LookAt(playerPos);
                findPlayer = true;
                FindPlayer();
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if(!findPlayer){
            normal();
        }else{
            chasePlayer();
        }
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

    void chasePlayer(){
        
        transform.position = Vector3.MoveTowards(transform.position, playerPos, 5 * Time.deltaTime);
    }
    void normal(){
        if(Vector3.Distance(transform.position, patronalObject.position) > 10){
            transform.position = Vector3.MoveTowards(transform.position, patronalObject.position, rotationSpeed * Time.deltaTime);
        }
        transform.RotateAround(patronalObject.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
