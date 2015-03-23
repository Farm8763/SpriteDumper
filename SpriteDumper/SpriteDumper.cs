using System;
using ICities;
using UnityEngine;

namespace SpriteDumper {

    public class SpriteDumper : LoadingExtensionBase, IUserMod {

        private GameObject gameObj;

        public string Name {
            get { return "Sprite Dumper"; }
        }

        public string Description {
            get { return "Dump sprites and wiki page for http://docs.skylinesmodding.com"; }
        }


        public override void OnLevelLoaded(LoadMode mode) {
            base.OnLevelLoaded(mode);

            if (gameObj == null) {
                gameObj = new GameObject("SpriteDumper");
                gameObj.AddComponent<SpriteDumperObj>();
            }
            Utils.Log("Loaded");
        }

        public override void OnLevelUnloading() {
            base.OnLevelUnloading();

            GameObject.Destroy(gameObj);
        }

    }

}
