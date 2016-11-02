using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    public class DragDropHandler : MonoBehaviour, IContainerIconHandler
    {
        private List<Container> containers;

        void Awake()
        {
            containers = new List<Container>();
            containers.AddRange(GetComponentsInChildren<Container>());
        }

        public void OnContainerIconDrag(ContainerIcon icon)
        {
            containers.ForEach((x) => x.GetComponent<ContainerController>().TurnLight(false));
            List<Container> intersectedContainers = containers.FindAll((x) => x.Intersects(icon));

            Container container = null;
            if (intersectedContainers.Count > 0)
            {
                container = intersectedContainers.Aggregate<Container>(
                    (x, y) => x.GetComponent<SpriteRenderer>().sortingOrder > y.GetComponent<SpriteRenderer>().sortingOrder ? x : y
                );
            }

            if (container != null && container.gameObject != icon.transform.parent.gameObject)
                container.GetComponent<ContainerController>().TurnLight(true);
        }

        public void OnContainerIconDrop(ContainerIcon icon)
        {
            containers.ForEach((x) => x.GetComponent<ContainerController>().TurnLight(false));
            List<Container> intersectedContainers = containers.FindAll((x) => x.Intersects(icon));

            Container container = null;
            if (intersectedContainers.Count > 0)
            {
                container = intersectedContainers.Aggregate<Container>(
                    (x, y) => x.GetComponent<SpriteRenderer>().sortingOrder > y.GetComponent<SpriteRenderer>().sortingOrder ? x : y
                );
            }

            if (container != null && container.gameObject != icon.transform.parent.gameObject)
                ExecuteEvents.Execute<IContainerHandler>(container.gameObject, null, (x, y) => x.OnContainerDrop(icon.transform.parent.GetComponent<Container>()));
            
        }
    }
}
