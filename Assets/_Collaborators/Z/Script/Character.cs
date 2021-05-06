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
        [HideInInspector] public KeyBase KB;
        [Space]
        //public List<Event> CharacterEvents;
        //public Event RepeatEvent;
        public List<Event> AddRepeatEvents;
        public int EventCoolRate;
        public int EventCoolDown;
        [HideInInspector] public int SacrificeToken;
        public int StartTime = -1;
        public int ReturnTime = -1;
        public bool Active;
        [Space]
        public List<Skill> Skills;
        public Skill PendingSkill;
        public Skill CurrentSkill;
        [Space]
        public TextMeshPro VitalityText;
        public TextMeshPro VitalityTextII;
        public TextMeshPro PassionText;
        public TextMeshPro PassionTextII;
        public TextMeshPro ReasonText;
        public TextMeshPro ReasonTextII;
        public GameObject VCI_Up;
        public GameObject VCI_Down;
        public GameObject PCI_Up;
        public GameObject PCI_Down;
        public GameObject RCI_Up;
        public GameObject RCI_Down;
        public GameObject VitalityLimit;
        public GameObject PassionLimit;
        public GameObject ReasonLimit;
        public GameObject Outline;
        public GameObject Mask;
        [Space]
        public GameObject Tooltip_Vitality;
        public GameObject Tooltip_Passion;
        public GameObject Tooltip_Reason;
        public GameObject ToolPivot_Vitality;
        public GameObject ToolPivot_Passion;
        public GameObject ToolPivot_Reason;
        public Vector2 TooltipDelay;
        public SkillIndicator SI;
        public Vector2 InfoRangeX;
        public Vector2 InfoRangeY;
        public bool StatusRendereActive;
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
        public string DefaultLayer;
        public string UpLayer;
        public List<SpriteRenderer> MaskedRederer;
        public List<TextMeshPro> MaskedText;
        public float CurrentMaskDelay;
        [Space]
        [TextArea]
        public string IntroText;
        [TextArea]
        public string InfoText;

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
            TooltipUpdate();
            if (CurrentMaskDelay >= 0)
            {
                CurrentMaskDelay -= Time.deltaTime;
                if (CurrentMaskDelay <= 0)
                    DisableMask();
            }
        }

        public void FixedUpdate()
        {
            PositionUpdate();
        }

        public void Render()
        {
            Outline.SetActive(GlobalControl.Main.GetSelectingCharacter() == this || GlobalControl.Main.HoldingCharacter == this
                || GlobalControl.Main.NewCharacters.Contains(this));

            if (GetPartner() && GlobalControl.Main.GetBoardActive() && !CurrentSkill && !GetPartner().CurrentSkill)
            {
                Character C = GetPartner();
                if (C.GetHidden_Vitality() || GetHidden_Vitality() || Mathf.Abs(C.GetVitality() - GetVitality()) <= 1)
                {
                    VCI_Up.SetActive(false);
                    VCI_Down.SetActive(false);
                }
                else if (C.GetVitality() > GetVitality())
                {
                    VCI_Up.SetActive(true);
                    VCI_Down.SetActive(false);
                }
                else
                {
                    VCI_Up.SetActive(false);
                    VCI_Down.SetActive(true);
                }

                if (C.GetHidden_Passion() || GetHidden_Passion() || Mathf.Abs(C.GetPassion() - GetPassion()) <= 1)
                {
                    PCI_Up.SetActive(false);
                    PCI_Down.SetActive(false);
                }
                else if (C.GetPassion() > GetPassion())
                {
                    PCI_Up.SetActive(true);
                    PCI_Down.SetActive(false);
                }
                else
                {
                    PCI_Up.SetActive(false);
                    PCI_Down.SetActive(true);
                }

                if (C.GetHidden_Reason() || GetHidden_Reason() || Mathf.Abs(C.GetReason() - GetReason()) <= 1)
                {
                    RCI_Up.SetActive(false);
                    RCI_Down.SetActive(false);
                }
                else if (C.GetReason() > GetReason())
                {
                    RCI_Up.SetActive(true);
                    RCI_Down.SetActive(false);
                }
                else
                {
                    RCI_Up.SetActive(false);
                    RCI_Down.SetActive(true);
                }
            }
            else
            {
                VCI_Up.SetActive(false);
                VCI_Down.SetActive(false);
                PCI_Up.SetActive(false);
                PCI_Down.SetActive(false);
                RCI_Up.SetActive(false);
                RCI_Down.SetActive(false);
            }

            if (!GetHidden_Vitality())
            {
                //VitalityText.text = (int)(GetVitality() / 10) + "";
                //VitalityTextII.text = (int)(GetVitality() % 10) + "";
                VitalityText.text = GetVitality() + "";
                if (GetVitality() > GlobalControl.Main.GetVitalityLimit())
                    VitalityLimit.SetActive(true);
                else
                    VitalityLimit.SetActive(false);
            }
            else
            {
                //VitalityText.text = "?";
                //VitalityTextII.text = "?";
                VitalityText.text = "?";
                VitalityLimit.SetActive(true);
            }

            if (!GetHidden_Passion())
            {
                //PassionText.text = (int)(GetPassion() / 10) + "";
                //PassionTextII.text = (int)(GetPassion() % 10) + "";
                PassionText.text = GetPassion() + "";
                if (GetPassion() > GlobalControl.Main.GetPassionLimit())
                    PassionLimit.SetActive(true);
                else
                    PassionLimit.SetActive(false);
            }
            else
            {
                //PassionText.text = "?";
                //PassionTextII.text = "?";
                PassionText.text = "?";
                PassionLimit.SetActive(true);
            }

            if (!GetHidden_Reason())
            {
                //ReasonText.text = (int)(GetReason() / 10) + "";
                //ReasonTextII.text = (int)(GetReason() % 10) + "";
                ReasonText.text = GetReason() + "";
                if (GetReason() > GlobalControl.Main.GetReasonLimit())
                    ReasonLimit.SetActive(true);
                else
                    ReasonLimit.SetActive(false);
            }
            else
            {
                //ReasonText.text = "?";
                //ReasonTextII.text = "?";
                ReasonText.text = "?";
                ReasonLimit.SetActive(true);
            }
        }

        public void TooltipUpdate()
        {
            bool Base = GlobalControl.Main.GetSelectingCharacter() == this && !GlobalControl.Main.HoldingCharacter;
            Vector2 cp = Cursor.Main.GetPosition();
            if (!Base)
                TooltipDelay.y = 0;
            if ((cp - (Vector2)ToolPivot_Vitality.transform.position).magnitude <= 1.5f)
                TooltipDelay = new Vector2(0, 0.7f);
            else if ((cp - (Vector2)ToolPivot_Passion.transform.position).magnitude <= 1.5f)
                TooltipDelay = new Vector2(1, 0.7f);
            else if ((cp - (Vector2)ToolPivot_Reason.transform.position).magnitude <= 1.5f)
                TooltipDelay = new Vector2(2, 0.7f);
            TooltipDelay.y -= Time.deltaTime;
            Tooltip_Vitality.SetActive(TooltipDelay.x == 0 && TooltipDelay.y > 0);
            Tooltip_Passion.SetActive(TooltipDelay.x == 1 && TooltipDelay.y > 0);
            Tooltip_Reason.SetActive(TooltipDelay.x == 2 && TooltipDelay.y > 0);

            if (/*CurrentSkill && */HoveringOnSkill() && SI.GetTarget())
            {
                if (!StatusRendereActive)
                    StatusRenderer.Main.Render(SI.GetTarget(), this);
                StatusRendereActive = true;
                if (Input.GetMouseButtonDown(0) && GlobalControl.Main.BoardActive && PendingSkill)
                {
                    if (!CurrentSkill)
                        ActivateSkill(PendingSkill);
                    else
                        OnSkillDisabled();
                }
            }
            else if (cp.x >= InfoRangeX.x + transform.position.x && cp.x <= InfoRangeX.y + transform.position.x
                && cp.y >= InfoRangeY.x + transform.position.y && cp.y <= InfoRangeY.y + transform.position.y && !GlobalControl.Main.HoldingCharacter)
            {
                if (!StatusRendereActive)
                    StatusRenderer.Main.Render(null, this);
                StatusRendereActive = true;
            }
            else
            {
                if (StatusRendereActive)
                    StatusRenderer.Main.Disable();
                StatusRendereActive = false;
            }
        }

        public bool HoveringOnSkill()
        {
            Vector2 cp = Cursor.Main.GetPosition();
            return (cp - (Vector2)SI.transform.position).magnitude <= 1.5f && !GlobalControl.Main.HoldingCharacter;
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
            if (!Active || (ReturnTime > 0 && ReturnTime < GlobalControl.Main.CurrentTime))
                return null;
            /*if (EventCoolDown > 0)
                return null;*/
            if (AddRepeatEvents.Count <= 0)
                return null;
            Event TE = null;
            int Priority = -1;
            foreach (Event E in AddRepeatEvents)
            {
                if (E.Pass(GetPair()) && E.GetPriority(GetPair()) > Priority)
                {
                    TE = E;
                    Priority = E.GetPriority(GetPair());
                    //print("Event " + E.gameObject.name + "; Character " + E.GetSource() + "; Priority " + Priority);
                }
            }
            return TE;
        }

        public void OnTriggerEvent(Event E)
        {
            EventCoolDown = EventCoolRate;
            GetKeyBase().ChangeKey("SacrificeEvent", 1);
        }

        public void AdvanceSequence(Event E)
        {
            /*if (CharacterEvents.Contains(E))
                CharacterEvents.Remove(E);*/
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

        public void StartOfTurn()
        {
            if (!Active)
                return;
            EventCoolDown--;
            if (PendingSkill)
                ResetPendingSkill();
            if (CurrentSkill)
                OnSkillDisabled();
            foreach (Skill S in Skills)
                S.StartOfTurn();
            List<Skill> New = new List<Skill>();
            foreach (Skill S in Skills)
            {
                if (S.CanTrigger(this))
                    New.Add(S);
            }
            if (New.Count > 0)
                FakeActivateSkill(New[Random.Range(0, New.Count)]);
            GetKeyBase().SetKey("Return", 0);
        }

        public void FakeActivateSkill(Skill S)
        {
            PendingSkill = S;
        }

        public void ResetPendingSkill()
        {
            PendingSkill = null;
        }

        public void ActivateSkill(Skill S)
        {
            CurrentSkill = S;
            //SkillIndicator.SetActive(true);
        }

        public void OnSkillTriggered()
        {
            if (!CurrentSkill)
                return;
            CurrentSkill = null;
            //SkillIndicator.SetActive(false);
        }

        public void OnSkillDisabled()
        {
            if (!CurrentSkill)
                return;
            CurrentSkill = null;
            //SkillIndicator.SetActive(false);
        }

        public void EndOfTurn()
        {
            if (!Active)
            {
                if (ReturnTime >= 0 && ReturnTime <= GlobalControl.Main.CurrentTime)
                {
                    GlobalControl.Main.ActivateCharacter(this, false);
                    GetKeyBase().SetKey("Return", 1);
                }
                return;
            }
            if (PendingSkill)
                ResetPendingSkill();
            if (CurrentSkill)
            {
                CurrentSkill.EmptyEffect(this);
                OnSkillDisabled();
            }
            if (GlobalControl.Main.GetSacrificeActive())
                GetKeyBase().SetKey("SacrificeEvent", 0);
        }

        public void ActivateMask()
        {
            Mask.SetActive(true);
            foreach (SpriteRenderer SR in MaskedRederer)
                SR.sortingLayerID = SortingLayer.NameToID(UpLayer);
            foreach (TextMeshPro Text in MaskedText)
                Text.sortingLayerID = SortingLayer.NameToID(UpLayer);
            GlobalControl.Main.MaskedCharacters.Add(this);
            CurrentMaskDelay = -1;
        }

        public void DisableMask()
        {
            Mask.SetActive(false);
            foreach (SpriteRenderer SR in MaskedRederer)
                SR.sortingLayerID = SortingLayer.NameToID(DefaultLayer);
            foreach (TextMeshPro Text in MaskedText)
                Text.sortingLayerID = SortingLayer.NameToID(DefaultLayer);
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

        public void MissingDelay(int Delay)
        {
            Active = false;
            ReturnTime = GlobalControl.Main.CurrentTime + Delay;
            if (GetPair())
                GlobalControl.Main.RemovePair(GetPair());
            Anim.SetTrigger("Death");
            if (CurrentSlot)
                CurrentSlot.Empty();
            CurrentSlot = null;
            StartCoroutine("MissingDelayIE");
        }

        public IEnumerator MissingDelayIE()
        {
            yield return new WaitForSeconds(3f);
            transform.position = new Vector3(300, 300);
        }

        public void Remove()
        {
            if (GetPair())
                GlobalControl.Main.RemovePair(GetPair());
            Anim.SetTrigger("Death");
            GlobalControl.Main.Characters.Remove(this);
            Destroy(gameObject, 3);
        }

        public string GetIntro()
        {
            return IntroText;
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
            GlobalControl.Main.RegisterStatChange(this, new Vector3(Value, 0, 0));
        }

        public void ActualChangeVitality(float Value)
        {
            if (Value == 0)
                return;
            float a = Info.Vitality + Value;
            if (a < 1)
                a = 1;
            if (Value > 0)
                Anim.SetTrigger("VitalityChange");
            else
                Anim.SetTrigger("VitalityChangeII");
            SetVitality(a);
        }

        public void SetVitality(float Value)
        {
            Info.SetVitality(Value);
        }

        public void ChangePassion(float Value)
        {
            GlobalControl.Main.RegisterStatChange(this, new Vector3(0, Value, 0));
        }

        public void ActualChangePassion(float Value)
        {
            if (Value == 0)
                return;
            float a = Info.Passion + Value;
            if (a < 1)
                a = 1;
            if (Value > 0)
                Anim.SetTrigger("PassionChange");
            else
                Anim.SetTrigger("PassionChangeII");
            SetPassion(a);
        }

        public void SetPassion(float Value)
        {
            Info.SetPassion(Value);
        }

        public void ChangeReason(float Value)
        {
            GlobalControl.Main.RegisterStatChange(this, new Vector3(0, 0, Value));
        }

        public void ActualChangeReason(float Value)
        {
            if (Value == 0)
                return;
            float a = Info.Reason + Value;
            if (a < 1)
                a = 1;
            if (Value > 0)
                Anim.SetTrigger("ReasonChange");
            else
                Anim.SetTrigger("ReasonChangeII");
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

        public KeyBase GetKeyBase()
        {
            if (!KB)
                KB = GetComponent<KeyBase>();
            return KB;
        }
    }
}