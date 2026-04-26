using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class FishAI : MonoBehaviour
{
    enum State { WANDERING, STOPPED, INTERESTED, HOOKED }

    [SerializeField] private GameObject[] locations;

    private NavMeshAgent nav;
    private GameObject fishing_bobber;
    private int currentLocation = 0;
    private State state = State.WANDERING;


    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        fishing_bobber = GameObject.Find("fishing_bobber");
        locations = new GameObject[]
        {
            GameObject.Find("Target1"),
            GameObject.Find("Target2"),
            GameObject.Find("Target3"),
            GameObject.Find("Target4"),
            GameObject.Find("Target5")
        };
        nav.SetDestination(locations[Random.Range(0,5)].transform.position);
    }


    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, fishing_bobber.transform.position);


        if (distance < 1f)
        {
            StartHooked();
        }
        else if (distance < 2f)
        {
            StartInterest();
        }
        else if (CloseToTarget(locations[currentLocation]))
        {
            StartStopped();
        }
        {
            StartWandering();
        }
    }



    void StartStopped()
    {
        SetState(State.STOPPED);
    }


    IEnumerator StartWandering()
    {
        if (state == State.STOPPED)
        {
            yield return new WaitForSeconds(2);
            currentLocation = Random.Range(0, 5);
        }
        SetState(State.WANDERING);
        nav.speed = 1f;
        nav.SetDestination(locations[currentLocation].transform.position);
    }


    void StartInterest()
    {
        SetState(State.INTERESTED);
        nav.speed = 0.5f;
        nav.SetDestination(fishing_bobber.transform.position);
    }

    void StartHooked()
    {
        SetState(State.HOOKED);
    }


    void SetState(State newState)
    {
        state = newState;
    }

    bool CloseToTarget(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
        {
            return true;
        }
        return false;
    }
}
