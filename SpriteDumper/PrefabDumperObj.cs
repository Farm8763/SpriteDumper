using System;
using System.IO;
using ColossalFramework.UI;
using UnityEngine;

namespace SpriteDumper {
    public class PrefabDumperObj : MonoBehaviour {

        private string prefabPath = "PrefabDumper\\PrefabDumper";
        private string wikiTemplate = "" +
                ".. WARNING FOR CONTRIBUTORS: Don't modify this file! It's generated with a mod (see below) and all changes made will be lost with the next update.\r\n\r\n" +
                "==========\r\n" +
                "Prefab {TYPE}\r\n" +
                "==========\r\n" +
                "Here you can find a list of prefabricated roads/buildings/vehicles.\r\n" + 
                "You can use these to get the VehicleInfo/BuildingInfo/NetInfo you want to create\r\n" +
                "\r\n\r\n {PREFABS} \r\n" +
                "About this page\r\n" +
                "---------------\r\n" +
                "This wiki page was created in game with the `SpriteDumper mod <https://github.com/worstboy32/SpriteDumper>`__ .\r\n" +
                "To modify the text in this document please create a PR on the mod on github.\r\n" +
                "If there are sprites missing you can run the mod and create a PR on the docs repo with the new generated file.\r\n" +
                "\r\n" +
                "Kudos to `Permutation <http://www.skylinesmodding.com/users/permutation/>`__ for sharing the method for dumping sprites.\r\n";

        void OnEnable() {
            Utils.Log("PrefabDumperObj created!");
        }

        void Update() {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D)) {
                Dump();
            }
        }

        private void Dump() {
            Utils.Log("Creating wiki page...");
            if (Utils.CreateDir(prefabPath)) {
                Utils.Log("Directory for prefab created at: '" + prefabPath + "'");
            }

            string prefabStr = "";
            int count = PrefabCollection<VehicleInfo>.LoadedCount();
            for (uint x = 0; x < count; x += 1)
            {
                prefabStr += "**" + "Name:" + PrefabCollection<VehicleInfo>.GetPrefab(x).name + ", Index: " + x + "**\r\n\r\n";

            }
            string wikiPage = wikiTemplate;
            wikiPage = wikiPage.Replace("{TYPE}", "VehicleInfo");
            System.IO.StreamWriter file = new System.IO.StreamWriter("PrefabDumper\\" + "VehiclePrefabs.rst");
            file.WriteLine(wikiPage.Replace("{PREFABS}", prefabStr));
            file.Close();
            
            prefabStr = "";
            count = PrefabCollection<BuildingInfo>.LoadedCount();
            for (uint x = 0; x < count; x += 1)
            {
                prefabStr += "**" + "Name:" + PrefabCollection<BuildingInfo>.GetPrefab(x).name + ", Index: " + x + "**\r\n\r\n";

            }
            wikiPage = wikiTemplate;
            wikiPage = wikiPage.Replace("{TYPE}", "BuildingInfo");
            System.IO.StreamWriter file = new System.IO.StreamWriter("PrefabDumper\\" + "BuildingPrefabs.rst");
            file.WriteLine(wikiPage.Replace("{PREFABS}", prefabStr));
            file.Close();
            
            prefabStr = "";
            count = PrefabCollection<NetinfoInfo>.LoadedCount();
            for (uint x = 0; x < count; x += 1)
            {
                prefabStr += "**" + "Name:" + PrefabCollection<NetinfoInfo>.GetPrefab(x).name + ", Index: " + x + "**\r\n\r\n";

            }
            wikiPage = wikiTemplate;
            wikiPage = wikiPage.Replace("{TYPE}", "NetInfo (Roads)");
            System.IO.StreamWriter file = new System.IO.StreamWriter("PrefabDumper\\" + "NetPrefabs.rst");
            file.WriteLine(wikiPage.Replace("{PREFABS}", prefabStr));
            file.Close();
            
            Utils.Log("Dumped " + count + " sprites to '" + spritePath + "'");
        }

    }
}
