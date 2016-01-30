using UnityEngine;

public class Crafting : MonoBehaviour
{
    static Crafting instance;
    public static Crafting Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Crafting");
                instance = go.AddComponent<Crafting>();
            }

            return instance;
        }
    }

    public Recipe[] Recipes;

    [System.Serializable]
    public class Recipe
    {
        public string Item1, Item2;
        public GameObject Result;
    }

    void Awake()
    {
        instance = this;
    }

    public GameObject GetCraftResult(string item1, string item2)
    {
        foreach (Recipe r in Recipes)
        {
            Debug.LogFormat("{0}-{1} compared to {2}-{3}", item1, item2, r.Item1, r.Item2);

            // Ignore order and case
            if ((string.Compare(item1, r.Item1, true) == 0) && (string.Compare(item2, r.Item2, true) == 0))
                return r.Result;
            else if ((string.Compare(item2, r.Item1, true) == 0) && (string.Compare(item1, r.Item2, true) == 0))
                return r.Result;
        }

        return null;
    }
}
