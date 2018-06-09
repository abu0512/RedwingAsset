using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    private List<Bullet> _bullets = new List<Bullet>();
    private List<StunBullet> _stunBullets = new List<StunBullet>();
    private List<SBullet> _sbullets = new List<SBullet>();
    private List<SStunBullet> _sstunBullets = new List<SStunBullet>();
    private List<EliteBullet> _EliteBullets = new List<EliteBullet>();
    private List<EliteStunBullet> _EliteStunBullets = new List<EliteStunBullet>();
    private List<EliteSBullet> _EliteSBullets = new List<EliteSBullet>();
    private List<EliteSStunBullet> _EliteSStunBullets = new List<EliteSStunBullet>();

    void Awake()
    {
        foreach (Bullet b in GetComponentsInChildren<Bullet>())
        {
            b.gameObject.SetActive(false);
            _bullets.Add(b);
        }

        foreach (StunBullet s in GetComponentsInChildren<StunBullet>())
        {
            s.gameObject.SetActive(false);

            _stunBullets.Add(s);
        }

        foreach (SBullet sb in GetComponentsInChildren<SBullet>())
        {
            sb.gameObject.SetActive(false);
            _sbullets.Add(sb);
        }

        foreach (SStunBullet ss in GetComponentsInChildren<SStunBullet>())
        {
            ss.gameObject.SetActive(false);

            _sstunBullets.Add(ss);
        }

        foreach (EliteBullet eb in GetComponentsInChildren<EliteBullet>())
        {
            eb.gameObject.SetActive(false);
            _EliteBullets.Add(eb);
        }

        foreach (EliteStunBullet es in GetComponentsInChildren<EliteStunBullet>())
        {
            es.gameObject.SetActive(false);

            _EliteStunBullets.Add(es);
        }

        foreach (EliteSBullet es in GetComponentsInChildren<EliteSBullet>())
        {
            es.gameObject.SetActive(false);
            _EliteSBullets.Add(es);
        }

        foreach (EliteSStunBullet se in GetComponentsInChildren<EliteSStunBullet>())
        {
            se.gameObject.SetActive(false);

            _EliteSStunBullets.Add(se);
        }
    }

    public Bullet SetBullet(QueenMushroom queen, Vector3 from, Vector3 target)
    {
        Bullet bullet = GetBullet();
        bullet.InitBullet(queen, from, target);

        return bullet;
    }

    public StunBullet SetStunBullet(QueenMushroom queen, Vector3 from, Vector3 target)
    {
        StunBullet stunbullet = GetStunBullet();
        stunbullet.InitStunBullet(queen, from, target);

        return stunbullet;
    }

    public SBullet SetSBullet(QueenMushroom queen, Vector3 from, float rotate)
    {
        SBullet sbullet = GetSBullet();
        sbullet.InitSBullet(queen, from, rotate);

        return sbullet;
    }

    public SStunBullet SetSStunBullet(QueenMushroom queen, Vector3 from, float rotate)
    {
        SStunBullet sstunbullet = GetSStunBullet();
        sstunbullet.InitSStunBullet(queen, from, rotate);

        return sstunbullet;
    }

    public EliteBullet SetEliteBullet(EliteShaman eliteshaman, Vector3 from, Vector3 target)
    {
        EliteBullet Elitebullet = GetEliteBullet();
        Elitebullet.InitEliteBullet(eliteshaman, from, target);

        return Elitebullet;
    }

    public EliteStunBullet SetEliteStunBullet(EliteShaman eliteshaman, Vector3 from, Vector3 target)
    {
        EliteStunBullet Elitestunbullet = GetEliteStunBullet();
        Elitestunbullet.InitEliteStunBullet(eliteshaman, from, target);

        return Elitestunbullet;
    }

    public EliteSBullet SetEliteSBullet(EliteShaman eliteshaman, Vector3 from, float rotate)
    {
        EliteSBullet Elitesbullet = GetEliteSBullet();
        Elitesbullet.InitEliteSBullet(eliteshaman, from, rotate);

        return Elitesbullet;
    }

    public EliteSStunBullet SetEliteSStunBullet(EliteShaman eliteshaman, Vector3 from, float rotate)
    {
        EliteSStunBullet Elitesstunbullet = GetEliteSStunBullet();
        Elitesstunbullet.InitEliteSStunBullet(eliteshaman, from, rotate);

        return Elitesstunbullet;
    }

    private Bullet GetBullet()
    {
        Bullet bullet = null;

        foreach (Bullet b in _bullets)
        {
            if (b.gameObject.activeInHierarchy)
                continue;

            bullet = b;
        }

        if (bullet == null)
            bullet = CreateBullet();

        bullet.gameObject.SetActive(true);

        return bullet;
    }

    private StunBullet GetStunBullet()
    {
        StunBullet stunbullet = null;

        foreach (StunBullet s in _stunBullets)
        {
            if (s.gameObject.activeInHierarchy)
                continue;

            stunbullet = s;
        }

        if (stunbullet == null)
            stunbullet = CreateStunBullet();

        stunbullet.gameObject.SetActive(true);

        return stunbullet;
    }

    private SBullet GetSBullet()
    {
        SBullet sbullet = null;

        foreach (SBullet sb in _sbullets)
        {
            if (sb.gameObject.activeInHierarchy)
                continue;

            sbullet = sb;
        }

        if (sbullet == null)
            sbullet = CreateSBullet();

        sbullet.gameObject.SetActive(true);

        return sbullet;
    }

    private SStunBullet GetSStunBullet()
    {
        SStunBullet sstunbullet = null;

        foreach (SStunBullet ss in _sstunBullets)
        {
            if (ss.gameObject.activeInHierarchy)
                continue;

            sstunbullet = ss;
        }

        if (sstunbullet == null)
            sstunbullet = CreateSStunBullet();

        sstunbullet.gameObject.SetActive(true);

        return sstunbullet;
    }

    private EliteBullet GetEliteBullet()
    {
        EliteBullet Elitebullet = null;

        foreach (EliteBullet eb in _EliteBullets)
        {
            if (eb.gameObject.activeInHierarchy)
                continue;

            Elitebullet = eb;
        }

        if (Elitebullet == null)
            Elitebullet = CreateEliteBullet();

        Elitebullet.gameObject.SetActive(true);

        return Elitebullet;
    }

    private EliteStunBullet GetEliteStunBullet()
    {
        EliteStunBullet Elitestunbullet = null;

        foreach (EliteStunBullet se in _EliteStunBullets)
        {
            if (se.gameObject.activeInHierarchy)
                continue;

            Elitestunbullet = se;
        }

        if (Elitestunbullet == null)
            Elitestunbullet = CreateEliteStunBullet();

        Elitestunbullet.gameObject.SetActive(true);

        return Elitestunbullet;
    }

    private EliteSBullet GetEliteSBullet()
    {
        EliteSBullet Elitesbullet = null;

        foreach (EliteSBullet be in _EliteSBullets)
        {
            if (be.gameObject.activeInHierarchy)
                continue;

            Elitesbullet = be;
        }

        if (Elitesbullet == null)
            Elitesbullet = CreateEliteSBullet();

        Elitesbullet.gameObject.SetActive(true);

        return Elitesbullet;
    }

    private EliteSStunBullet GetEliteSStunBullet()
    {
        EliteSStunBullet Elitesstunbullet = null;

        foreach (EliteSStunBullet ee in _EliteSStunBullets)
        {
            if (ee.gameObject.activeInHierarchy)
                continue;

            Elitesstunbullet = ee;
        }

        if (Elitesstunbullet == null)
            Elitesstunbullet = CreateEliteSStunBullet();

        Elitesstunbullet.gameObject.SetActive(true);

        return Elitesstunbullet;
    }

    private StunBullet CreateStunBullet()
    {
        StunBullet stunbullet = null;
        GameObject stunbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/StunBullet");
        stunbullet = Instantiate(stunbulletObject).GetComponent<StunBullet>();
        stunbullet.transform.SetParent(transform);
        stunbullet.gameObject.SetActive(false);
        _stunBullets.Add(stunbullet);

        return stunbullet;
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = null;
        GameObject bulletObject = (GameObject)Resources.Load("Prefabs/Bullets/Bullet");
        bullet = Instantiate(bulletObject).GetComponent<Bullet>();
        bullet.transform.SetParent(transform);
        bullet.gameObject.SetActive(false);
        _bullets.Add(bullet);

        return bullet;
    }

    private SStunBullet CreateSStunBullet()
    {
        SStunBullet sstunbullet = null;
        GameObject sstunbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/SStunBullet");
        sstunbullet = Instantiate(sstunbulletObject).GetComponent<SStunBullet>();
        sstunbullet.transform.SetParent(transform);
        sstunbullet.gameObject.SetActive(false);
        _sstunBullets.Add(sstunbullet);

        return sstunbullet;
    }

    private SBullet CreateSBullet()
    {
        SBullet sbullet = null;
        GameObject sbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/SBullet");
        sbullet = Instantiate(sbulletObject).GetComponent<SBullet>();
        sbullet.transform.SetParent(transform);
        sbullet.gameObject.SetActive(false);
        _sbullets.Add(sbullet);

        return sbullet;
    }

    private EliteStunBullet CreateEliteStunBullet()
    {
        EliteStunBullet Elitestunbullet = null;
        GameObject ElitestunbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/EliteStunBullet");
        Elitestunbullet = Instantiate(ElitestunbulletObject).GetComponent<EliteStunBullet>();
        Elitestunbullet.transform.SetParent(transform);
        Elitestunbullet.gameObject.SetActive(false);
        _EliteStunBullets.Add(Elitestunbullet);

        return Elitestunbullet;
    }

    private EliteBullet CreateEliteBullet()
    {
        EliteBullet Elitebullet = null;
        GameObject ElitebulletObject = (GameObject)Resources.Load("Prefabs/Bullets/EliteBullet");
        Elitebullet = Instantiate(ElitebulletObject).GetComponent<EliteBullet>();
        Elitebullet.transform.SetParent(transform);
        Elitebullet.gameObject.SetActive(false);
        _EliteBullets.Add(Elitebullet);

        return Elitebullet;
    }

    private EliteSStunBullet CreateEliteSStunBullet()
    {
        EliteSStunBullet Elitesstunbullet = null;
        GameObject ElitesstunbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/EliteSStunBullet");
        Elitesstunbullet = Instantiate(ElitesstunbulletObject).GetComponent<EliteSStunBullet>();
        Elitesstunbullet.transform.SetParent(transform);
        Elitesstunbullet.gameObject.SetActive(false);
        _EliteSStunBullets.Add(Elitesstunbullet);

        return Elitesstunbullet;
    }

    private EliteSBullet CreateEliteSBullet()
    {
        EliteSBullet Elitesbullet = null;
        GameObject ElitesbulletObject = (GameObject)Resources.Load("Prefabs/Bullets/EliteSBullet");
        Elitesbullet = Instantiate(ElitesbulletObject).GetComponent<EliteSBullet>();
        Elitesbullet.transform.SetParent(transform);
        Elitesbullet.gameObject.SetActive(false);
        _EliteSBullets.Add(Elitesbullet);

        return Elitesbullet;
    }
}