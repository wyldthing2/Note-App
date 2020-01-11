using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//The program is slow because it reads the whole procedure list everytime, maybe sorting it would let us make some assumptions and ony search from the first in each group?

public class MainScript : MonoBehaviour
{

    [SerializeField] TMP_Dropdown PatientDropdown;
    [SerializeField] public TMP_Text PatientDropdownText;
    [SerializeField] TMP_Dropdown NoteDropdown;
    [SerializeField] public TMP_Text NoteDropdownText;

    [SerializeField] int selectedPatient;
    [SerializeField] int selectedProcedure;

    [SerializeField] public string DateToEnter;
    [SerializeField] public string ProcedureToEnter;

    [Space]

    [SerializeField] GameObject PanelAddingPatient;
    [SerializeField] TMP_InputField AddPatientField;
    
    [Space]

    [SerializeField] GameObject PanelAddingNote;
    [SerializeField] TMP_InputField AddNoteField;
    



    public List<Patient> PatientList = new List<Patient>();

    public class Patient
    {
        public int id;
        public string PatientIdentifier;
        public List<string> DateOfProcedure = new List<string>();
        public List<string> ProcedureName = new List<string>();
        
    }
    public class Procedure
    {
        public bool PRP;
        public bool AUoF;
        public bool Fly;

        //FUTURE FEATURE the latest radiographs already entered are under the patient class
        //pano, bw, per and their dates
        public List<string> Radiographs;

        public List<RadiographicFindings> RadiographicFindingsList = new List<RadiographicFindings>();

        public class RadiographicFindings
        {
            public bool AssociatedWithATooth;
            public int ToothNumber;
            //radiolucency
            //M or D, apical, L/ F attached gingiva of ,right and left posterior mandible
            //INTERPROXIMAL or facial to multiple teeth
            public string Location;
            public string Size;
            //radiolucency, opcity, mixed, horizontal 
            public string Description;
            public int DiagnosisID;
        }

        public List<ClinicalFindings> ClinicalFindingsList = new List<ClinicalFindings>();

        public class ClinicalFindings
        {
            public bool AssociatedWithATooth;
            public int ToothNumber;
            //radiolucency
            //M or D, apical, L/ F attached gingiva of ,right and left posterior mandible
            //INTERPROXIMAL or facial to multiple teeth
            public string Location;
            public string Size;
            //radiolucency, opcity, mixed, horizontal 
            public string Description;
            public int DiagnosisID;
        }

    }

    //public void EditPatient()


    public void UpdatePatientListDisplay()
    {
        PatientDropdown.ClearOptions();
        List<string> tempPatientList = new List<string>();

        foreach (Patient patient in PatientList)
        {
            tempPatientList.Add(patient.PatientIdentifier);

        }
        PatientDropdown.AddOptions(tempPatientList);

    }

    public void SetSelectedPatient()
    {
        //foreach 
        //Debug.Log(PatientDropdownText.text);
        for (int i =0; i < PatientList.Count; i++)
        {
            if (PatientList[i].PatientIdentifier == PatientDropdownText.text)
            {
                selectedPatient = i;
                UpdateProcedureListDisplay();
            }
        }
    }

    public void UpdateProcedureListDisplay()
    {
        NoteDropdown.ClearOptions();
        List<string> tempNoteList = new List<string>();


        for (int i = 0; i < PatientList[selectedPatient].DateOfProcedure.Count; i++ )
        {
            string NewStringForList = PatientList[selectedPatient].DateOfProcedure[i] + " - " + PatientList[selectedPatient].ProcedureName[i];
            /*
            for (int x = 0; x < tempNoteList.Count; x++ )
            {
                if (tempNoteList[x] == NewStringForList)
                {

                    for (int y = 2; y < 10; y++)
                    {
                        bool matchFound = false;
                        for (int z = 0; z < tempNoteList.Count; z++)
                        {
                            if (tempNoteList[z] == NewStringForList + y)
                            {
                                matchFound = true;
                                break;
                            }
                        }
                        if (!matchFound)
                        {
                            NewStringForList = NewStringForList + y;
                            break;
                        }
                        
                        
                    }
                    
                }
            }
            */
            tempNoteList.Add(NewStringForList);
        }

        NoteDropdown.AddOptions(tempNoteList);
    }


    //This doesn't seem like I finished it
    public void SetSelectedProcedure()
    {
        for (int i = 0; i < PatientList[selectedPatient].DateOfProcedure.Count; i++)
        {
            string procedureAndDateName = PatientList[selectedPatient].DateOfProcedure + " - " + PatientList[selectedPatient].ProcedureName;
            if (PatientList[i].PatientIdentifier == NoteDropdownText.text)
            {
                selectedPatient = i;
            }
        }
    }

    bool PatientPanelToggle = true;

    public void ButtonToAddPatient()
    {
        PanelAddingPatient.SetActive(PatientPanelToggle);
        PatientPanelToggle = !PatientPanelToggle;
    }

    public void ButtonToSubmitPatient()
    {

        AddPatient(AddPatientField.text);
        AddPatientField.text = "";
        PanelAddingPatient.SetActive(false);
        PatientPanelToggle = true;

    }

    public void AddPatient(string identifier)
    {
        if (identifier != null)
        {
            Patient patient = new Patient();
            patient.PatientIdentifier = identifier;
            patient.id = PatientList.Count;
            PatientList.Add(patient);
            UpdatePatientListDisplay();
            //Sort by recent, sort by alphabet
        }
        //Sort by recent, sort by alphabet
    }

    bool procedurePanelToggle = true;

    public void ButtonToAddProcedure()
    {
        PanelAddingNote.SetActive(procedurePanelToggle);
        procedurePanelToggle = !procedurePanelToggle;
    }

    public void ButtonToSubmitProcedure()
    {

        AddProcedure(AddNoteField.text);
        AddNoteField.text = "";
        PanelAddingNote.SetActive(false);
        procedurePanelToggle = true;

    }

    public void AddProcedure(string dateEntered)
    {

        bool duplicateFound = false;

        for (int i = 0; i < PatientList[selectedPatient].DateOfProcedure.Count; i++)
        {
            if (PatientList[selectedPatient].DateOfProcedure[i] == dateEntered)
            {
                duplicateFound = true;
                break;
            }
        }

        string generatedProcedureName = "Empty";

        if (duplicateFound)
        {
            for (int y = 2; y < 10; y++)
            {
                bool matchFound = false;
                for (int z = 0; z < PatientList[selectedPatient].ProcedureName.Count; z++)
                {
                    if (PatientList[selectedPatient].ProcedureName[z] == generatedProcedureName + y)
                    {
                        matchFound = true;
                        break;
                    }
                }
                if (!matchFound)
                {
                    generatedProcedureName = generatedProcedureName + y;
                    break;
                }


            }
        }
        PatientList[selectedPatient].DateOfProcedure.Add(dateEntered);
        PatientList[selectedPatient].ProcedureName.Add(generatedProcedureName);
        UpdateProcedureListDisplay();
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
         

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
