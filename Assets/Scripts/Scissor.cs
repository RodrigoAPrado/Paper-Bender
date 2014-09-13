using UnityEngine;
using System.Collections;

public class Scissor : MonoBehaviour 
{
    #region FollowWaypoint Vars
    public Transform[] waypoints;
    private int currentWaypoint;
    public float speed = 5;
    public float rotSpeed = 3;
    #endregion
    public bool finish = false;
    public float selfDestructionCount = 1;
    public Color color;


	// Use this for initialization
	void Start () 
    {
        color = GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!finish)
            FollowWaypoints();
        else
            SelfDestruction();
	}

    public void FollowWaypoints ()
    {
        Vector3 Dir = waypoints[currentWaypoint].position - transform.position;
        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpeed);

        if (Dir.magnitude > 1)
        {
            rigidbody2D.velocity = Dir.normalized * speed;
        }
        else
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
                finish = true;
        }
    }

    public void SelfDestruction()
    {
        color.a = selfDestructionCount;
        this.GetComponent<SpriteRenderer>().color = color;
        if (selfDestructionCount > 0)
        {
            selfDestructionCount -= Time.deltaTime;
        }
        else
            GameObject.Destroy(gameObject);
    }
}
