using UnityEngine;

public class Reposition : MonoBehaviour
{
   
    private Collider2D coll;

    #region unity life cycle
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    #endregion

    //collider에서 벗어날시 발생하는 로직
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos= transform.position;

        switch (transform.tag)
        {
            case "Ground":
                //플레이어랑 맵의 거리구하기
                float distance_x = playerPos.x - myPos.x;
                float distance_y = playerPos.y - myPos.y;
                //방향 구하기
                float direction_X = distance_x < 0 ? -1:1; 
                float direction_Y = distance_y < 0 ? -1:1;

                //절대값으로 바꾸기
                distance_x = Mathf.Abs(distance_x);
                distance_y = Mathf.Abs(distance_y);

                //x y거리에따라 스폰위치 조정
                if(distance_x >distance_y)
                {
                    transform.Translate(Vector3.right * direction_X * 40);
                }
                else if(distance_y >distance_x)
                {
                    transform.Translate(Vector3.up * direction_Y * 40);
                }

                break;


            case "Enemy":

                if(coll.enabled) //죽은놈은 실행 x
                {
                    Vector3 distance_Monster = playerPos - myPos;

                    // 하나로 겹치는거 막기위한 로직 
                    Vector3 Ran_pos= new Vector3(UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-3, 3), 0);

                  
                    transform.Translate(Ran_pos + distance_Monster * 2);
                }

                break;
        }

        
    }
}
