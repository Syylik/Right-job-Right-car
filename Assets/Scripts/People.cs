using UnityEngine;
using UnityEngine.EventSystems;

public class People : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float speed;
    public GameManager.Proffesions peopleProffesion;
    private Outline outline;

    [HideInInspector] public Transform target;
    [HideInInspector] public bool moving = false;
    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
    private void Update()
    {
        if(moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!moving && GameManager.instance.gameState == GameManager.GameState.PLAY) outline.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!moving && GameManager.instance.gameState == GameManager.GameState.PLAY) outline.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!moving) GameManager.instance.ClickOnPeople(this);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent<Car>(out Car car))
        {
            if(car.carProffesion == peopleProffesion)
            {
                GameManager.instance.isMoving = false;
                GameManager.instance.isChosing = false;
                Destroy(gameObject);
            }
            else
            {
                GameManager.instance.Lose();
                Destroy(gameObject);
            }
        }
    }
}
