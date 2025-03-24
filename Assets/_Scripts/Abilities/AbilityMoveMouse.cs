using UnityEngine;
using UnityEngine.AI;

public class AbilityMoveMouse : Ability<AbilityMoveMouseData>
{

    private Camera camera;
    private NavMeshPath path;
    private Vector3[] corners;

    public AbilityMoveMouse(AbilityMoveMouseData data, CharacterControl owner) : base(data, owner)
    {
        camera = Camera.main;
        path = new NavMeshPath();

    }

    public override void Update()
    {
        if ( owner == null || owner.cc == null)
            return;

        InputMouse();
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

        DrawDebugPath();

    }

    private void DrawDebugPath()
    {
        if (corners == null) return;

        // 점2개 -> 선1개
        for (int i=0; i<corners.Length-1; i++)
            Debug.DrawLine(corners[i], corners[i+1], Color.red, 3f);
    }


}
