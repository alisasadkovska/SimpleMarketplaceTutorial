[System.Serializable]
public enum CollectedResourceType{Money, Food, Helmet, Sword, Shield}

[System.Serializable]
public class ResourceType
{
    public CollectedResourceType collectedResourceType;
    public float timeToCollect;
    public int amountToAdd;
}
