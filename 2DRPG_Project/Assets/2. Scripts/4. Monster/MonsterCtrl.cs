using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using TMPro;

namespace Main
{
    //���׻��� �ӽ� ����
    enum StateMachine
    { 
        Patron, //���� ( idle, walk �ִϸ��̼��� ����, �����ɽ�Ʈ, �������� �Ǵ� )
        Fallow, //�߰� ( �÷��̾�������θ� �̵�, �������� ���̶�� ���� )
        Attack, //���� ( �����ֱ�� ���ݸ� )
        Damaged,//�ǰ� ( sendMassage�޾Ƽ� ü�� üũ�� �ǰ�����, �������·� )
        Death,  //���� ( ü�� üũ�� �ִϸ��̼� ��� ������ �̺�Ʈ ȣ��, ���� ���� )
    };

    //�� ��Ʈ��
    public class MonsterCtrl : MonoBehaviour
    {
        //������Ʈ
        public Rigidbody2D rig;
        public SpriteRenderer sr;//�ڽ��� �̹��� (�̹����� ������ȯ)
        public Slider hpBar;
        public Animator anim;
        public CapsuleCollider2D collider2d;
        public MonsterHitBoxColl monsterColl; //��Ʈ�ڽ� üũ��
        public Transform hitbox_Direction;// ������ȯ�� ��Ʈ�ڽ� ��ġ ������

        public Animator emotion;

        //üũ��
        public bool isChanged; //������ üũ
        public int turnDir; //-1 0 1 ���� ���� ������
        public bool isDead; //������ ��� ������ ����

        //���� ������
        public float speed, maxSpeed; //�ӵ� , �ִ�ӵ�
        public Vector3 ground_Position; //��ġ ����

        //�ӽ�
        public float hp;
        public Text hp_txt;
        public TextMeshProUGUI name_txt;//csv���� �ҷ��� ������ (ü��,����, ���ݷ�, �̸�)

        //ü�� ��
        public Player_Attack_Defend player_Attack_Defend;

        //�Ÿ���� 
        float distance; //�÷��̾���� �Ÿ����
        public float destination; //�÷��̾�� ����
        public Transform player;

        //����
        float tempSpeed, maxTempSpeed;
        public float attackTimer;//�����ֱ�
        public float temp_TimeCheck; //�ӽ� �ð��˻��

        public float attack_Distance; //���ݰ��ɰŸ� 0.8f
        public float fallow_Distance; //�߰ݰ��ɰŸ� 1.3f
        public float patrol_Distance; //�߰ݰ��ɰŸ� 6f

        public Vector3 hitbox_SizeCtrl; //��Ʈ�ڽ��� ���� ����

        //�ǰݽ� 
        public UsePopUp damagedPopUp;

        //�ʱ�ȭ
        StateMachine state = StateMachine.Patron;

        //����� �̿��� �þ߰� ���� //�߰ݹ���, ���� ��������
        public Vector3 gizmo_Position;

        [SerializeField] bool debugMode = false;

        [Header("View Config ����")]
        [Range(0f, 360f)]
        [SerializeField] float horizontalView_Angle        = 0f;
        [SerializeField] float viewRadius                  = 1f;

        [Range(-180f, 180f)]//�����̼� ���� Ž�� ������ ���� ����� ��ġ���� �ʾƾ� �Ҷ�,
        [SerializeField] float z_viewRotate                = 0f;

        [SerializeField] LayerMask targetMask;
        [SerializeField] LayerMask obstacleMask;

        //target �˻��� ������ �ڷ������� ����
        public Dictionary<Collider2D, string> hitedTargetContainer2 = new Dictionary<Collider2D, string>();
        //public List<Collider2D> hitedTargetContainer = new List<Collider2D>();

        float horizontalView_HalfAngle = 0f;

        private void Awake()
        {
            horizontalView_HalfAngle = horizontalView_Angle * 0.5f;
        }

        void Start()
        {
            /* public���� inspector���� ���� �����ϴ� ���� �� ���̴�
            //�Ҵ�
            //rig = GetComponent<Rigidbody2D>(); //������ ����
            //sr = GetComponent<SpriteRenderer>(); //�� ���İ� ������ ���ϰ� ����, �¿� ���� �����ϰ� ���
            //hpBar = GetComponentInChildren<Slider>(); //���� ������Ʈ�� �����̴�
            //anim = GetComponent<Animator>(); //�ִϸ��̼� ����
            //collider2d = GetComponent<CapsuleCollider2D>(); //���� ���Ŀ� �ɸ��� �Ÿ��� �ʵ��� ���ֱ� ���� ������

            //find �Ҵ�
            //player = FindObjectOfType<PlayerMove>().transform; //�÷��̾� ����
            //monsterColl = FindObjectOfType<MonsterColl>();
            */

            ground_Position = new Vector3(1, -1, 0); //����

            //�ʱ�ȭ
            turnDir = 1;

            //�ӽ�
            hp = 100;

            hp_txt.text = hp + " / " + 100; //ü�� ǥ��
        }

        //�ݽð� ���� = ����, �ð���� = ���
        //�Է��� -180~180�� Up Vector ����  Direction���� ��ȯ���ִ� AngleToDirZ �Լ�
        private Vector3 AngleToDirZ(float angleInDegree)
        {
            //���� Direction���� ��ȯ�ϱ� ����
            float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
            return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
        }

        //�þ߰� ����� �Լ�
        private void OnDrawGizmos()
        {
            if (debugMode)
            {
                horizontalView_HalfAngle = horizontalView_Angle * 0.5f;

                Vector3 originPos = transform.position + gizmo_Position;

                Gizmos.DrawWireSphere(originPos, viewRadius);

                //����/�� ��輱, ���� ����
                Vector3 horizontalRightDir = AngleToDirZ(-horizontalView_HalfAngle + z_viewRotate);
                Vector3 horizontalLeftDir = AngleToDirZ(horizontalView_HalfAngle + z_viewRotate);
                Vector3 lookDir = AngleToDirZ(z_viewRotate);

                //���� ��輱 �׸���
                Debug.DrawRay(originPos, horizontalLeftDir * viewRadius, Color.cyan);
                Debug.DrawRay(originPos, lookDir * viewRadius, Color.green);
                Debug.DrawRay(originPos, horizontalRightDir * viewRadius, Color.cyan);

                //���� �� �þ߿� �����ְ� ��� �þ߰� �����ִ��� Ȯ�� �� �� �ֵ�.
                FindViewTargets();
            }
        }

        public Dictionary<Collider2D,string> FindViewTargets()
        {
            //hitedTargetContainer.Clear();
            hitedTargetContainer2.Clear();

            Vector2 originPos = transform.position  + gizmo_Position;
            //��ġ����, �������� �ִ� Mask�� Collider Ÿ���� �ֱ� (������ ������ Ÿ�� Ȯ��)
            Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, viewRadius, targetMask);

            foreach (Collider2D hitedTarget in hitedTargets)
            {
                Vector2 targetPos = hitedTarget.transform.position + gizmo_Position;     //���� �ݶ��̴� ��ü ��ġ
                Vector2 dir       = (targetPos - originPos).normalized; //���� ���� (������)
                Vector2 lookDir   = AngleToDirZ(z_viewRotate);          //(�þ߰� ����)

                //float angle = Vector.Angle(lookDir, dir) �Ʒ� ������ �� �ڵ�� ���� ���� ������ ����
                float dot   = Vector2.Dot(lookDir, dir);//�� ���� ������ ������ ����
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;// Acos�� ������ �־� Degree�� ��ȯ (�þ߰� -> �������� ���� : ����� ����)

                if (angle <= horizontalView_HalfAngle)// ���� �� ������ ���� �ִٸ� == �þ߰� ���� ����
                {

                    //�� ��ġ, �� ����, ������ ũ��, ������ ��ֹ� ����ũ
                    RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, viewRadius, obstacleMask);

                    print(rayHitedTarget);
                    
                    if (rayHitedTarget)//�����Ѵٸ�
                    {
                        if (debugMode)
                        {
                            //��ֹ������� Ÿ���� ���� ���Ѵ� 
                            Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                        }
                    }
                    else //�������� �ʴ´ٸ�
                    {
                        //hitedTargetContainer.Add(hitedTarget);// Ȯ���� �� �� �ִ� ����� Add�� ���ش�.
                        hitedTargetContainer2.Add(hitedTarget, hitedTarget.gameObject.tag);

                        if (debugMode)
                        {
                            Debug.DrawLine(originPos, targetPos, Color.red);
                        }
                    }
                }
            }//foreach

            if (hitedTargetContainer2.Count > 0)
            {
                //eturn hitedTargetContainer.ToArray();
                return hitedTargetContainer2;
            }
            else
            {
                return null;
            }
        }

        //�ǰ� Player_Attack_Defend()���� ���ݽ� �ִϸ��̼����� (�ϸ�ũ)
        //�̺�Ʈ�Լ��� ȣ�⿡ ���� SendMassage�� ����
        void Damaged()
        {
            //�ӽ�
            hp -= 10;
            
            //ü�¹� ǥ��
            hpBar.value = hp * 0.01f;

            if (hp > 0)//ü���� ���Ҵٸ�
            {
                //�ǰ� �ִϸ��̼�
                anim.SetTrigger("damaged");

                //�ǰݻ��·� ��ȯ
                state = StateMachine.Damaged;

                //text�� ǥ��
                hp_txt.text = hp + " / " + 100;

                //�� ������Ʈ�� �ǰ�
                damagedPopUp.WhichDamaged(gameObject);
            }
            else//ü���� 0 ���϶��
            {
                //�׾���
                isDead = true;

                //���� �ִϸ��̼�
                anim.SetTrigger("death");

                //text�� ǥ��
                hp_txt.text = 0 + " / " + 100;

                //ü���� ���ٸ� ���� ���·� ��ȯ
                state = StateMachine.Death;
            }
        }

        //�ǰ��� ������ �ִϸ��̼� �̺�Ʈ �Լ�
        void DamagedEnd()
        {
            //���� ���·�
            state = StateMachine.Fallow;
        }

        //���� ����
        void PatronMove()
        {
            //������ (���� * �ӵ� , y)
            rig.velocity = new Vector2(turnDir * speed, rig.velocity.y);

            //�����ɽ�Ʈ �� üũ
            Check();
        }

        //������ üũ
        void Check()
        {
            //�����Ϳ� ���̼� ���̱�
            Debug.DrawRay(transform.position + ground_Position, Vector3.down * 1);
            //��� ���⿡ �ִ� ��ü
            RaycastHit2D hit = Physics2D.Raycast(transform.position + ground_Position, Vector3.down, 1f);

            //���� ��ü�� ���ٸ� == ��������
            if (!hit)
            {
                //������ �ٲ��ְ�
                TurnDir();
  
                //�߰ݻ����϶� ���̻� �i�� �ʰ�
                if (state == StateMachine.Fallow)
                {
                    //�ٽ� �����ϵ��� �Ѵ�
                    state = StateMachine.Patron;
                }
            }
            else //���� ��ü�� �ִٸ�
            {
                //�ױװ˻�
                switch (hit.collider.gameObject.tag)
                {
                    case "Ground"://���̶�� ����
                        isChanged = false;
                        break;
                    case "Player"://�÷��̾ ���� 
                        //���̾��Ű ĳ���� ������Ʈ �� : TalkingRange�� �ɸ� (�ش� Tag : Player �����ؾ� ����)
                        isChanged = false;

                        break;
                    default:// �̿��� ��ü, ���� ��ȯ
                        //� �ɸ����� �ִٸ� �������¿�����
                        if (state == StateMachine.Patron)
                        {
                            //������ �ٲ��ش�.
                            TurnDir();
                        }
                        break;
                }
            }//if(hit)

            //ĳ���� ������ȯ
            switch (turnDir)
            {
                case -1:
                    ground_Position = new Vector3(-1, -1, 0); // �����ɽ�Ʈ ��ġ ����
                    anim.SetBool("isWalk", true); //�ִϸ��̼�
                    sr.flipX = true; //�̹��� ������

                    z_viewRotate = Z_turn_View(turnDir, z_viewRotate); //�þ߰� ������ȯ�� 
                    //transform.localScale = new Vector2(2,2); �������� ����ϸ� �ڽ� ������Ʈ���� ���� ���������� -> UI�� ���� ��������� �Ѵ�.
                    break;
                case 0:
                    anim.SetBool("isWalk", false);

                    z_viewRotate = Z_turn_View(turnDir, z_viewRotate); //�þ߰� ������ȯ�� 
                    break;
                case 1:
                    ground_Position = new Vector3(1, -1, 0); // ��������Ʈ ��ġ ����
                    anim.SetBool("isWalk", true); //�ִϸ��̼� Ȱ��ȭ
                    sr.flipX = false;//�̹��� ������

                     z_viewRotate = Z_turn_View(turnDir, z_viewRotate); //�þ߰� ������ȯ�� 
                    //transform.localScale = new Vector2(-2,2);
                    break;
            }
        }//check

        //i == case ���� , z == �����̼� �� : ������ �ٲ� �� ������ִ� �Լ�
        float Z_turn_View(int i,float z)
        {
            //i  ������� �϶�
            if (i == 1)
            {
                //���� ������ �����
                if (z < 0)
                {
                    z *= -1;
                    return z;
                }
                else
                {
                    return z;
                }
            }
            //i �������� �϶�
            else if (i == -1)
            {
                //��� ������ ������
                if (z > 0)
                {
                    z *= -1;
                    return z;
                }
                else
                {
                    return z;
                }
            }
            //�̿� 
            else
            {
                //�״��
                return z;
            }
        }

        //���� ������
        void TurnDir()
        {
            //������ �� �ٲ���ٸ�
            if (!isChanged)//false
            {
                //������ȯ
                turnDir *= -1;
                z_viewRotate *= -1;

                // ���� ���� �Ϸ� true
                isChanged = true;

                //��ġ�� ���������� �߶��� �� �־
                CancelInvoke();

                //�������� �����ֱ�
                Invoke("RandomDir", Random.Range(2f, 4f));
            }
        }

        //����
        void RandomDir()
        {
            //������ȯ
            turnDir = Random.Range(-1, 2);

            //���ȣ��
            Invoke("RandomDir", Random.Range(2f, 4f));
        }

        void Update()
        {
            //�Ӹ������� ���󰡵���
            hp_txt.transform.position = new Vector2(transform.position.x, hp_txt.transform.position.y);
            name_txt.transform.position = new Vector2(transform.position.x, name_txt.transform.position.y);
            hpBar.transform.position = new Vector2(transform.position.x, hpBar.transform.position.y);

            print(state);

            //����ִٸ�
            if (!isDead)
            {
                //�Ÿ����
                distance = Vector2.Distance(transform.position, player.position);

                //player������ �Ÿ� (����)
                destination = player.position.x - transform.position.x;
                //��� ������ ���� ���⼳��
                destination = (destination < 0) ? -1 : 1;

                //������ ����ϸ� �����ǹǷ�
                if (attackTimer <= 3f)
                {
                    //�����ֱ� = �ð� ��
                    attackTimer += Time.deltaTime;
                }
                
                //��¥ ����� �˻�� ������ ����
                temp_TimeCheck = attackTimer;

                //���¿� ���� ���ǹ�
                switch (state)
                {
                    case StateMachine.Patron: //���� ����
                        Patron();
                        break;
                    case StateMachine.Fallow: //�߰� ����
                        Fallow();
                        break;
                    case StateMachine.Attack: //���� ����
                        Attack();
                        break;
                    case StateMachine.Damaged: //�ǰ� ����
                                             //ĳ���ͷκ��� SendMessage�� ���� ȣ��
                        break;
                    case StateMachine.Death: //���� ����
                                             //�ִϸ��̼��� ������ �̺�Ʈ�Լ��� ȣ��
                        break;
                }

                //�Ÿ��� ���� ���׻��� ����
                //Act_Distance();

            }//if(!isDead) ����ִٸ�
        }//update

        //�Ÿ��� ���� ���׻��� ���� �Լ�
        /*
        void Act_Distance()
        {

            print("�����Ÿ�");
            //��������
            state = StateMachine.Patron;

            //���ݻ���
            state = StateMachine.Attack;

            //�Ÿ��� ���� ����
            if (distance < fallow_Distance) //���ݰŸ�
            {
                print("���ݰŸ�");

                //���ݻ���
                state = StateMachine.Attack;

            }
            else if (fallow_Distance <= distance && distance < patrol_Distance) //�߰ݰŸ�
            {

                if (player)
                    //��������
                    state = StateMachine.Fallow;
            }
            else if (fallow_Distance <= distance) //�����Ÿ�
            {
                print("�����Ÿ�");
                //��������
                state = StateMachine.Patron;
            }
            
        }
        */

        //���� �Լ�
        void Patron()
        {
            print("��������");
            //���� ������
            PatronMove();

            //ü���� �ְ� �þ߰��� ���´ٸ�
            if (player_Attack_Defend.hp > 0  && hitedTargetContainer2.ContainsValue(player.tag))
            {
                //print(hitedTargetContainer2.ContainsValue(player.tag)); true Ȯ��

                //�������ٰ� �߰��Ϸ��� �� �� (��ü �Ÿ� ����)
                if (fallow_Distance <= distance && distance < patrol_Distance) //�߰ݰŸ�
                {
                    //�߰ݻ���
                    state = StateMachine.Fallow;
                    emotion.SetTrigger("Dead");
                    emotion.SetBool("isDead", true);
                }
                //�������ٰ� �����Ϸ��� �� �� (��ü �Ÿ� ����)
                if (distance < fallow_Distance) //���ݰŸ�
                {
                    //���ݻ���
                    state = StateMachine.Attack;
                    emotion.SetTrigger("Dead");
                    emotion.SetBool("isDead", true);
                }
            }
        }

        //�߰� �Լ�
        void Fallow()
        {
            if (hitedTargetContainer2.ContainsValue(player.tag))
            {
                print("�߰ݻ���");
                //���Ⱚ�� ���� ������ ���� (PatronMove���� �״�� �������� �ִϸ��̼��� ���ڴ��)
                switch (destination)
                {
                    case 0:
                        //����
                        anim.SetBool("isWalk", false);

                        z_viewRotate = Z_turn_View(0, z_viewRotate); //�þ߰� ������ȯ�� 
                        break;
                    case 1:
                        //������ ������
                        anim.SetBool("isWalk", true);
                        sr.flipX = false;

                        z_viewRotate = Z_turn_View(1, z_viewRotate); //�þ߰� ������ȯ�� 

                        hitbox_Direction.localPosition = hitbox_SizeCtrl; //��Ʈ�ڽ� ����
                                                                          //monsterColl.RightDirBox();
                        break;
                    case -1:
                        //���� ������
                        anim.SetBool("isWalk", true);
                        sr.flipX = true;

                        z_viewRotate = Z_turn_View(-1, z_viewRotate); //�þ߰� ������ȯ�� 

                        hitbox_Direction.localPosition = new Vector3(-hitbox_SizeCtrl.x, hitbox_SizeCtrl.y); //��Ʈ�ڽ� ����
                                                                                                             //monsterColl.LeftDirBox();
                        break;
                }
                //�߰��ϱ�
                rig.velocity = new Vector2(destination * maxSpeed, rig.velocity.y);

                if (distance < fallow_Distance) //��������� ���ݰŸ�
                {
                    //���ݻ���
                    state = StateMachine.Attack;
                    emotion.SetBool("isDead", true);
                }
            }//�þ߰��� ������

            //�߰��ϴٰ� �־����ų�, �ݶ��̴��� ���ٸ�
            if (patrol_Distance <= distance) //�����Ÿ�
            {
                //��������
                state = StateMachine.Patron;
                emotion.SetBool("isDead", false);
            }
        }//�߰��Լ�

        void Attack()
        {
            //ü���� �������� �� 
            if (player_Attack_Defend.hp > 0)
            {
                if (distance < attack_Distance)
                {
                    //����
                    destination = 0;
                    anim.SetBool("isWalk", false);
                }

                print("���ݻ���");

                //2�ʰ� �����ٸ�
                if (temp_TimeCheck >= 2f)
                {
                    //���� �ִϸ��̼� ���
                    anim.SetBool("isAttack", true);
                    anim.SetTrigger("attack");

                    print("���");

                    //���� �ֱ� �ʱ�ȭ
                    attackTimer = 0f;
                    temp_TimeCheck = 0;
                }
            }//if (player_Attack_Defend.hp >= 0 )

            else//����
            {
                //��������
                state = StateMachine.Patron;

                //���� �ִϸ��̼� ��Ȱ��ȭ
                anim.SetBool("isAttack", false);
                emotion.SetBool("isDead", false);
            }

            //�����ϴٰ� �Ÿ��� �־�����
            if (fallow_Distance <= distance && distance < patrol_Distance ) //�߰ݰŸ�
            {
                //�߰ݻ���
                state = StateMachine.Fallow;

                //���� �ִϸ��̼� ��Ȱ��ȭ
                anim.SetBool("isAttack", false);
            }
            //�߰ݰŸ����� �־����ų� �þ߰����� �����
            if (patrol_Distance <= distance ) //�����Ÿ�
            {
                //��������
                state = StateMachine.Patron;

                //���� �ִϸ��̼� ��Ȱ��ȭ
                anim.SetBool("isAttack", false);
                emotion.SetBool("isDead", false);
            }
        }


        //���ݽ��۽�, �ִϸ��̼ǿ� ȣ���� �̺�Ʈ �Լ�
        public void AttackStart()
        {
            //���ݿ� ���� ��Ʈ�ڽ�collider ���ֱ�
            monsterColl.hitBox.enabled = true;

            //�ӵ� ����
            tempSpeed = speed;
            maxTempSpeed = maxSpeed;

            //0 ���ݽÿ��� ������ x 
            speed = 0;
            maxSpeed = 0;
        }

        //���� ������, �ִϸ��̼ǿ� ȣ���� �̺�Ʈ �Լ�
        public void AttackEnd()
        {
            //���ݿ� ���� ��Ʈ�ڽ�collider ���ֱ�
            monsterColl.hitBox.enabled = false;

            //������ �ӵ� �ٽ� ����
            speed = tempSpeed;
            maxSpeed = maxTempSpeed;
        }

        //���ϸ��̼� �̺�Ʈ�Լ����� ȣ��
        void Death()
        {
            //�ݶ��̴� ����
            collider2d.enabled = false;
            //������ �ٵ� ����
            rig.simulated = false;

            //������ٰ� ����
            sr.color = Vector4.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, 0f), 1f);

            if (sr.color.a <= 0.3f)
            {
                anim.enabled = false;

                //���� �ı�
                Destroy(gameObject);
            }
        }

    }//class
}//namespace