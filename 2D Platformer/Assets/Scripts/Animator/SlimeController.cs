using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public Animator animator;
    public bool isRun = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ExcuteRun()
    {
        isRun = !isRun;
        animator.SetBool("Is Run", isRun);
    }
    public void TakeDamage()
    {
        animator.SetTrigger("Take Damage");
    }
}
