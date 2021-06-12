using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    private Weapon weapon;
    private PlayerMovement playerMove;

    private void Start()
    {
        weapon = GameObject.FindWithTag("Weapon").GetComponent<Weapon>();
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        SetGameActive(false);
    }

    public void SetGameActive(bool value)
    {
        weapon.active = value;
        playerMove.active = value;
    }
}
