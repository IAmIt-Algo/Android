using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    public interface IBoatSwipeHandler : IEventSystemHandler
    {
        void OnBoatSwiped(Vector3 start, Vector3 end, Coast coast);
    }
}
