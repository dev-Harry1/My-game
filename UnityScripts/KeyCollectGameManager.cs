diff --git a/UnityScripts/KeyCollectGameManager.cs b/UnityScripts/KeyCollectGameManager.cs
new file mode 100644
index 0000000000000000000000000000000000000000..252218588f7ea4488437bb0bfe17a1a737fa3abf
--- /dev/null
+++ b/UnityScripts/KeyCollectGameManager.cs
@@ -0,0 +1,51 @@
+using UnityEngine;
+using TMPro;
+
+public class KeyCollectGameManager : MonoBehaviour
+{
+    public static KeyCollectGameManager Instance;
+
+    [Header("Progress")]
+    public int keysNeeded = 5;
+    public int keysCollected;
+
+    [Header("UI (Optional)")]
+    public TextMeshProUGUI objectiveText;
+
+    private void Awake()
+    {
+        if (Instance == null)
+        {
+            Instance = this;
+        }
+        else
+        {
+            Destroy(gameObject);
+            return;
+        }
+
+        UpdateObjectiveText();
+    }
+
+    public void CollectKey()
+    {
+        keysCollected++;
+        UpdateObjectiveText();
+
+        if (keysCollected >= keysNeeded)
+        {
+            Debug.Log("All keys collected. Find and open the exit!");
+        }
+    }
+
+    public bool HasAllKeys()
+    {
+        return keysCollected >= keysNeeded;
+    }
+
+    private void UpdateObjectiveText()
+    {
+        if (objectiveText == null) return;
+        objectiveText.text = $"Keys: {keysCollected}/{keysNeeded}";
+    }
+}
