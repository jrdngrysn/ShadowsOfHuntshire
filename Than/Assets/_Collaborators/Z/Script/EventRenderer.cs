using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class EventRenderer : MonoBehaviour {
        public bool Active;
        public bool Deciding;
        public bool Activating;
        [Space]
        public int Index;
        public Animator Anim;
        public TextMeshPro ContentText;
        public TextMeshPro AddContentText;
        public TextMeshPro NameText;
        public TextMeshPro QuestionText;
        public ChoiceRenderer CRI;
        public ChoiceRenderer CRII;
        public ChoiceRenderer CRIII;
        public List<float> AttachPositions;
        [Space]
        public Event CurrentEvent;
        public Event CurrentAddEvent;
        public Pair CurrentPair;

        // Start is called before the first frame update
        void Start()
        {
            if (Active)
                Activate(CurrentEvent, CurrentPair);
        }

        // Update is called once per frame
        void Update()
        {
            Render();
        }

        public void Render()
        {
            if (!GetEvent())
            {
                ContentText.text = "";
                AddContentText.text = "";
                NameText.text = "";
                QuestionText.text = "";
                CRI.Render(null);
                CRII.Render(null);
                CRIII.Render(null);
                return;
            }
            ContentText.text = GetEvent().GetContent();
            if (GetAddEvent())
                AddContentText.text = GetAddEvent().GetContent();
            else
                AddContentText.text = "";
            if (!GetEvent().GetSource() || !GetEvent().DisplaySource)
                NameText.text = "";
            else
                NameText.text = GetEvent().GetSource().GetName();
            //if (GetCurrentEC())
                //QuestionText.text = GetCurrentEC().EffectText;
            //else
                QuestionText.text = GetEvent().AddContent;
            CRI.Render(GetEvent().GetChoices()[0]);
            CRII.Render(GetEvent().GetChoices()[1]);
            if (GetAddEvent())
                CRIII.Render(GetAddEvent().GetChoices()[0]);
            else
                CRIII.Render(null);
        }

        public void Decide(int Index)
        {
            StartCoroutine(DecideBuffer(Index));
        }

        public IEnumerator DecideBuffer(int Index)
        {
            while (Activating)
                yield return 0;
            Effect(Index);
        }

        public void Effect(int index)
        {
            List<Character> t = new List<Character>();
            if (index == 2)
            {
                if (GetAddEvent().FreeSources.Count == 0)
                {
                    if (GetSourcePair())
                    {
                        t.Add(GetSourcePair().GetCharacter(0));
                        t.Add(GetSourcePair().GetCharacter(1));
                    }
                }
                else
                {
                    foreach (string s in GetAddEvent().FreeSources)
                        t.Add(Character.Find(s));
                }
                GetAddEvent().GetChoices()[index - 2].Effect(t, out Event _);
                GlobalControl.Main.ResolveEvent(Index);
                Disable();
            }
            else
            {
                if (GetEvent().FreeSources.Count == 0)
                {
                    if (GetSourcePair())
                    {
                        t.Add(GetSourcePair().GetCharacter(0));
                        t.Add(GetSourcePair().GetCharacter(1));
                    }
                }
                else
                {
                    foreach (string s in GetEvent().FreeSources)
                        t.Add(Character.Find(s));
                }
                EventChoice EC = GetEvent().GetChoices()[index];
                EC.Effect(t, out Event AddEvent);
                if (EC.TriggerSequence && Character.Find(GetEvent().Source))
                    Character.Find(GetEvent().Source).AdvanceSequence(GetEvent());
                if (!AddEvent)
                {
                    GlobalControl.Main.ResolveEvent(Index);
                    Disable();
                }
                else
                    AddActivate(AddEvent);
            }
        }

        public void Activate(Event E, Pair P)
        {
            CurrentEvent = E;
            CurrentAddEvent = null;
            CurrentPair = P;
            StartCoroutine("ActivateIE");
        }

        public IEnumerator ActivateIE()
        {
            Anim.SetBool("Active", true);
            Activating = true;
            yield return new WaitForSeconds(1f);
            CRI.Activate(GetEvent().GetChoices()[0]);
            CRII.Activate(GetEvent().GetChoices()[1]);
            yield return new WaitForSeconds(0.25f);
            Activating = false;
        }

        public void AddActivate(Event E)
        {
            CurrentAddEvent = E;
            StartCoroutine("AddActivateIE");
        }

        public IEnumerator AddActivateIE()
        {
            Anim.SetTrigger("Add");
            Activating = true;
            CRI.Disable();
            CRII.Disable();
            yield return new WaitForSeconds(0.15f);
            CRIII.Activate(GetAddEvent().GetChoice(0));
            yield return new WaitForSeconds(0.25f);
            Activating = false;
        }

        public void Disable()
        {
            Anim.SetBool("Active", false);
            CRI.Disable();
            CRII.Disable();
            CRIII.Disable();
        }

        public Event GetEvent()
        {
            return CurrentEvent;
        }

        public Event GetAddEvent()
        {
            return CurrentAddEvent;
        }

        public Pair GetSourcePair()
        {
            return CurrentPair;
        }

        public EventChoice GetCurrentEC()
        {
            if (CRI.MouseOnDelay > 0)
                return CRI.CurrentEC;
            if (CRII.MouseOnDelay > 0)
                return CRII.CurrentEC;
            if (CRIII.MouseOnDelay > 0)
                return CRIII.CurrentEC;
            return null;
        }
    }
}