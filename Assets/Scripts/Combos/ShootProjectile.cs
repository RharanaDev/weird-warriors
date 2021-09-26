using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject prefab, bigPrefab, stunPrefab;
    public GameObject current;
    public int id;
    public bool singleMissile;
    public void Throw(int number)
    {
        switch (number)
        {
            case 1:
                id++;
                current = Instantiate(prefab, transform.position, transform.rotation);
                if (!singleMissile)
                {
                    current.SendMessage("Numbered", id);
                }
                if (id == 3)
                {
                    id = 0;
                }
                break;
            case 2:
                Instantiate(bigPrefab, transform.position, transform.rotation);
                break;
            case 3:
                Instantiate(stunPrefab, transform.position, transform.rotation);
                break;
        }
    }
}
