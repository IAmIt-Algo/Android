using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level4
{
    public class Tower : MonoBehaviour
    {
        private Stack<Turtle> turtles;

        [SerializeField]
        private GameObject highlight;
        [SerializeField]
        private bool lastTower;

        void Awake()
        {
            turtles = new Stack<Turtle>();

            List<Turtle> unsorted = new List<Turtle>();
            unsorted.AddRange(GetComponentsInChildren<Turtle>());

            if (unsorted.Count > 0)
            {
                List<Turtle> sorted = unsorted.OrderByDescending((x) => x.Weight).ToList<Turtle>();
                foreach (var turtle in sorted)
                {
                    turtles.Push(turtle);
                }
            }
        }

        void Start()
        {
            EnableTopTurtle();
        }

        public bool Intersects(Turtle turtle)
        {
            foreach (var towerTurtle in turtles.ToArray<Turtle>())
            {
                if (towerTurtle.GetComponent<SpriteRenderer>().bounds.Intersects(turtle.GetComponent<SpriteRenderer>().bounds))
                    return true;
            }

            if (GetComponent<SpriteRenderer>().bounds.Intersects(turtle.GetComponent<SpriteRenderer>().bounds))
                return true;

            return false;
        }

        public bool Contains(Turtle turtle)
        {
            return turtles.Contains(turtle);
        }

        public void TurnLight(bool onoff)
        {
            if (onoff)
                highlight.GetComponent<SpriteRenderer>().enabled = true;
            else
                highlight.GetComponent<SpriteRenderer>().enabled = false;
        }

        public void PushTurtle(Turtle turtle)
        {
            float maxY = transform.position.y;
            GameObject maxObject = this.gameObject;
            foreach (var towerTurtle in turtles.ToList<Turtle>())
            {
                if (towerTurtle.transform.position.y + towerTurtle.GetComponent<Renderer>().bounds.size.y / 2 > maxY)
                {
                    maxY = towerTurtle.transform.position.y + towerTurtle.GetComponent<Renderer>().bounds.size.y / 2;
                    maxObject = towerTurtle.gameObject;
                }
            }

            Vector3 newPosition = transform.position;

            if (gameObject != maxObject && turtle.gameObject != maxObject)
            {
                newPosition.y = maxObject.transform.position.y +
                    3 * (turtle.GetComponent<Renderer>().bounds.size.y +
                    maxObject.GetComponent<Renderer>().bounds.size.y) / 10;
            }
            else
            {
                newPosition.y = transform.position.y +
                    3 * (turtle.GetComponent<Renderer>().bounds.size.y) / 8;
            }

            turtle.transform.parent = transform;
            turtle.transform.position = newPosition;
        }

        public void PopTurtle()
        {
            turtles.Pop();
        }

        public void EnableTopTurtle()
        {
            if (turtles.Count > 0)
            {
                turtles.Peek().GetComponent<TurtleController>().IsDragable = true;
            }
        }

        public void DisableTopTurtle()
        {
            if (turtles.Count > 0)
            {
                turtles.Peek().GetComponent<TurtleController>().IsDragable = false;
            }
        }

        public void TryPushTurtle(Turtle turtle)
        {
            if (turtles.Count == 0)
            {
                turtle.transform.parent.GetComponent<Tower>().PopTurtle();
                turtle.transform.parent.GetComponent<Tower>().EnableTopTurtle();
                PushTurtle(turtle);
                turtles.Push(turtle);
                ExecuteEvents.ExecuteHierarchy<ITowerPushHandler>(gameObject, null, (x, y) => x.OnTurtlePushAccept());
                ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnTurtlePush());

                if (lastTower)
                    ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnTurtlePushLastTower(this));
            }
            else
            {
                if (Contains(turtle) || turtles.Peek().Weight <= turtle.Weight)
                {
                    ExecuteEvents.Execute<ITowerPushHandler>(turtle.gameObject, null, (x, y) => x.OnTurtlePushRefuse());
                }
                else
                {
                    turtle.transform.parent.GetComponent<Tower>().PopTurtle();
                    turtle.transform.parent.GetComponent<Tower>().EnableTopTurtle();
                    DisableTopTurtle();
                    PushTurtle(turtle);
                    turtles.Push(turtle);
                    ExecuteEvents.ExecuteHierarchy<ITowerPushHandler>(gameObject, null, (x, y) => x.OnTurtlePushAccept());
                    ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnTurtlePush());

                    if (lastTower)
                        ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnTurtlePushLastTower(this));
                }
            }
        }

        public int TurtlesCount
        {
            get
            {
                return turtles.Count;
            }
        }
    }
}
