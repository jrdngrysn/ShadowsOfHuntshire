using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Slot : MonoBehaviour {
        public bool IniActive;
        public Vector2Int Position;
        public Vector2 ERPosition;
        public Character CurrentCharacter;

        public void Awake()
        {
            if (IniActive)
                GlobalControl.Main.AddSlot(this);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AssignCharacter(Character C)
        {
            CurrentCharacter = C;
            C.AssignSlot(this);
        }

        public void Empty()
        {
            CurrentCharacter = null;
        }

        public Character GetCharacter()
        {
            return CurrentCharacter;
        }

        public Slot GetPairSlot()
        {
            if (GlobalControl.Main.GetSlot(Position.x + 1, Position.y) && !GlobalControl.Main.GetSlot(Position.x + 1, Position.y).GetCharacter())
                return GlobalControl.Main.GetSlot(Position.x + 1, Position.y);
            else if (GlobalControl.Main.GetSlot(Position.x - 1, Position.y) && !GlobalControl.Main.GetSlot(Position.x - 1, Position.y).GetCharacter())
                return GlobalControl.Main.GetSlot(Position.x - 1, Position.y);
            else if (GlobalControl.Main.GetSlot(Position.x + 1, Position.y) && GlobalControl.Main.GetSlot(Position.x + 1, Position.y).GetCharacter()
                && !GlobalControl.Main.GetSlot(Position.x + 1, Position.y).GetCharacter().GetPair())
                return GlobalControl.Main.GetSlot(Position.x + 1, Position.y);
            else if (GlobalControl.Main.GetSlot(Position.x - 1, Position.y) && GlobalControl.Main.GetSlot(Position.x - 1, Position.y).GetCharacter()
                && !GlobalControl.Main.GetSlot(Position.x - 1, Position.y).GetCharacter().GetPair())
                return GlobalControl.Main.GetSlot(Position.x - 1, Position.y);
            return null;
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void OnMouseEnter()
        {
            if (GlobalControl.Main.GetBoardActive())
                GlobalControl.Main.SelectingSlot = this;
        }

        public void OnMouseExit()
        {
            if (GlobalControl.Main.SelectingSlot == this)
                GlobalControl.Main.SelectingSlot = null;
        }
    }
}