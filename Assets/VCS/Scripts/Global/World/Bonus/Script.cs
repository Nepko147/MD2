using UnityEngine;

public class World_Bonus : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float luckyTime;
    [SerializeField] private bool distortionEffect;
    [SerializeField] private AudioClip bonusSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private World_BonusString popUpString;
    public Animator anim;     
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private float onSpeed;
    private int bonusType;
    private string effect;           

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();        
        onSpeed = 0;

        //Проверка на возможность игры      
        if (ControlPers_Globalist.Singletone.canPlay())
        {
            onSpeed = speed;
            bonusType = Random.Range(1, 10);
            bonusType = ControlPers_Globalist.Singletone.IsLuckyTime() ? 3 : bonusType;
            switch (bonusType)
            {
                case 1:
                    body.name = "BonusUp";
                    anim.SetInteger("type", bonusType);
                    distortionEffect = true;
                    effect = "+1 UP";
                    break;
                case 2:
                    body.name = "BonusLuck";
                    boxCollider.size = new Vector2(0.32f, 0.32f);
                    anim.SetInteger("type", bonusType);
                    distortionEffect = true;
                    effect = "Lucky time!";
                    break;
                case > 2:
                    body.name = "BonusCoin";
                    body.transform.localScale = new Vector2(3.0f, 3.0f);
                    boxCollider.size = new Vector2(0.16f, 0.16f);
                    anim.SetInteger("type", 3);
                    distortionEffect = false;
                    effect = "+1 Coin";
                    break;
            }
        }       
    }

    private void FixedUpdate()
    {
        //Проверка на возможность игры      
        if (!ControlPers_Globalist.Singletone.canPlay())
        {
            body.linearVelocity = new Vector2(0, 0);
            anim.StartPlayback();
            return;
        }
        anim.StopPlayback();

        if (body.GetRelativeVector(body.linearVelocity).x < onSpeed)
        {
            body.linearVelocity = new Vector2(-onSpeed * ControlPers_Globalist.Singletone.GetDifficultyScale(), 0);
        }

        //Проверка на контакт с игроком
        if (boxCollider.bounds.Intersects(World_Player.Singletone.GetComponent<BoxCollider2D>().bounds))
        {
            switch (bonusType)
            {
                case 1:
                    ControlPers_AudioManager.Singletone.PlaySound(bonusSound);
                    World_Player.Singletone.takeDamage(-1);
                    
                    break;
                case 2:
                    ControlPers_AudioManager.Singletone.PlaySound(bonusSound);
                    ControlPers_Globalist.Singletone.StartLuckyTime(luckyTime);
                    World_BonusSpawner.Singletone.SetLuckyTimer();
                    
                    break;
                case > 2:
                    ControlPers_AudioManager.Singletone.PlaySound(coinSound);
                    World_Player.Singletone.TakeCoin(1);
                    
                    break;
            }
            popUpString.DisplayPopUp(effect, body);
            if (distortionEffect)
            {                
                AppScreen_Camera_DistortionDynamic distortion = AppScreen_Camera_DistortionDynamic.Singletone;
                Vector2 screenPosition = distortion.GetComponent<Camera>().WorldToScreenPoint(transform.position);
                distortion.distortionStart = true;
                distortion.posX = screenPosition.x /= Screen.width;
                distortion.posY = screenPosition.y /= Screen.height;
            }
            Destroy(this.gameObject);
        }

        //Уничтожаем объект, когда он уходит за пределы экрана
        if (this.gameObject.transform.position.x <= -23.6f)
        {            
            Destroy(this.gameObject);
        }
    }
 }
