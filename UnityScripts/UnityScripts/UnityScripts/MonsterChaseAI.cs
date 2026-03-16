diff --git a/UnityScripts/MonsterChaseAI.cs b/UnityScripts/MonsterChaseAI.cs
new file mode 100644
index 0000000000000000000000000000000000000000..8c4ee62b4ab64114bae35cbdb5c403680c52aebe
--- /dev/null
+++ b/UnityScripts/MonsterChaseAI.cs
@@ -0,0 +1,112 @@
+using UnityEngine;
+using UnityEngine.AI;
+
+[RequireComponent(typeof(NavMeshAgent))]
+public class MonsterChaseAI : MonoBehaviour
+{
+    public enum MonsterState
+    {
+        Roaming,
+        Chasing
+    }
+
+    [Header("References")]
+    public Transform player;
+    public PlayerController playerController;
+
+    [Header("Ranges")]
+    public float visionRange = 12f;
+    public float hearingRange = 8f;
+    public float catchDistance = 1.8f;
+
+    [Header("Speeds")]
+    public float roamSpeed = 2.5f;
+    public float chaseSpeed = 5.5f;
+
+    [Header("Roaming")]
+    public float roamRadius = 16f;
+    public float roamPointTolerance = 2f;
+
+    private NavMeshAgent agent;
+    private MonsterState currentState;
+    private Vector3 spawnPoint;
+
+    private void Awake()
+    {
+        agent = GetComponent<NavMeshAgent>();
+        spawnPoint = transform.position;
+        currentState = MonsterState.Roaming;
+        agent.speed = roamSpeed;
+        SetNewRoamTarget();
+    }
+
+    private void Update()
+    {
+        if (player == null) return;
+
+        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
+        bool canSeePlayer = distanceToPlayer <= visionRange && HasLineOfSight();
+        bool canHearPlayer = playerController != null && playerController.IsRunning && distanceToPlayer <= hearingRange;
+
+        if (canSeePlayer || canHearPlayer)
+        {
+            currentState = MonsterState.Chasing;
+        }
+        else if (currentState == MonsterState.Chasing && distanceToPlayer > visionRange * 1.5f)
+        {
+            currentState = MonsterState.Roaming;
+            SetNewRoamTarget();
+        }
+
+        switch (currentState)
+        {
+            case MonsterState.Roaming:
+                agent.speed = roamSpeed;
+                if (!agent.pathPending && agent.remainingDistance <= roamPointTolerance)
+                {
+                    SetNewRoamTarget();
+                }
+                break;
+
+            case MonsterState.Chasing:
+                agent.speed = chaseSpeed;
+                agent.SetDestination(player.position);
+                break;
+        }
+
+        if (distanceToPlayer <= catchDistance)
+        {
+            Debug.Log("Game Over: The pink monster caught you.");
+            Time.timeScale = 0f;
+        }
+    }
+
+    private bool HasLineOfSight()
+    {
+        Vector3 origin = transform.position + Vector3.up * 1.2f;
+        Vector3 direction = (player.position + Vector3.up - origin).normalized;
+
+        if (Physics.Raycast(origin, direction, out RaycastHit hit, visionRange))
+        {
+            return hit.transform == player;
+        }
+
+        return false;
+    }
+
+    private void SetNewRoamTarget()
+    {
+        for (int i = 0; i < 10; i++)
+        {
+            Vector3 randomOffset = Random.insideUnitSphere * roamRadius;
+            randomOffset.y = 0f;
+            Vector3 randomPoint = spawnPoint + randomOffset;
+
+            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit navHit, 4f, NavMesh.AllAreas))
+            {
+                agent.SetDestination(navHit.position);
+                return;
+            }
+        }
+    }
+}
