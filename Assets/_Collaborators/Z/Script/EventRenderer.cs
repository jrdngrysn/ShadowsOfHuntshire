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
        public bool PageChanging;
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
        public ChoiceRenderer SingleRenderer;
        public ConfirmButton CB;
        public List<GameObject> EndPageObjects;
        public List<float> AttachPositions;
        [Space]
        public Event CurrentEvent;
        public Event CurrentAddEvent;
        public Pair CurrentPair;
        public ChoiceRenderer CurrentCR;
        public int CurrentPage;

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
            CB.SetRender(CurrentCR && CB.MouseOn);
            if (!GetEvent())
            {
                ContentText.text = "";
                AddContentText.text = "";
                NameText.text = "";
                QuestionText.text = "";
                CRI.Render(null);
                CRII.Render(null);
                CRIII.Render(null);
                foreach (GameObject G in EndPageObjects)
                    G.SetActive(true);
                return;
            }
            if (CurrentPage == GetEvent().GetMaxPage())
            {
                foreach (GameObject G in EndPageObjects)
                    G.SetActive(true);
            }
            else
            {
                foreach (GameObject G in EndPageObjects)
                    G.SetActive(false);
            }
            ContentText.text = ProcessContent(GetEvent().GetContent(CurrentPage), GetEvent().GetSource());
            if (GetAddEvent())
                AddContentText.text = ProcessContent(GetAddEvent().GetContent(CurrentPage), GetEvent().GetSource());
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
            if (GetEvent().GetChoices()[0] && !GetEvent().GetChoices()[1] && SingleRenderer)
            {
                if (CurrentPage == GetEvent().GetMaxPage())
                {
                    SingleRenderer.Render(GetEvent().GetChoices()[0]);
                    CRI.Render(null);
                    CRII.Render(null);
                }
                else
                {
                    SingleRenderer.Render(null);
                    CRI.Render(null);
                    CRII.Render(null);
                }
            }
            else
            {
                if (CurrentPage == GetEvent().GetMaxPage())
                {
                    CRI.Render(GetEvent().GetChoices()[0]);
                    CRII.Render(GetEvent().GetChoices()[1]);
                    if (SingleRenderer)
                        SingleRenderer.Render(null);
                }
                else
                {
                    CRI.Render(null);
                    CRII.Render(null);
                    if (SingleRenderer)
                        SingleRenderer.Render(null);
                }
            }
            if (GetAddEvent())
            {
                if (CurrentPage == GetEvent().GetMaxPage())
                    CRIII.Render(GetAddEvent().GetChoices()[0]);
                else
                    CRIII.Render(null);
            }
            else
            {
                CRIII.Render(null);
            }
        }

        public void Select(int Index)
        {
            if (CurrentCR)
                CurrentCR.OnUnselect();
            if (Index == 0)
            {
                if (GetEvent().GetChoices()[0] && !GetEvent().GetChoices()[1] && SingleRenderer)
                    CurrentCR = SingleRenderer;
                else
                    CurrentCR = CRI;
            }
            else if (Index == 1)
                CurrentCR = CRII;
            else if (Index == 2)
                CurrentCR = CRIII;
            CurrentCR.OnSelect();
        }

        public void TryDecide()
        {
            if (Activating)
                return;
            if (GetEvent() && !CurrentCR && CurrentPage != GetEvent().GetMaxPage() && !PageChanging && !Activating)
            {
                StartCoroutine("PageChange");
                return;
            }
            if (!CurrentCR)
                return;
            Decide(CurrentCR.Index);
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
            GlobalControl.Main.PlaySound("Select");
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
            if (!E)
                return;
            CurrentPage = 0;
            CurrentEvent = E;
            CurrentAddEvent = null;
            CurrentPair = P;
            CurrentCR = null;
            StartCoroutine("ActivateIE");
        }

        public IEnumerator ActivateIE()
        {
            Anim.SetBool("Active", true);
            Activating = true;
            CB.Active = true;
            CB.MouseOn = false;
            yield return new WaitForSeconds(1f);
            ChoiceRendererCheck();
            yield return new WaitForSeconds(0.25f);
            Activating = false;
        }

        public void ChoiceRendererCheck()
        {
            if (CurrentPage == GetEvent().GetMaxPage())
            {
                if (GetEvent().GetChoices()[0] && !GetEvent().GetChoices()[1] && SingleRenderer)
                    SingleRenderer.Activate(GetEvent().GetChoices()[0]);
                else
                {
                    CRI.Activate(GetEvent().GetChoices()[0]);
                    CRII.Activate(GetEvent().GetChoices()[1]);
                }
            }
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
            if (SingleRenderer)
                SingleRenderer.Disable();
            yield return new WaitForSeconds(0.15f);
            CRIII.Activate(GetAddEvent().GetChoice(0));
            yield return new WaitForSeconds(0.25f);
            Activating = false;
        }

        public IEnumerator PageChange()
        {
            PageChanging = true;
            Anim.SetTrigger("PageChange");
            yield return new WaitForSeconds(0.25f);
            CurrentPage++;
            ChoiceRendererCheck();
            yield return new WaitForSeconds(0.25f);
            PageChanging = false;
        }

        public void Disable()
        {
            Anim.SetBool("Active", false);
            CRI.Disable();
            CRII.Disable();
            CRIII.Disable();
            if (SingleRenderer)
                SingleRenderer.Disable();
            CB.Active = false;
        }

        public string ProcessContent(string Ori, Character Source)
        {
            string C = "";
            string S = Ori;
            while (S.IndexOf("*") != -1)
            {
                C += S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (S.IndexOf("*") == -1)
                    break;
                string Key = S.Substring(0, S.IndexOf("*"));
                S = S.Substring(S.IndexOf("*") + 1);
                if (Key == "Someone" && Source.GetPartner())
                    C += Source.GetPartner().GetName();
                else if (Key == "Someone")
                    C += "Someone";
                else if (Key == "The Command You Want To Add")
                    C += "The Displayed Text";
                else if (Key == "ONE")
                    C += Source.GetName();
                else if (Key == "TWO" && Source.GetPartner())
                    C += Source.GetPartner().GetName();
                else
                    C += Key;
            }
            C += S;
            return C;
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
    }
}