using UnityEngine;
using UnityEngine.UI;
public class TimerCircle : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private ActionObject ActionObjectsCallBack;
    private float time;
    public float MaxTime;
    private bool isStop;

    private void OnEnable()
    {
        time = MaxTime;
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
            image.fillAmount = time / MaxTime;
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                ActionObjectsCallBack.DoAfterTimer();
                time = MaxTime;
            }
        }
    }


}
