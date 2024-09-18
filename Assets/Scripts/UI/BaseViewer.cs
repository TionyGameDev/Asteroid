using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC
{
    public interface IViewer {
        void OnShow();
    }
    interface IViewer<Tcontroller> : IViewer
    {
        Tcontroller controller { get; set; }
    }
    public abstract class BaseViewer<T> : MonoBehaviour, IViewer<T> where T : IController
    {
        public T controller { get; set;}
        
        public void SetController(T controller) 
        { 
            this.controller = controller; 
            if (controller != null)
            {
                controller.SetViewer(this);
            }
        }

        public abstract void OnShow();
        

        
    }
}