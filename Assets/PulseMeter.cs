using Gameplay.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PulseMeter : MonoBehaviour
{
    #region pulses
    protected float pulse1 = 1.1f;
    protected float pulse2 = 1.2f;
    protected float pulse3 = 1.3f;
    public void setPulse(float p1,float p2,float p3)
    {
        pulse1 = p1;
        pulse2 = p2;
        pulse3 = p3;
    }
    #endregion
    [HideInInspector] private float timer = 0.0f;
    DateTime _time;
    [HideInInspector] float second;
    float _t;
    [SerializeField]
    private UnitBattleIdentity _battleIdentity;
    public UnitBattleIdentity BattleIdentity => _battleIdentity;
    [SerializeField] private Apple _apple;
    [HideInInspector]public Apple _Apple => _apple;
    [SerializeField] private knifeWeapon[] _knifeWeapon;
    public void SetVisibleApple()
    {
        int RandomApple = UnityEngine.Random.Range(0, 4);
        if (RandomApple == 1) { _apple.gameObject.SetActive(true); }
        else 
        {
            _apple.gameObject.SetActive(false);
            if (_knifeWeapon.Length > 0) { foreach (var _k in _knifeWeapon) { _k.gameObject.SetActive(false); } }
        }
    }
    public void StartLevel(int lvl)
    {
        level = lvl;
        setPulse(2, 8.5f, 18);
        _time = DateTime.Now;
        timer = Time.time - Time.deltaTime;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    IEnumerator WaitAndPrint()
    {
        timer = 0;
        while (timer < 25)
        {
            yield return null;
            timer = Time.time - Time.deltaTime;
        }
        
    }
    int level = 0;
    public void UpdateTimer0()
    {
        var _timespan = _time.AddSeconds(timer);
        float koef = (1 / 60) * _timespan.Minute;
        var koef1 = pulse2 * timer * (360 / 60);
        second = _timespan.Second;
        timer = Time.time - Time.deltaTime;
        if (timer > 24)
        {
            //timer = 0;
            //timer = Time.time - Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0, 0, koef1);
    }
    public void UpdateTime1()
    {
        var _timespan = _time.AddSeconds(timer);
        float koef = (1 / 60) * _timespan.Minute;
        var koef1 = pulse1 * timer * (360 / 60);
        second = _timespan.Second;
        timer = Time.time - Time.deltaTime;
        if (timer > 5)
        {
            if (second > 10 && second < 15 || second > 20 && second < 25 || second > 45)
            {
                koef1 *= pulse3;
            }
            else { koef1 *= pulse2; }
        }
        if (timer > 24)
        {
            //timer = 0;
            //timer = Time.time - Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0, 0, koef1);
    }
    public void UpdateTime2()
    {
        var _timespan = _time.AddSeconds(timer);
        float koef = (1 / 60) * _timespan.Minute;
        var koef1 = pulse3 * timer * (360 / 60);
        timer = Time.time - Time.deltaTime;
        second = _timespan.Second;
        if (timer > 24)
        {
            //timer = 0;
            //timer = Time.time - Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0, 0, koef1);
    }
    // Update is called once per frame
    void Update()
    {
        if (level == 0)
        {
            UpdateTimer0();
        }
        if (level == 1)
        {
            UpdateTime2();
        }
        if (level >= 2)
        {
            timer = Time.time - Time.deltaTime;
            var _timespan = _time.AddSeconds(timer);
            float koef = (1 / 60) * _timespan.Minute;
            var koef1 = pulse1 * timer * (360 / 60);
            second = _timespan.Second;
            
            if (timer > 5)
            {
                if (second > 10 && second < 15 || second > 20 && second < 25 || second > 45)
                {
                    koef1 *= pulse3;
                }
                else { koef1 *= pulse2; }
            }
            transform.rotation = Quaternion.Euler(0, 0, koef1);
        }
        if (level >= FindObjectOfType<UIPlay>()._Circles_.Length-1)
        {
            //level = 0;
            //SceneManager.LoadScene("SampleScene");
        }
    }
}
