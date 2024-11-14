using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RadialSkillCooldown: IPlayerSkillCooldown
{
    private Image grayMask = null;
    private Text timeText = null;

    public RadialSkillCooldown(Image grayMask, Text timeText)
    {
        this.grayMask = grayMask;
        this.timeText = timeText;
    }

    private async Task OnUpdate(float second)
    {
        float cnt = second;
        while(cnt >= 0)
        {
            timeText.text = string.Format("{0:0.0}", cnt);
            grayMask.fillAmount = cnt / second;
            cnt -= Time.deltaTime;
            await Task.Yield();
        }
    }

    public void BeginCoolDown(float second)
    {
        grayMask.gameObject.SetActive(true);
        _ = OnUpdate(second);
    }

    public void AfterCoolDown()
    {
        grayMask.gameObject.SetActive(false);
    }
}