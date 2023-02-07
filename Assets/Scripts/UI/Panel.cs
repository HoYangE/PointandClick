using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject panel;
    public GameObject button;

    public void OpenPanel()
    {
        if (panel == null) return;
        var animator = panel.GetComponent<Animator>();
        if (animator == null) return;
        var id = Animator.StringToHash("open");
        var isOpen = animator.GetBool(id);
        animator.SetBool(id, !isOpen);
    }

    public void OpenButton()
    {
        if (button == null) return;
        var animator = button.GetComponent<Animator>();
        if (animator == null) return;
        var id = Animator.StringToHash("open");
        var isOpen = animator.GetBool(id);
        animator.SetBool(id, !isOpen);
    }
}
