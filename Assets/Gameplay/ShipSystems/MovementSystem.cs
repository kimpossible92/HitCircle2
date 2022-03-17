using System.Collections;
using UnityEngine;
using Gameplay.Spaceships;
using Gameplay.ShipName;

namespace Gameplay.ShipSystems
{
    public class MovementSystem : MonoBehaviour
    {

        [SerializeField]
        private float _lateralMovementSpeed;
        public GameObject Circle;
        public GameObject Handle;
        [SerializeField]
        private float _longitudinalMovementSpeed;
        public PlayerKnife _playerKnife;
        [SerializeField]
        //private
        float speedX = 3;
        [SerializeField]
        LayerMask lestnica_mask;
        bool entireRazor=false;
        public GameObject RotateGO;
        [HideInInspector]public bool OClone=false;
        public void ToMoved(bool m)
        {
            move = m;
        }
        [SerializeField]bool move = false;
        private void Start()
        {
            Vibration.Init();
        }
        private void Update()
        {

        }
        public RaycastHit2D RayCast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance, LayerMask mask, Color color, bool drawGizmo = false)
        {
            if (drawGizmo)
            {
                Debug.DrawRay(rayOriginPoint, rayDirection * rayDistance, color);
            }
            return Physics2D.Raycast(rayOriginPoint, rayDirection, rayDistance, mask);
        }
        public IEnumerator GetEnumeratorlestnica1(Vector2 rayDir, float updown, float distances, Vector2 addvector, bool iskinematic)
        {
            while (!entireRazor)
            {
                transform.Translate(new Vector3(0, updown, 0));
                yield return null;
            }
        }
        #region Move
        public void loadMove()
        {
            _lateralMovementSpeed = 250.0f;
            Invoke("oldmove", 8.0f);
        }
        public void MousePosition(float amount)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime*speedX, amount, transform.position.z);
        }
        public void LateralMovement(float amount)
        {
            if (tag == "bonus") { if (transform.position.y < -5.2f) { } else { Move(amount * _longitudinalMovementSpeed, Vector3.up); } }
            else Move(amount * _lateralMovementSpeed, Vector3.right);
        }
        public void LongMovement(float amount)
        {
            Move(amount * 10, Vector3.up);
        }
        public void LateralRotate(float amount)
        {
            transform.rotation = Quaternion.Euler(0, 0, amount);
        }
        public void LongitudinalMovement(float amount)
        {
            if (tag == "bonus") { if (transform.position.y < -5.2f) { } else { Move(amount * _longitudinalMovementSpeed, Vector3.up); } }
            else { Move(amount * _longitudinalMovementSpeed, Vector3.up); }
        }
        public void HorizontalMovement(float amount)
        {
            if (tag == "bonus") { if (transform.position.y < -5.2f) { } else { Move(amount * _longitudinalMovementSpeed, Vector3.up); } }
            else Move(amount * _longitudinalMovementSpeed, Vector3.left);
        }
        private void oldmove()
        {
            _lateralMovementSpeed = 50.0f;
        }
        private void Move(float amount, Vector3 axis)
        {
            transform.Translate(amount * axis.normalized);
        }
        #endregion
        bool ingredientFly = false;
        IEnumerator StartAnimateRotate(GameObject item)
        {
            //if (ingrCountTarget[i] > 0)
            //ingrCountTarget[i]--;

            ingredientFly = true;
            GameObject ingr = RotateGO.gameObject;//

            //if (targetBlocks > 0)
            //{
            //    ingr = blocksObject.transform.gameObject;
            //}
            AnimationCurve curveX = new AnimationCurve(new Keyframe(0, item.transform.localPosition.x), new Keyframe(0.4f, ingr.transform.position.x));
            AnimationCurve curveY = new AnimationCurve(new Keyframe(0, item.transform.localPosition.y), new Keyframe(0.5f, ingr.transform.position.y));
            curveY.AddKey(0.2f, item.transform.localPosition.y + UnityEngine.Random.Range(-2, 0.5f));
            float startTime = Time.time;
            Vector3 startPos = item.transform.localPosition;
            float speed = UnityEngine.Random.Range(0.4f, 0.6f);
            float distCovered = 0;
            while (distCovered < 0.5f && item != null)
            {
                distCovered = (Time.time - startTime) * speed;
                item.transform.localPosition = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), 0);
                item.transform.Rotate(Vector3.back, Time.deltaTime * 30);
                yield return new WaitForFixedUpdate();
            }
            Destroy(item);
            ingredientFly = false;
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (FindObjectOfType<UIPlay>()._Loose == true) { return; }
            if (collision.gameObject.tag =="circle")
            {
                entireRazor = true;
                move = false;
                transform.SetParent(Circle.transform);
                Vibration.Vibrate();
                _playerKnife.VisibleEffect();
                //GetComponent<MovementSystem>().enabled = false;
            }

            if (collision.gameObject.tag == "knife" &&
                collision.gameObject.GetComponent<MovementSystem>().OClone == false)
            {
                return;
            }
            if (collision.gameObject.tag == "knife"&&
                OClone==true)
            {
                transform.SetParent(RotateGO.transform);
                StartCoroutine(StartAnimateRotate(gameObject));
                FindObjectOfType<PlayerKnife>().BackUI();
            }
        }
    }
}
