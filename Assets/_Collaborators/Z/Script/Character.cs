using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class Character : MonoBehaviour {
        public Animator Anim;
        public CharacterInfo Info;
        public string Name;
        [Space]
        public List<Event> CharacterEvents;
        //public List<Event> FreeEvents;
        public Event RepeatEvent;
        public int EventCoolRate;
        public int EventCoolDown;
        public int StartTime = -1;
        public bool Active;
        [Space]
        public TextMeshPro VitalityText;
        public TextMeshPro PassionText;
        public TextMeshPro ReasonText;
        public GameObject VitalityLimit;
        public GameObject PassionLimit;
        public GameObject ReasonLimit;
        public GameObject Outline;
        public GameObject Mask;
        [Space]
        public Slot CurrentSlot;
        public Pair CurrentPair;
        public Vector2 OriPosition;
        public Vector2 TargetPosition;
        public AnimationCurve PositionCurve;
        public float PositionDelay;
        public bool Highlighted;
        public AnimationCurve HighlightedCurve;
        public float HighlightedDelay;
        public float CurrentPositionTime;
        public float OriZ;

        public void Awake()
        {
            GlobalControl.Main.Characters.Add(this);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Render();
            CurrentPositionTime -= Time.deltaTime;
            PositionUpdate();
        }

        public void FixedUpdate()
        {
            PositionUpdate();
        }

        public void Render()
        {
            Outline.SetActive(GlobalControl.Main.GetSelectingCharacter() == this || GlobalControl.Main.HoldingCharacter == this);
            if (!GetHidden_Vitality())
            {
                VitalityText.text = GetVitality() + "";
                if (GetVitality() > GlobalControl.Main.GetVitalityLimit())
                    VitalityLimit.SetActive(true);
                else
                    VitalityLimit.SetActive(false);
            }
            else
            {
                VitalityText.text = "??";
                VitalityLimit.SetActive(false);
            }

            if (!GetHidden_Passion())
            {
                PassionText.text = GetPassion() + "";
                if (GetPassion() > GlobalControl.Main.GetPassionLimit())
                    PassionLimit.SetActive(true);
                else
                    PassionLimit.SetActive(false);
            }
            else
            {
                PassionText.text = "??";
                PassionLimit.SetActive(false);
            }

            if (!GetHidden_Reason())
            {
                ReasonText.text = GetReason() + "";
                if (GetReason() > GlobalControl.Main.GetReasonLimit())
                    ReasonLimit.SetActive(true);
                else
                    ReasonLimit.SetActive(false);
            }
            else
            {
                ReasonText.text = "??";
                ReasonLimit.SetActive(false);
            }
        }

        public void Activate()
        {
            Active = true;
            Anim.SetTrigger("Activate");
        }

        public bool TryBark()
        {
            if (!GetComponent<Bark>() || !GetComponent<Bark>().CanBark())
                return false;
            GetComponent<Bark>().Effect();
            return true;
        }

        public Event GetEvent()
        {
            if (EventCoolDown > 0)
                return null;
            if (CharacterEvents.Count <= 0 && !RepeatEvent)
                return null;
            Event TE;
            if (CharacterEvents.Count <= 0)
                TE = RepeatEvent;
            else
                TE = CharacterEvents[0];
            if (!TE.Pass(GetPair()))
                return null;
            return TE;
        }

        public void OnTriggerEvent(Event E)
        {
            EventCoolDown = EventCoolRate;
        }

        public void AdvanceSequence(Event E)
        {
            if (CharacterEvents.Contains(E))
                CharacterEvents.Remove(E);
        }

        public void BoundValueChange(float V, float P, float R)
        {
            if (V > GetVitality() + 1)
            {
                ChangeVitality(1);
            }
            else if (V < GetVitality() - 1)
            {
                ChangeVitality(-1);
            }
            else if (GetHidden_Vitality())
            {
                SetHidden_Vitality(false);
            }

            if (P > GetPassion() + 1)
            {
                ChangePassion(1);
            }
            else if (P < GetPassion() - 1)
            {
                ChangePassion(-1);
            }
            else if (GetHidden_Passion())
            {
                SetHidden_Passion(false);
            }

            if (R > GetReason() + 1)
            {
                ChangeReason(1);
            }
            else if (R < GetReason() - 1)
            {
                ChangeReason(-1);
            }
            else if (GetHidden_Reason())
            {
                SetHidden_Reason(false);
            }
        }

        public void IniStat(float V, float P, float R)
        {
            SetVitality(V);
            SetPassion(P);
            SetReason(R);
        }

        public Pair GetPair()
        {
            foreach (Pair P in GlobalControl.Main.Pairs)
            {
                if (this == P.GetCharacter(0) || this == P.GetCharacter(1))
                    return P;
            }
            return null;
        }

        public Character GetPartner()
        {
            if (!GetPair())
                return null;
            Pair P = GetPair();
            if (this == P.GetCharacter(0))
                return P.GetCharacter(1);
            else if (this == P.GetCharacter(1))
                return P.GetCharacter(0);
            return null;
        }

        public void CreatePair(Character C)
        {
            Slot Ori = null;
            Slot Target = CurrentSlot.GetPairSlot();
            while (!Target)
            {
                Ori = GlobalControl.Main.GetNextSlot(Ori);
                Target = Ori.GetPairSlot();
            }
            if (Ori && Ori != CurrentSlot)
                ChangeSlot(Ori);
            if (Target.GetCharacter())
                Target.GetCharacter().ChangeSlot(GlobalControl.Main.GetNextSlot(Target));
            C.PutDown(Target);

            GameObject G = Instantiate(GlobalControl.Main.PairPrefab);
            Pair P = G.GetComponent<Pair>();
            P.Ini(this, C);
            P.SetPosition((CurrentSlot.GetPosition() + C.CurrentSlot.GetPosition()) * 0.5f);
            GlobalControl.Main.AddPair(P);

            /*int b = Random.Range(0, 2);
            if (b == 0)
            {
                if (!TryBark())
                    C.TryBark();
            }
            else
            {
                if (!C.TryBark())
                    TryBark();
            }*/
            C.TryBark();
        }

        public bool CanPair()
        {
            return !GetPair();
        }

        public void ChangeSlot(Slot S)
        {
            if (CurrentSlot)
                CurrentSlot.Empty();
            S.AssignCharacter(this);
        }

        public void AssignSlot(Slot S)
        {
            PositionChange(S.GetPosition());
            CurrentSlot = S;
        }

        public void PickUp()
        {
            if (GetPair())
                GlobalControl.Main.RemovePair(GetPair());
            if (CurrentSlot)
                CurrentSlot.Empty();
            CurrentSlot = null;
            PositionChange(Cursor.Main.GetPosition());
            GlobalControl.Main.HoldingCharacter = this;
        }

        public void PutDown(Slot S)
        {
            GlobalControl.Main.HoldingCharacter = null;
            S.AssignCharacter(this);
        }

        public void PositionChange(Vector2 Target)
        {
            OriPosition = transform.position;
            TargetPosition = Target;
            if (!Highlighted)
                CurrentPositionTime = PositionDelay;
            else
                CurrentPositionTime = HighlightedDelay;
        }

        public void SetPosition(Vector2 Target)
        {
            OriPosition = Target;
            TargetPosition = Target;
            CurrentPositionTime = 0f;
            PositionUpdate();
        }

        public void PositionUpdate()
        {
            if (GlobalControl.Main.HoldingCharacter == this)
            {
                if (CurrentPositionTime <= 0)
                {
                    Vector2 b = Cursor.Main.GetPosition();
                    transform.position = new Vector3(b.x, b.y, OriZ - 4);
                    return;
                }
                else
                {
                    TargetPosition = Cursor.Main.GetPosition();
                }
            }

            float v;
            if (!Highlighted)
                v = 1 - (CurrentPositionTime / PositionDelay);
            else
                v = 1 - (CurrentPositionTime / HighlightedDelay);
            if (v > 1)
                v = 1;
            Vector2 a;
            if (!Highlighted)
                a = OriPosition + (TargetPosition - OriPosition) * PositionCurve.Evaluate(v);
            else
                a = OriPosition + (TargetPosition - OriPosition) * HighlightedCurve.Evaluate(v);
            if (GlobalControl.Main.HoldingCharacter == this)
                transform.position = new Vector3(a.x, a.y, OriZ - 4);
            else if (CurrentSlot == GlobalControl.Main.SacrificeSlot)
                transform.position = new Vector3(a.x, a.y, OriZ - 3);
            else if (Highlighted)
                transform.position = new Vector3(a.x, a.y, OriZ - 1);
            else
                transform.position = new Vector3(a.x, a.y, OriZ);
        }

        public void EndOfTurn()
        {
            EventCoolDown--;
        }

        public void ActivateMask()
        {
            Mask.SetActive(true);
            GlobalControl.Main.MaskedCharacters.Add(this);
        }

        public void DisableMask()
        {
            Mask.SetActive(false);
        }

        public bool CanDie()
        {
            return GetVitality() <= GlobalControl.Main.GetVitalityLimit()
                && GetPassion() <= GlobalControl.Main.GetPassionLimit() && GetReason() <= GlobalControl.Main.GetReasonLimit()
                && !GetHidden_Vitality() && !GetHidden_Passion() && !GetHidden_Reason();
        }

        public void Death()
        {
            Remove();
        }

        public void Killed()
        {
            Remove();
        }

        public void Missing()
        {
            Remove();
        }

        public void Remove()
        {
            if (GetPair())
                GlobalControl.Main.RemovePair(GetPair());
            Anim.SetTrigger("Death");
            GlobalControl.Main.Characters.Remove(this);
            Destroy(gameObject, 3);
        }

        public static Character Find(string Name)
        {
            for (int i = GlobalControl.Main.Characters.Count - 1; i >= 0; i--)
            {
                if (GlobalControl.Main.Characters[i].GetName() == Name)
                    return GlobalControl.Main.Characters[i];
            }
            return null;
        }

        //-------------------------------------------------------------------------------------------------------------

        public string GetName()
        {
            return Name;
        }

        public void ChangeVitality(float Value)
        {
            float a = Info.Vitality + Value;
            if (a < 1)
                a = 1;
            if (a != GetVitality())
                Anim.SetTrigger("VitalityChange");
            SetVitality(a);
        }

        public void SetVitality(float Value)
        {
            Info.SetVitality(Value);
        }

        public void ChangePassion(float Value)
        {
            float a = Info.Passion + Value;
            if (a < 1)
                a = 1;
            if (a != GetPassion())
                Anim.SetTrigger("PassionChange");
            SetPassion(a);
        }

        public void SetPassion(float Value)
        {
            Info.SetPassion(Value);
        }

        public void ChangeReason(float Value)
        {
            float a = Info.Reason + Value;
            if (a < 1)
                a = 1;
            if (a != GetReason())
                Anim.SetTrigger("ReasonChange");
            SetReason(a);
        }

        public void SetReason(float Value)
        {
            Info.SetReason(Value);
        }

        public void SetHidden_Vitality(bool Value)
        {
            if (Value != GetHidden_Vitality())
                Anim.SetTrigger("VitalityChange");
            Info.SetHidden_Vitality(Value);
        }

        public void SetHidden_Passion(bool Value)
        {
            if (Value != GetHidden_Passion())
                Anim.SetTrigger("PassionChange");
            Info.SetHidden_Passion(Value);
        }

        public void SetHidden_Reason(bool Value)
        {
            if (Value != GetHidden_Reason())
                Anim.SetTrigger("ReasonChange");
            Info.SetHidden_Reason(Value);
        }

        public float GetVitality()
        {
            return Info.Vitality;
        }

        public float GetPassion()
        {
            return Info.Passion;
        }

        public float GetReason()
        {
            return Info.Reason;
        }

        public bool GetHidden_Vitality()
        {
            return Info.Hidden_Vitality;
        }

        public bool GetHidden_Passion()
        {
            return Info.Hidden_Passion;
        }

        public bool GetHidden_Reason()
        {
            return Info.Hidden_Reason;
        }
    }
}