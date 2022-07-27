[System.Serializable]
public class DevicesScheme
{
    public Device[] deviceArray;
    public class Device
    {
        public int deviceType; //1 - analog, 2 - digital
        public int actionColisionType; // 1 - Calncel type, 2 - warning type, 3 - wating type
    }
}

