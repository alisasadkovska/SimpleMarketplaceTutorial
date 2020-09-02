using System.Collections;
using UnityEngine;

public class ResourceGathering : MonoBehaviour
{
    public ResourceType[] resourceTypes;
    public  int maxResourceAmount = 1000000;

    void Awake()
    {
        foreach(ResourceType resource in resourceTypes)
        {
            switch(resource.collectedResourceType)
            {
                case CollectedResourceType.Money:
                StartCoroutine(CollectMoney(resource.amountToAdd, resource.timeToCollect));
                break;
                case CollectedResourceType.Food:
                StartCoroutine(CollectFood(resource.amountToAdd, resource.timeToCollect));
                break;
                 case CollectedResourceType.Helmet:
                StartCoroutine(CollectHelmet(resource.amountToAdd, resource.timeToCollect));
                break;
                 case CollectedResourceType.Sword:
                StartCoroutine(CollectSword(resource.amountToAdd, resource.timeToCollect));
                break;
                 case CollectedResourceType.Shield:
                StartCoroutine(CollectShield(resource.amountToAdd, resource.timeToCollect));
                break;
            }
        }
    }

   IEnumerator CollectMoney(int amountToAdd, float timeToCollect){
       while(true){
       yield return new WaitForSeconds(timeToCollect);

        if(PlayerStats.Money + amountToAdd <= maxResourceAmount){
           PlayerStats.Money += amountToAdd;
        }
   }
   }

   IEnumerator CollectFood(int amountToAdd, float timeToCollect){
       while(true){
       yield return new WaitForSeconds(timeToCollect);

       if(PlayerStats.Food + amountToAdd <= maxResourceAmount){
           PlayerStats.Food += amountToAdd;
       }
       }
   }

   IEnumerator CollectHelmet(int amountToAdd, float timeToCollect){
       while(true){
       yield return new WaitForSeconds(timeToCollect);
       PlayerStats.Helmets += amountToAdd;

       if(PlayerStats.Helmets + amountToAdd <= maxResourceAmount){
            PlayerStats.Helmets += amountToAdd;
       }
       }
   }
   IEnumerator CollectSword(int amountToAdd, float timeToCollect){
        while(true){
       yield return new WaitForSeconds(timeToCollect);
       
       if(PlayerStats.Swords + amountToAdd <= maxResourceAmount){
             PlayerStats.Swords += amountToAdd;
       }
       }
   }
   IEnumerator CollectShield(int amountToAdd, float timeToCollect){
         while(true){
       yield return new WaitForSeconds(timeToCollect);
      
       if(PlayerStats.Shields + amountToAdd <= maxResourceAmount){
            PlayerStats.Shields += amountToAdd;
       }
       }
   }
   
}
