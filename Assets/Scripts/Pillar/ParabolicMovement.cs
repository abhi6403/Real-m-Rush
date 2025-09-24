using UnityEngine;
using Random = UnityEngine.Random;

public class ParabolicMovement : MonoBehaviour
{
    [Header("Parabola Settings")]
    public float height = 2f;
    public float duration = 1.5f;

    private Vector3[] points;   // auto-generated targets
    private Vector3 p0, p1, p2;
    private float elapsedTime = 0f;
    private bool isMoving = false;
    private bool startMoving = false;
    private bool hasChosenTarget = false;
    private int nextIndex = -1;
    private Vector3 startPos;

    private Rigidbody rb;
    private Transform player;
    private Vector3 lastPosition;

    // Reference to the player's controller
    private RealmRush.Player.PlayerController playerController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        startPos = transform.position;
        lastPosition = startPos;
        startMoving = false;

        // ✅ Auto-generate 2 target points relative to this pillar
        points = new Vector3[2];
        points[0] = transform.position + new Vector3(-6f, 4f, 0f);
        points[1] = transform.position + new Vector3(6f, 4f, 0f);
    }

    private void Update()
    {
        if (startMoving)
        {
            // Calculate platform movement delta
            Vector3 platformDelta = transform.position - lastPosition;

            MoveObject();

            if (player != null && playerController != null)
            {
                CharacterController controller = player.GetComponent<CharacterController>();
                if (controller != null)
                {
                    Vector3 totalMove = platformDelta + playerController.GetMoveDirection() * Time.deltaTime;
                    controller.Move(totalMove);
                }
                else
                {
                    player.position += platformDelta;
                }
            }
        }
        else
        {
            rb.MovePosition(startPos);
        }

        lastPosition = transform.position;
    }

    void ChooseNextTarget()
    {
        if (points == null || points.Length == 0) return;

        int rand = Random.Range(0, points.Length);
        if (rand == nextIndex) rand = (rand + 1) % points.Length;

        nextIndex = rand;
        StartParabola(points[nextIndex]);
    }

    void StartParabola(Vector3 destination)
    {
        elapsedTime = 0f;
        isMoving = true;

        p0 = transform.position;
        p2 = destination;

        Vector3 mid = (p0 + p2) / 2f;
        p1 = new Vector3(mid.x, mid.y + height, mid.z);
    }

    private void MoveObject()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            Vector3 m1 = Vector3.Lerp(p0, p1, t);
            Vector3 m2 = Vector3.Lerp(p1, p2, t);
            Vector3 targetPos = Vector3.Lerp(m1, m2, t);

            rb.MovePosition(targetPos);

            if (t >= 1f)
            {
                isMoving = false;
                ChooseNextTarget();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startMoving = true;
            player = other.transform;

            var playerView = other.GetComponent<RealmRush.Player.PlayerView>();
            if (playerView != null)
            {
                playerController = playerView.GetPlayerController();
            }

            if (!hasChosenTarget)
            {
                ChooseNextTarget();
                hasChosenTarget = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startMoving = false;
            player = null;
            playerController = null;
        }
    }
}
