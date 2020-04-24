using Pathfinding;
using Pathfinding.RVO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RVOAgent : MonoBehaviour {

    public float repathRate = 1;

    private float nextRepath = 0;

    private Transform target;
    private bool canSearchAgain = true;

    private RVOController controller;
    private CharacterController characterController;

    public float maxSpeed = 10;

    Path path = null;

    List<Vector3> vectorPath;
    int wp;

    public float moveNextDist = 1;
    public float slowdownDistance = 1;
    public LayerMask groundMask;

    Seeker seeker;

    public void Awake()
    {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<RVOController>();
        characterController = GetComponent<CharacterController>();
    }

    /// <summary>Set the point to move to</summary>
    public void SetTarget(Transform target)
    {
        this.target = target;
        RecalculatePath();
    }

    public void Stop()
    {
        this.target = null;

        vectorPath.Clear();
    }

    public void RecalculatePath()
    {
        if(!target)
        {
            return;
        }

        canSearchAgain = false;
        nextRepath = Time.time + repathRate * (Random.value + 0.5f);
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    public void OnPathComplete(Path _p)
    {
        ABPath p = _p as ABPath;

        canSearchAgain = true;

        if (path != null) path.Release(this);
        path = p;
        p.Claim(this);

        if (p.error)
        {
            wp = 0;
            vectorPath = null;
            return;
        }


        Vector3 p1 = p.originalStartPoint;
        Vector3 p2 = transform.position;
        p1.y = p2.y;
        float d = (p2 - p1).magnitude;
        wp = 0;

        vectorPath = p.vectorPath;
        Vector3 waypoint;

        if (moveNextDist > 0)
        {
            for (float t = 0; t <= d; t += moveNextDist * 0.6f)
            {
                wp--;
                Vector3 pos = p1 + (p2 - p1) * t;

                do
                {
                    wp++;
                    waypoint = vectorPath[wp];
                } while (controller.To2D(pos - waypoint).sqrMagnitude < moveNextDist * moveNextDist && wp != vectorPath.Count - 1);
            }
        }
    }

    public void Update()
    {
        if (Time.time >= nextRepath && canSearchAgain)
        {
            RecalculatePath();
        }

        Vector3 pos = transform.position;

        if (vectorPath != null && vectorPath.Count != 0)
        {
            while ((controller.To2D(pos - vectorPath[wp]).sqrMagnitude < moveNextDist * moveNextDist && wp != vectorPath.Count - 1) || wp == 0)
            {
                wp++;
            }

            // Current path segment goes from vectorPath[wp-1] to vectorPath[wp]
            // We want to find the point on that segment that is 'moveNextDist' from our current position.
            // This can be visualized as finding the intersection of a circle with radius 'moveNextDist'
            // centered at our current position with that segment.
            var p1 = vectorPath[wp - 1];
            var p2 = vectorPath[wp];

            // Calculate the intersection with the circle. This involves some math.
            var t = VectorMath.LineCircleIntersectionFactor(controller.To2D(transform.position), controller.To2D(p1), controller.To2D(p2), moveNextDist);
            // Clamp to a point on the segment
            t = Mathf.Clamp01(t);
            Vector3 waypoint = Vector3.Lerp(p1, p2, t);

            // Calculate distance to the end of the path
            float remainingDistance = controller.To2D(waypoint - pos).magnitude + controller.To2D(waypoint - p2).magnitude;
            for (int i = wp; i < vectorPath.Count - 1; i++) remainingDistance += controller.To2D(vectorPath[i + 1] - vectorPath[i]).magnitude;

            // Set the target to a point in the direction of the current waypoint at a distance
            // equal to the remaining distance along the path. Since the rvo agent assumes that
            // it should stop when it reaches the target point, this will produce good avoidance
            // behavior near the end of the path. When not close to the end point it will act just
            // as being commanded to move in a particular direction, not toward a particular point
            var rvoTarget = (waypoint - pos).normalized * remainingDistance + pos;
            // When within [slowdownDistance] units from the target, use a progressively lower speed
            var desiredSpeed = Mathf.Clamp01(remainingDistance / slowdownDistance) * maxSpeed;
            Debug.DrawLine(transform.position, waypoint, Color.red);
            controller.SetTarget(rvoTarget, desiredSpeed, maxSpeed);
        }
        else
        {
            // Stand still
            controller.SetTarget(pos, maxSpeed, maxSpeed);
        }

        // Get a processed movement delta from the rvo controller and move the character.
        // This is based on information from earlier frames.
        var movementDelta = controller.CalculateMovementDelta(Time.deltaTime);
        pos = movementDelta;

        // Rotate the character if the velocity is not extremely small
        if (Time.deltaTime > 0 && movementDelta.magnitude / Time.deltaTime > 0.01f)
        {
            var rot = transform.rotation;
            var targetRot = Quaternion.LookRotation(movementDelta, controller.To3D(Vector2.zero, 1));
            const float RotationSpeed = 5;
            if (controller.movementPlane == MovementPlane.XY)
            {
                targetRot = targetRot * Quaternion.Euler(-90, 180, 0);
            }
            transform.rotation = Quaternion.Slerp(rot, targetRot, Time.deltaTime * RotationSpeed);
        }

        if (controller.movementPlane == MovementPlane.XZ)
        {
            RaycastHit hit;
            if (Physics.Raycast(pos + Vector3.up, Vector3.down, out hit, 100, groundMask))
            {
                pos.y = hit.point.y;
            }
        }

       // transform.position = pos;
        characterController.Move(pos);
    }
}

