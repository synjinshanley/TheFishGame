using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FishAI : MonoBehaviour
{
    enum State { WANDERING, STOPPED, INTERESTED, HOOKED }

    [SerializeField] private GameObject[] locations;
    [SerializeField] private State state = State.STOPPED;

    private NavMeshAgent nav;
    private GameObject fishing_bobber;

    private int currentLocation = 0;
    private bool isWaiting = false; // ✅ flag to prevent coroutine spam

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        locations = new GameObject[]
        {
            GameObject.Find("Target1"),
            GameObject.Find("Target2"),
            GameObject.Find("Target3"),
            GameObject.Find("Target4"),
            GameObject.Find("Target5")
        };

        currentLocation = Random.Range(0, locations.Length);
        nav.SetDestination(locations[currentLocation].transform.position);
        SetState(State.WANDERING);
    }

    void Update()
    {
        if (fishing_bobber == null)
        {
            fishing_bobber = GameObject.Find("fishing_bobber");
        }

        float distance = fishing_bobber != null
            ? Vector3.Distance(transform.position, fishing_bobber.transform.position)
            : float.MaxValue;

        if (distance < 2f)
        {
            StartHooked();
        }
        else if (distance < 4f)
        {
            StartInterest();
        }
        else if (CloseToTarget(locations[currentLocation]))
        {
            StartStopped();
        }
        else if (state == State.WANDERING)
        {
            // Already heading somewhere, do nothing
        }
    }

    void StartStopped()
    {
        if (state == State.STOPPED || isWaiting) return; // ✅ don't re-trigger if already waiting
        SetState(State.STOPPED);
        nav.ResetPath();
        StartCoroutine(WaitThenWander());
    }

    IEnumerator WaitThenWander()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);

        // Pick a different target than the current one
        int newLocation;
        do {
            newLocation = Random.Range(0, locations.Length);
        } while (newLocation == currentLocation); // ✅ ensures it always moves to a new target

        currentLocation = newLocation;
        isWaiting = false;
        StartWandering();
    }

    void StartWandering()
    {
        SetState(State.WANDERING);
        nav.speed = 1f;
        nav.SetDestination(locations[currentLocation].transform.position);
    }

    void StartInterest()
    {
        if (state == State.INTERESTED) return;
        SetState(State.INTERESTED);
        nav.speed = 0.5f;
        nav.SetDestination(fishing_bobber.transform.position);
    }

    void StartHooked()
    {
        if (state == State.HOOKED) return;
        SetState(State.HOOKED);
        nav.speed = 0f;
        gameManager.instance.TriggerQTE(gameObject);
        nav.ResetPath();
    }

    void SetState(State newState)
    {
        state = newState;
    }

    bool CloseToTarget(GameObject target)
    {
        //DEBUG.Log($"Distance to {target.name}: {Vector3.Distance(transform.position, target.transform.position)}");
        return Vector3.Distance(transform.position, target.transform.position) < 1f;
    }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position + Vector3.up * 1.5f, state.ToString());
#endif
    }
}