using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Objects")]
    public GameObject puzzle1Object; // e.g., key, lever, or item
    public GameObject puzzle2Object;
    public GameObject puzzle3Object;

    [Header("Doors/Unlockables")]
    public GameObject door1; // door to unlock after puzzle 1
    public GameObject door2; // door to unlock after puzzle 2
    public GameObject exitDoor; // final door after puzzle 3

    [Header("Audio Feedback")]
    public AudioClip puzzleSolvedSound;
    private AudioSource audioSource;

    private int puzzleStage = 0; // track progression

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdatePuzzleObjects();
    }

    // Call this when a puzzle item is solved or activated
    public void PuzzleSolved(GameObject solvedObject)
    {
        // Check which puzzle was solved
        if (solvedObject == puzzle1Object && puzzleStage == 0)
        {
            Debug.Log("Puzzle 1 solved!");
            UnlockDoor(door1);
            PlaySound();
            puzzleStage = 1;
            UpdatePuzzleObjects();
        }
        else if (solvedObject == puzzle2Object && puzzleStage == 1)
        {
            Debug.Log("Puzzle 2 solved!");
            UnlockDoor(door2);
            PlaySound();
            puzzleStage = 2;
            UpdatePuzzleObjects();
        }
        else if (solvedObject == puzzle3Object && puzzleStage == 2)
        {
            Debug.Log("Puzzle 3 solved! Final door unlocked!");
            UnlockDoor(exitDoor);
            PlaySound();
            puzzleStage = 3;
        }
        else
        {
            Debug.Log("Cannot solve this puzzle yet.");
        }
    }

    void UnlockDoor(GameObject door)
    {
        if (door != null)
        {
            // Simple unlock: deactivate door collider to allow passing
            Collider col = door.GetComponent<Collider>();
            if (col != null) col.enabled = false;

            // Optional: animate the door opening
            Animator anim = door.GetComponent<Animator>();
            if (anim != null) anim.SetTrigger("Open");

            // Optional: move the door upward if no animation
            else door.transform.position += new Vector3(0, 2f, 0);
        }
    }

    void PlaySound()
    {
        if (puzzleSolvedSound != null)
            audioSource.PlayOneShot(puzzleSolvedSound);
    }

    void UpdatePuzzleObjects()
    {
        // Only enable the next puzzle object in sequence
        puzzle1Object?.SetActive(puzzleStage == 0);
        puzzle2Object?.SetActive(puzzleStage == 1);
        puzzle3Object?.SetActive(puzzleStage == 2);
    }
}