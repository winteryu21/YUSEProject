using UnityEngine;
using UnityEngine.UI;

public class Button_Togle : MonoBehaviour
{
    [Header("Setting")]
    public UpgradeType targetStat;
    public float amount = 5f;
    public int price = 5;

    [Header("button Image")]
    public Image buttonImage;
    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;
    
    //꺼진지 켜진지 확인
    private bool isApplied = false;


    void Start()
    {
        
        if(buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }

        UpdateColor();
    }


    //버튼 클릭 함수
    public void OnbuttonClick()
    {
        if(!isApplied)
        {
            //돈이 충분해서 쓸수잇으면
            if(UpgradeManager.Instance.SpendGold(price)==true)
            {
                UpgradeManager.Instance.ApplyUpgrade(targetStat, amount);
                isApplied = true;
            }
            else
            {
                Debug.Log("돈이없음");
                return;
            }
            
        }

        else
        {
            UpgradeManager.Instance.refund(price);
            UpgradeManager.Instance.ApplyUpgrade(targetStat, -amount);
            isApplied = false;

        }
        UpdateColor();
    }

    // 색상 변환 함수
    private void UpdateColor()
    {
        if(buttonImage != null)
        {
            buttonImage.color=isApplied? inactiveColor : activeColor;
        }
    }
}
