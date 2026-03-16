diff --git a/UnityScripts/PlayerController.cs b/UnityScripts/PlayerController.cs
new file mode 100644
index 0000000000000000000000000000000000000000..94cb5b7c2b7d782d7c1d30640727efe8e9fe2ed1
--- /dev/null
+++ b/UnityScripts/PlayerController.cs
@@ -0,0 +1,72 @@
+using UnityEngine;
+
+[RequireComponent(typeof(CharacterController))]
+public class PlayerController : MonoBehaviour
+{
+    [Header("Movement")]
+    public float walkSpeed = 4f;
+    public float runSpeed = 7f;
+    public float gravity = -20f;
+
+    [Header("Look")]
+    public Transform cameraPivot;
+    public float mouseSensitivity = 140f;
+    public float lookClamp = 80f;
+
+    private CharacterController controller;
+    private float verticalVelocity;
+    private float xRotation;
+
+    public bool IsRunning { get; private set; }
+
+    private void Awake()
+    {
+        controller = GetComponent<CharacterController>();
+        Cursor.lockState = CursorLockMode.Locked;
+        Cursor.visible = false;
+    }
+
+    private void Update()
+    {
+        HandleLook();
+        HandleMovement();
+    }
+
+    private void HandleLook()
+    {
+        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
+        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
+
+        xRotation -= mouseY;
+        xRotation = Mathf.Clamp(xRotation, -lookClamp, lookClamp);
+
+        if (cameraPivot != null)
+        {
+            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
+        }
+
+        transform.Rotate(Vector3.up * mouseX);
+    }
+
+    private void HandleMovement()
+    {
+        float horizontal = Input.GetAxis("Horizontal");
+        float vertical = Input.GetAxis("Vertical");
+
+        Vector3 move = transform.right * horizontal + transform.forward * vertical;
+        IsRunning = Input.GetKey(KeyCode.LeftShift);
+        float speed = IsRunning ? runSpeed : walkSpeed;
+
+        if (controller.isGrounded && verticalVelocity < 0f)
+        {
+            verticalVelocity = -2f;
+        }
+
+        verticalVelocity += gravity * Time.deltaTime;
+
+        Vector3 velocity = move * speed;
+        velocity.y = verticalVelocity;
+
+        controller.Move(velocity * Time.deltaTime);
+    }
+}
