using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Frogy : MonoBehaviour
{


    [SerializeField, Range(0, 1)] float moveDuration = 0.1f;
    [SerializeField, Range(0, 1)] float JumpHeight = 0.5f;
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip BGMClip;
    [SerializeField] AudioClip JumpClip;
    [SerializeField] AudioClip GetCoinClip;
    [SerializeField] AudioClip CrashClip;
    [SerializeField] AudioClip EagleClip;


    public UnityEvent<Vector3> OnJumpEnd;
    public UnityEvent<int> OnGetCoin;
    public UnityEvent OnCarCollision;
    public UnityEvent OnDie;

    private bool isMoveable = false;

    private void Start()
    {
        audioManager.PlayBGM(BGMClip);
    }
    void Update()
    {
        
        if (isMoveable == false)
            return;

        if (DOTween.IsTweening(transform))
            return;

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // transform.DOMoveZ(transform.position.z + 1, 0,5f );
            direction += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // transform.DOMoveZ(transform.position.z - 1, 0,5f );
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // transform.DOMoveZ(transform.position.x + 1, 0,5f );
            direction += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // transform.DOMoveZ(transform.position.x - 1, 0,5f );
            direction += Vector3.left;
        }


        if (direction == Vector3.zero)
            return;

        Move(direction);
        
    }

    public void Move(Vector3 direction)
    {
        // transform.DoMoveZ(transform.position.z + direction.x, 0.5f);
        // transform.DoMoveX(transform.position.x + direction.z, 0.5f);

        //  checka apakah target posisi valid
        var targetPosition = transform.position + direction;
        if (targetPosition.x < leftMoveLimit ||
            targetPosition.x > rightMoveLimit ||
            targetPosition.z < backMoveLimit ||
            Tree.AllPositions.Contains(targetPosition))
            {
            targetPosition = transform.position;
            }
        transform.DOJump(
                        targetPosition, 
                        JumpHeight,
                        1,
                        moveDuration)
                        .onComplete = BroadcastPositionOnJumpEnd;
        transform.forward = direction;

        audioManager.PlaySFX(JumpClip);
        // var seq = DOTween.Sequence();
        // seq.append(transform.DOMoveY(JumpHeight,moveDuration * 0.5f));
        // seq.append(transform.DOMoveY(0,moveDuration * 0.5f));
    }
    public void SetMoveable(bool value)
    {
        isMoveable = value;
    }
    
    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMoveLimit = -horizontalSize /2;
        rightMoveLimit = horizontalSize /2;
        backMoveLimit = backLimit;
    }
    private void BroadcastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        { if (transform.localScale.y == 0.1f)
                return;
            transform.DOScale(new Vector3(2, 0.1f, 2), 0.2f);
            audioManager.PlaySFXCrash(CrashClip);

            isMoveable = false;
            OnCarCollision.Invoke();
            Invoke("Die", 3);

        } 
        else if (other.CompareTag("Coin"))
        {
            var coin = other.GetComponent<Coin>();
            OnGetCoin.Invoke(coin.Value);
            coin.Collected();
            audioManager.PlaySFXcoin(GetCoinClip);
        }
        else if (other.CompareTag("Eagle"))
        {
            if (this.transform != other.transform)
            {
                
                this.transform.SetParent(other.transform);
                Invoke("Die", 3);
                audioManager.PlaySFX(EagleClip);
            }
        }
    }
    private void Die()
    {
        OnDie.Invoke();
    }
}
