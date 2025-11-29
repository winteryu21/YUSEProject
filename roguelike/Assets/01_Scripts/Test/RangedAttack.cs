using UnityEngine;

public class RangedAttack : Equipment
{
    [Header("Weapon Stats")]
    public string weaponName = "부적";
    public float fireRate = 1.0f;        // 초당 발사 횟수
    public float BulletSpeed = 20f;  // 투사체 속도 
    public Transform firePos;
    public bool isbullet;

    private Rigidbody2D rigid;
    private float damage;
    private float speed;
    private float nextFireTIme = 0f;



    #region unity life cycle
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        if(isbullet)
        {
            return;
        }

        base.Update();
        if(_currentCooldown<=0)
        {
            PerformAttack();
            _currentCooldown = baseCooldown;
        }
    }


    #endregion


    #region 충돌 관련
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isbullet)
            return;

        if (!collision.CompareTag("Enemy"))
            return;


        Destroy(gameObject); //임시
        //gameObject.SetActive(false); 풀로 재활용시
    }


    private void OnTriggerExit2D(Collider2D collision)
    {



        if (!collision.CompareTag("Area"))
            return;

         Destroy(gameObject); //임시
        //gameObject.SetActive(false); 풀로 재활용시
    }


    #endregion

    protected override void PerformAttack()
    {
       


        //플레이어 스프라이트 방향 // 나빼고 찾아라
        SpriteRenderer player_sprite = transform.parent.GetComponentInParent<SpriteRenderer>();

        Quaternion shootRotation = Quaternion.Euler(0, 0, -90);

        //스프라이트 찾아서 잇으면
        if(player_sprite !=null)
        {
            //오른쪽 일때 똑같이 반전시켜 발사
            if (player_sprite.flipX)
            {
   
                shootRotation = Quaternion.Euler(0, 0, -90);
            }
            //왼쪽일때
            else
            {
                shootRotation = Quaternion.Euler(0, 0, 90);
            }
        }

        
        //프리펩 생성하고 스크립트 가져오기
        GameObject copy = Instantiate(gameObject, transform.position, shootRotation);
        RangedAttack copyScript = copy.GetComponent<RangedAttack>();

        // 발사되는건 총알로 변경
        copyScript.isbullet = true;

        //발사되는건 투명도를 올려서 보이게 일단 무겁지만 구현에 집중
        //나가는건 색깔 투명도를 복구해서 보이게하기
        SpriteRenderer sprite = copy.GetComponent<SpriteRenderer>(); 
        if(sprite != null)
        {
            Color color = sprite.color;
            color.a = 1f;
            sprite.color = color;
        }
        //발사
        copyScript.shoot();
        
    }

    public void shoot()
    {
        rigid.linearVelocity = transform.up * BulletSpeed;
    }



}
