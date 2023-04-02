using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("pop");
        Time.timeScale = 0;
    }

    public void PopDown()
    {
        animator.SetTrigger("close");
        popUpBox.SetActive(false);
        Time.timeScale = 1;
    }
}
