using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public static int enemiesKilledCount = 0;
    [SerializeField] public static int seconds = 0;
    [SerializeField] public static int minutes = 0;
    private float timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            seconds++;
            timer = 0f;

            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }
        }
    }
}
