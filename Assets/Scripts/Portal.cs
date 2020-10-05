using System;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public static event Action OnPlayerEntered = delegate { };

    [SerializeField] Transform target;
    [SerializeField] Sprite innerInactive;
    [SerializeField] Sprite outerInactive;
    [SerializeField] Sprite innerActive;
    [SerializeField] Sprite outerActive;
    [SerializeField] SpriteRenderer innerRenderer;
    [SerializeField] SpriteRenderer outerRenderer;

    Animator animator;

    bool levelComplete;

    private void Start()
    {
        levelComplete = false;
        LevelManager.OnScoreReached += SetLevelComplete;
        LevelManager.OnLevelLoaded += SetLevelIncomplete;

        animator = GetComponent<Animator>();

        innerRenderer.sprite = innerInactive;
        outerRenderer.sprite = outerInactive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null && !levelComplete)
        {
            collision.transform.position = target.position;
        }
        else if (collision.GetComponent<PlayerController>() != null && levelComplete)
        {
            collision.transform.position = target.position;
            OnPlayerEntered();
        }
    }

    void SetLevelComplete()
    {
        levelComplete = true;
        innerRenderer.sprite = innerActive;
        outerRenderer.sprite = outerActive;
    }

    void SetLevelIncomplete()
    {
        levelComplete = false;
        innerRenderer.sprite = innerInactive;
        outerRenderer.sprite = outerInactive;
    }

}
