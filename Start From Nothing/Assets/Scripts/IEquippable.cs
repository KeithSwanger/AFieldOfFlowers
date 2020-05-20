
public interface IEquippable
{
    bool Equip();
    bool Unequip();

    void OnRayEnter();
    void OnRayExit();

    EquipableType GetEquipableType();
}
