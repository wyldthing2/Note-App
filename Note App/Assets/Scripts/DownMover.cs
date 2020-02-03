using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMover : MonoBehaviour
{

    //executes the move down on its children
    //Fills the premade element and assigns a new element as its child (makes a chain that can be removed from)
    //how do I make a button report itself?

    [SerializeField] public List<GameObject> ElementsToMoveDown = new List<GameObject>();
    [SerializeField] public List<RectTransform> ElementToMoveDownRectTransforms = new List<RectTransform>();
    [SerializeField] public List<DownMover> ElementoMoveDownDownMovers = new List<DownMover>();

    [SerializeField] public GameObject PrefabToGenerate;

    [SerializeField] public GameObject ThisPlaceholderTemplate;

    [SerializeField] public int ThisPlaceholderID;

    public MainScript mainScript;

    public void MoveFollowersDown()
    {

    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        mainScript.PlaceholderTemplates.Add(ThisPlaceholderTemplate);
        mainScript.RegisterPlaceholderTemplateToAllLists();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
