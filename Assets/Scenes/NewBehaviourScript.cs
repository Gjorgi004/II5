using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [Header("Weapons")]
    public GameObject[] weapons; 
    
    private int currentWeaponIndex = 0;

    void Start()
    {
        
        SelectWeapon(currentWeaponIndex);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);

       
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) CycleWeapon(-1);
        if (scroll < 0f) CycleWeapon(1);
    }

    void SelectWeapon(int index)
    {
        
        if (index < 0 || index >= weapons.Length) return;

        
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        
        currentWeaponIndex = index;
        weapons[currentWeaponIndex].SetActive(true);
    }

    void CycleWeapon(int direction)
    {
        int newIndex = (currentWeaponIndex + direction + weapons.Length) % weapons.Length;
        SelectWeapon(newIndex);
    }
}
