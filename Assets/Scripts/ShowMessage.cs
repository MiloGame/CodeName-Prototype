using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    TextMeshProUGUI messageBar;
    private void Start()
    {
        messageBar = this.GetComponent<TextMeshProUGUI>();
    }
    public void showMessage(string m)
    {
        messageBar.text = m;
        messageBar.gameObject.SetActive(true);
        StartCoroutine(HideMessageAfterDelay());
    }
    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        messageBar.gameObject.SetActive(false);
    }
}
