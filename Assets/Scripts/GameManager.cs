using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mouseGhost;
    public GameObject SelectedTower { get; private set; }
    private SpriteRenderer mouseGhostSr;
    [SerializeField] private LayerMask layerBlockBuild;

    void Start()
    {
        mouseGhostSr = mouseGhost.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (SelectedTower is null)
        {
            mouseGhostSr.sprite = null;
        }
        else
        {
            mouseGhostSr.sprite = SelectedTower.GetComponent<SpriteRenderer>().sprite;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        mouseGhost.transform.position = mousePos;

        if (CanBuild())
        {
            // do something
        }
    }

    public void SetTower(GameObject tower)
    {
        SelectedTower = tower;
    }

    public bool CanBuild()
    {
        if (Physics2D.OverlapBox(mouseGhost.transform.position, new Vector2(0.9f, 0.9f), 0, layerBlockBuild) != null)
        {
            mouseGhostSr.color = Color.red;
            return false;
        }
        else
        {
            mouseGhostSr.color = Color.white;
            return true;
        }
    }
}
