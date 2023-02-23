using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for Animation Events.
public class PlayerAniEvents : MonoBehaviour
{
    [SerializeField] TopDownMovementComponent topDownMovementComponent;

    void FreezePlayer() {
        topDownMovementComponent.DisableMovement();
    }

    void UnFreezePlayer() {
        topDownMovementComponent.EnableMovement();
    }

    void SetPlayerMovement(float ms) {
        topDownMovementComponent.movementSpeed = ms;
    }

    void ForcedPlayerMove(float dirX) {
        Debug.Log("forced move");
        topDownMovementComponent.moveScripted(dirX, 0, 12);
    } 
}
