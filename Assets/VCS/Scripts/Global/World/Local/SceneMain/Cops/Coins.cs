using UnityEngine;

public class World_Local_SceneMain_Cops_Coins : MonoBehaviour
{
    public static World_Local_SceneMain_Cops_Coins SingleOnScene { get; private set; }

    [SerializeField] private GameObject coin_prefab;
    [SerializeField] private GameObject[] coins_positions;

    public void Coins_Spawn()
    {
            GameObject _inst;

            for (var _i = 0; _i < coins_positions.Length; ++_i)
            {
                _inst = Instantiate(coin_prefab, coins_positions[_i].transform.position, coins_positions[_i].transform.rotation, World_Entity.SingleOnScene.transform);
            
                if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_IsBought())
                {
                    _inst.GetComponent<World_Local_SceneMain_Bonus_Coin>().CoinMagnet_Trigger();
                }
            }
    }

    private void Awake()
    {
        SingleOnScene = this;
    }

    private void Start()
    {
        for (var _i = 0; _i < coins_positions.Length; ++_i)
        {
            coins_positions[_i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
