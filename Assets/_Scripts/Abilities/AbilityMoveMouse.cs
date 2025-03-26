using UnityEngine;
using UnityEngine.AI;

public class AbilityMoveMouse : Ability<AbilityMoveMouseData>
{

    private Camera camera;
    private NavMeshPath path;
    private Vector3[] corners;
    private int next;
    private bool isArrived = false;

    public AbilityMoveMouse(AbilityMoveMouseData data, CharacterControl owner) : base(data, owner)
    {
        camera = Camera.main;
        path = new NavMeshPath();

        isArrived = true;

    }

    public override void Update()
    {
        if ( owner == null || owner.rb == null)
            return;

        InputMouse();
        MoveAnimation();
    }

    public override void FixedUpdate()
    {
        if ( owner == null || owner.rb == null)
            return;

        FollowPath();

    }

    private void InputMouse()
    {
        // 0 : Left, 1: Right, 2: Wheel
        if (Input.GetMouseButtonDown(1))
        { 
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out var hit))
                SetDestination(hit.point);


        }
    }

    private void SetDestination(Vector3 destination)
    {
        
        if (NavMesh.CalculatePath(owner.transform.position, destination, -1, path) == false)
            return;
        
        corners = path.corners;
        next = 1;
        isArrived = false;


    }


    Quaternion _lookrot;
    private void FollowPath()
    {
        if( corners == null || corners.Length <= 0 || isArrived == true)
            return;
        
        // 다음 위치
        Vector3 nexttarget = corners[next];
        // 최종 위치
        Vector3 finaltarget = corners[corners.Length-1];
        // 다음 위치 방향
        Vector3 direction = (nexttarget - owner.transform.position).normalized;
        direction.y = 0;

        //회전
        if (direction != Vector3.zero)
            _lookrot = Quaternion.LookRotation(direction);

        owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, _lookrot, data.rotatePerSec * Time.deltaTime);

        //이동
        //50을 곱한 이유 = movePerSec와 linearVelocity 값의 범위를 맞추기 위한 상수
        Vector3 movement = direction * data.movePerSec *50f * Time.deltaTime;
        Vector3 velocity = new Vector3(movement.x, owner.rb.linearVelocity.y, movement.z);

        owner.rb.linearVelocity = velocity;

        //도착 확인
        if (Vector3.Distance(nexttarget, owner.rb.position) <= data.stopdistance)
        {
            next++;

            //최종 목적지
            if ( next >= corners.Length )
            {
                isArrived = true;
                owner.rb.linearVelocity = Vector3.zero;
            }
        }

        // 최종 위치 준비 도착
        if (Vector3.Distance(finaltarget, owner.rb.position) <= data.runtostopDistance)
        {
            owner.animator?.CrossFadeInFixedTime("RUNTOSTOP", 0.1f, 0, 0f); 
        }

    }

    private void MoveAnimation()
    {
        float a = isArrived ? 0f : data.movePerSec;

        float spd = Mathf.Lerp(owner.animator.GetFloat("moveSpeed"), a, Time.deltaTime * 10f);
        owner.animator.SetFloat("moveSpeed", spd);
    }


}
