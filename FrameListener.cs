using System;
using Leap;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

namespace Assets.Scripts
{
    class FrameListener
    {
        public Frame frame;
        public bool LRotation = false;
        public bool twoFingerSwipe = false;
        bool thumbExtended = false;
        bool indexExtended = false;
        bool middleExtended = false;
        bool ringExtended = false;
        bool pinkyExtended = false;
        int frameCounter = 5;
        public float thumbTipVelocity = 0;
        public float indexTipVelocityX = 0;
        public float indexTipVelocityY = 0;
        public float indexTipVelocityZ = 0;

        public float handRightPitch = 0;
        public float handLeftPitch = 0;


        public void OnFrame(object sender, FrameEventArgs args)
        {
            if (frameCounter <= 0)
            {
                Debug.Log("Frame!");
                frame = args.frame;
                //handData();
                setExtendedFingers();

                if (doesLrotation())
                {
                    LRotation = true;
                }
                else if (does2FingerSwipe())
                {
                    twoFingerSwipe = true;
                }
                else
                {
                    LRotation = false;
                    twoFingerSwipe = false;
                }
                frameCounter = 10;
            }
            else
            {
                frameCounter--;
            }

        }

        public void handData()
        {
            Debug.Log("Hands detected: " + frame.Hands.Count);
            if (frame.Hands.Count > 0)
            {
                foreach (Hand hand in frame.Hands)
                {
                    if (hand.IsLeft)
                    {
                        handLeftPitch = hand.Direction.Pitch;
                    }
                    else if (hand.IsRight)
                    {
                        handRightPitch = hand.Direction.Pitch;
                    }
                    Debug.Log("hand: " + hand.Id);
                    Debug.Log("Fingers detected" + hand.Fingers.Count);
                    foreach (Finger finger in hand.Fingers)
                    {
                        Debug.Log(finger.Type);
                        if (finger.Type.Equals(Finger.FingerType.TYPE_INDEX) && finger.IsExtended)
                        {
                            Debug.Log("POINT!");
                        }
                    }
                }
            }
        }

        private void setExtendedFingers()
        {
            if (frame.Hands.Count > 0)
            {
                foreach (Hand hand in frame.Hands)
                {
                    foreach (Finger finger in hand.Fingers)
                    {
                        Debug.Log(finger.Type);
                        if (finger.Type.Equals(Finger.FingerType.TYPE_THUMB))
                        {
                            if (finger.IsExtended)
                            {
                                thumbExtended = true;
                                thumbTipVelocity = finger.TipVelocity.y;
                            }
                            else
                            {
                                thumbExtended = false;
                            }
                        }
                        else if (finger.Type.Equals(Finger.FingerType.TYPE_INDEX))
                        {
                            if (finger.IsExtended)
                            {
                                indexExtended = true;
                                indexTipVelocityX = finger.TipVelocity.x;
                                indexTipVelocityY = finger.TipVelocity.y;
                                indexTipVelocityZ = finger.TipVelocity.z;
                            }
                            else
                            {
                                indexExtended = false;
                            }
                        }
                        else if (finger.Type.Equals(Finger.FingerType.TYPE_MIDDLE))
                        {
                            if (finger.IsExtended)
                            {
                                middleExtended = true;
                            }
                            else
                            {
                                middleExtended = false;
                            }
                        }
                        else if (finger.Type.Equals(Finger.FingerType.TYPE_RING))
                        {
                            if (finger.IsExtended)
                            {
                                ringExtended = true;
                            }
                            else
                            {
                                ringExtended = false;
                            }
                        }
                        else if (finger.Type.Equals(Finger.FingerType.TYPE_PINKY))
                        {
                            if (finger.IsExtended)
                            {
                                pinkyExtended = true;
                            }
                            else
                            {
                                pinkyExtended = false;
                            }
                        }

                    }
                }
            }
            string indent = "   ";
            Debug.Log("FINGER STATUS:");
            Debug.Log(indent + "Thumb: " + thumbExtended);
            Debug.Log(indent + "Index: " + indexExtended);
            Debug.Log(indent + "Middle: " + middleExtended);
            Debug.Log(indent + "Ring: " + ringExtended);
            Debug.Log(indent + "Pinky: " + pinkyExtended);
        }

        private bool doesLrotation()
        {
            bool gestureDetected = false;

            if (thumbExtended && indexExtended && !middleExtended && !ringExtended)
            {
                gestureDetected = true;
            }

            return gestureDetected;
        }

        private bool does2FingerSwipe()
        {
            bool gestureDetected = false;

            if (indexExtended && middleExtended && !ringExtended)
            {
                gestureDetected = true;
            }

            return gestureDetected;
        }
    }
}
