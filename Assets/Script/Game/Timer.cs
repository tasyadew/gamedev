using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] string timesUpText;
    public float totalTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = totalTime.ToString();   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTimer()
    {
        StartCoroutine(IEStartTimer());
    }

    IEnumerator IEStartTimer()
    {
        while(totalTime > 0)
        {
            yield return new WaitForSeconds(1f);
            totalTime--;
            timer.text = totalTime.ToString(); 
        }

        timer.color = Color.red;
        timer.text = timesUpText;
    }
}
