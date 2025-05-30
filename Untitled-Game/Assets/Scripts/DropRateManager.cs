using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;
    void OnDestroy()
    {
        /** Check if game is still loaded before trying to spawn in drops. */
        if (!this.gameObject.scene.isLoaded)
        {
            return;
        }

        /** Generate random number to check if items needs to spawn. */
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        foreach ( Drops rate in drops )
        {
            if ( randomNumber <= rate.dropRate )
            {
                possibleDrops.Add(rate);
            }
        }

        if ( possibleDrops.Count > 0 )
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
