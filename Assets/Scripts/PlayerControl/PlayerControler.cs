using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class PlayerControler
{
    //params
    float originspeed = 4.5f;
    float speed;
    float initSpeed = 7f;        
    float gravity = 9.8f;
    float barrierTherehold = 0.25f;
    float barrierAngle = 45f;
    float playerFormerAngle;

    public float deltaangle;
    //varibles
    private Vector3 moveDirection;       // 角色移动方向dw
    private Vector3 moveDirectionY;       // 角色移动方向
    private float verticalSpeed;         // 角色垂直速度
    public bool isGrounded;             // 角色是否在地面上
    private bool isSlop;             // 角色是否在斜坡上
    private bool isStair;             // 角色是否在斜坡上
    private bool isBarrierForward;             // 角色是否碰到其他物体
    private bool isBarrierBack;             // 角色是否碰到其他物体
    private bool isBarrierLeft;             // 角色是否碰到其他物体
    private bool isBarrierRight;             // 角色是否碰到其他物体
    public bool shouldrotate = false;
    private bool isrotate = true;

    public AnimatorPlayer ApPlayer;
    GameObject player;
    GameObject cam;
    
    float verticalInput, horizontalInput,mouseX,mouseY;
    
    Vector3 charactorForwad, charactorForwadNormalized,characterRight,characterFront,normal;
    LoadManager lm;
    private Vector3 barrierhitfrontVector3;
    private Vector3 barrierhitbackVector3;
    private Vector3 barrierhitleftVector3;
    private Vector3 barrierhitrightVector3;
    private CapsuleCollider playercapsuleCollider;
    private Vector3 playerpointBottom, playerpointTop;
    private float playerradius;
    private Vector3 timeToReachTargetRotation;
    private Vector3 currentTargetRotation;
    private Vector3 dampedTagetRotationVelocity;
    private Vector3 dampedTargetRotationPassedTime;
    public Vector3 RotateDirection;
    private float targetRotationYAngle;
    private Quaternion targetRotation;
    private float timepress;
    public bool ismove;
    public float tpsturnbodyangle;
    public bool tpsmouseshouldrotate;
    private Quaternion TpstargetRotation;
    private float TpstargetRotationYAngle;

    public PlayerControler(LoadManager LM)
    {
        lm = LM;
        speed = originspeed;
        deltaangle = 10000f;
        ////事件有顺序执行的特点，所以这样可以实现按顺序刷新
        //LM.playerMoveEvent += MouseInvisible;
        //LM.playerMoveEvent += Dash;
        ApPlayer = lm._ap;
        //NotificationCenter.Instance.AddObserver("MouseInvisible",MouseInvisible);

        NotificationCenter.Instance.AddObserver("Move", Move);

        NotificationCenter.Instance.AddObserver("Land animation not finfished",LockPlayerPosxz);
        NotificationCenter.Instance.AddObserver("Land animation finfished", UnLockPlayerPosxz);
        cam = lm.Getcam;
        //类是引用类型变量
        player = lm.Getplayer;
        playercapsuleCollider = player.GetComponent<CapsuleCollider>();
        playerradius = playercapsuleCollider.radius;
    }
    //freshevent
    public void  PlayerMoveFresh()
    {
        DetectCollide();
        Jump();
        Dash();
        Move(null);
    }

 

    // Start is called before the first frame update
    public void MouseInvisible()
    {
        //mouse invisible
        Cursor.visible = false;
        //mouse locked
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LockPlayerPosxz(object[] data)
    {
        NotificationCenter.Instance.RemoveObserver("Move", Move);

    }
    void UnLockPlayerPosxz(object[] data)
    {

        NotificationCenter.Instance.AddObserver("Move", Move);

    }
    private void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            speed = (float)2 * originspeed;
        }
        else
        {
            NotDash();
        }
    }
    private void NotDash()
    {
        speed = originspeed;
    }
    private void DetectCollide()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsBarrierRight();
            IsBarrierLeft();
            IsBarrierForward();
            IsBarrierBack();
        }

        if (Input.GetAxis("Horizontal") <= 0 && isBarrierRight)
        {
            isBarrierRight = false;

        }
        else if (!isBarrierRight)
        {
            IsBarrierRight();
        }
        if (Input.GetAxis("Horizontal") >= 0 && isBarrierLeft)
        {
            isBarrierLeft = false;
        }
        else if (!isBarrierLeft)
        {
            IsBarrierLeft();
        }
        if (Input.GetAxis("Vertical") <= 0 && isBarrierForward)
        {
            isBarrierForward = false;

        }
        else if (!isBarrierForward)
        {
            IsBarrierForward();
        }

        if (Input.GetAxis("Vertical") >= 0&& isBarrierBack)
        {
            isBarrierBack = false;
        }
        else if(!isBarrierBack)
        {
            IsBarrierBack();
        }
        
    }
    //walk&run
    private void Move(object[] data)
    {
       
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        ismove = (verticalInput != 0) || (horizontalInput != 0);
        if (horizontalInput > 0)
        {

            RotateDirection.x = 1;
        }
        else if (horizontalInput < 0)
        {
            RotateDirection.x = -1;
        }
        else
        {
            RotateDirection.x = 0;
        }

        if (verticalInput > 0)
        {
            RotateDirection.z = 1;
        }

        else if (verticalInput < 0)
        {
            RotateDirection.z = -1;
        }
        else
        {
            RotateDirection.z = 0;
        }
        RotateDirection.y = 0f;
        //charactorForwad =Vector3.forward;
        ////左右移动
        //characterRight = Vector3.right * horizontalInput;
        ////前后移动
        //charactorForwadNormalized = new Vector3(charactorForwad.x, 0, charactorForwad.z);

        //characterFront = charactorForwadNormalized * verticalInput;

        //   moveDirection = characterRight + characterFront;
        switch (lm._cc.GetCameraMode)
        {
            case 0:
                FpsMove();
                break;
            case 1:
                FreeLookMove();
                break;
            case 2:
                TPSMove();
                break;
        }



        if (isBarrierLeft)
        {
            //Debug.Log("hit left");
            Vector3 tmp = Vector3.Project(moveDirection, -player.transform.right);
          //  if (tmp != Vector3.zero) moveDirection -= tmp;
            
            if (tmp != Vector3.zero) moveDirection = moveDirection -tmp;

            //    moveDirection += pushtmp;
        }
        if (isBarrierRight)
        {
            //Debug.Log("hit Right");

            Vector3 tmp = Vector3.Project(moveDirection, player.transform.right);
          //  if (tmp != Vector3.zero) moveDirection -= tmp;
            if (tmp != Vector3.zero) moveDirection = moveDirection - tmp;
            //    moveDirection += pushtmp;
        }
        if (isBarrierForward)
        {
            //Debug.Log("hit front");
            Vector3 tmp = Vector3.Project(moveDirection, player.transform.forward);
           // //Debug.Log($"tmp{tmp}moveDirection{moveDirection}verticalInput{verticalInput}isBarrierForward{isBarrierForward}");
            //   moveDirection -= tmp;
            if (tmp != Vector3.zero) moveDirection = moveDirection - tmp ;
            // //Debug.LogFormat("characterfront{0}verticalInput{1}moveDirection{2}tmp{3}",characterFront,verticalInput,moveDirection,tmp);
        }
        if (isBarrierBack)
        {
            //Debug.Log("hit back");
            Vector3 tmp = Vector3.Project(moveDirection, -player.transform.forward);
          //  if (tmp != Vector3.zero) moveDirection -= tmp;
            if (tmp != Vector3.zero) moveDirection = moveDirection - tmp ;
            //     moveDirection += pushtmp;
        }

        if (IsSlope())
        {
            //Debug.Log("hit slope");
            Vector3 projectedMovement = Vector3.ProjectOnPlane(moveDirection, normal);
            player.transform.position += projectedMovement.normalized * speed * Time.deltaTime;
        }
        else
        {
            
            player.transform.position +=  moveDirection.normalized * speed * Time.deltaTime;
        }

    }

    private void FpsMove()
    {
        Debug.Log("tpsmouseshouldrotate" + tpsmouseshouldrotate);
        if (ismove && ApPlayer.GetcurrentAnimationName != "RotatePlus" &&
            ApPlayer.GetcurrentAnimationName != "RotateInv")
        {

            var fbdir = player.transform.forward* RotateDirection.z;

            //左右移动
            var lrdir = player.transform.right * RotateDirection.x;

            moveDirection = lrdir + fbdir;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

    }

    private void TPSMove()
    {
        if (ismove)
        {
           // Debug.Log("deltangle" + (targetRotationYAngle - player.transform.rotation.eulerAngles.y));
            TpsRotate();

        }


         Debug.Log("tpsmouseshouldrotate" + tpsmouseshouldrotate);
        if (ismove && ApPlayer.GetcurrentAnimationName != "RotatePlus" &&
            ApPlayer.GetcurrentAnimationName != "RotateInv")
        {
       
            var fbdir = Quaternion.Euler(0f, TPSGetRotateAngle(), 0f)  * Vector3.forward*
                            RotateDirection.z;

            //左右移动
            var lrdir = player.transform.right * RotateDirection.x;
            
            moveDirection = lrdir + fbdir;


        }
        else
        {
            moveDirection = Vector3.zero;
        }
        var rotateori = lm._cc.tpsrotateangle;
        if (rotateori > 0 && rotateori < 180)
        {
            tpsturnbodyangle = rotateori;
        }
        else if (rotateori > 180)
        {
            tpsturnbodyangle = rotateori - 360f;
        }

        if (rotateori < 0 && rotateori > -180)
        {
            tpsturnbodyangle = rotateori;
        }
        else if (rotateori < -180)
        {
            tpsturnbodyangle = rotateori + 360f;
        }


        if (ApPlayer.IsPlayFinish && (ApPlayer.GetcurrentAnimationName == "RotatePlus" ||
                                      ApPlayer.GetcurrentAnimationName == "RotateInv"))
        {
            tpsmouseshouldrotate = false;
        }

        if (Mathf.Abs(tpsturnbodyangle) >= 10f && !ismove)
        {
            tpsmouseshouldrotate = true;
        }


        if (tpsmouseshouldrotate)
        {
            if (ApPlayer.IsPlayFinish && (ApPlayer.GetcurrentAnimationName == "RotatePlus" ||
                                          ApPlayer.GetcurrentAnimationName == "RotateInv"))
            {
                tpsmouseshouldrotate = false;
            }
        }
        //  Debug.Log("timepress"+timepress+"shouldrotate"+shouldrotate+ "ApPlayer.IsPlayFinish"+ ApPlayer.IsPlayFinish);
        //if (shouldrotate)
        //{
        //    //  Debug.Log("before rotate angle" + tttt + "targetangle" + targetRotation.eulerAngles.y);
        //    deltaangle = targetRotation.eulerAngles.y - tttt;
        //    if (ApPlayer.IsPlayFinish && (ApPlayer.GetcurrentAnimationName == "RotatePlus" ||
        //                                  ApPlayer.GetcurrentAnimationName == "RotateInv"))
        //    {
        //        shouldrotate = false;
                
        //    }
        //}
        //else
        //{
        //    Rotate();
        //    deltaangle = 0;
        //    tttt = player.transform.rotation.eulerAngles.y;
        //}
    }
    private void FreeLookMove()
    {
        if (ismove)
        {
            timepress += Time.deltaTime;
            targetRotationYAngle = GetRotateAngle(RotateDirection.x, RotateDirection.z, true);
            Debug.Log("deltangle"+ (targetRotationYAngle - player.transform.rotation.eulerAngles.y));
            if (Mathf.Abs(targetRotationYAngle - player.transform.rotation.eulerAngles.y) < 5f)
            {
                shouldrotate = false;
            }
            else
            {
                shouldrotate = true;
            }
        }
        else
        {
            timepress = 0;
        }
       
        //  Debug.Log("should rotate"+shouldrotate+player.transform.rotation+targetRotation);
        if (ismove && ApPlayer.GetcurrentAnimationName != "RotatePlus" &&
            ApPlayer.GetcurrentAnimationName != "RotateInv")
        {
            moveDirection = Quaternion.Euler(0f, GetRotateAngle(RotateDirection.x, RotateDirection.z, true), 0f) *
                            Vector3.forward;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        if (ApPlayer.IsPlayFinish && (ApPlayer.GetcurrentAnimationName == "RotatePlus" ||
                                      ApPlayer.GetcurrentAnimationName == "RotateInv"))
        {
            shouldrotate = false;
        }

        if (timepress > 1.5f)
        {
            shouldrotate = false;

        }
        //  Debug.Log("timepress"+timepress+"shouldrotate"+shouldrotate+ "ApPlayer.IsPlayFinish"+ ApPlayer.IsPlayFinish);
        if (shouldrotate)
        {
            //  Debug.Log("before rotate angle" + tttt + "targetangle" + targetRotation.eulerAngles.y);
            deltaangle = targetRotation.eulerAngles.y - playerFormerAngle;
            if (ApPlayer.IsPlayFinish && (ApPlayer.GetcurrentAnimationName == "RotatePlus" ||
                                          ApPlayer.GetcurrentAnimationName == "RotateInv"))
            {
                shouldrotate = false;
            }
        }
        else
        {
            Rotate();
            deltaangle = 0;
            playerFormerAngle = player.transform.rotation.eulerAngles.y;
        }
    }

    //jump
    private void Jump()
    {
        moveDirectionY = moveDirection;
        moveDirectionY.x = 0;
        moveDirectionY.z = 0;
        // 判断角色是否在地面上
        if (IsGrounded())
        {
            object[] postnoticeani = { true };
            // NotificationCenter.Instance.PostNotification("Player isgrounded", postnoticeani);

                // 应用跳跃逻辑
                if (Input.GetButtonDown("Jump") && ApPlayer.GetcurrentAnimationName != "RotatePlus" &&
                    ApPlayer.GetcurrentAnimationName != "RotateInv")
            {
                //Debug.Log("jump called isground");
                verticalSpeed = initSpeed;
            }
            else
            {
                // 重置垂直速度
                verticalSpeed = 0f;
                Ray ray = new Ray(player.transform.position, Vector3.down);
                
                float rayDistance = 0.05f;
                RaycastHit hitinfoHit;
                // 检测射线是否与地面碰撞
                if (Physics.Raycast(ray, out hitinfoHit, rayDistance))
                {
                    player.transform.position= hitinfoHit.point;
                }
      
            }
        }
        else
        {
            // 应用重力逻辑
            if (verticalSpeed <= (initSpeed/2))
            {
                verticalSpeed -= gravity * Time.deltaTime;
            }
            else
            {
                verticalSpeed -= 2*gravity * Time.deltaTime;
            }
        //    //Debug.Log("jump called isair");
            
            //verticalSpeed -= gravity * Time.deltaTime;
        }

        // 将垂直速度应用到角色移动方向
        moveDirectionY.y = verticalSpeed * Time.deltaTime;
        //if ((player.transform.position.y - hitpositionVector3.y)<0.05f)
        //{
        //    player.transform.position = new Vector3(player.transform.position.x, hitpositionVector3.y+0.06f, player.transform.position.z);
        //}
        //else
        //{
            // 更新角色的位置
            player.transform.position += moveDirectionY;
        //}
    }
        
    public bool IsGrounded()
    {

        // 创建一个射线，从角色的底部向下发射
        // Ray ray = new Ray(player.transform.position, Vector3.down);
        //// //Debug.Log("isgrounded transform"+player.transform.position);
        // ////Debug.Log(player.name);
        // float rayDistance = 0.05f;
        // RaycastHit hitinfoHit;
        // // 检测射线是否与地面碰撞
        // if (Physics.Raycast(ray,out hitinfoHit,rayDistance))
        // {
        //     //Debug.Log("is ground");
        //     isGrounded = true;
        //     hitpositionVector3 = hitinfoHit.point;
        // }
        // else
        // {
        //     //Debug.Log("not ground");
        //     isGrounded = false;
        //     hitpositionVector3 = Vector3.zero;
        // }
        //playerpointTop = player.transform.position + playercapsuleCollider.center + player.transform.up * (playercapsuleCollider.height * 0.5f - playercapsuleCollider.radius);
        playerpointTop = player.transform.position +  player.transform.up * (playercapsuleCollider.height * 0.5f - playercapsuleCollider.radius);
        
        playerpointBottom = player.transform.position + playercapsuleCollider.center - player.transform.up * (playercapsuleCollider.height * 0.5f - playercapsuleCollider.radius);
           LayerMask ignoreMask = ~(1 << 8);
           Collider[] colliders = Physics.OverlapCapsule(playerpointTop, playerpointBottom, playerradius, ignoreMask);
           //Debug.DrawLine(playerpointBottom, playerpointTop, Color.green);
           if (colliders.Length != 0)
           {
               isGrounded = true;
             //  //Debug.Log("is ground");
            //player.transform.position = new Vector3(player)
        }
           else
           {
              // //Debug.Log("not ground");
               isGrounded = false;
           }

        return isGrounded;
    }
    //is slope
    private bool IsSlope()
    {
        float slopeHeightMaxDistance = 0.1f;
        Vector3 raycastDirection = Vector3.down; // 例如，向下射线
        Vector3 transformedDirection = lm.transform.TransformDirection(raycastDirection);
        Ray ray = new Ray(player.transform.position + player.transform.up*playerradius, transformedDirection);
        RaycastHit hit;
        //Debug.DrawRay(player.transform.position, transformedDirection);
        //该射线起点为 第一个参数，朝向y轴正下方，这样才能射到地面
        if (Physics.Raycast(ray, out hit,slopeHeightMaxDistance))
        {
            if (barrierhitbackVector3!= Vector3.up)
            {
                isSlop = true;
                normal = barrierhitbackVector3;
            }
            else if (barrierhitfrontVector3!=Vector3.up)
            {
                isSlop = true;
                normal = barrierhitfrontVector3;
            }
            else if (barrierhitleftVector3!=Vector3.up)
            {
                isSlop = true;
                normal = barrierhitleftVector3;
            }
            else if (barrierhitrightVector3!=Vector3.up)
            {
                isSlop = true;
                normal = barrierhitrightVector3;
            }
            else if (hit.normal!=Vector3.up)
            {
                normal = hit.normal;
                isSlop = true;
            }
            else
            {
                isSlop = false;
            }
            //return hit.normal != Vector3.up;//法线方向不重合就是斜面
        }

       
        return isSlop;
    }
    //isbarrier
    private bool IsBarrierForward()
    {
        // 创建一个射线，从角色的底部向前发射
        Ray rayfront = new Ray(player.transform.position + Vector3.up*playerradius, player.transform.forward);

        //Debug.DrawRay(player.transform.position + Vector3.up * playerradius, player.transform.forward);
        
        RaycastHit hit;
        // 检测射线是否与地面碰撞
        if (Physics.Raycast(rayfront, out hit, barrierTherehold))
        {
            // 获取两个向量的单位向量
            Vector3 normalizedVectorA = hit.normal.normalized;
            Vector3 normalizedVectorB = player.transform.forward.normalized;
            barrierhitfrontVector3 = hit.normal;
            // 计算向量之间的夹角（以弧度为单位）
            float angleInRadians = Mathf.Acos(Vector3.Dot(normalizedVectorA.normalized, normalizedVectorB.normalized));

            // 将弧度转换为角度
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
            //Debug.Log("Front angle"+angleInDegrees);
            if (angleInDegrees >= (90f + barrierAngle))
            {
                //Debug.Log("超过角度");
                isBarrierForward = true;
            }          
        }
        else
        {
            isBarrierForward = false;
            barrierhitfrontVector3 = Vector3.up;
        }
        
        return isBarrierForward;
    }

    private bool IsBarrierBack()
    {
        Vector3 transformedDirectionback = -player.transform.forward;
        Ray rayback = new Ray(player.transform.position + Vector3.up * playerradius, transformedDirectionback);
        //Debug.DrawRay(player.transform.position + Vector3.up * playerradius, transformedDirectionback);
        RaycastHit hit;
        // 检测射线是否与地面碰撞
        if (Physics.Raycast(rayback, out hit, barrierTherehold))
        {
            // 获取两个向量的单位向量
            Vector3 normalizedVectorA = hit.normal.normalized;
            Vector3 normalizedVectorB = transformedDirectionback.normalized;
            barrierhitbackVector3 = hit.normal;
            // 计算向量之间的夹角（以弧度为单位）
            float angleInRadians = Mathf.Acos(Vector3.Dot(normalizedVectorA.normalized, normalizedVectorB.normalized));

            // 将弧度转换为角度
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
           // //Debug.Log("Back angle" + angleInDegrees);
            if (angleInDegrees>=(90f+barrierAngle))
            {
                isBarrierBack = true;
            }   
        }
        else
        {
            barrierhitbackVector3 =Vector3.up;
            isBarrierBack = false;
        }
        return isBarrierBack;
    }

    private bool IsBarrierLeft()
    {

        Vector3 transformedDirectionleft = -player.transform.right;

        Ray rayleft = new Ray(player.transform.position + Vector3.up * playerradius, transformedDirectionleft);

        //Debug.DrawRay(player.transform.position + Vector3.up * playerradius, transformedDirectionleft);

        RaycastHit hit;
        // 检测射线是否与地面碰撞
        if (Physics.Raycast(rayleft, out hit, barrierTherehold))
        {
            // 获取两个向量的单位向量
            Vector3 normalizedVectorA = hit.normal.normalized;
            Vector3 normalizedVectorB = transformedDirectionleft.normalized;
            barrierhitleftVector3 = hit.normal;
            // 计算向量之间的夹角（以弧度为单位）
            float angleInRadians = Mathf.Acos(Vector3.Dot(normalizedVectorA.normalized, normalizedVectorB.normalized));

            // 将弧度转换为角度
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
           // //Debug.Log("Left angle" + angleInDegrees);
            if (angleInDegrees>=(90f+barrierAngle))
                isBarrierLeft = true;
        }
        else
        {
          //  //Debug.Log("Left not hit barrier");
            isBarrierLeft = false;
            barrierhitleftVector3=Vector3.up;
        }

        return isBarrierLeft;
    }

    private bool IsBarrierRight()
    {
        Vector3 transformedDirectionright = player.transform.right;
        // 创建一个射线，从角色的底部向下发射
        Ray rayright = new Ray(player.transform.position + Vector3.up * playerradius, transformedDirectionright);
        //Debug.DrawRay(player.transform.position + Vector3.up * playerradius, transformedDirectionright);
        RaycastHit hit;
        // 检测射线是否与地面碰撞
        if (Physics.Raycast(rayright, out hit, barrierTherehold) )
        {
            // 获取两个向量的单位向量
            Vector3 normalizedVectorA = hit.normal.normalized;
            Vector3 normalizedVectorB = transformedDirectionright.normalized;
            barrierhitrightVector3 = hit.normal;
            // 计算向量之间的夹角（以弧度为单位）
            float angleInRadians = Mathf.Acos(Vector3.Dot(normalizedVectorA.normalized, normalizedVectorB.normalized));

            // 将弧度转换为角度
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        //    //Debug.Log("Right angle" + angleInDegrees);
            if (angleInDegrees>=(90f+barrierAngle))
                isBarrierRight = true;
        }
        else
        {
          //  //Debug.Log("Right not hit barrier");
            isBarrierRight = false;
            barrierhitrightVector3=Vector3.up;
        }

        return isBarrierRight;
    }

    //rotate
    public void Rotate() //player rotate towards
    {
        //  Debug.Log("player angle"+currentTargetRotation.y+"directionangle"+directionangle);
       
       // player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 0.02f);
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, targetRotation, 2f);
        
        //player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, directionangle, 0f),0.35f);
        //    dampedTargetRotationPassedTime.y = 0f;
        //}
        //RotateTowardsTargetRotation();
    }

    private float GetRotateAngle(float xlocation, float zlocation, bool isconsidercamera)
    {
        float directionangle = Mathf.Atan2(xlocation, zlocation) * Mathf.Rad2Deg;
        if (directionangle < 0)
        {
            directionangle += 360f;
        }
        if (isconsidercamera)
        {
            directionangle += cam.transform.eulerAngles.y;
        }


        if (directionangle > 360f)
        {
            directionangle -= 360f;
        }

        targetRotation = Quaternion.Euler(0f, directionangle, 0f);
        return directionangle;
    }
    private float TPSGetRotateAngle()
    {
        float directionangle = 0;
        if (cam.transform.eulerAngles.y > 180)
        {
            directionangle =-360 + (cam.transform.eulerAngles.y + 360)%360;
        }
        else
        {
            directionangle = (cam.transform.eulerAngles.y+360)%360;
        }
        //Debug.Log("directionangle after"+ directionangle+ "tpsrotateangle"+lm._cc.tpsrotateangle);
        TpstargetRotation = Quaternion.Euler(0f, directionangle , 0f);

        return directionangle;
    }
    public void TpsRotate() //player rotate towards
    {
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, TpstargetRotation, 2f);
    }
}
