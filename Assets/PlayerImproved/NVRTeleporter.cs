using UnityEngine;
using System.Collections;
using NewtonVR;

//[RequireComponent(typeof(NVRHand))]
    public class NVRTeleporter : MonoBehaviour
    {
        public Color LineColor;
        public float LineWidth = 0.02f;
        public float MaxTeleportDistance = 10.0f;

        public bool RestrictToLayer = true;
        public LayerMask Layer;

        private LineRenderer Line;

        private NVRHand Hand;

        private NVRPlayer Player;

        // TODO: Orientation for teleport
        public NVRButtons LaserEnableButton = NVRButtons.Trigger;
        public NVRButtons TeleportButton = NVRButtons.Y;

        private void Awake()
        {

        }

        private void Start()
        {
            Line = this.GetComponent<LineRenderer>();
            Hand = this.GetComponent<NVRHand>();

            if (Line == null)
            {
                Line = this.gameObject.AddComponent<LineRenderer>();
            }

            if (Line.sharedMaterial == null)
            {
                Line.material = new Material(Shader.Find("Unlit/Color"));
                Line.material.SetColor("_Color", LineColor);
                NVRHelpers.LineRendererSetColor(Line, LineColor, LineColor);
            }

            Line.useWorldSpace = true;
            Player = Hand.Player;
        }

        private void LateUpdate()
        {
            Line.enabled = (Hand != null && Hand.Inputs[LaserEnableButton].SingleAxis > 0.01f);

            if (Line.enabled == true)
            {
                Line.material.SetColor("_Color", LineColor);
                NVRHelpers.LineRendererSetColor(Line, LineColor, LineColor);
                NVRHelpers.LineRendererSetWidth(Line, LineWidth, LineWidth);

                RaycastHit hitInfo;
                bool hit = Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, 1000);
                Vector3 endPoint = transform.position;

                if (hit == true)
                {
                // TODO: Display marker at hit location
                    endPoint = hitInfo.point;

                    bool isInLayer;
                    if (RestrictToLayer)
                    {
                        isInLayer = ((Layer & 1 << hitInfo.transform.gameObject.layer) ==
                                     1 << hitInfo.transform.gameObject.layer);
                    }
                    else
                    {
                        isInLayer = true;
                    }

                    if (hitInfo.distance <= MaxTeleportDistance && isInLayer)
                    {

                        if (Hand.Inputs[TeleportButton].PressDown == true)
                        {
                            NVRInteractable LHandInteractable = Player.LeftHand.CurrentlyInteracting;
                            NVRInteractable RHandInteractable = Player.RightHand.CurrentlyInteracting;


                            Vector3 offset = Player.Head.transform.position - Player.transform.position;
                            offset.y = 0;

                            Player.transform.position = hitInfo.point - offset;
                            if (LHandInteractable != null)
                            {
                                LHandInteractable.transform.position = Player.LeftHand.transform.position;
                            }

                            if (RHandInteractable != null)
                            {
                                RHandInteractable.transform.position = Player.RightHand.transform.position;
                            }
                        }
                    }
                }
                else
                {
                    endPoint = this.transform.position + (this.transform.forward * 1000f);
                }

                Line.SetPositions(new Vector3[] { this.transform.position, endPoint });
            }
        }
    }
