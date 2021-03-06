using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class StaticAssign : MonoBehaviour {
        public GlobalControl GC;
        public PauseControl PC;
        public KeyBase KB;
        public StatusRenderer STR;
        public Cursor MainCursor;

        public void Awake()
        {
            GlobalControl.Main = GC;
            Cursor.Main = MainCursor;
            KeyBase.Main = KB;
            StatusRenderer.Main = STR;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}