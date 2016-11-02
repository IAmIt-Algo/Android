using UnityEngine;

namespace Mindblower.Level4
{
    public class TowerTurtlesAligner : MonoBehaviour
    {
        public void OnTurtlePushed(Turtle turtle)
        {
            float maxY = transform.position.y;
            GameObject maxObject = this.gameObject;
            foreach (var towerTurtle in GetComponentsInChildren<Turtle>())
            {
                if (towerTurtle == turtle)
                    continue;

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
    }
}
