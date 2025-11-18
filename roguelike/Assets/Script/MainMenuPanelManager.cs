using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MainMenuPanelManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject LobbyPanel;
    public GameObject UpgradePanel;

    public Text press_Anykey;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (press_Anykey == null) return;
        float newAlpha = AlphaChange();
        BlinkText(newAlpha);

        ShowLobbyPanel();
    }

    //아무키나 누르면 로비로 넘어가기
    void ShowLobbyPanel()
    {
        if(Input.anyKeyDown && MainPanel.activeSelf && !LobbyPanel.activeSelf)
        {
            MainPanel.SetActive(false);
            LobbyPanel.SetActive(true);
        }
    }





    //text blink 함수
    private float AlphaChange()
    {
        float normalizedAlpha = Mathf.PingPong(Time.time * 0.5f, 1f);  
        return normalizedAlpha;
    }

    //알파값 바꿔주는 함수
    void BlinkText(float alpha)
    {
        Color newColor = press_Anykey.color;
        newColor.a = alpha;
        press_Anykey.color = newColor;
    }


    public void ShowUpgradePanel()
    {
        if(!UpgradePanel.activeSelf)
        {
            UpgradePanel.SetActive(true);
        }
    }

    public void CloseUpgradePanel()
    {
        if(UpgradePanel.activeSelf)
        {
            UpgradePanel.SetActive(false);
        }
    }

}
