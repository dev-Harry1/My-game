diff --git a/UnityScripts/KeyPickup.cs b/UnityScripts/KeyPickup.cs
new file mode 100644
index 0000000000000000000000000000000000000000..cdc008d226ad78baae34c45efe6fdd52ea020520
--- /dev/null
+++ b/UnityScripts/KeyPickup.cs
@@ -0,0 +1,16 @@
+using UnityEngine;
+
+public class KeyPickup : MonoBehaviour
+{
+    private void OnTriggerEnter(Collider other)
+    {
+        if (!other.CompareTag("Player")) return;
+
+        if (KeyCollectGameManager.Instance != null)
+        {
+            KeyCollectGameManager.Instance.CollectKey();
+        }
+
+        Destroy(gameObject);
+    }
+}
