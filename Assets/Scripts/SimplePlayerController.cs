#if ENABLE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM_PACKAGE
#define USE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Controls;
#endif
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

using UnityEngine;

namespace UnityTemplateProjects
{
    public class SimplePlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
        public float boost = 3.5f;

        [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
        public float positionLerpTime = 0.2f;

        [Header("Rotation Settings")]
        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
        public float rotationLerpTime = 0.01f;

        [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
        public bool invertY = false;

		public float lookSensitivity = 0.01f;
		public float moveSensitivity = 0.5f;
		public float jumpForce = 10f;

		public bool onGround=false;

		Vector3 GetInputTranslationDirection()
		{
			Vector3 direction = new Vector3();
			Keyboard keyboard = Keyboard.current;
			if(keyboard.wKey.IsPressed())
			{
				direction += Vector3.forward;
			}
			if (keyboard.sKey.IsPressed())
			{
				direction += Vector3.forward*-1;
			}
			if (keyboard.aKey.IsPressed())
			{
				direction += Vector3.right*-1;
			}
			if (keyboard.dKey.IsPressed())
			{
				direction += Vector3.right;
			}
			if (keyboard.qKey.IsPressed())
			{
				direction += Vector3.up*-1;
			}
			if (keyboard.eKey.IsPressed())
			{
				direction += Vector3.up;
			}
			
			return direction;
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		void Update()
        {
            Vector3 translation = Vector3.zero;

			Keyboard keyboard = Keyboard.current;
			Mouse mouse = Mouse.current;
			// Exit Sample  
			if (keyboard.escapeKey.IsPressed())
			{
				Application.Quit();
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#endif
			}
			// Rotation
			var mouseMovement = new Vector2(mouse.delta.x.ReadValue()* lookSensitivity, mouse.delta.y.ReadValue() * (invertY ? 1 : -1)* lookSensitivity);

			var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);
			transform.Rotate(new Vector3(mouseMovement.y * mouseSensitivityFactor, mouseMovement.x * mouseSensitivityFactor, 0));
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

			// Translation
			translation = GetInputTranslationDirection() * Time.deltaTime*moveSensitivity;

			// Speed up movement when shift key held
			if (keyboard.leftShiftKey.IsPressed())
			{
				translation *= 5;
			}

			// Modify movement by a boost factor (defined in Inspector and modified in play mode through the mouse scroll wheel)
			//if (mouse.scroll.y.ReadValue() != 0) Debug.Log(mouse.scroll.y.ReadValue());
			//boost += mouse.scroll.y.ReadValue()/120 * 0.2f;
			translation *= Mathf.Pow(2.0f, boost);

			//GetComponent<Rigidbody>().AddForceAtPosition(translation,Vector3.zero);
			transform.Translate(translation);

			// Hide and lock cursor when right mouse button pressed
			if (keyboard.spaceKey.wasPressedThisFrame && onGround)
			{
				//GetComponent<Rigidbody>().AddForceAtPosition(transform.up*jumpForce, Vector3.zero);
				onGround = false;
			}
		}
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.layer == LayerMask.GetMask("World"))
			{
				onGround = true;
			}
		}

	}

}