using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerEventHandles : MonoBehaviour
{
    [SerializeField] private Unidad unidad;
    [SerializeField] private AudioClip attackSound;

    public void PlayAttackSound() => unidad.audioHandle.OnPlay(attackSound);
}
