
[System.Serializable]
public class DevicesScheme
{
    public Device[] deviceArray;

    [System.Serializable]
    public class Device
    {
        public int deviceType; //0 - analog, 1 - digital
        public int actionColisionType; // 0 - Calncel type, 1 - wating type, 2 - warning type
    }
}

