using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMover : MonoBehaviour
{

    //executes the move down on its children
    //Fills the premade element and assigns a new element as its child (makes a chain that can be removed from)
    //how do I make a button report itself?

    // O registers ID and knows its ID
    // O Can move the whole list down
    // O can activate a the premade element and instantiate one inactive
    // O the new elements have the right followers

    [SerializeField] public RectTransform RectTransformOfFollower;
    [SerializeField] public DownMover DownMoverOfFollower;
    [SerializeField] public bool HasFollower = false;

    [SerializeField] public RectTransform RectTransformOfLeader;
    [SerializeField] public DownMover DownMoverOfLeader;
    [SerializeField] public bool HasLeader = false;


    [SerializeField] public RectTransform RectTransformOfPremadeElementToActivate;
    [SerializeField] public DownMover DownMoverOfHiddenPrefab;
    [SerializeField] public bool HasHiddenPrefab = false;
    [SerializeField] public GameObject PremadeElementToActivate;
    [SerializeField] public DownMover DownMoverOfPremadeElementToActivate;
    [SerializeField] public GameObject PrefabToGenerate;

    [SerializeField] public GameObject ThisPlaceholderTemplate;
    [SerializeField] public RectTransform ThisPlaceholderTemplateRectTransform;

    [SerializeField] public int ThisPlaceholderID;
    [SerializeField] public RectTransform ScrollViewRectTransform;

    public MainScript mainScript;

    public void MoveFollowersDown(int placeholderID)
    {

        if (HasHiddenPrefab)
        {
            //RectTransformOfPremadeElementToActivate.position = new Vector3(RectTransformOfPremadeElementToActivate.position.x, RectTransformOfPremadeElementToActivate.position.y - mainScript.PlaceholderTemplateRectTransforms[placeholderID].rect.height, 5);
        }

        if (HasFollower)
        {
            RectTransformOfFollower.position = new Vector3(RectTransformOfFollower.position.x, RectTransformOfFollower.position.y - mainScript.PlaceholderTemplateRectTransforms[placeholderID].rect.height , 5);
            DownMoverOfFollower.MoveFollowersDown(placeholderID);
        }
    }


    public void MoveFollowersUp(int placeholderID)
    {
        if (HasHiddenPrefab)
        {
            RectTransformOfPremadeElementToActivate.position = new Vector3(RectTransformOfPremadeElementToActivate.position.x, RectTransformOfPremadeElementToActivate.position.y + mainScript.PlaceholderTemplateRectTransforms[placeholderID].rect.height, 5);
        }

        if (HasFollower)
        {
            RectTransformOfFollower.position = new Vector3(RectTransformOfFollower.position.x, RectTransformOfFollower.position.y + mainScript.PlaceholderTemplateRectTransforms[placeholderID].rect.height, 5);
            DownMoverOfFollower.MoveFollowersUp(placeholderID);
        }
    }

    [SerializeField] public List<GameObject> PrefabsList = new List<GameObject>();

    public void ActivateSpecificPrefabA()
    {
        GameObject PrefabTemp = PrefabToGenerate;
        PrefabToGenerate = PrefabsList[0];
        ActivatePrefabAndInstantiateHiddenOne();
        PrefabToGenerate = PrefabTemp;
    }

    public void ActivateSpecificPrefabB()
    {
        GameObject PrefabTemp = PrefabToGenerate;
        PrefabToGenerate = PrefabsList[1];
        ActivatePrefabAndInstantiateHiddenOne();
        PrefabToGenerate = PrefabTemp;
    }

    public void ActivateSpecificPrefabC()
    {
        GameObject PrefabTemp = PrefabToGenerate;
        PrefabToGenerate = PrefabsList[2];
        ActivatePrefabAndInstantiateHiddenOne();
        PrefabToGenerate = PrefabTemp;
    }

    public void ActivatePrefabAndInstantiateHiddenOne()
    {
        //PremadeElementToActivate.SetActive(true);

        //Spawn new prefab, give the old prefab's follower to the new one.
        GameObject PrefabHolder = Instantiate(PrefabToGenerate, new Vector3(this.GetComponent<RectTransform>().rect.width * .5f + PrefabToGenerate.GetComponent<RectTransform>().rect.width*.5f, this.GetComponent<RectTransform>().position.y, this.GetComponent<RectTransform>().position.z), this.transform.rotation , this.transform.parent.transform);
        DownMover DownMoverOfPrefabHolder = PrefabHolder.GetComponent<DownMover>();
        DownMoverOfPrefabHolder.mainScript = mainScript;
        DownMoverOfPrefabHolder.ThisPlaceholderTemplate = PrefabHolder.transform.GetChild(0).gameObject;
        DownMoverOfPrefabHolder.ThisPlaceholderTemplateRectTransform = DownMoverOfPrefabHolder.ThisPlaceholderTemplate.GetComponent<RectTransform>();
        DownMoverOfPrefabHolder.RegisterTemplate();
        DownMoverOfPrefabHolder.RectTransformOfFollower = this.GetComponent<RectTransform>();
        DownMoverOfPrefabHolder.DownMoverOfFollower = this.GetComponent<DownMover>();
        DownMoverOfPrefabHolder.HasFollower = true;

        DownMoverOfPrefabHolder.DownMoverOfLeader = DownMoverOfLeader;
        DownMoverOfPrefabHolder.RectTransformOfLeader = RectTransformOfLeader;

        
        DownMoverOfLeader.DownMoverOfFollower = DownMoverOfPrefabHolder;
        DownMoverOfLeader.RectTransformOfFollower = PrefabHolder.GetComponent<RectTransform>();
        DownMoverOfLeader = DownMoverOfPrefabHolder;
        RectTransformOfLeader = PrefabHolder.GetComponent<RectTransform>();
        DownMoverOfLeader.HasLeader = true;
        ScrollViewRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ScrollViewRectTransform.rect.height + DownMoverOfLeader.ThisPlaceholderTemplateRectTransform.rect.height);
        DownMoverOfLeader.MoveFollowersDown(DownMoverOfLeader.ThisPlaceholderID);
        Debug.Log(ThisPlaceholderID);


        //PremadeElementToActivate = PrefabHolder;
        //DownMoverOfPremadeElementToActivate = DownMoverOfPrefabHolder;
        //RectTransformOfPremadeElementToActivate = PremadeElementToActivate.GetComponent<RectTransform>();

        //RectTransformOfLeader.position = new Vector3(RectTransformOfLeader.position.x, this.GetComponent<RectTransform>().position.y, 0);
        //DownMoverOfPremadeElementToActivate.HasLeader = true;
        //DownMoverOfLeader.DownMoverOfLeader.MoveFollowersDown(ThisPlaceholderID);
        //PremadeElementToActivate.SetActive(false);

    }

    public void RegisterTemplate()
    {
        mainScript.PlaceholderTemplates.Add(ThisPlaceholderTemplate);
        mainScript.RegisterPlaceholderTemplateToAllLists();
        ThisPlaceholderTemplateRectTransform = mainScript.PlaceholderTemplateRectTransforms[ThisPlaceholderID];
    }

    public static List<GameObject> ElementsThatCanBeDeleted = new List<GameObject>();

    public int ElementThatCanBeDeletedID;





    public void ConfirmDeleteElement ()
    {

    }

    public void DeleteElement()
    {

        MoveFollowersUp(ThisPlaceholderID);

        //leader downmove + rect
        DownMoverOfLeader.DownMoverOfFollower = DownMoverOfFollower;
        DownMoverOfLeader.RectTransformOfFollower = RectTransformOfFollower;
        

        //follower downmove, rect, + gameobject
        DownMoverOfFollower.DownMoverOfLeader = DownMoverOfLeader;
        DownMoverOfFollower.RectTransformOfLeader = RectTransformOfLeader;

        Destroy(this.gameObject);


    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    private void Awake()
    {
        if(ThisPlaceholderTemplate != null)
        {
            mainScript.PlaceholderTemplates.Add(ThisPlaceholderTemplate);
            mainScript.RegisterPlaceholderTemplateToAllLists();
            ThisPlaceholderTemplateRectTransform = mainScript.PlaceholderTemplateRectTransforms[ThisPlaceholderID];

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
