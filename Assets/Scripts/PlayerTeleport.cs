using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerTeleport : MonoBehaviour
{
    [Header("Teleport Stats")]
    [SerializeField] private Material teleportMaterialOn;
    [SerializeField] private Material teleportMaterialOff;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float slowMotionFactor = 0.15f;
    [SerializeField] private float delayDuration = 0.3f;
    [SerializeField] private GameObject player;

    private GameObject teleportTarget;
    private CharacterController playerController;

    private bool isTeleporting = false;

    private AudioSource audioSource;
    [Header("Sounds")]
    [SerializeField] private AudioClip teleportActive;
    [SerializeField] private AudioClip teleportAction;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = player.GetComponent<CharacterController>();

        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (teleportTarget != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, teleportTarget.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }

    }

    public void Teleport()
    {
        if (isTeleporting)
        {
            return;
        }

        if (teleportTarget == null)
        {
            lineRenderer.enabled = false;
            return;
        }
        isTeleporting = true;
        StartCoroutine(TeleportWithDelay());
    }

    private IEnumerator TeleportWithDelay()
    {
        Time.timeScale = slowMotionFactor;

        yield return new WaitForSecondsRealtime(delayDuration);

        if (teleportTarget != null)
        {
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            audioSource.clip = teleportAction;
            audioSource.Play();

            Vector3 targetOriginalPosition = teleportTarget.transform.position;
            Vector3 playerOriginalPosition = player.transform.position;


            player.transform.position = targetOriginalPosition;
            teleportTarget.transform.position = playerOriginalPosition;

            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }

        yield return new WaitForSecondsRealtime(delayDuration);

        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(delayDuration);

        isTeleporting = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Teleportable Object")
        {
            audioSource.clip = teleportActive;
            audioSource.Play();
            teleportTarget = collision.gameObject;
            MeshRenderer meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = teleportMaterialOn;
            }

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Teleportable Object")
        {
            teleportTarget = null;
            MeshRenderer meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = teleportMaterialOff;
            }
        }
    }

}
