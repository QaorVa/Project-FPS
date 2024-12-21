using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Prompt")]
    [SerializeField] private TextMeshProUGUI promptText;

    [Header("Hitmarker")]
    [SerializeField] private Image hitmarkerImage;
    [SerializeField] private AudioSource hitmarkerAudio;
    public float fadeDuration = 1f;

    [Header("Ammo Count")]
    [SerializeField] private TextMeshProUGUI ammoCountText;

    private float hitTimer;
    private bool isHit;
    void Start()
    {

    }

    private void Update()
    {
        if (isHit)
        {
            hitTimer += Time.deltaTime;

            if (hitTimer >= fadeDuration)
            {
                StartCoroutine(FadeOutHitmarker());
            }
        }

    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void OnEnemyHit()
    {
        hitmarkerImage.color = new Color(hitmarkerImage.color.r, hitmarkerImage.color.g, hitmarkerImage.color.b, 1f);
        hitmarkerAudio.Play();
        hitTimer = 0f;
        isHit = true;
    }

    private IEnumerator FadeOutHitmarker()
    {
        float startAlpha = 1;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            hitmarkerImage.color = new Color(hitmarkerImage.color.r, hitmarkerImage.color.g, hitmarkerImage.color.b, alpha);
            yield return null;
        }

        hitmarkerImage.color = new Color(hitmarkerImage.color.r, hitmarkerImage.color.g, hitmarkerImage.color.b, 0f);

        isHit = false;
    }


    public void UpdateAmmoCount(int currentAmmo, int maxAmmo)
    {
        ammoCountText.text = currentAmmo + "/" + maxAmmo;
    }
}
