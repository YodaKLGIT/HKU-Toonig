using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;

    public void Attack()
    {
        animator.SetTrigger("Attack");
        Debug.Log(name + " attacks!");
    }
}
