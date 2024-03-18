using UnityEngine;

public class SmartSpecialization : MonoBehaviour
{
    private PlayerControls pController;
    [SerializeField] private CharSwitcher cs;
    [SerializeField] GameObject smartSwitch;
    [SerializeField] SmartSwitchScript switchScript;

    public bool pressable = true;

    private void Awake()
    {
        pController = new PlayerControls();
        //pController.Grounded.Interact. += _ => pressable = true;
    }

    private void OnEnable()
    {
        pController.Grounded.Enable();
    }

    private void OnDisable()
    {
        pController.Grounded.Disable();
    }

    private void Start()
    {
        Debug.Log("Initialized");
    }

    private void FixedUpdate()
    {
        if (cs.GetCurrentRat() != 1) return;

        if (pController.Grounded.Interact.IsPressed() && pressable)
        {
            pressable = false;
            switchScript.activateSwitch();
        }
        else if (!pController.Grounded.Interact.IsPressed())
        {
            pressable = true;
        }
    }

    public void setRangeSwitch(GameObject theSwitch)
    {
        smartSwitch = theSwitch;
        switchScript = smartSwitch.GetComponent<SmartSwitchScript>();
    }

    public void deactivateSwitch()
    {
        smartSwitch = null;
        switchScript = null;
    }

    public void smartSwitchInteract()
    {
        Debug.Log("Triggered");
        if (smartSwitch != null)
        {
            switchScript.activateSwitch();
        }
    }
}
