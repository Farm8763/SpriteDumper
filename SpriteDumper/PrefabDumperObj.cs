using System;
using System.IO;
using ColossalFramework.UI;
using UnityEngine;

namespace SpriteDumper {
    public class PrefabDumperObj : MonoBehaviour {
        private string template =
                "namespace SkylinesGIS\n" +
                "{\n" +
                "    class PrefabNames\n" +
                "    {\n" +
                "        class Buildings\n" +
                "        {\n" +
                "{BUILDINGS}\n" +
                "        }\n\n" +
                "        class Vehicles\n" +
                "        {\n" +
                "{VEHICLES}\n" +
                "        }\n" + 
                "        class Roads\n" +
                "        {\n" + 
                "{ROADS}\n" +
                "        }\n" +
                "    }\n"
                + "}";

        void OnEnable() {
            Utils.Log("PrefabDumperObj created!");
        }

        void Update() {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D)) {
                Dump();
            }
        }

        private void Dump() {
            Utils.Log("Creating wiki pages...");

            string prefabStr = "";
            int count = PrefabCollection<VehicleInfo>.LoadedCount();
            for (uint x = 0; x < count; x += 1)
            {
                prefabStr += "            public static string " + PrefabCollection<VehicleInfo>.GetPrefab(x).name.Replace(" ", "").Replace("-", "") + " = \"" + PrefabCollection<VehicleInfo>.GetPrefab(x).name + "\";\n";

            }
            
            System.IO.StreamWriter file = new System.IO.StreamWriter("PrefabDumper\\" + "PrefabNames.cs");
            template = template.Replace("{VEHICLES}", prefabStr);
            Utils.Log("Dumped " + count);
            
            prefabStr = "";
            count = PrefabCollection<BuildingInfo>.LoadedCount();
            for (uint x = 0; x < count; x += 1)
            {
                prefabStr += "            public static string " + PrefabCollection<BuildingInfo>.GetPrefab(x).name.Replace(" ", "").Replace("-", "") + " = \"" + PrefabCollection<BuildingInfo>.GetPrefab(x).name + "\";\n";

            }
            template = template.Replace("{BUILDINGS}", prefabStr);
            Utils.Log("Dumped " + count);
            
            prefabStr = "";
            count = PrefabCollection<NetInfo>.LoadedCount();
            for (uint x = 0; x < count; x += 1)
            {
                prefabStr += "            public static string " + PrefabCollection<NetInfo>.GetPrefab(x).name.Replace(" ", "").Replace("-", "") + " = \"" + PrefabCollection<NetInfo>.GetPrefab(x).name + "\";\n";

            }
            template = template.Replace("{ROADS}", prefabStr);
            file.WriteLine(template);
            file.Close();

            Utils.Log("Dumped " + count);
        }

    }
}
