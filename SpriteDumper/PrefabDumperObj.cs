using System;
using System.IO;
using ColossalFramework.UI;
using UnityEngine;

namespace SpriteDumper {
    public class PrefabDumperObj : MonoBehaviour {
        private string wikiTemplate = "" +
                ".. WARNING FOR CONTRIBUTORS: Don't modify this file! It's generated with a mod (see below) and all changes made will be lost with the next update.\r\n\r\n" +
                "==========\r\n" +
                "Accessing prefabricated BuildingInfo,VehicleInfo,NetInfo objects\r\n" +
                "==========\r\n" +
                "Here you can find all prefabricated road/building/vehicle names.\r\n" +
                "You can use these names to get the BuildingInfo/VehicleInfo/NetInfo of the object you want to build programatically\r\n" +
                "To use these names to get the BuildingInfo/VehicleInfo/NetInfo, use the code below:\r\n" +
                "    .. code-block:: C#\r\n" +
                "\n\r" +
                "        NetInfo myRoadInfo = PrefabCollection<NetInfo>.FindLoaded(string name);\r\n" +
                "        VehicleInfo myVehicleInfo = PrefabCollection<VehicleInfo>.FindLoaded(string name);\r\n" +
                "        BuildingInfo myBuildingInfo = PrefabCollection<BuildingInfo>.FindLoaded(string name);\r\n" +
                "\n\r" +
                "Below is a class containing all the names of the prefabs.\r\n" +
                "" +
                "    .. code-block:: C#" +
                "\n\r" +
                "{PREFABCLASS}" +
                "" +
                "This wiki page was created in game with the `SpriteDumper mod <https://github.com/worstboy32/SpriteDumper>`__ .\r\n" +
                "To modify the text in this document please create a PR on the mod on github.\r\n" +
                "If there are names missing you can run the mod and create a PR on the docs repo with the new generated file.\r\n" +
                "Take care when regenerating this page to not include any of your custom building/vehicle/road names. Disable other mods first!" +
                "\r\n" +
                "Kudos to `Permutation <http://www.skylinesmodding.com/users/permutation/>`__ for sharing the method for dumping sprites.\r\n";
        
        private string template =
                "namespace <namespace>\n" +
                "{\n" +
                "    public static class PrefabNames\n" +
                "    {\n" +
                "        public static class Buildings\n" +
                "        {\n" +
                "{BUILDINGS}\n" +
                "        }\n\n" +
                "        public static class Vehicles\n" +
                "        {\n" +
                "{VEHICLES}\n" +
                "        }\n" +
                "        public static class Roads\n" +
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
            
            string aLine, wikiCode = null;
            StringReader strReader = new StringReader(template);
            while (true)
            {
                aLine = strReader.ReadLine();
                if (aLine != null)
                {
                    wikiCode = wikiCode + "        " + aLine + "\r";
                }
                else
                {
                    wikiCode = wikiCode + "\n";
                    break;
                }
            }
            System.IO.StreamWriter wikiFile = new System.IO.StreamWriter("PrefabDumper\\" + "PrefabNames.rst");
            wikiTemplate = wikiTemplate.Replace("{PREFABCLASS}", wikiCode);
            wikiFile.WriteLine(wikiTemplate);
            wikiFile.Close();
            Utils.Log("Dumped wiki page.");
        }

    }
}
