using Unity.VisualScripting;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    private int i = 0;
    [SerializeField] private float speed = 2f;

    void Update()
    {
        Debug.Log(i);

        // Kolla om vi nått waypointen
        if (Vector2.Distance(transform.position, waypoints[i].transform.position) < 0.1f)
        {
            i++;

            if (i >= waypoints.Length)
                i = 0;
        }

        // Flytta plattformen
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[i].transform.position,
            speed * Time.deltaTime
        );
    }

    // --- Player attach/detach funktionalitet ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
