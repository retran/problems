public class ParkingSystem
{
    private int[] _slots = new int[3];

    public ParkingSystem(int big, int medium, int small)
    {
        _slots[0] = big;
        _slots[1] = medium;
        _slots[2] = small;
    }

    public bool AddCar(int carType)
    {
        carType--;
        if (_slots[carType] > 0)
        {
            _slots[carType]--;
            return true;
        }

        return false;
    }
}