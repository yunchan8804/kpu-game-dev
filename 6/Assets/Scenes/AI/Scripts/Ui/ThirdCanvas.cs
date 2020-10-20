namespace Scenes.AI.Ui
{
    using UnityEngine;

    public class ThirdCanvas : NavigationalCanvas
    {
        public void GoToSecond()
        {
            NavigationalCanvasManager.Instance.Show("second");
        }
    }
}