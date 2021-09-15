using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Luke
{
    public class MouseCursor : MonoBehaviour
    {
        public Vector3 mousePos;
        public Texture2D cursorArrow;

        public event Action<NPCInfomation> OnClickSuspectEvent;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.SetCursor(cursorArrow, mousePos, CursorMode.ForceSoftware);
        
            //will only work properly for the build of the game (this locks the mouse cursor within the game screen bounds)
            Cursor.lockState = CursorLockMode.Confined;
        }

        // Update is called once per frame
        void Update()
        {
            Mouse();
        }

        public void Mouse()
        {
            mousePos = Input.mousePosition;
        }

        public void OnClick()
        {
            NPCInfomation npcinfo;
            OnClickSuspectEvent?.Invoke(npcinfo);
        }
    }
}
