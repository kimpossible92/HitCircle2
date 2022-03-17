using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.ShipName;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIPlay : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button PlayButton;
    [SerializeField] PulseMeter[] Circles = new PulseMeter[3];
    [HideInInspector]public PulseMeter[] _Circles_ => Circles;
    [SerializeField] Text _textMeshPro;
    PulseMeter Current;
    public string[] levelsPath = { "7 0 0","6 1 1", "8 0 1" };
    private bool loose = false;
    public bool _Loose => loose;
    [HideInInspector]public bool isStated = false;
    public void OnSetLoose(bool l1)
    {
        loose = l1;
    }
    private int _knifes;
    [SerializeField] private Gameplay.Spaceships.Spaceship spaceship;
    [SerializeField] private Gameplay.ShipName.PlayerKnife playerKnife; 
    public int Knifes => _knifes;
    public void minusKnife()
    {
        _knifes -= 1;
    }
    private int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetStartLevel();
    }
    public void LevelMinus()
    {
        level = 0;
        isStated = false;
        //StopCoroutine(Wa)
        //SceneManager.LoadScene("SampleScene");
    }
    public void SetStartLevel()
    {
        loose = false;
        
        if (level <= 2)
        {
            playerKnife.InstanceSystem(level);
            string[] paths = levelsPath[level].Split(' ');

            _knifes = int.Parse(paths[0]);
            if (_knifes < 5) { _knifes = 5; }
            if (Current != null) { Destroy(Current.gameObject); Current = null; }
            _Circle1();
            if (level == 1) { isStated = true; }
            level += 1;
        }
        else
        {
            playerKnife.InstanceSystem(1);
            string[] paths = levelsPath[1].Split(' ');

            _knifes = int.Parse(paths[0]);
            if (Current != null) { Destroy(Current.gameObject); Current = null; }
            _Circle1();
            level += 1;
        }
        print(level);
    }
    public void _Circle1()
    {
        if (level <= Circles.Length-1)
        {
            var Circle1 = Instantiate(Circles[level], Vector3.zero, Quaternion.identity);
            Circle1.SetVisibleApple();
            Current = Circle1;
            Current.StartLevel(level);
            FindObjectOfType<PlayerKnife>().SetCircle(Current.gameObject);
        }
        else {
            var Circle1 = Instantiate(Circles[2], Vector3.zero, Quaternion.identity);
            Current = Circle1;
            Current.StartLevel(2);
            FindObjectOfType<PlayerKnife>().SetCircle(Current.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        _textMeshPro.text = _knifes.ToString();
        if (_knifes <= 0)
        {
            PlayButton.gameObject.SetActive(true);
        }
        else if (loose)
        {
            PlayButton.gameObject.SetActive(true);
        }
        else { PlayButton.gameObject.SetActive(false); }
    }
}
