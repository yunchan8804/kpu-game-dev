namespace Scenes.AI.Ui
{
    using System.Collections.Generic;
    using System.Linq;
    using KPU;
    using UnityEngine;

    public class NavigationalCanvasManager : SingletonBehaviour<NavigationalCanvasManager>
    {
        private List<NavigationalCanvas> _navigationalCanvases;

        private void Awake()
        {
            _navigationalCanvases = GetComponentsInChildren<NavigationalCanvas>(true).ToList();
        }

        public void Show(string canvasName)
        {
            var foundedCanvas = _navigationalCanvases.FirstOrDefault(c => c.CanvasName == canvasName);
            if (foundedCanvas != null)
            {
                _navigationalCanvases.Remove(foundedCanvas);
                _navigationalCanvases.Add(foundedCanvas);
                
                for (var i = 0; i < _navigationalCanvases.Count; i++)
                {
                    var canvas = _navigationalCanvases[i];
                    var canvasComponent = canvas.GetComponent<Canvas>();
                    
                    canvas.gameObject.SetActive(true);
                    
                    canvasComponent.overrideSorting = true;
                    canvasComponent.sortingOrder = i;
                    
                    canvas.gameObject.SetActive(false);
                }

                foundedCanvas.gameObject.SetActive(true);
            }
        }
    }
}