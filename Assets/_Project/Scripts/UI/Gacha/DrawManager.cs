using System.Collections;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Animator blindAnimator;

    public void OneDrawButton() => StartCoroutine(DrawCharacters(1));

    public void TenDrawButton() => StartCoroutine(DrawCharacters(10));

    public void CheckButton() => blindAnimator.Play("Idle");

    private IEnumerator DrawCharacters(int count)
    {
        blindAnimator.Play("Blind");

        yield return new WaitUntil(() =>
        {
            AnimatorStateInfo stateInfo = blindAnimator.GetCurrentAnimatorStateInfo(0);

            return stateInfo.IsName("Blind") && stateInfo.normalizedTime >= 1;
        });

        uint unitId;

        for (int i = 0; i < count; i++)
        {
            unitId = GetRandomUnitID(0, 7);

            AddUnit(unitId);
        }

        PlayerManager.Instance.SaveData();

        CheckButton();
    }

    private void AddUnit(uint id)
    {
        if (PlayerManager.Instance.units.ContainsKey(id)) return;

        PlayerManager.Instance.units.Add(id, new PlayerUnitInfo(id, 0));
    }

    private uint GetRandomUnitID(uint min, uint max) => (uint)RandomModule.GetSecurityRandom(min, max);
}
