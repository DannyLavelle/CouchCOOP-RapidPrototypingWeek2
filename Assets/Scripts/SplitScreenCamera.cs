using UnityEngine;

public class SplitScreenCamera : MonoBehaviour
{
    public Transform[] players; // References to player GameObjects or their transforms
    public float minOrthographicSize = 5f;
    public float maxOrthographicSize = 10f;
    public float smoothness = 5f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (players.Length == 0)
            return;

        // Calculate the bounding box that encompasses all player positions
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform player in players)
        {
            bounds.Encapsulate(player.position);
        }

        // Calculate the desired orthographic size based on the bounding box's size
        float targetOrthoSize = Mathf.Clamp(bounds.size.x / 2f, minOrthographicSize, maxOrthographicSize);

        // Smoothly adjust the camera's position and orthographic size
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, Time.deltaTime * smoothness);
        cam.transform.position = Vector3.Lerp(cam.transform.position, bounds.center - new Vector3(0f, 0f, 10f), Time.deltaTime * smoothness);
    }
}
