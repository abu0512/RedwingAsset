using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteStunBullet : MonoBehaviour {

    protected EliteShaman _EliteShaman;
    protected Vector3 _direction;
    private Vector3 _target;

    [SerializeField]
    protected float _speed;

    private float DeleteTime;

    void Start()
    {
        DeleteTime = 0;
    }

    void Update()
    {
        DeleteTime += Time.deltaTime;
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (DeleteTime >= 5f)
        {
            gameObject.SetActive(false);
            DeleteTime = 0;
        }
    }

    public void InitEliteStunBullet(EliteShaman eliteshaman, Vector3 from, Vector3 target)
    {
        _EliteShaman = eliteshaman;
        from.y += 0.6f;
        from.z += 0.3f;
        from.x += Random.Range(-2.4f, 2.4f);
        target.y = from.y;
        transform.position = from;
        _direction = (target - from).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MapObject" ||
              other.tag == "Player" ||
              other.tag == "Shild")
        {
            if (other.tag == "Player")
            {
                CPlayerSturn._instance.isSturn = true;
                CPlayerManager._instance.PlayerHp(0.2f, 1, _EliteShaman.AttackDamage);
            }
            else if (other.tag == "Shild")
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, _EliteShaman.AttackDamage);
            }
            gameObject.SetActive(false);
        }
    }
}
