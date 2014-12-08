using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using Mogre;
using MogreFramework;


namespace Quickstart2010
{

    public class MoveDemo : OgreWindow
    {
        AnimationState mAnimationState = null; //The AnimationState the moving object
        float mDistance = 0.0f;              //The distance the object has left to travel
        Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        Vector3 mDestination = Vector3.ZERO; // The destination the object is moving towards
        LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        float mWalkSpeed = 50.0f;  // The speed at which the object is moving


        protected override void CreateSceneManager()
        {
            // Create SceneManager
            SceneManager = Root.CreateSceneManager(SceneType.ST_EXTERIOR_CLOSE);

            //Set ambient light
            SceneManager.AmbientLight = ColourValue.White;

            // Create the Robot entity
            Entity ent = SceneManager.CreateEntity("Robot", "robot.mesh");

            // Create the Robot's SceneNode
            SceneNode node = SceneManager.RootSceneNode.CreateChildSceneNode("RobotNode",
                             new Vector3(0.0f, 0.0f, 0.25f));
            node.AttachObject(ent);

            // Create knot objects so we can see movement
            ent = SceneManager.CreateEntity("Knot1", "knot.mesh");
            node = SceneManager.RootSceneNode.CreateChildSceneNode("Knot1Node",
                new Vector3(0.0f, -10.0f, 25.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);
            //
            ent = SceneManager.CreateEntity("Knot2", "knot.mesh");
            node = SceneManager.RootSceneNode.CreateChildSceneNode("Knot2Node",
                new Vector3(550.0f, -10.0f, 50.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);
            //
            ent = SceneManager.CreateEntity("Knot3", "knot.mesh");
            node = SceneManager.RootSceneNode.CreateChildSceneNode("Knot3Node",
                new Vector3(-100.0f, -10.0f, -200.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);

            // Create the walking list
            mWalkList = new LinkedList<Vector3>();
            mWalkList.AddLast(new Vector3(550.0f, 0.0f, 50.0f));
            mWalkList.AddLast(new Vector3(-100.0f, 0.0f, -200.0f));
            mWalkList.AddLast(new Vector3(0.0f, 0.0f, 25.0f));

            // Set idle animation
            mAnimationState = SceneManager.GetEntity("Robot").GetAnimationState("Idle");
            mAnimationState.Loop = true;
            mAnimationState.Enabled = true;
        }

        protected override void CreateInputHandler()
        {
            base.CreateInputHandler();
            this.Root.FrameStarted += new FrameListener.FrameStartedHandler(FrameStarted);


        }

        protected bool nextLocation()
        {
            return true;
        }

        bool FrameStarted(FrameEvent evt)
        {
            //Update the Animation State.
            mAnimationState.AddTime(evt.timeSinceLastFrame * mWalkSpeed / 20);
            return true;
        }
    }
}