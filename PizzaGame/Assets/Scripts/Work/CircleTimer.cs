using UnityEngine;
using UnityEngine.UI;
public class TimerCircle : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private ActionObject ActionObjectsCallBack;
    public float Time;
    public float MaxTime;
    private bool isStop;

    private void OnEnable()
    {
        Time = MaxTime;
        image.fillAmount = 1;
    }

    public void StopTimer()
    {
        isStop = true;
    }

    public void ContinueTimer()
    {
        isStop = false;
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);

        if (!isStop)
        {
            image.fillAmount = Time / MaxTime;
            if (Time > 0)
            {
                Time -= UnityEngine.Time.deltaTime;
            }
            else
            {
                ActionObjectsCallBack.DoAfterTimer();
                Time = MaxTime;
            }
        }
    }
}
