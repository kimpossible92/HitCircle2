using Gameplay.ShipSystems;
using Gameplay.Spaceships;
using System.Collections;
using UnityEngine;

namespace Gameplay.ShipName
{
    public class PlayerKnife : Ship
    {
        #region joystick
        //[SerializeField] protected Joystick joystick;
        //[SerializeField] protected JoyButton joyButton;
        //[SerializeField] protected JoyButton joyButton2;
        private float joyHorizontal;
        private float joyVertical;
        protected bool jump;
        #endregion
        float pointrot = 0f;
        [SerializeField] bool MouseHeel=true;
        [SerializeField] float speed = 0.5f;
        //[SerializeField] Fin Finish;
        private float pointx;
        [SerializeField]LayerMask layerMask;
        public float AmountInt(float amount)
        {
            return amount;
        }
        Vector2 _moveDirection;
        Vector2 _mouseDirection;
        private Vector3 inputRotation;
        private Vector3 mousePlacement;
        private Vector3 screenCentre;
        [SerializeField] private GameObject RotateGO;
        [SerializeField]private UIPlay uIPlay;
        [SerializeField] private GameObject effect1, effect2;
        public void VisibleEffect()
        {
            if (!uIPlay.isStated)
            {
                effect1.SetActive(true);
                effect2.SetActive(false);
            }
            else
            {
                effect1.SetActive(false);
                effect2.SetActive(true);
            }
            Invoke("NotVisibleEffect", 0.3f);
        }
        private void NotVisibleEffect()
        {
            effect1.SetActive(false);
            effect2.SetActive(false);
        }
        public void InstanceSystem(int _current)
        {
            if (_current <= 2) { }
            else { _current = 2; }
            var myobject = FindObjectOfType<WeaponSystem>().myWeapons[_current];
            var knifeClone = Instantiate(myobject, transform);
            knifeClone.transform.localPosition = Vector3.zero;
            if (systemCurrent != null) {
                Destroy(systemCurrent.gameObject); 
                systemCurrent = null;
            }
            systemCurrent = knifeClone.GetComponent<MovementSystem>();
            FindObjectOfType<Spaceship>().SetMovement(systemCurrent);
            
        }
        void FindCrap()
        {
            screenCentre = new Vector3(Screen.width * 0.5f, 0, Screen.height * 0.5f);
            mousePlacement = Input.mousePosition;
            mousePlacement.z = mousePlacement.y;
            mousePlacement.y = 0;
            inputRotation = mousePlacement - screenCentre;
        }
        [SerializeField] LayerMask lestnica_mask, ButtonLayer;
        float yrot = 0;
        float rotationHorizontal;
        [SerializeField]GameObject Circle;
        public void SetCircle(GameObject go)
        {
            Circle = go;
        }
        
        public RaycastHit2D RayCast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance, LayerMask mask, Color color, bool drawGizmo = false)
        {
            if (drawGizmo)
            {
                Debug.DrawRay(rayOriginPoint, rayDirection * rayDistance, color);
            }
            return Physics2D.Raycast(rayOriginPoint, rayDirection, rayDistance, mask);
        }
        public MovementSystem systemCurrent;
        public void BackUI()
        {

            uIPlay.LevelMinus();
            uIPlay.OnSetLoose(true);
        }
        protected override void ProcessHandling(MovementSystem movementSystem)
        {
            if (movementSystem == null) { return; }
            _moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            pointrot += Input.GetAxis("Horizontal") * Time.deltaTime * -100;
            #region MouseHeel
            movementSystem.RotateGO = RotateGO;
            movementSystem.Circle = Circle;
            movementSystem.Handle = gameObject;
            foreach (var _apple in FindObjectsOfType<Apple>())
            {
                _apple.RotateGO = RotateGO;
            }
            if (Input.GetMouseButtonDown(0)&&uIPlay.Knifes!=0)
            {

                if (RayCast(transform.position, Vector2.up, 5f, lestnica_mask, Color.green, true))
                {
                    var knife = Instantiate(movementSystem, transform.position, Quaternion.identity);
                    knife.OClone = true;
                    knife.Circle = Circle;
                    knife._playerKnife = this;
                    knife.Handle = gameObject;
                    knife.RotateGO = RotateGO;
                    knife.transform.Translate(0,2.35f,0);
                    systemCurrent = knife;
                    uIPlay.minusKnife();
                    //knife.transform.SetParent(Circle.transform);
                    //StartCoroutine(knife.GetEnumeratorlestnica1(Vector2.up, 1.5f, 1f, Vector2.zero, false));
                   
                }
            }
            #endregion
            movementSystem.LongMovement(_moveDirection.y * Time.deltaTime);
            //if (transform.position.x > GetComponent<CollShip>().limitx)
            //{
            //    transform.position = new Vector3(GetComponent<CollShip>().limitx - 2, transform.position.y, transform.position.z);
            //}

            //if (transform.position.x < GetComponent<CollShip>().limitx1)
            //{
            //    transform.position = new Vector3(GetComponent<CollShip>().limitx1 + 2, transform.position.y, transform.position.z);
            //}

        }
        
        private void Update()
        {
            PlayerHandle();
        }
        Vector3 awakePosition;
        private void Awake()
        {
            effect1.SetActive(false);
            effect2.SetActive(false);
            //awakePosition = transform.position;
        }

        protected override void ProcessFire(WeaponSystem fireSystem)
        {

        }
       
    }
}
